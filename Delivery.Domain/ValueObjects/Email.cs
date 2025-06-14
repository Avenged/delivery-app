using Delivery.Shared;
using System.Text.Json;

namespace Delivery.Domain.ValueObjects;

public sealed record Email(TneString Value)
{
    public static Email Create(TneString value)
    {
        return new Email(value);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}