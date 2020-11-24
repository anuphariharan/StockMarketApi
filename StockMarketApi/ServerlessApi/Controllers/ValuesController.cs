using Microsoft.AspNetCore.Mvc;

namespace ServerlessApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return "Hello from Lambda!";
        }
    }
}
