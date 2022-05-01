using JorgeCastroReto.Dto;
using JorgeCastroReto.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JorgeCastroReto.Contracts
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetEmployees();
        public Task<Employee> GetEmployee(int id);
        public Task<Employee> CreateEmployee(EmployeeForCreationDto Employee);
        public Task<Employee> UpdateEmployee(int id, EmployeeForUpdateDto Employee);

    }
}
