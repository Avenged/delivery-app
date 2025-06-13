using Delivery.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Delivery.Infrastructure.Converters;

public class EmailListToJsonConverter : ValueConverter<IReadOnlyEmailList, string>
{
    private static readonly JsonSerializerOptions options = new()
    {
        Converters = { new EmailListJsonConverter() }
    };

    public EmailListToJsonConverter() : base(
        v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
        v => JsonSerializer.Deserialize<EmailList>(v, options) ?? EmailList.Create())
    { }
}