using BaseProject.Application.Interfaces.Mapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        private IMediator mediator;
        private ILogger<T> logger;
        private IMapper mapper;

        protected IMediator Mediator
            => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ILogger<T> Logger 
            => logger ??= HttpContext.RequestServices.GetService<ILogger<T>>();
        protected IMapper Mapper
            => mapper ??= HttpContext.RequestServices.GetService<IMapper>();
    }
}
