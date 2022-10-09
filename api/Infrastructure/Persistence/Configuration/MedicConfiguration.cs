using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

internal class MedicConfiguration : IEntityTypeConfiguration<Medic>
{
    public void Configure(EntityTypeBuilder<Medic> builder)
    {
        builder.HasIndex(q => q.Email).IsUnique();
        builder
            .HasMany(q => q.Patients)
            .WithOne(p => p.Medic)
            .HasForeignKey(q => q.MedicId);
    }
}

