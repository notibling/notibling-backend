using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotiblingBackend.Application.Interfaces.UseCases.User.Company;
using NotiblingBackend.Contracts.DTOs.Company;
using NotiblingBackend.Utilities.Exceptions;

namespace NotiblingBackendAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class CompanyController : ControllerBase
    {
        private readonly IAddCompany _cuAddCompany;
        private readonly IGetByIdCompany _cuGetByIdCompany;
        private readonly IGetAllCompany _cuGetAllCompany;
        private readonly IUpdateCompany _cuUpdateCompany;
        private readonly ISoftDeleteCompany _cuDeleteCompany;

        public CompanyController(IAddCompany cuAddCompany, IGetByIdCompany cuGetByIdCompany, IGetAllCompany cuGetAllCompany, IUpdateCompany cuUpdateCompany, ISoftDeleteCompany cuDeleteCompany)
        {
            _cuAddCompany = cuAddCompany;
            _cuGetByIdCompany = cuGetByIdCompany;
            _cuGetAllCompany = cuGetAllCompany;
            _cuUpdateCompany = cuUpdateCompany;
            _cuDeleteCompany = cuDeleteCompany;
        }

        [HttpPost("add-company")]
        //[Route("add-company")]
        public async Task<ActionResult> Add([FromBody] AddCompanyDto company)
        {
            try
            {
                await _cuAddCompany.Add(company);
                return Ok(company);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CompanyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) 
            {
                return BadRequest($"{ex.Message}, {ex.InnerException}");
            }
        }

        //[HttpGet("{id}")]
        [HttpGet("get-company/{id}")]
        [Authorize]
        public async Task<ActionResult<GetCompanyDto>> Get(int? id)
        {
            try
            {
                var company = await _cuGetByIdCompany.GetById(id);

                if (company == null) 
                { 
                    return NotFound(); 
                }

                return Ok(company);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CompanyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-companies")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetCompanyDto>>> Get()
        {
            try
            {
                var companies = await _cuGetAllCompany.GetAll();

                if (companies.Count() < 0)
                {
                    return NotFound();
                }

                return Ok(companies);
            }
            catch (CompanyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("update-company/{companyId}")]
        [Authorize]
        public async Task<ActionResult> Update(string companyId, [FromBody] UpdateCompanyDto updateCompanyDto)
        {
            try
            {
                if(updateCompanyDto == null) 
                    return BadRequest("No hay datos para realizar la actualizacion.");

               if(!await _cuUpdateCompany.Update(companyId, updateCompanyDto))
                    return BadRequest("No se pudieron actualizar los datos.");

                return NoContent();
            }
            catch (CompanyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-company/{companyId}")]
        [Authorize]
        public async Task<ActionResult> Delete(string companyId)
        {
            try
            {
                if (string.IsNullOrEmpty(companyId))
                    return BadRequest("El identificador de la empresa está vacío.");

                if (!await _cuDeleteCompany.SoftDelete(companyId))
                    return NotFound($"No se encontró una empresa con el ID {companyId}.");

                return NoContent();
            }
            catch (CompanyException ex)
            {
                return Conflict(ex.Message); // 409 Conflict para restricciones de eliminación
            }
            catch (Exception ex)
            {
                // Registro del error (logger)
                return StatusCode(500, $"Se produjo un error interno. {ex}");
            }
        }
    }
}
