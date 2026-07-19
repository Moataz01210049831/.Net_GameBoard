using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyBGList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        [Route("/error")]

        public IActionResult Error()
        {
            return   Problem(
            title: "An unexpected error occurred testing developmet.",
            statusCode: 500
        );
        }


        [HttpGet]
        [Route("/test")]
        public IActionResult Test()
        {
            throw new Exception("test");
        }
    }
}
