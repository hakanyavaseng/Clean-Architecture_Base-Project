using BaseProject.Application.Features.Queries.Products.GetAllProducts;
using BaseProject.Application.Interfaces.Repositories.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put()
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return Ok();
        }

    }
}
