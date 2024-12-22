using Microsoft.EntityFrameworkCore;
using NotiblingBackend.Application.Interfaces.UseCases.User.Company;
using NotiblingBackend.Application.Mapper;
using NotiblingBackend.Contracts.DTOs.Company;
using NotiblingBackend.Domain.Enums;
using NotiblingBackend.Domain.Interfaces.Repositories;
using NotiblingBackend.Utilities.Exceptions;
using NotiblingBackend.Utilities.Security;
using NotiblingBackend.Utilities.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.UseCases.User.Company
{
    public class AddCompany : IAddCompany
    {
        #region Dependency Injection
        ICompanyRepository _companyRepository;

        public AddCompany(ICompanyRepository companyRepository)
        {
            this._companyRepository = companyRepository;
        }
        #endregion
        public async Task Add(AddCompanyDto companyDto)
        {
            if (companyDto == null)
                throw new CompanyException("No se puede dar de alta el usuario");

            //if (!PasswordValidator.IsValid(companyDto.Password))
            //    throw new CompanyException("La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula, un número y un carácter especial.");

            //var company = CompanyMapper.AddFromDto(companyDto);

            //company.UserName = company.GenerateUserName();

            //company.Validate();

            //Encriptar contraseña
            //companyDto.Password = PasswordEncryptor.HashPassword(companyDto.Password);

            //company.CompanyRole = CompanyRole.Free;
            //company.UserRole = UserRole.Company;

            //company.SetRegistrationDate();

            try
            {
                await _companyRepository.Add(companyDto);

            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
