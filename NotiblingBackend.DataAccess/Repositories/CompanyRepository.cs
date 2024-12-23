using Microsoft.EntityFrameworkCore;
using NotiblingBackend.Contracts.DTOs.Company;
using NotiblingBackend.Domain.Entities;
using NotiblingBackend.Domain.Interfaces.Repositories;
using NotiblingBackend.Mapping;
using NotiblingBackend.Utilities.Exceptions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NotiblingBackend.DataAccess.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        readonly NBContext _dbContext;

        public CompanyRepository(NBContext dbContext)
        {
            _dbContext = dbContext;
        }


        #region CRUD
        public async Task Add(AddCompanyDto obj)
        {
            if (obj == null)
                throw new CompanyException("Error los campos no pueden estar vacíos.");

            try
            {
                var company = CompanyMapper.AddFromDto(obj);
                company.UserName = company.GenerateUserName();
                _dbContext.Entry(company).Property(c => c.UpdatedAt).IsModified = true;
                await _dbContext.Companies.AddAsync(company);
                await _dbContext.SaveChangesAsync();
            }
            catch (CompanyException ex)
            {
                throw new CompanyException(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(message: ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetCompanyDto>> GetAll()
        {
            try
            {

                var companies = await _dbContext.Companies
                    .Include(c => c.CompanyLevels)
                    .Include(c => c.CompanyType)
                    .Include(c => c.Industry)
                     .Where(c => !c.IsDeleted) // Excluir eliminados
                    .Select(c => new GetCompanyDto
                    {
                        Id = c.Id,
                        CompanyId = c.CompanyId.ToString(),
                        UserName = c.UserName,
                        CompanyName = c.CompanyName,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email,
                        LegalName = c.LegalName,
                        Rut = c.Rut,
                        Country = c.Country,
                        FoundationDate = c.FoundationDate,
                        Phone = c.Phone,
                        WebSite = c.WebSite,
                        CompanyType = new CompanyTypeDto()
                        {
                            TypeCompanyId = c.CompanyType.TypeCompanyId,
                            TypeName = c.CompanyType.TypeName,
                            Abbreviation = c.CompanyType.Abbreviation
                        },
                        CompanyLevels = new CompanyLevelsDto()
                        {
                            LevelCompanyId = c.CompanyLevels.LevelCompanyId,
                            LevelName = c.CompanyLevels.LevelName
                        },
                        Industry = new IndustryDto()
                        {
                            IndustryId = c.Industry.IndustryId,
                            IndustryName = c.Industry.IndustryName
                        },
                        //CreatedAt = c.CreatedAt,
                        //UpdatedAt = c.UpdatedAt,
                        CompanyRole = c.CompanyRole.ToString(),
                        UserRole = c.UserRole.ToString(),
                        AccountStatus = c.AccountStatus.ToString(),
                        TimeZone = c.TimeZone,

                    })
                    .ToListAsync();

                return companies;
            }
            catch (CompanyException ex)
            {
                throw new CompanyException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }

        public async Task<GetCompanyDto> GetById(int? id)
        {
            try
            {
                if (id == null) throw new CompanyException("El ID no puede ser nulo.");
                if (id <= 0) throw new CompanyException("El ID no puede ser un numero negativo.");

                var company = await _dbContext.Companies
                    .Include(c => c.CompanyLevels)
                    .Include(c => c.CompanyType)
                    .Include(c => c.Industry)
                    .Where(c => c.Id == id && !c.IsDeleted)
                    .Select(c => new GetCompanyDto
                    {
                        Id = c.Id,
                        CompanyId = c.CompanyId.ToString(),
                        UserName = c.UserName,
                        CompanyName = c.CompanyName,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email,
                        LegalName = c.LegalName,
                        Rut = c.Rut,
                        Country = c.Country,
                        FoundationDate = c.FoundationDate,
                        Phone = c.Phone,
                        WebSite = c.WebSite,
                        CompanyType = new CompanyTypeDto()
                        {
                            TypeCompanyId = c.CompanyType.TypeCompanyId,
                            TypeName = c.CompanyType.TypeName,
                            Abbreviation = c.CompanyType.Abbreviation
                        },
                        CompanyLevels = new CompanyLevelsDto()
                        {
                            LevelCompanyId = c.CompanyLevels.LevelCompanyId,
                            LevelName = c.CompanyLevels.LevelName
                        },
                        Industry = new IndustryDto()
                        {
                            IndustryId = c.Industry.IndustryId,
                            IndustryName = c.Industry.IndustryName
                        },
                        CompanyRole = c.CompanyRole.ToString(),
                        UserRole = c.UserRole.ToString(),
                        AccountStatus = c.AccountStatus.ToString(),
                        TimeZone = c.TimeZone,

                    })
                    .FirstOrDefaultAsync();

                return company ?? throw new CompanyException($"No existe una empresa con el ID: {id}.");
            }
            catch (CompanyException ex)
            {
                throw new CompanyException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Company> GetByGuid(string companyId)
        {
            try
            {
                var company = await _dbContext.Companies
                    .Where(c => c.CompanyId.ToString() == companyId && !c.IsDeleted)
                    .FirstOrDefaultAsync();

                return company == null ? throw new CompanyException("La empresa no existe.") : company;
            }
            catch (CompanyException ex)
            {
                throw new CompanyException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new CompanyException(ex.Message);
            }
        }
        public async Task Update(Company company)
        {
            try
            {
                company.UpdateModifiedDate();

                _dbContext.Companies.Attach(company);

                _dbContext.Entry(company).Property(c => c.UpdatedAt).IsModified = true;

                await _dbContext.SaveChangesAsync();
            }
            catch (CompanyException ex)
            {
                throw new CompanyException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> SoftDelete(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
                throw new CompanyException("El identificador de la empresa no puede estar vacío.");

            try
            {

                var company = await GetByGuid(companyId);

                company.IsDeleted= true;

                company.DeletedModifiedDate();

                _dbContext.Companies.Attach(company);

                _dbContext.Entry(company).Property(c => c.DeletedAt).IsModified = true;

                await _dbContext.SaveChangesAsync();
                return true;
            }            
            catch (DbUpdateException ex)
            {
                throw new CompanyException("Error al eliminar la empresa. Por favor, inténtelo de nuevo más tarde.", ex);
            }
            catch (CompanyException ex)
            {
                throw new CompanyException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Restore(string companyId)
        {
            var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.CompanyId.ToString() == companyId);

            if (company == null || !company.IsDeleted)
                throw new CompanyException("La empresa no existe o no está eliminada.");

            company.IsDeleted = false;
            //company.DeletedAt = null;

            await _dbContext.SaveChangesAsync();
            return true;
        }
        #endregion

    }
}