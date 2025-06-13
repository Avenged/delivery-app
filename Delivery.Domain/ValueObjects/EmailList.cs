namespace Delivery.Domain.ValueObjects;

public sealed class EmailList : List<Email>, IReadOnlyEmailList
{
    private EmailList(IEnumerable<Email> collection) : base(collection)
    {
    }

    public static EmailList Create(params IEnumerable<Email> emails)
    {
        return new EmailList(emails);
    }
}