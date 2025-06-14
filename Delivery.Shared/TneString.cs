using System.Diagnostics;

namespace Delivery.Shared;

/// <summary>
/// TrimmedNonEmptyString. This is a string that is trimmed and not empty.
/// </summary>
[DebuggerDisplay("{Value}")]
public record TneString : SmartString<TnePolicy>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    protected override Type EqualityContract => base.EqualityContract;

    public TneString(string value) : base(value) { }

    public override string ToString() => Value;

    public static explicit operator TneString(string value) => new(value);
    public static implicit operator string(TneString instance) => instance.Value;
}