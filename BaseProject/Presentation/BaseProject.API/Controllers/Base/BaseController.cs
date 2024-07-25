using BaseProject.Application.Interfaces.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase where T : BaseController<T>
    {
   
    }
}
