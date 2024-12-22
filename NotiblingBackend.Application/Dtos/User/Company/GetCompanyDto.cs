using NotiblingBackend.Domain.Entities;
using NotiblingBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.Dtos.User.Company
{
    public class GetCompanyDto
    {
        public int Id { get; set; }
        public Guid CompanyId { get; internal set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? LegalName { get; set; }
        public string? Rut { get; set; }
        public string Country { get; internal set; }
        public DateOnly FoundationDate { get; internal set; }
        public string? Phone { get; set; }
        public string? WebSite { get; internal set; }
        public CompanyTypeDto CompanyType { get; set; }
        public CompanyLevelsDto CompanyLevels { get; set; }
        public IndustryDto Industry { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CompanyRole { get; set; }
        public string UserRole { get; set; }
        public string AccountStatus { get; set; }
    }
}
