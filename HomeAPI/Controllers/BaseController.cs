using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HomeAPI.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected virtual void Logs(string str)
        {
            Log.Error(str);
        }
    }
}
