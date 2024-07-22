﻿using BaseProject.Application.Features.Queries.Products.GetAllProducts;
using BaseProject.Application.Interfaces.Repositories.Common;
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
        public TestController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            throw new Exception("Test exception");
            var products = await mediator.Send(new GetAllProductsRequest());
            return Ok(products);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetWithFilterAsync(QueryParameters queryParameters)
        {
            var products = await mediator.Send(new GetAllProductsRequest { QueryParameters = queryParameters });
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Post()
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
            throw new ValidationException()
            return Ok();
        }

    }
}
