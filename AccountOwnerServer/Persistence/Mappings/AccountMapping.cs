using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mappings
{
    internal class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("ACCOUNT", "CIA");

            builder.Property(p => p.ID)
                .HasColumnName("ACCOUNTID");

            builder.Property(p => p.DateCreated)    
                .HasColumnName("DATECREATED");

            builder.Property(p => p.AccountType)
                .HasColumnName("ACCOUNTTYPE");

            builder.Property(p => p.OwnerFK)
                .HasColumnName("OWNERFK");
        }
    }
}
