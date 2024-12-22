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
using System.Reflection.Emit;

namespace NotiblingBackend.DataAccess
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");

            //Index & Unique
            builder.HasIndex(c => c.CompanyId).IsUnique();
            builder.HasIndex(c => c.Email).IsUnique();
            builder.HasIndex(c => c.UserName).IsUnique();
            builder.HasIndex(c => c.LegalName).IsUnique();
            builder.HasIndex(c => c.Rut).IsUnique();

            //builder.HasOne(c => c.CompanyType)
            //    .WithMany()
            //    .HasForeignKey(c => c.CompanyType.CompanyTypeId);

            //Excluir registros eliminados automáticamente
            //builder.HasQueryFilter(c => !c.IsDeleted);

            //Relacion
            builder.HasOne(c => c.CompanyType)
                .WithMany(c=> c.Companies)
                .HasForeignKey(c => c.CompanyTypeId);
            builder.HasOne(c => c.CompanyLevels)
                .WithMany(c => c.Companies)
                .HasForeignKey(c => c.CompanyLevelId);
            builder.HasOne(c => c.Industry)
                .WithMany(c => c.Companies)
                .HasForeignKey(c => c.IndustryId);

            //GUID
            builder.Property(c => c.CompanyId).HasDefaultValueSql("gen_random_uuid()");

            //Enum Converter
            var userRoleConverter = new EnumToStringConverter<UserRole>();
            builder.Property(e => e.UserRole).HasConversion(userRoleConverter);
            var accountStatusConverter = new EnumToStringConverter<AccountStatus>();
            builder.Property(e => e.AccountStatus).HasConversion(accountStatusConverter);
            //var companyTypeConverter = new EnumToStringConverter<CompanyType>();
            //builder.Property(e => e.CompanyType).HasConversion(companyTypeConverter);
            var companyRoleConverter = new EnumToStringConverter<CompanyRole>();
            builder.Property(e => e.CompanyRole).HasConversion(companyRoleConverter);

        }
    }
}
