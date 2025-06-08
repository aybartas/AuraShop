using AuraShop.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AuraShop.Order.Persistence.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd();

        }
    }
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
            builder.HasKey(x => x.OrderNumber);
            builder.Property(x => x.OrderNumber).ValueGeneratedNever();
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasMany(x => x.Items).WithOne(item => item.Order).HasForeignKey(x => x.OrderNumber);
            builder.OwnsOne(o => o.ShippingAddress, sa =>
            {
                sa.Property(a => a.Street)
                    .HasColumnName("ShippingStreet")
                    .IsRequired();

                sa.Property(a => a.City)
                    .HasColumnName("ShippingCity")
                    .IsRequired();

                sa.Property(a => a.State)
                    .HasColumnName("ShippingState")
                    .IsRequired();

                sa.Property(a => a.ZipCode)
                    .HasColumnName("ShippingZipCode")
                    .IsRequired();

                sa.Property(a => a.Country)
                    .HasColumnName("ShippingCountry")
                    .IsRequired();
            });

            builder.Property(o => o.Status).HasConversion<string>();
        }
    }
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd();

            builder.Property(x => x.UnitPrice).IsRequired();
            builder.Property(x => x.ProductName).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();

        }
    }
}
