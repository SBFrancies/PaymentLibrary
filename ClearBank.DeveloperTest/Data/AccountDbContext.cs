using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearBank.DeveloperTest.Data
{
    public class AccountDbContext : DbContext
    {
        public DbSet<AccountEntity> Accounts { get; set; }

        public DbSet<BackupAccountEntity> BackUpAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountEntity>()
                .HasKey(e => e.AccountNumber)
                .HasRequired(e => e.AccountNumber);

            modelBuilder.Entity<AccountEntity>()
                .Property(e => e.AllowedPaymentSchemes).HasColumnType("int");

            modelBuilder.Entity<AccountEntity>()
                .Property(e => e.Status).HasColumnType("int");

            modelBuilder.Entity<AccountEntity>()
                .ToTable("Accounts");

            modelBuilder.Entity<BackupAccountEntity>()
                .HasKey(e => e.AccountNumber)
                .HasRequired(e => e.AccountNumber);

            modelBuilder.Entity<BackupAccountEntity>()
                .Property(e => e.AllowedPaymentSchemes).HasColumnType("int");

            modelBuilder.Entity<BackupAccountEntity>()
                .Property(e => e.Status).HasColumnType("int");

            modelBuilder.Entity<BackupAccountEntity>()
                .ToTable("BackupAccounts");
        }
    }
}
