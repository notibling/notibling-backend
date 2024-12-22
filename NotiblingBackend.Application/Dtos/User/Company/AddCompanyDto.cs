using NotiblingBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.Dtos.User.Company
{
    public class AddCompanyDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string LegalName { get; set; }
        public string Rut { get; set; }
        public string Phone { get; set; }
        public int CompanyLevelId { get; set; }
        public int IndustryId { get; set; }
        public string TimeZone { get; set; }
        public string Country { get; set; }
        public int CompanyTypeId { get; set; }
        public DateOnly FoundationDate { get; set; }
        public string? WebSite { get; set; }
        //public UserRole UserRole { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }
        //public AccountStatus AccountStatus { get; set; }
    }
}
