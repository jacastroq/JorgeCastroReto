using Dapper;
using JorgeCastroReto.Context;
using JorgeCastroReto.Contracts;
using JorgeCastroReto.Dto;
using JorgeCastroReto.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace JorgeCastroReto.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DapperContext _context;

        public DepartmentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var query = "SELECT * FROM departments";
            using (var connection = _context.CreateConnection())
            {
                var departments = await connection.QueryAsync<Department>(query);
                return departments.ToList();
            }
        }

        public async Task<Department> GetDepartment(int id)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var query = "SELECT * FROM departments WHERE id = @Id AND status = 1";
            using (var connection = _context.CreateConnection())
            {
                var department = await connection.QuerySingleOrDefaultAsync<Department>(query, new { id });
                return department;
            }

        }

        public async Task<Department> CreateDepartment(DepartmentForCreationDto Department)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var query = "INSERT INTO departments (created_by, description, name, phone, created_date, status) VALUES (@CreatedBy, @Description, @Name, @Phone , NOW(), 1);" +
                " SELECT * FROM departments WHERE id = LAST_INSERT_ID()";

            var parameters = new DynamicParameters();
            parameters.Add("CreatedBy", Department.CreatedBy, DbType.String);
            parameters.Add("Description", Department.Description, DbType.String);
            parameters.Add("Name", Department.Name, DbType.String);
            parameters.Add("Phone", Department.Phone, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var insertedDepartment = await connection.QuerySingleAsync<Department>(query, parameters);

                return insertedDepartment;
            }

        }


        public async Task<Department> UpdateDepartment(int id, DepartmentForUpdateDto Department)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var query = "UPDATE departments SET modified_by = @ModifiedBy, description = @Description, name = @Name, phone = @Phone, modified_date = NOW(), status = 1 WHERE id = @Id; SELECT * FROM departments WHERE id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("ModifiedBy", Department.ModifiedBy, DbType.String);
            parameters.Add("Description", Department.Description, DbType.String);
            parameters.Add("Name", Department.Name, DbType.String);
            parameters.Add("Phone", Department.Phone, DbType.String);
            parameters.Add("Id", id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var UpdatedDepartment = await connection.QuerySingleAsync<Department>(query, parameters);

                return UpdatedDepartment;
            }

        }



    }
}
