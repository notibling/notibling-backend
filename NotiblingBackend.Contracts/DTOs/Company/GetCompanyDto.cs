using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Contracts.DTOs.Company
{
    public class GetCompanyDto
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? LegalName { get; set; }
        public string? Rut { get; set; }
        public string Country { get; set; }
        public DateOnly FoundationDate { get; set; }
        public string? Phone { get; set; }
        public string? WebSite { get; set; }
        public CompanyTypeDto CompanyType { get; set; }
        //public int CompanyTypeId { get; set; }
        public CompanyLevelsDto CompanyLevels { get; set; }
        //public int CompanyLevelId { get; set; }
        public IndustryDto Industry { get; set; }
        //public int IndustryId { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }
        public string CompanyRole { get; set; }
        public string UserRole { get; set; }
        public string AccountStatus { get; set; }
        public string TimeZone { get; set; }
    }
}
