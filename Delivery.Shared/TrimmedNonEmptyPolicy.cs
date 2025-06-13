namespace Delivery.Shared;

public sealed class TrimmedNonEmptyPolicy : IStringPolicy
{
    public string Normalize(string input) => input.Trim();
    public bool IsValid(string normalized) => normalized.Length > 0;
}