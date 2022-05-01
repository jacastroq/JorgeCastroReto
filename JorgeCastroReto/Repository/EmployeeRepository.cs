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
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var query = "SELECT * FROM employees";
            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employee>(query);
                return employees.ToList();
            }
        }

        public async Task<Employee> GetEmployee(int id)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var query = "SELECT * FROM employees WHERE id = @Id AND status = 1";
            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.QuerySingleOrDefaultAsync<Employee>(query, new { id });
                return employee;
            }

        }

        public async Task<Employee> CreateEmployee(EmployeeForCreationDto Employee)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var query = "INSERT INTO employees (created_by, age, email, name, position, surname, created_date, status) VALUES (@CreatedBy, @Age, @Email, @Name , @Position , @Surname , NOW(), 1);" +
                " SELECT * FROM employees WHERE id = LAST_INSERT_ID()";

            var parameters = new DynamicParameters();
            parameters.Add("CreatedBy", Employee.CreatedBy, DbType.String);
            parameters.Add("Age", Employee.Age, DbType.Int32);
            parameters.Add("Email", Employee.Email, DbType.String);
            parameters.Add("Name", Employee.Name, DbType.String);
            parameters.Add("Position", Employee.Position, DbType.String);
            parameters.Add("Surname", Employee.Surname, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var insertedEmployee = await connection.QuerySingleAsync<Employee>(query, parameters);

                return insertedEmployee;
            }

        }


        public async Task<Employee> UpdateEmployee(int id, EmployeeForUpdateDto Employee)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var query = "UPDATE employees SET modified_by = @ModifiedBy, age = @Age, email = @Email, name = @Name, position = @Position, surname = @Surname, modified_date = NOW(), status = 1 WHERE id = @Id; SELECT * FROM employees WHERE id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("ModifiedBy", Employee.ModifiedBy, DbType.String);
            parameters.Add("Age", Employee.Age, DbType.Int32);
            parameters.Add("Email", Employee.Email, DbType.String);
            parameters.Add("Name", Employee.Name, DbType.String);
            parameters.Add("Position", Employee.Position, DbType.String);
            parameters.Add("Surname", Employee.Surname, DbType.String);
            parameters.Add("Id", id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var UpdatedEmployee = await connection.QuerySingleAsync<Employee>(query, parameters);

                return UpdatedEmployee;
            }

        }


    }
}
