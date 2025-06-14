namespace Delivery.Shared;

public sealed class NePolicy : IStringPolicy
{
    public string Normalize(string input) => input;
    public bool IsValid(string normalized) => !string.IsNullOrWhiteSpace(normalized);
}