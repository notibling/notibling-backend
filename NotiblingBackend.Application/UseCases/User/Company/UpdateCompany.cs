using NotiblingBackend.Application.Interfaces.UseCases.User.Company;
using NotiblingBackend.Contracts.DTOs.Company;
using NotiblingBackend.Domain.Enums;
using NotiblingBackend.Domain.Interfaces.Repositories;
using NotiblingBackend.Mapping;
using NotiblingBackend.Utilities.Exceptions;

namespace NotiblingBackend.Application.UseCases.User.Company
{
    public class UpdateCompany : IUpdateCompany
    {
        #region Dependency Injection
        ICompanyRepository _companyRepository;
        IUserRepository _userRepository;

        public UpdateCompany(ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }
        #endregion
        public async Task<bool> Update(string companyId, UpdateCompanyDto companyDto)
        {

            if (string.IsNullOrEmpty(companyId)) throw new CompanyException("El ID no puede ser nulo.");

            if (companyDto == null)
                throw new CompanyException("No se han enviado datos para editar.");

            if (!string.IsNullOrEmpty(companyDto.UserName) && await _userRepository.VerifyUsername(companyDto.UserName))
                throw new CompanyException("El nombre de usuario ingresado ya existe.");

            var company = await _companyRepository.GetByGuid(companyId);

            Updating(company, companyDto);

            await _companyRepository.Update(company);
            return true;
        }

        private static void Updating(Domain.Entities.Company company, UpdateCompanyDto companyDto)
        {
            company.UserName = string.IsNullOrEmpty(companyDto.UserName) ? company.UserName : companyDto.UserName;
            company.CompanyName = string.IsNullOrEmpty(companyDto.CompanyName) ? company.CompanyName : companyDto.CompanyName;
            company.LegalName = string.IsNullOrEmpty(companyDto.LegalName) ? company.LegalName : companyDto.LegalName;
            company.FirstName = string.IsNullOrEmpty(companyDto.FirstName) ? company.FirstName : companyDto.FirstName;
            company.LastName = string.IsNullOrEmpty(companyDto.LastName) ? company.LastName : companyDto.LastName;
            company.Rut = string.IsNullOrEmpty(companyDto.Rut) ? company.Rut : companyDto.Rut;
            company.Country = string.IsNullOrEmpty(companyDto.Country) ? company.Country : companyDto.Country;
            company.FoundationDate = companyDto.FoundationDate ?? company.FoundationDate;
            company.Phone = string.IsNullOrEmpty(companyDto.Phone) ? company.Phone : companyDto.Phone;
            company.WebSite = string.IsNullOrEmpty(companyDto.WebSite) ? company.WebSite : companyDto.WebSite;

            if (companyDto.CompanyLevelId > 0 && company.CompanyLevelId != companyDto.CompanyLevelId)
                company.CompanyLevelId = (int)companyDto.CompanyLevelId;

            if (companyDto.IndustryId > 0 && company.IndustryId != companyDto.IndustryId)
                company.IndustryId = (int)companyDto.IndustryId;

            if (companyDto.CompanyTypeId > 0 && company.CompanyTypeId != companyDto.CompanyTypeId)
                company.CompanyTypeId = (int)companyDto.CompanyTypeId;
        }
    }
}
