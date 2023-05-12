using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Order)
            .WithMany(x => x.Medicines)
            .HasForeignKey(x => x.OrderId);
    }
}
