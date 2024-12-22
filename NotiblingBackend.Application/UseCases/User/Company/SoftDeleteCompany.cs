using NotiblingBackend.Application.Interfaces.UseCases.User.Company;
using NotiblingBackend.Domain.Interfaces.Repositories;
using NotiblingBackend.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.UseCases.User.Company
{
    public class SoftDeleteCompany : ISoftDeleteCompany
    {
        #region Dependency Injection
        ICompanyRepository _companyRepository;

        public SoftDeleteCompany(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        #endregion
        public Task<bool> SoftDelete(string companyid)
        {
            if(string.IsNullOrEmpty(companyid)) 
                throw new CompanyException("Error en el identificador de la empresa.");
           
            return _companyRepository.SoftDelete(companyid);
        }
    }
}
