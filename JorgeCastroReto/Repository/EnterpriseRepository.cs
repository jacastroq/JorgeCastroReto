using Dapper;
using JorgeCastroReto.Context;
using JorgeCastroReto.Contracts;
using JorgeCastroReto.Entities;
using System.Collections.Generic;
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
                var companies = await connection.QueryAsync<Enterprise>(query);
                return companies.ToList();
            }
        }

    }
}
