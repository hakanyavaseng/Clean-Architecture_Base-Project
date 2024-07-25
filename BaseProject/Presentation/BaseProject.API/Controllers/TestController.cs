using BaseProject.API.Controllers.Base;
using BaseProject.Application.Interfaces.Services.Tokens;
using BaseProject.Domain.Filtering;
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
        public async Task<IActionResult> GetWithFilterAsync(QueryParameters queryParameters)
        {
            return Ok();
        }

        [HttpPost("/a")]
        public IActionResult Post([FromForm] int a)
        {
            return Ok();
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
