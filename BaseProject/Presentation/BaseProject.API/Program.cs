using BaseProject.Persistence;
using BaseProject.Application;
using BaseProject.Persistence.Filtering;
using BaseProject.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new MatchModeConverter());
                });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceLayerServices(builder.Configuration);
builder.Services.AddApplicationLayerServices();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
