using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Helpers
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Utils
    {
        public static void CoverNull<T>(T model) where T : class
        {
            if (model == null)
            {
                return;
            }
            var typeFromHandle = typeof(T);
            var properties = typeFromHandle.GetProperties();
            var array = properties;
            for (var i = 0; i < array.Length; i++)
            {
                var propertyInfo = array[i];
                if (propertyInfo.GetValue(model, null) == null)
                {
                    propertyInfo.SetValue(model, GetDefaultVal(propertyInfo.PropertyType.Name), null);
                }
            }
        }
        public static object GetDefaultVal(string typename)
        {
            return typename switch
            {
                "Boolean" => false,
                "DateTime" => default(DateTime),
                "Date" => default(DateTime),
                "Double" => 0.0,
                "Single" => 0f,
                "Int32" => 0,
                "String" => string.Empty,
                "Decimal" => 0m,
                _ => null,
            };
        }

        /// <summary>
        /// 获取项目目录
        /// </summary>
        /// <returns></returns>
        //public static string GetProjectDirectory(string projectName)
        //{
        //    string projectDirectory = AppContext;
        //    string projectPath = Path.Combine(projectDirectory, projectName);
        //    return projectPath;
        //}

        #region IP
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        public static string GetIP(HttpRequest request)
        {
            if (request == null)
            {
                return "";
            }

            var ip = request.Headers["X-Real-IP"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = request.Headers["X-Forwarded-For"].FirstOrDefault();
            }
            if (string.IsNullOrEmpty(ip))
            {
                ip = request.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            }
            if (string.IsNullOrEmpty(ip) || !IsIP(ip))
            {
                ip = "127.0.0.1";
            }

            return ip;
        }
        #endregion

        /// <summary>
        /// 根据枚举值获取枚举说明
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum value)
        {
            // 获取枚举类型的类型
            Type enumType = value.GetType();
            // 获取枚举值对应的字段信息
            System.Reflection.FieldInfo fieldInfo = enumType.GetField(value.ToString());
            // 获取字段上的 DescriptionAttribute 特性
            if (fieldInfo != null)
            {
                System.ComponentModel.DescriptionAttribute[] attributes =
                    (System.ComponentModel.DescriptionAttribute[])fieldInfo.GetCustomAttributes(
                        typeof(System.ComponentModel.DescriptionAttribute), false);
                // 返回 Description 属性的值，如果找到了则返回该值，否则返回枚举值的字符串形式
                if (attributes != null && attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }
            return value.ToString();
        }


        public static bool ToBool(this object thisValue, bool errorvalue = false)
        {
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out bool reval))
            {
                return reval;
            }
            return errorvalue;
        }
    }
}
