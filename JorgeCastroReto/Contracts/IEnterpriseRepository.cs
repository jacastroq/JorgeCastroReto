using JorgeCastroReto.Dto;
using JorgeCastroReto.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JorgeCastroReto.Contracts
{
    public interface IEnterpriseRepository
    {
        public Task<IEnumerable<Enterprise>> GetEnterprises();
        public Task<Enterprise> GetEnterprise(int id);
        public Task<Enterprise> CreateEnterprise(EnterpriseForCreationDto enterprise);
        public Task<Enterprise> UpdateEnterprise(int id,EnterpriseForUpdateDto enterprise);


    }
}
