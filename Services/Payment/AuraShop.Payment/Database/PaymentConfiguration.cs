using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuraShop.Payment.Database;

public class PaymentConfiguration : IEntityTypeConfiguration<Features.Payments.Payment>
{
    public void Configure(EntityTypeBuilder<Features.Payments.Payment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.OrderNumber).IsRequired();
        builder.Property(x => x.Amount).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.CreateDate).IsRequired();
        builder.Property(x => x.PaymentReferenceId).IsRequired(false);
    }
}