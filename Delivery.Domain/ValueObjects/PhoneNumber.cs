using Delivery.Shared;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Delivery.Domain.ValueObjects;

[DebuggerDisplay("{Value}")]
public sealed partial record PhoneNumber : ValueObjectBase
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static readonly Regex _phoneRegex = PhoneNumberRegex();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public TneString Value { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    protected override Type EqualityContract => base.EqualityContract;

    private PhoneNumber(TneString value)
    {
        Value = value;
    }

    public static PhoneNumber Create(TneString value)
    {
        if (!_phoneRegex.IsMatch(value))
            throw new ArgumentException("Invalid phone number format.", nameof(value));

        return new PhoneNumber(value);
    }

    public static bool TryCreate(TneString? value, out PhoneNumber? result)
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

    [GeneratedRegex(@"^\+?[0-9]{7,15}$", RegexOptions.Compiled)]
    private static partial Regex PhoneNumberRegex();

    public static implicit operator string(PhoneNumber instance) => instance.Value;
}