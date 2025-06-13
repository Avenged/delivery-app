using Delivery.Shared;

namespace Delivery.Domain.ValueObjects;

public sealed record PersonalName(
    TrimmedNonEmptyString GivenName,
    TrimmedNonEmptyString FamilyName,
    TrimmedNonEmptyString? MiddleName = null,
    TrimmedNonEmptyString? Prefix = null,
    TrimmedNonEmptyString? Suffix = null)
{
    public string FullName =>
        string.Join(" ", new[]
        {
            Prefix,
            GivenName,
            MiddleName,
            FamilyName,
            Suffix
        }.Where(x => !string.IsNullOrWhiteSpace(x?.Value)));

    public static PersonalName Create(
        TrimmedNonEmptyString givenName,
        TrimmedNonEmptyString familyName,
        TrimmedNonEmptyString? middleName = null,
        TrimmedNonEmptyString? prefix = null,
        TrimmedNonEmptyString? suffix = null)
    {
        return new PersonalName(
            givenName,
            familyName,
            middleName,
            prefix,
            suffix);
    }
}