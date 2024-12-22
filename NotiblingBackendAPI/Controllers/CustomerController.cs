using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NotiblingBackendAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {

        [HttpPost("add-customer")]
        public void Add([FromBody] string value)
        {
        }

        [HttpGet("get-customer/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("get-customers")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpPatch("update-customer/{id}")]
        public void Update(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
