namespace Delivery.Shared;

public sealed class TnePolicy : IStringPolicy
{
    public string Normalize(string input) => input.Trim();
    public bool IsValid(string normalized) => normalized.Length > 0;
}