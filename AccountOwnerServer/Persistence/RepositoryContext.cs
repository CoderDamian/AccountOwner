using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Persistence.Mappings;
using System.Diagnostics;
using System.Reflection.Emit;

namespace Persistence
{
    public class RepositoryContext : DbContext
    {
        internal DbSet<Account> Accounts{ get; set; }
        internal DbSet<Owner> Owners { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base (options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (OracleConfiguration.TnsAdmin is null)
            {
                OracleConfiguration.TnsAdmin = @"C:\Users\Fmla\Documents\OracleWallet\MyERP\";
                OracleConfiguration.WalletLocation = OracleConfiguration.TnsAdmin;
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AccountMapping());
            builder.ApplyConfiguration(new OwnerMapping());
        }
    }
}