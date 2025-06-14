using System.Diagnostics;

namespace Delivery.Shared;

/// <summary>
/// Represents a non-empty string value. This means the string cannot be null, empty, or consist only of whitespace characters.
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed record NeString : SmartString<NePolicy>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    protected override Type EqualityContract => base.EqualityContract;

    public NeString(string value) : base(value) { }

    public override string ToString() => Value;

    public static explicit operator NeString(string value) => new(value);
    public static implicit operator string(NeString instance) => instance.Value;
}