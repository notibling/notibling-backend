using NotiblingBackend.Application.Dtos.User.Company;
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
    public class GetByIdCompany : IGetByIdCompany
    {
        #region Dependency Injection
        ICompanyRepository _companyRepository;

        public GetByIdCompany(ICompanyRepository companyRepository)
        {
            this._companyRepository = companyRepository;
        }
        #endregion

        public async Task<Contracts.DTOs.Company.GetCompanyDto> GetById(int? id) {

            //if (id == null) throw new CompanyException("El ID no puede ser nulo.");

            var company = await _companyRepository.GetById(id);
            if (company == null) throw new CompanyException($"No existe registro de una empresa con el ID {id}");

            //var companyDto = CompanyMapper.ToDto(company);
            return company;

            //throw new NotImplementedException();
        }
    }
}
