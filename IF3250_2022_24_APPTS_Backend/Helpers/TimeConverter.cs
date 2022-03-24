using System.Text.Json.Serialization;
using System.Globalization;
using System.Text.Json;

namespace IF3250_2022_24_APPTS_Backend.Helpers
{
    public class TimeConverter : JsonConverter<TimeOnly>
    {
        private string formatTime = "HH:mm";
        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeOnly.ParseExact(reader.GetString(), formatTime, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(formatTime));
        }
    }
}
