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
    public class EnterpriseRepository : IEnterpriseRepository
    {
        private readonly DapperContext _context;

        public EnterpriseRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enterprise>> GetEnterprises()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var query = "SELECT * FROM enterprises";
            using (var connection = _context.CreateConnection())
            {
                var enterprises = await connection.QueryAsync<Enterprise>(query);
                return enterprises.ToList();
            }
        }

        public async Task<Enterprise> GetEnterprise(int id)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var query = "SELECT * FROM enterprises WHERE id = @Id AND status = 1";
            using (var connection = _context.CreateConnection())
            {
                var enterprise = await connection.QuerySingleOrDefaultAsync<Enterprise>(query, new { id });
                return enterprise;
            }

        }

        public async Task<Enterprise> CreateEnterprise(EnterpriseForCreationDto enterprise)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var query = "INSERT INTO enterprises (created_by, address, name, phone, created_date, status) VALUES (@CreatedBy, @Address, @Name, @Phone , NOW(), 1);" +
                " SELECT * FROM enterprises WHERE id = LAST_INSERT_ID()";

            var parameters = new DynamicParameters();
            parameters.Add("CreatedBy", enterprise.CreatedBy, DbType.String);
            parameters.Add("Address", enterprise.Address, DbType.String);
            parameters.Add("Name", enterprise.Name, DbType.String);
            parameters.Add("Phone", enterprise.Phone, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var insertedEnterprise = await connection.QuerySingleAsync<Enterprise>(query, parameters);

                return insertedEnterprise;
            }

        } 
        

        public async Task<Enterprise> UpdateEnterprise(int id,EnterpriseForUpdateDto enterprise)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var query = "UPDATE enterprises SET modified_by = @ModifiedBy, address = @Address, name = @Name, phone = @Phone, modified_date = NOW(), status = 1 WHERE id = @Id; SELECT * FROM enterprises WHERE id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("ModifiedBy", enterprise.ModifiedBy, DbType.String);
            parameters.Add("Address", enterprise.Address, DbType.String);
            parameters.Add("Name", enterprise.Name, DbType.String);
            parameters.Add("Phone", enterprise.Phone, DbType.String);
            parameters.Add("Id", id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var UpdatedEnterprise = await connection.QuerySingleAsync<Enterprise>(query, parameters);

                return UpdatedEnterprise;
            }

        }



      







































































    }
}
