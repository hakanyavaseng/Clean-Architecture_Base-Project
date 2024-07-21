using BaseProject.Application.Features.Queries.Products.GetAllProducts;
using BaseProject.Application.Interfaces.Repositories.Common;
using BaseProject.Domain.Filtering;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Web;

namespace BaseProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMediator mediator;
        public TestController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await mediator.Send(new GetAllProductsRequest());
            return Ok(products);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetWithFilterAsync([FromBody] string filtersJson)
        {
            var filters = JsonSerializer.Deserialize<List<QueryFilter>>(filtersJson);
            var products = await mediator.Send(new GetAllProductsRequest { Filters = filters });
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Post()
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
