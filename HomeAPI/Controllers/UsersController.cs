using Microsoft.AspNetCore.Mvc;

namespace HomeAPI.Controllers
{
    [Route("homeapi/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public class LoginModel
        {
            public string code { get; set; }
            public string password { get; set; }
            public string username { get; set; }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
                return Ok(new
                {
                    code = 0,
                    message = "登录成功",
                    data = new
                    {
                        token = "token-admin",
                    }
                });           
        }

        [HttpGet("info")]
        public IActionResult info()
        {
            return Ok(new
            {
                code = 0,
                message = "获取用户详情成功",
                data = new
                {
                    username = "admin",
                    roles = new[]
                    {
                        "admin"
                    }
                }
            });
        }
    }
}
