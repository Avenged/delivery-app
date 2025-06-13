using Delivery.Domain.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Delivery.Infrastructure.Converters;

public class EmailListJsonConverter : JsonConverter<EmailList>
{
    public override EmailList Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var emails = JsonSerializer.Deserialize<List<Email>>(ref reader, options);
        return EmailList.Create(emails?.ToArray() ?? []);
    }

    public override void Write(Utf8JsonWriter writer, EmailList value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (List<Email>)value, options);
    }
}