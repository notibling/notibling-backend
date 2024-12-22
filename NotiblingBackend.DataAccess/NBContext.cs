using Microsoft.EntityFrameworkCore;
using NotiblingBackend.Domain.Entities;
using NotiblingBackend.Domain.Enums;
using NotiblingBackend.Domain.Enums.Converter;

namespace NotiblingBackend.DataAccess
{
    public class NBContext : DbContext
    {
        public NBContext(DbContextOptions options) : base(options)
        {
        }

        #region DbSet
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyType> CompaniesTypes { get; set; }
        public DbSet<CompanyLevels> CompaniesLevels { get; set; }
        public DbSet<Industry> Industries { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            // Configuración TPT
            modelBuilder.Entity<User>()
                .UseTptMappingStrategy();
            // Filtro global aplicado a la clase base
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDeleted);
            //Time Zone
            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasColumnType("timestamp without time zone") // Almacenar sin zona horaria (en UTC)
                .IsRequired()
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .Property(u => u.UpdatedAt)
                .HasColumnType("timestamp without time zone"); // Almacenar sin zona horaria (en UTC)
                //.ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<User>()
                .Property(u => u.DeletedAt)
                .HasColumnType("timestamp without time zone") // Almacenar sin zona horaria (en UTC)
                .ValueGeneratedOnAddOrUpdate();

            // Configuración para Employee
            modelBuilder.Entity<Employee>()
                .HasBaseType<User>();  // Employee hereda de User

            // Configuración para Company
            modelBuilder.Entity<Company>()
                .HasBaseType<User>();  // Company hereda de User

            // Configuración para Customer
            modelBuilder.Entity<Customer>()
                .HasBaseType<User>();  // Company hereda de User

            //Apply Configuration
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());

            base.OnModelCreating(modelBuilder);

            //Datos Iniciales
            modelBuilder.Entity<CompanyType>().HasData(
                new CompanyType { TypeCompanyId = 1, TypeName = "Unipersonal", Abbreviation = "UP" },
                new CompanyType { TypeCompanyId = 2, TypeName = "Cooperativas", Abbreviation = "COOP" },
                new CompanyType { TypeCompanyId = 3, TypeName = "Sociedad Anónima", Abbreviation = "SA" },
                new CompanyType { TypeCompanyId = 4, TypeName = "Sociedad por Acciones Simplificada", Abbreviation = "SAS" },
                new CompanyType { TypeCompanyId = 5, TypeName = "Sociedad de Responsabilidad Limitada", Abbreviation = "SRL" },
                new CompanyType { TypeCompanyId = 6, TypeName = "Sociedad Colectiva", Abbreviation = "SC" },
                new CompanyType { TypeCompanyId = 7, TypeName = "Sociedad en Comandita Simple", Abbreviation = "SCS" },
                new CompanyType { TypeCompanyId = 8, TypeName = "Sociedad en Comandita por Acciones", Abbreviation = "SCA" },
                new CompanyType { TypeCompanyId = 9, TypeName = "Sociedad de Capital e Industria", Abbreviation = "SCI" }
            );


            modelBuilder.Entity<CompanyLevels>().HasData(
                new CompanyLevels { LevelCompanyId = 1, LevelName = "Local" },
                new CompanyLevels { LevelCompanyId = 2, LevelName = "Nacional" },
                new CompanyLevels { LevelCompanyId = 3, LevelName = "Regional" },
                new CompanyLevels { LevelCompanyId = 4, LevelName = "Internacional" }
            );

