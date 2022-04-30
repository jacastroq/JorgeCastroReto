using JorgeCastroReto.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JorgeCastroReto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnterprisesController : ControllerBase
    {
        private readonly IEnterpriseRepository _enterpriseRepo;

        public EnterprisesController(IEnterpriseRepository enterpriseRepo)
        {
            _enterpriseRepo = enterpriseRepo;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <returns>List about all enterprises </returns>
		[HttpGet]
		public async Task<IActionResult> GetEnterprises()
		{
			try
			{
				var companies = await _enterpriseRepo.GetEnterprises();
				return Ok(companies);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}


	}
}
