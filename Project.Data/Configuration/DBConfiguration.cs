using Microsoft.AspNet.Identity.EntityFramework;
using Project.Model;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Data.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<USER>
    {
        public UserConfiguration()
        {
            ToTable("USERS").HasKey(p => p.USER_ID);
            Property(u => u.USER_ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(u => u.USERNAME).HasMaxLength(10).IsRequired();
            Property(u => u.PASSWORD).HasMaxLength(500).IsRequired();
        }

    }
    public class UserTokenConfiguration : EntityTypeConfiguration<USER_TOKEN>
    {
        public UserTokenConfiguration()
        {
            ToTable("USER_TOKENS").HasKey(p => p.TOKEN_ID);
            Property(d => d.TOKEN_ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(d => d.GRANTED_ON).HasColumnType("datetime2").HasPrecision(0);
            Property(d => d.EXPIRES_ON).HasColumnType("datetime2").HasPrecision(0);
        }

    }
    public class AccountConfiguration : EntityTypeConfiguration<ACCOUNT>
    {
        public AccountConfiguration()
        {
            ToTable("ACCOUNTS").HasKey(p => p.ACCOUNT_ID);
            Property(d => d.ACCOUNT_ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(d => d.ACCOUNT_NO).HasColumnName("ACCOUNT_NO");
            Property(d => d.FIRSTNAME).HasMaxLength(50).IsRequired();
            Property(d => d.LASTNAME).HasMaxLength(50).IsRequired();
            Property(d => d.MIDDLENAME).HasMaxLength(50);
            Property(d => d.STATUS).IsRequired();
        }
    }
    public class PaymentConfiguration : EntityTypeConfiguration<PAYMENT>
    {
        public PaymentConfiguration()
        {
            ToTable("PAYMENTS").HasKey(p => p.PAYMENT_ID);
            Property(d => d.PAYMENT_ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
            Property(d => d.ACCOUNT_NO).IsRequired();
            Property(d => d.AMOUNT).IsRequired();
            Property(d => d.DATE).HasColumnType("datetime2").HasPrecision(0);
        }
    }
}