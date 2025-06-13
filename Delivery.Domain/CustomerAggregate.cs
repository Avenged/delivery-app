using Delivery.Domain.ValueObjects;
using Delivery.Shared;

namespace Delivery.Domain;

public class CustomerAggregate : IAggregateRoot
{
    public static CustomerAggregate Create(
        Guid id, 
        PersonalName name, 
        PhoneNumber phoneNumber, 
        Address address,
        IReadOnlyEmailList emails,
        DateTime dateOfBirth)
    {
        DateTime now = DateTime.UtcNow;

        CustomerAggregate customer = new()
        {
            Id = id,
            Name = name,
            PhoneNumber = phoneNumber,
            Address = address,
            Emails = emails,
            DateOfBirth = dateOfBirth,
            CreatedAt = now,
            UpdatedAt = now,
        };
        return customer;
    }

    private CustomerAggregate() { }

    public Guid Id { get; private set; }
    public PersonalName Name { get; private set; } = null!;
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public IReadOnlyEmailList Emails { get; private set; } = null!;
    public DateTime DateOfBirth { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
}