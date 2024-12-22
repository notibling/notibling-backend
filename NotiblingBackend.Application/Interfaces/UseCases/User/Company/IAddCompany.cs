using NotiblingBackend.Contracts.DTOs.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.Interfaces.UseCases.User.Company
{
    public interface IAddCompany
    {
        Task Add(AddCompanyDto companyDto);
    }
}
