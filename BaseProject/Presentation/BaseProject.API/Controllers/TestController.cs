using BaseProject.Application.Features.Queries.Products.GetAllProducts;
using BaseProject.Application.Interfaces.Repositories.Common;
using BaseProject.Application.Interfaces.Services.Tokens;
using BaseProject.Domain.Filtering;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Web;

namespace BaseProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ITokenService tokenService;

        public TestController(IMediator mediator, ITokenService tokenService)
        {
            this.mediator = mediator;
            this.tokenService = tokenService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string token = await tokenService.GenerateTokenAsync(Guid.NewGuid().ToString());
            return Ok(token);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetWithFilterAsync(QueryParameters queryParameters)
        {
            var products = await mediator.Send(new GetAllProductsRequest { QueryParameters = queryParameters });
            return Ok(products);
        }

        [HttpPost("/a")]
        public IActionResult Post([FromForm] int a)
        {
            throw new UnauthorizedException("Test exception");

            return Ok();
        }

        [HttpPut]
        public IActionResult Put()
        {
            throw new ForbiddenException("Test exception");
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            throw new ValidationException();
            return Ok();
        }

    }
}
