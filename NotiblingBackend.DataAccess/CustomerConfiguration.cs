using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NotiblingBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotiblingBackend.Domain.Enums;
using NotiblingBackend.Domain.Enums.Converter;

namespace NotiblingBackend.DataAccess
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            //Unique
            builder.HasIndex(c => c.Email).IsUnique();
            builder.HasIndex(c => c.UserName).IsUnique();
            builder.HasIndex(c => c.IdentityDocument).IsUnique();

            //Enum Converter
            var userRoleConverter = new EnumToStringConverter<UserRole>();
            builder.Property(e => e.UserRole).HasConversion(userRoleConverter);
            var accountStatusConverter = new EnumToStringConverter<AccountStatus>();
            builder.Property(e => e.AccountStatus).HasConversion(accountStatusConverter);
            var genderConverter = new EnumToStringConverter<Gender>();
            builder.Property(e => e.Gender).HasConversion(genderConverter);

        }
    }
}
