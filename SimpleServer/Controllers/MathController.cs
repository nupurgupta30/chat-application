using Microsoft.AspNetCore.Mvc;

namespace SimpleServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MathController : ControllerBase
    {
        // GET api/math/add?num1=5&num2=3
        [HttpGet("add")]
        public ActionResult<int> AddNumbers(int num1, int num2)
        {
            return num1 + num2;
        }
    }
}
