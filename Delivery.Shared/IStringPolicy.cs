namespace Delivery.Shared;

public interface IStringPolicy
{
    string Normalize(string input);
    bool IsValid(string normalized);
}
