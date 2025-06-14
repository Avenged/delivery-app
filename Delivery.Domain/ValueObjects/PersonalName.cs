using Delivery.Shared;
using System.Text.Json;

namespace Delivery.Domain.ValueObjects;

public sealed record PersonalName(
    TneString GivenName,
    TneString FamilyName,
    TneString? MiddleName = null,
    TneString? Prefix = null,
    TneString? Suffix = null) : ValueObjectBase
{
    [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
    protected override Type EqualityContract => base.EqualityContract;

    public string FullName =>
        string.Join(" ", new[]
        {
            Prefix,
            GivenName,
            MiddleName,
            FamilyName,
            Suffix
        }.Where(x => !string.IsNullOrWhiteSpace(x?.Value)));

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, SerializerOptions.ToString);
    }

    public static PersonalName Create(
        TneString givenName,
        TneString familyName,
        TneString? middleName = null,
        TneString? prefix = null,
        TneString? suffix = null)
    {
        return new PersonalName(
            givenName,
            familyName,
            middleName,
            prefix,
            suffix);
    }
}