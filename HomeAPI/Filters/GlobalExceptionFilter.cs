﻿using Common.Helpers;
using HomeAPI.Model;
using HomeAPI.Model.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace HomeAPI.Filters
{
    /// <summary>
    ///  全局异常过滤器
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        readonly IWebHostEnvironment hostEnvironment;
        public GlobalExceptionFilter(IWebHostEnvironment _hostEnvironment)
        {
            hostEnvironment = _hostEnvironment;
        }
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                var result = new ResultData
                {
                    Code = ResultCode.Error,
                    Msg = "服务器发生未处理的异常"
                };
                if (hostEnvironment.IsDevelopment())
                {
                    result.Msg += "：" + context.Exception.Message;
                    result.Data = context.Exception.StackTrace;
                }
                context.Result = new ContentResult
                {
                    StatusCode = (int)ResultCode.Error,
                    ContentType = "application/json;charset=utf-8",
                    Content = result.ToJson()
                };
                Log.Error($"服务器内部错误：{result.ToJson()}");
                context.ExceptionHandled = true;
            }
        }
    }
}
