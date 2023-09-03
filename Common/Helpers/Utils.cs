using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace Common.Helpers
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// 将一个对象的属性中的空值（null）替换为其对应类型的默认值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="model">模型</param>
        public static void CoverNull<T>(T model) where T : class
        {
            // 如果模型为空则直接返回，不需要进行处理
            if (model == null)
            {
                return;
            }
            var typeFromHandle = typeof(T); // 获取模型的类型
            var properties = typeFromHandle.GetProperties(); // 获取模型的属性集合
            var array = properties; // 为了方便遍历，将属性集合赋值给数组
            for (var i = 0; i < array.Length; i++)
            {
                var propertyInfo = array[i]; // 获取当前属性信息
                // 检查属性值是否为空
                if (propertyInfo.GetValue(model, null) == null)
                {
                    // 如果属性值为空，则使用默认值替换空值
                    propertyInfo.SetValue(model, GetDefaultVal(propertyInfo.PropertyType.Name), null);
                }
            }
        }

        /// <summary>
        /// 根据类型名称获取默认值
        /// </summary>
        /// <param name="typename">类型名称</param>
        /// <returns>默认值</returns>
        public static object GetDefaultVal(string typename)
        {
            switch (typename)
            {
                case "Boolean":
                    return false; // 布尔类型的默认值为false
                case "Byte":
                case "SByte":
                case "Int16":
                case "UInt16":
                case "Int32":
                    return 0;
                case "UInt32":
                case "Int64":
                case "UInt64":
                case "IntPtr":
                case "UIntPtr":
                    return 0; // 整数类型的默认值为0
                case "Char":
                    return '\0'; // 字符类型的默认值为'\0'
                case "String":
                    return string.Empty; // 字符类型的默认值为'\0'
                case "Single":
                    return 0.0f; // 单精度浮点数类型的默认值为0.0
                case "Double":
                    return 0.0; // 双精度浮点数类型的默认值为0.0
                case "Decimal":
                    return 0.0m; // 十进制数类型的默认值为0.0
                case "DateTime":
                    return default(DateTime);
                case "Date":
                    return default(DateTime);
                default:
                    return null; // 其他类型返回null（引用类型）
            }
        }

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
        /// <summary>
        /// 获取IP
        /// </summary>
        /// <param name="request"></param>
        /// <returns>找不到时返回127.0.0.1</returns>
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
        /// 将对象转换为布尔类型
        /// </summary>
        /// <param name="thisValue">要转换的对象</param>
        /// <param name="errorvalue">转换失败时的默认值，默认为false</param>
        /// <returns>转换后的布尔值</returns>
        public static bool ToBool(this object thisValue, bool errorvalue = false)
        {
            // 检查对象是否为空、是否为 DBNull，并尝试将其转换为布尔类型
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out bool reval))
            {
                // 转换成功，返回转换后的布尔值
                return reval;
            }

            // 转换失败，返回指定的默认值
            return errorvalue;
        }

        /// <summary>
        /// 判断字符串是否为纯数字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string input)
        {
            Regex regex = new Regex("^[0-9]+$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断字符串是否为中文（包含中文字符）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsChinese(this string input)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]");
            return regex.IsMatch(input);
        }

        /// <summary>
        ///  分转为元的金额转换
        /// </summary>
        /// <param name="amountInCents">值</param>
        /// <returns></returns>
        public static decimal ConvertToYuan(long amountInCents)
        {
            decimal amountInYuan = amountInCents / 100m;
            return amountInYuan;
        }

        /// <summary>
        ///  元转换为分的金额转换
        /// </summary>
        /// <param name="amountInYuan">值</param>
        /// <returns></returns>
        public static long ConvertToCents(decimal amountInYuan)
        {
            long amountInCents = (long)(amountInYuan * 100);
            return amountInCents;
        }
    }
}
