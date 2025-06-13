namespace Delivery.Shared;

public record TrimmedNonEmptyString : SmartString<TrimmedNonEmptyPolicy>
{
    public TrimmedNonEmptyString(string value) : base(value) { }

    public static explicit operator TrimmedNonEmptyString(string value) => new(value);
    public static implicit operator string(TrimmedNonEmptyString instance) => instance.Value;
}