using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mappings
{
    internal class OwnerMapping : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("CIA", "OWNER");

            builder.Property(p => p.ID)
                .HasColumnName("OWNERID");

            builder.Property(p => p.Name)
                .HasColumnName("OWNERID");

            builder.Property(p => p.DateOfBirth)
                .HasColumnName("DATEOFBIRTH");

            builder.Property(p => p.Address)
                .HasColumnName("ADDRESS");
        }
    }
}
