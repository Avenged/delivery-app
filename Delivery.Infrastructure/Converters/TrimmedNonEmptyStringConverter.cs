using Delivery.Shared;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Delivery.Infrastructure.Converters;

public class TrimmedNonEmptyStringConverter : ValueConverter<TneString, string>
{
    public TrimmedNonEmptyStringConverter()
        : base(
            v => v.Value,
            v => new TneString(v))
    { }
}