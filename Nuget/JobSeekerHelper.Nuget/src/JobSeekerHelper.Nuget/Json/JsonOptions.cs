using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace JobSeekerHelper.Nuget.Json;

public class JsonOptions
{
    internal static readonly JsonSerializerOptions DefaultSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        TypeInfoResolver = JsonSerializer.IsReflectionEnabledByDefault ? JsonOptions.CreateDefaultTypeResolver() : JsonTypeInfoResolver.Combine(new ReadOnlySpan<IJsonTypeInfoResolver>())
    };

    /// <summary>
    /// Gets the <see cref="T:System.Text.Json.JsonSerializerOptions" />.
    /// </summary>
    public JsonSerializerOptions SerializerOptions { get; } = new JsonSerializerOptions(JsonOptions.DefaultSerializerOptions);

#nullable disable
    private static IJsonTypeInfoResolver CreateDefaultTypeResolver()
    {
        return (IJsonTypeInfoResolver) new DefaultJsonTypeInfoResolver();
    }
}