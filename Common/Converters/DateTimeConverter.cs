using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Converters
{
    /// <summary>
    /// 自定义的日期转换器 DateTimeConverter，继承自 System.Text.Json.Serialization.JsonConverter<DateTime>
    /// </summary>
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _dateFormat;

        public DateTimeConverter(string dateFormat)
        {
            _dateFormat = dateFormat;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), _dateFormat, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateFormat));
        }
    }
}
