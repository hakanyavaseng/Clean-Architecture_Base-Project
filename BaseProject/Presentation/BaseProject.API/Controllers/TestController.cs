using BaseProject.API.Controllers.Base;
using BaseProject.Application.Interfaces.Repositories.Common;
using BaseProject.Application.Interfaces.Services.Tokens;
using BaseProject.Domain.DTOs.AppUserDtos;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Filtering;
using BaseProject.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using System.Net;

namespace BaseProject.API.Controllers
{

    public class TestController : BaseController<TestController>
    {
        private readonly ILogger<Product> logger;
        private readonly ITokenService token;
        private readonly UserManager<AppUser> userManager;
        private readonly IRepositoryManager repositoryManager;
        public TestController(ILogger<Product> logger, ITokenService token, UserManager<AppUser> userManager, IRepositoryManager repositoryManager)
        {

            this.logger = logger;
            this.token = token;
            this.userManager = userManager;
            this.repositoryManager = repositoryManager;
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

        [HttpPost("CreateUser")]
        public async Task<IActionResult> Post([FromBody] Create_User_Dto dto)
        {
            var user = new AppUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };
            IdentityResult result = await userManager.CreateAsync(user, dto.Password);
            await repositoryManager.SaveAsync();

            return Ok(user.Id);
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
