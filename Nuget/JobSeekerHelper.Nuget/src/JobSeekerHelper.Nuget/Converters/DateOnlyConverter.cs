using System.Text.Json;
using System.Text.Json.Serialization;

namespace JobSeekerHelper.Nuget.Converters;

public class DateOnlyConverter : JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (value is not null &&
            DateOnly.TryParseExact(value, Format, null, System.Globalization.DateTimeStyles.None, out var date))
        {
            return date;
        }

        throw new JsonException($"Invalid DateOnly format. Expected format: {Format}");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format));
    }
}