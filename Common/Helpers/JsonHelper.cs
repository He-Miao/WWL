using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Common.Helpers
{
    /// <summary>
    /// json帮助类
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// //将对象转为JSON字符串。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            });
        }

        /// <summary>
        /// 将json字符串转为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            })!;//使用“!”操作符强制断言返回值不为 null
        }
    }
}
