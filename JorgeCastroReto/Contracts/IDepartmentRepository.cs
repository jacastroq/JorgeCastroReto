using JorgeCastroReto.Dto;
using JorgeCastroReto.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JorgeCastroReto.Contracts
{
    public interface IDepartmentRepository
    {
        public Task<IEnumerable<Department>> GetDepartments();
        public Task<Department> GetDepartment(int id);
        public Task<Department> CreateDepartment(DepartmentForCreationDto Department);
        public Task<Department> UpdateDepartment(int id, DepartmentForUpdateDto Department);
    }
}
