using NotiblingBackend.Contracts.DTOs.Company;
using NotiblingBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Domain.Interfaces.Repositories
{
    public interface ICompanyRepository : ICrud<AddCompanyDto, Company, GetCompanyDto>
    {
    }
}
