using JorgeCastroReto.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JorgeCastroReto.Contracts
{
    public interface IEnterpriseRepository
    {
        public Task<IEnumerable<Enterprise>> GetEnterprises();

    }
}
