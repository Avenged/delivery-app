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
        v => NewMethod1(v),
        v => NewMethod(v))
    { }

    private static string NewMethod1(IReadOnlyEmailList v)
    {
        //string[] values = [.. v.Select(v => v.Value.Value)];
        var s = JsonSerializer.Serialize(v, options);
        return s;
    }

    private static IReadOnlyEmailList NewMethod(string v)
    {
        var s = JsonSerializer.Deserialize<IReadOnlyEmailList>(v, options) ?? EmailList.Create([]);
        return s;
    }
}