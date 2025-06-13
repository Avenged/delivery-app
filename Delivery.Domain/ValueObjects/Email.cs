using Delivery.Shared;
using System.Text.Json;

namespace Delivery.Domain.ValueObjects;

public sealed record Email(TrimmedNonEmptyString Value)
{
    public static Email Create(TrimmedNonEmptyString value)
    {
        return new Email(value);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}