using BaseProject.Persistence;
using BaseProject.Application;
using BaseProject.Infrastructure;
using BaseProject.Persistence.Filtering;
using BaseProject.API.Middlewares;
using BaseProject.Mapper;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using Serilog.Context;
using BaseProject.API.Filters;
using FluentValidation.AspNetCore;
using ServiceRegistration = BaseProject.Application.ServiceRegistration;
using BaseProject.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services
                .AddControllers(configure =>
                {
                    configure.Filters.Add<ValidationFilter>();
                })
                .AddFluentValidation(p =>
                {
                    p.RegisterValidatorsFromAssemblyContaining<AppUserValidator>();
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new MatchModeConverter());
                });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceLayerServices(builder.Configuration);
builder.Services.AddApplicationLayerServices();
builder.Services.AddInfrastructureLayerServices(builder.Configuration);
builder.Services.AddCustomMapper();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var columOptions = new ColumnOptions()
{
    AdditionalColumns = new Collection<SqlColumn>
    {
         new SqlColumn { ColumnName = "RemoteIpAddress", DataType = SqlDbType.NVarChar, DataLength = 50 },
         new SqlColumn { ColumnName = "UserId", DataType = SqlDbType.NVarChar, DataLength=200 }
    }
};

Logger logConfig = new LoggerConfiguration()
                        .WriteTo.Console()
                        .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), "Logs",
                         autoCreateSqlTable: true,
                         columnOptions: columOptions)
                        .WriteTo.Seq(builder.Configuration["Seq:ServerUrl"])
                        .MinimumLevel.Warning()
                        .Enrich.FromLogContext()
                        .CreateLogger();

builder.Host.UseSerilog(logConfig);


builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpLogging();

app.Use(async (context, next) =>
{
    var remoteIp = context.Connection.RemoteIpAddress;
    LogContext.PushProperty("RemoteIpAddress", remoteIp);

    var userId = context.User?.Identity?.Name ?? "denemeUserId";
    LogContext.PushProperty("UserId", userId);

    await next();
});

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();



app.MapControllers();

app.Run();
