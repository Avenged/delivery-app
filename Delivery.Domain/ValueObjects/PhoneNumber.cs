using Delivery.Shared;
using System.Text.RegularExpressions;

namespace Delivery.Domain.ValueObjects;

public sealed record PhoneNumber
{
    private static readonly Regex _phoneRegex = new(@"^\+?[0-9]{7,15}$", RegexOptions.Compiled);

    public TrimmedNonEmptyString Value { get; }

    private PhoneNumber(TrimmedNonEmptyString value)
    {
        Value = value;
    }

    public static PhoneNumber Create(TrimmedNonEmptyString value)
    {
        if (!_phoneRegex.IsMatch(value))
            throw new ArgumentException("Invalid phone number format.", nameof(value));

        return new PhoneNumber(value);
    }

    public static bool TryCreate(TrimmedNonEmptyString? value, out PhoneNumber? result)
    {
        result = null;

        if (value is null)
            return false;

        if (!_phoneRegex.IsMatch(value))
            return false;

        result = new PhoneNumber(value);
        return true;
    }

    public override string ToString() => Value;
}