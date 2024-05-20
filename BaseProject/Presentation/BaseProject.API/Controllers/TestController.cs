using BaseProject.Application.Interfaces.Repositories.Common;
using BaseProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IRepositoryManager repositoryManager;
        public TestController(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await repositoryManager.GetReadRepository<Product>().GetAllAsync();
           
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
