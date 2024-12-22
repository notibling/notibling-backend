using NotiblingBackend.Application.Dtos;
using NotiblingBackend.Contracts.DTOs.Company;
using NotiblingBackend.Domain.Entities;
using NotiblingBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.Mapper
{
    public class CompanyMapper
    {
        //protected static Company AddFromDto(AddCompanyDto companyDto)
        //{
        //    return new Company()
        //    {
        //        FirstName = companyDto.FirstName.Trim(),
        //        LastName = companyDto.LastName.Trim(),
        //        Email = companyDto.Email.ToLower().Trim(),
        //        Password = companyDto.Password.Trim(),
        //        CompanyName = companyDto.CompanyName.Trim(),
        //        LegalName = companyDto.LegalName.Trim(),
        //        Rut = companyDto.Rut.Trim(),
        //        Phone = companyDto.Phone.Trim(),
        //        CompanyLevelId = companyDto.CompanyLevelId,
        //        IndustryId = companyDto.IndustryId,
        //        CompanyTypeId = companyDto.CompanyTypeId,
        //        TimeZone = companyDto.TimeZone.Trim(),
        //        Country = companyDto.Country.Trim(),
        //        FoundationDate = companyDto.FoundationDate,
        //        WebSite = companyDto.WebSite.Trim(),
        //    };
        //}

        //internal static Company FromEditDto(UpdateCompanyDto companyDto)
        //{
        //    if (companyDto == null)
        //        throw new ArgumentNullException(nameof(companyDto), "Los datos no pueden ser nulo.");

        //    return new Company()
        //    {
        //        //UserName = companyDto.UserName.Trim(),
        //        //CompanyName = companyDto.CompanyName.Trim(),
        //        //LegalName = companyDto.LegalName.Trim(),
        //        //FirstName = companyDto.FirstName.Trim(),
        //        //LastName = companyDto.LastName.Trim(),
        //        //Rut = companyDto.Rut.Trim(),
        //        //Country = companyDto.Country.Trim(),
        //        //FoundationDate = (DateOnly)companyDto.FoundationDate, 
        //        //Phone = companyDto.Phone.Trim(),
        //        //WebSite = companyDto.WebSite,
        //        //CompanyLevelId = (int)companyDto.CompanyLevelId,
        //        //IndustryId = (int)companyDto.IndustryId,
        //        //CompanyTypeId = (int)companyDto.CompanyTypeId,

        //        UserName = companyDto.UserName?.Trim(),
        //        CompanyName = companyDto.CompanyName?.Trim(),
        //        LegalName = companyDto.LegalName?.Trim(),
        //        FirstName = companyDto.FirstName?.Trim(),
        //        LastName = companyDto.LastName?.Trim(),
        //        Rut = companyDto.Rut?.Trim(),
        //        Country = companyDto.Country?.Trim(),
        //        FoundationDate = companyDto.FoundationDate ?? default, // Si es null, usa el valor predeterminado de DateOnly
        //        Phone = companyDto.Phone?.Trim(),
        //        WebSite = companyDto.WebSite?.Trim(),
        //        CompanyLevelId = companyDto.CompanyLevelId ?? 0, // Si es null, asigna 0
        //        IndustryId = companyDto.IndustryId ?? 0,
        //        CompanyTypeId = companyDto.CompanyTypeId ?? 0,
        //    };
        //}

        //internal static CompanyDto ToDto(Company company)
        //{
        //    return new CompanyDto()
        //    {
        //        Id = company.Id,
        //        CompanyId = company.CompanyId,
        //        UserName = company.UserName,
        //        CompanyName = company.CompanyName,
        //        FirstName = company.FirstName,
        //        LastName = company.LastName,
        //        Email = company.Email,
        //        LegalName = company.LegalName,
        //        Rut = company.Rut,
        //        Country = company.Country,
        //        FoundationDate = company.FoundationDate,
        //        Phone = company.Phone,
        //        WebSite = company.WebSite,
        //        CompanyType = new CompanyTypeDto()
        //        {
        //            TypeCompanyId = company.CompanyType.TypeCompanyId,
        //            TypeName = company.CompanyType.TypeName,
        //            Abbreviation = company.CompanyType.Abbreviation,
        //        },
        //        CompanyLevels = new CompanyLevelsDto()
        //        {                    
        //            LevelCompanyId = company.CompanyLevels.LevelCompanyId,
        //            LevelName = company.CompanyLevels.LevelName,
        //        },
        //        Industry = new IndustryDto()
        //        {
        //            IndustryId = company.Industry.IndustryId,
        //            IndustryName = company.Industry.IndustryName,
        //        },
        //        CreatedAt = company.GetLocalTime(company.CreatedAt),
        //        UpdatedAt = company.GetLocalTime(company.UpdatedAt),
        //        CompanyRole = company.CompanyRole.ToString(),
        //        UserRole = company.UserRole.ToString(),
        //        AccountStatus = company.AccountStatus.ToString(),
        //    };
        //}



        //internal static IEnumerable<CompanyDto> ToList(IEnumerable<Company> companies)
        //{
        //    var companiesDto = companies.Select(c => ToDto(c)).ToList();
            
        //    return companiesDto;
        //}
    }
}
