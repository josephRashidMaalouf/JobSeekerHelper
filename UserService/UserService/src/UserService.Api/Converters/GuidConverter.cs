using System.Text.Json;
using System.Text.Json.Serialization;

namespace UserService.Api.Converters;

public class GuidConverter : JsonConverter<Guid>
{
    public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String &&
            Guid.TryParse(reader.GetString(), out var guid))
        {
            return guid == Guid.Empty ? Guid.NewGuid() : guid;
        }

        return Guid.NewGuid();
    }

    public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}