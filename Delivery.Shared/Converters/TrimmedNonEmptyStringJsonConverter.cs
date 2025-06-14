using System.Text.Json;
using System.Text.Json.Serialization;

namespace Delivery.Shared.Converters;

public class TrimmedNonEmptyStringJsonConverter : JsonConverter<TneString>
{
    public override TneString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? str = reader.GetString();

        if (string.IsNullOrWhiteSpace(str))
            throw new JsonException("Failed to deserialize a TrimmedNonEmptyString. Value cannot be null, empty, or white-space.");

        return new TneString(str);
    }

    public override void Write(Utf8JsonWriter writer, TneString value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Value, options);
    }
}
