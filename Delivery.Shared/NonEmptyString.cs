namespace Delivery.Shared;

/// <summary>
/// Represents a non-empty string value. This means the string cannot be null, empty, or consist only of whitespace characters.
/// </summary>
public sealed record NonEmptyString : SmartString<NonEmptyPolicy>
{
    public NonEmptyString(string value) : base(value) { }

    public static explicit operator NonEmptyString(string value) => new(value);
}