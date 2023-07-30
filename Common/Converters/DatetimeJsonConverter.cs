using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Converters
{
    /// <summary>
    /// Json日期格式化
    /// </summary>
    public class DatetimeJsonConverter : JsonConverter<DateTime>
    {
        // 重写 Read 方法，将 JSON 字符串转换为 DateTime 对象
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // 判断令牌类型是否为字符串
            if (reader.TokenType == JsonTokenType.String)
            {
                // 尝试将字符串转换为 DateTime 对象
                if (DateTime.TryParse(reader.GetString(), out DateTime date))
                    return date;
            }
            // 无法转换或不是字符串类型，则直接获取 DateTime 对象
            return reader.GetDateTime();
        }

        // 重写 Write 方法，将 DateTime 对象转换为 JSON 字符串
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // 将 DateTime 格式化为指定格式的字符串，并写入到 JSON 字符串中
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
