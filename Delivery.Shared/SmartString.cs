namespace Delivery.Shared;

public abstract record SmartString<TPolicy> where TPolicy : IStringPolicy, new()
{
    public string Value { get; }

    protected SmartString(string value)
    {
        var policy = new TPolicy();
        var normalized = policy.Normalize(value);

        if (!policy.IsValid(normalized))
            throw new ArgumentException($"Invalid value for {typeof(TPolicy).Name}: \"{value}\"", nameof(value));

        Value = normalized;
    }

    public override string ToString() => Value;

    public static bool TryNew(string? input, out SmartString<TPolicy>? result)
    {
        result = null;
        if (input is null) return false;

        TPolicy policy = new();
        string normalized = policy.Normalize(input);

        if (!policy.IsValid(normalized))
            return false;

        result = (SmartString<TPolicy>?)Activator.CreateInstance(
            typeof(SmartString<TPolicy>).Assembly.FullName!,
            typeof(SmartString<TPolicy>).FullName!, true, 0, null,
            [input], null, null)!.Unwrap();

        return true;
    }
}
