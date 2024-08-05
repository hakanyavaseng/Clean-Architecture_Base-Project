using BaseProject.API.Controllers.Base;
using BaseProject.Application.Interfaces.Services.Tokens;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Filtering;
using BaseProject.Domain.Models;
using BaseProject.Domain.Models.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using System.Net;

namespace BaseProject.API.Controllers
{

    public class TestController : BaseController<TestController>
    {
        private readonly ILogger<Product> logger;
        private readonly ITokenService token;

        public TestController(ILogger<Product> logger, ITokenService token)
        {

            this.logger = logger;
            this.token = token;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
             return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetToken()
        {
            var token = await this.token.GenerateTokenAsync("AAAAA-BBBBBB-CCCCCC-DDDDDD");
            return Ok(new
            {
                AccessToken = token,
                Success = true
            });
        }

        [Authorize]
        [HttpPost("test/{a}")]
        public Result Post(int a)
        {
            throw new NotFoundException("Requested source not found!");
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
