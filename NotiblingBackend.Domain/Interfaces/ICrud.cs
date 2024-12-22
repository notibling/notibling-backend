using NotiblingBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Domain.Interfaces
{
    public interface ICrud<TInputAdd, T, TOutput> 
        where TInputAdd : class 
        where T : class
        where TOutput : class
    {
        //#region CRUD
        Task Add(TInputAdd obj);
        Task<TOutput> GetById(int? id);
        Task<T> GetByGuid(string companyId);
        Task<IEnumerable<TOutput>> GetAll();
        Task Update(T obj);
        Task<bool> SoftDelete(string companyId);

        //Task<bool> AddCustomer(Customer customer);
        //Task<bool> AddEmployee(Employee employee);
        //Task<bool> UpdateCustomer(Customer customer);
        //Task<bool> UpdateEmployee(Employee employee);
        //Task<IEnumerable<Company>> FindAllCompany();
        //Task<IEnumerable<Customer>> FindAllCustomer();
        //Task<IEnumerable<Employee>> FindAllEmployee();
        //Task<Company> FindComapanyById(int id);
        //Task<Customer> FindCustomerById(int id);
        //Task<Employee> FindEmployeeById(string EmployeeId);
        //Task<bool> SoftDelete(int id);
        //#endregion

        //#region Company
        //Task<Company> FindCompanyByEmail(string email);
        //#endregion

        //#region Customer
        //Task<Customer> FindCustomerByEmail(string email);
        //Task<Customer> FindByIdentityDocument(string identityDocument);
        //#endregion
    }
}
