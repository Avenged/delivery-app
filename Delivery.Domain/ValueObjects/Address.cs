using Delivery.Shared;
using System.Diagnostics;
using System.Text.Json;

namespace Delivery.Domain.ValueObjects;

public sealed record Address(
    TneString Street,
    TneString City,
    TneString State,
    TneString PostalCode,
    TneString Country) : ValueObjectBase
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    protected override Type EqualityContract => base.EqualityContract;

    public static Address Create(
        TneString street,
        TneString city,
        TneString state,
        TneString postalCode,
        TneString country)
    {
        return new Address(
            street,
            city,
            state,
            postalCode,
            country);
    }

    public override string ToString()
        => JsonSerializer.Serialize(this, SerializerOptions.ToString);

    public string FullAddress =>
        $"{Street}, {City}, {State} {PostalCode}, {Country}";
}
