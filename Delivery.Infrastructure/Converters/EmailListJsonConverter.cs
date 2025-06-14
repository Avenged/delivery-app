using Delivery.Domain.ValueObjects;
using Delivery.Shared;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Delivery.Infrastructure.Converters;

public class EmailListJsonConverter : JsonConverter<IReadOnlyEmailList>
{
    public override EmailList Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        List<string>? emails = JsonSerializer.Deserialize<List<string>>(ref reader, options) ?? [];
        return EmailList.Create(emails.Select(x => Email.Create(new TneString(x))));
    }

    public override void Write(Utf8JsonWriter writer, IReadOnlyEmailList value, JsonSerializerOptions options)
    {
        string[] values = [.. value.Select(v => v.Value.Value)];
        JsonSerializer.Serialize(writer, values, options);
    }
}