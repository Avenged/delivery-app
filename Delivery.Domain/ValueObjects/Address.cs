using Delivery.Shared;

namespace Delivery.Domain.ValueObjects;

public sealed record Address(
    TrimmedNonEmptyString Street,
    TrimmedNonEmptyString City,
    TrimmedNonEmptyString State,
    TrimmedNonEmptyString PostalCode,
    TrimmedNonEmptyString Country)
{
    public static Address Create(
        TrimmedNonEmptyString street,
        TrimmedNonEmptyString city,
        TrimmedNonEmptyString state,
        TrimmedNonEmptyString postalCode,
        TrimmedNonEmptyString country)
    {
        return new Address(
            street,
            city,
            state,
            postalCode,
            country);
    }

    public string FullAddress =>
        $"{Street}, {City}, {State} {PostalCode}, {Country}";
}
