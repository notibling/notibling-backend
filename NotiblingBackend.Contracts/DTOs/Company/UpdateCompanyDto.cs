using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Contracts.DTOs.Company
{
    public class UpdateCompanyDto
    {
        public string? UserName { get; set; }
        public string? CompanyName { get; set; }
        public string? LegalName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Rut { get; set; }
        public string? Country { get; set; }
        public DateOnly? FoundationDate { get; set; }
        public string? Phone { get; set; }
        public string? WebSite { get; set; }
        public int? CompanyLevelId { get; set; }
        public int? IndustryId { get; set; }
        public int? CompanyTypeId { get; set; }
    }
}
