using Common.Enums;
using Common.Helpers;
using HomeAPI.Model;
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

        /// <summary>
        /// 构造统一返回结构
        /// </summary>
        /// <param name="statusCodes">状态码</param>
        /// <param name="msg">消息</param>
        /// <param name="obj">数据对象</param>
        /// <returns></returns>
        [HttpGet]
        protected virtual IActionResult JsonResult(ResultCode statusCodes, string msg,  object? obj)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                msg = statusCodes.GetEnumDescription();
            }
            return Ok(new ResultData { Code = statusCodes, Msg = msg, Data = obj==null? new object():obj });
        }
    }
}
