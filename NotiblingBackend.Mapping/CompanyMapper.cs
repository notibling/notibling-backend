using NotiblingBackend.Contracts.DTOs.Company;
using NotiblingBackend.Domain.Entities;

namespace NotiblingBackend.Mapping
{
    public class CompanyMapper
    {
        public static Company AddFromDto(AddCompanyDto companyDto)
        {
            return new Company()
            {
                FirstName = companyDto.FirstName,
                LastName = companyDto.LastName,
                Email = companyDto.Email.ToLower(),
                Password = companyDto.Password,
                CompanyName = companyDto.CompanyName,
                LegalName = companyDto.LegalName,
                Rut = companyDto.Rut,
                Phone = companyDto.Phone,
                CompanyLevelId = companyDto.CompanyLevelId,
                IndustryId = companyDto.IndustryId,
                CompanyTypeId = companyDto.CompanyTypeId,
                TimeZone = companyDto.TimeZone,
                Country = companyDto.Country,
                FoundationDate = companyDto.FoundationDate,
                WebSite = companyDto.WebSite,
            };
        }

        public static UpdateCompanyDto FromEdit(CompanyDto companyDto)
        {
            if (companyDto == null)
                throw new ArgumentNullException(nameof(companyDto), "Los datos no pueden ser nulo.");

            return new UpdateCompanyDto()
            {
                UserName = companyDto.UserName?.Trim(),
                CompanyName = companyDto.CompanyName?.Trim(),
                LegalName = companyDto.LegalName?.Trim(),
                FirstName = companyDto.FirstName?.Trim(),
                LastName = companyDto.LastName?.Trim(),
                Rut = companyDto.Rut?.Trim(),
                Country = companyDto.Country?.Trim(),
                FoundationDate = companyDto.FoundationDate,
                Phone = companyDto.Phone?.Trim(),
                WebSite = companyDto.WebSite?.Trim(),
                CompanyLevelId = companyDto.CompanyLevelId,
                IndustryId = companyDto.IndustryId,
                CompanyTypeId = companyDto.CompanyTypeId,
            };
        }

        public static CompanyDto ToDto(Company company)
        {
            return new CompanyDto()
            {
                Id = company.Id,
                //CompanyId = company.CompanyId,
                UserName = company.UserName,
                CompanyName = company.CompanyName,
                FirstName = company.FirstName,
                LastName = company.LastName,
                Email = company.Email,
                LegalName = company.LegalName,
                Rut = company.Rut,
                Country = company.Country,
                FoundationDate = company.FoundationDate,
                Phone = company.Phone,
                WebSite = company.WebSite,
                CompanyTypeId = company.CompanyType.TypeCompanyId,
                CompanyLevelId = company.CompanyLevels.LevelCompanyId,
                IndustryId = company.Industry.IndustryId,
                CompanyRole = company.CompanyRole.ToString(),
                UserRole = company.UserRole.ToString(),
                AccountStatus = company.AccountStatus.ToString(),
            };
        }

        public static Company ToEntity(UpdateCompanyDto company)
        {
            return new Company()
            {
                UserName = company.UserName,
                CompanyName = company.CompanyName,
                LegalName = company.LegalName,
                FirstName = company.FirstName,
                LastName = company.LastName,
                Rut = company.Rut,
                Country = company.Country,
                FoundationDate = (DateOnly)company.FoundationDate,
                Phone = company.Phone,
                WebSite = company.WebSite,
                CompanyTypeId = (int)company.CompanyTypeId,
                CompanyLevelId = (int)company.CompanyLevelId,
                IndustryId = (int)company.IndustryId
            };
        }

        internal static IEnumerable<CompanyDto> ToList(IEnumerable<Company> companies)
        {
            var companiesDto = companies.Select(c => ToDto(c)).ToList();

            return companiesDto;
        }
    }
}
