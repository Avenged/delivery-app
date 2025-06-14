using Delivery.Shared.Converters;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Delivery.Shared;

public static class SerializerOptions
{
    public static new readonly JsonSerializerOptions ToString;

    static SerializerOptions()
    {
        ToString = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = false,
        };
        ToString.Converters.Add(new TrimmedNonEmptyStringJsonConverter());
    }
}