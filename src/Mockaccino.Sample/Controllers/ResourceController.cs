using Microsoft.AspNetCore.Mvc;

namespace Mockaccino.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly ILogger<ResourceController> _logger;

        public ResourceController(ILogger<ResourceController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        [MockFilter(MockName = "GetResourceById")]
        public Task<IActionResult> Get(string id)
        {
            throw new Exception("Mock not found!");
        }
    }
}