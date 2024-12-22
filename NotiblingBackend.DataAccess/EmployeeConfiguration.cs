using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NotiblingBackend.Domain.Entities;
using NotiblingBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotiblingBackend.Domain.Enums.Converter;
using System.Reflection.Emit;

namespace NotiblingBackend.DataAccess
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            //Unique
            builder.HasIndex(e => e.EmployeeId).IsUnique();

            //Enum Converter
            var userRoleConverter = new EnumToStringConverter<UserRole>();
            builder.Property(e => e.UserRole).HasConversion(userRoleConverter);
            var accountStatusConverter = new EnumToStringConverter<AccountStatus>();
            builder.Property(e => e.AccountStatus).HasConversion(accountStatusConverter);
        }
    }
}
