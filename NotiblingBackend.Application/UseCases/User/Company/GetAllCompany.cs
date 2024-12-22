using NotiblingBackend.Application.Interfaces.UseCases.User.Company;
using NotiblingBackend.Application.Mapper;
using NotiblingBackend.Contracts.DTOs.Company;
using NotiblingBackend.Domain.Interfaces.Repositories;
using NotiblingBackend.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.UseCases.User.Company
{
    public class GetAllCompany : IGetAllCompany
    {
        #region Dependency Injection
        ICompanyRepository _companyRepository;

        public GetAllCompany(ICompanyRepository companyRepository)
        {
            this._companyRepository = companyRepository;
        }
        #endregion
        public async Task<IEnumerable<GetCompanyDto>> GetAll()
        {
            var companies = await _companyRepository.GetAll() ?? throw new CompanyException("No hay empresas registradas.");

            //var companiesDto = CompanyMapper.ToList(companies);
            return companies;
        }
    }
}