            modelBuilder.Entity<Industry>().HasData(
                new Industry { IndustryId = 1, IndustryName = "Technologies" },
                new Industry { IndustryId = 2, IndustryName = "Agriculture" },
                new Industry { IndustryId = 3, IndustryName = "Food & Beverages" },
                new Industry { IndustryId = 4, IndustryName = "Automotive" },
                new Industry { IndustryId = 5, IndustryName = "Banking & Finance" },
                new Industry { IndustryId = 6, IndustryName = "Real Estate" },
                new Industry { IndustryId = 7, IndustryName = "Construction" },
                new Industry { IndustryId = 8, IndustryName = "Consulting" },
                new Industry { IndustryId = 9, IndustryName = "Education" },
                new Industry { IndustryId = 10, IndustryName = "Energy" },
                new Industry { IndustryId = 11, IndustryName = "Entertainment" },
                new Industry { IndustryId = 12, IndustryName = "Pharmaceutical" },
                new Industry { IndustryId = 13, IndustryName = "Government & Public Sector" },
                new Industry { IndustryId = 14, IndustryName = "Hospitality & Tourism" },
                new Industry { IndustryId = 15, IndustryName = "Textile Industry" },
                new Industry { IndustryId = 16, IndustryName = "Engineering" },
                new Industry { IndustryId = 17, IndustryName = "Logistics & Transportation" },
                new Industry { IndustryId = 18, IndustryName = "Manufacturing" },
                new Industry { IndustryId = 19, IndustryName = "Media & Communications" },
                new Industry { IndustryId = 20, IndustryName = "Mining" },
                new Industry { IndustryId = 21, IndustryName = "Fashion & Retail" },
                new Industry { IndustryId = 22, IndustryName = "Advertising & Marketing" },
                new Industry { IndustryId = 23, IndustryName = "Chemicals" },
                new Industry { IndustryId = 24, IndustryName = "Human Resources" },
                new Industry { IndustryId = 25, IndustryName = "Healthcare & Medicine" },
                new Industry { IndustryId = 26, IndustryName = "Insurance" },
                new Industry { IndustryId = 27, IndustryName = "Environmental Services" },
                new Industry { IndustryId = 28, IndustryName = "Legal Services" },
                new Industry { IndustryId = 29, IndustryName = "Personal Services" },
                new Industry { IndustryId = 30, IndustryName = "Telecommunications" },
                new Industry { IndustryId = 31, IndustryName = "Veterinary" },
                new Industry { IndustryId = 32, IndustryName = "Housing & Urban Development" },
                new Industry { IndustryId = 33, IndustryName = "Art & Culture" },
                new Industry { IndustryId = 34, IndustryName = "Science & Technology" },
                new Industry { IndustryId = 35, IndustryName = "Sports & Recreation" },
                new Industry { IndustryId = 36, IndustryName = "Higher Education" },
                new Industry { IndustryId = 37, IndustryName = "Electronics" },
                new Industry { IndustryId = 38, IndustryName = "Events & Conferences" },
                new Industry { IndustryId = 39, IndustryName = "Fintech" },
                new Industry { IndustryId = 40, IndustryName = "Environmental Management" },
                new Industry { IndustryId = 41, IndustryName = "Home & Garden" },
                new Industry { IndustryId = 42, IndustryName = "Import & Export" },
                new Industry { IndustryId = 43, IndustryName = "Civil Engineering" },
                new Industry { IndustryId = 44, IndustryName = "Mechanical Engineering" },
                new Industry { IndustryId = 45, IndustryName = "Research & Development" },
                new Industry { IndustryId = 46, IndustryName = "Jewelry" },
                new Industry { IndustryId = 47, IndustryName = "Maintenance & Repair" },
                new Industry { IndustryId = 48, IndustryName = "Mining & Extraction" },
                new Industry { IndustryId = 49, IndustryName = "Consumer Products" },
                new Industry { IndustryId = 50, IndustryName = "Digital Advertising" },
                new Industry { IndustryId = 51, IndustryName = "Public Relations" },
                new Industry { IndustryId = 52, IndustryName = "Security & Defense" },
                new Industry { IndustryId = 53, IndustryName = "Financial Services" },
                new Industry { IndustryId = 54, IndustryName = "Technical Services" },
                new Industry { IndustryId = 55, IndustryName = "Information Technology" },
                new Industry { IndustryId = 56, IndustryName = "Air Transportation" },
                new Industry { IndustryId = 57, IndustryName = "Maritime Transportation" },
                new Industry { IndustryId = 58, IndustryName = "Land Transportation" },
                new Industry { IndustryId = 59, IndustryName = "Sales & Distribution" }
            );

        }
    }
}
