using BaseProject.Domain.Enums;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace BaseProject.Persistence.Filtering
{
    public class MatchModeConverter : JsonConverter<MatchMode>
    {
        public override MatchMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return value switch
            {
                "Equals" => MatchMode.Equals,
                "Contains" => MatchMode.Contains,
                "StartsWith" => MatchMode.StartsWith,
                "EndsWith" => MatchMode.EndsWith,
                "GreaterThan" => MatchMode.GreaterThan,
                "LessThan" => MatchMode.LessThan,
                _ => throw new JsonException($"Unknown MatchMode value: {value}")
            };
        }

        public override void Write(Utf8JsonWriter writer, MatchMode value, JsonSerializerOptions options)
        {
            var stringValue = value switch
            {
                MatchMode.Equals => "Equals",
                MatchMode.Contains => "Contains",
                MatchMode.StartsWith => "StartsWith",
                MatchMode.EndsWith => "EndsWith",
                MatchMode.GreaterThan => "GreaterThan",
                MatchMode.LessThan => "LessThan",
                _ => throw new ArgumentOutOfRangeException()
            };
            writer.WriteStringValue(stringValue);
        }
    }
}