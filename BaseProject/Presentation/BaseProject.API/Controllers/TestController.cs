using BaseProject.API.Controllers.Base;
using BaseProject.Application.Interfaces.Services.Tokens;
using BaseProject.Domain.Filtering;
using BaseProject.Domain.Models;
using BaseProject.Domain.Models.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.API.Controllers
{

    public class TestController : BaseController<TestController>
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
             return Ok();
        }

        [HttpPost("[action]")]
        public Result GetWithFilterAsync(QueryParameters queryParameters)
        {
            return Result.Successful();
        }

        [HttpPost("/a")]
        public Result Post([FromForm] int a)
        {
            return Result.Fail(CommonErrors.NotFoundException);
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }

    }
}
