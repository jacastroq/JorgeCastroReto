using JorgeCastroReto.Contracts;
using JorgeCastroReto.Dto;
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
		/// GET ALL ENTERPRISES
		/// </summary>
		/// <returns>List about all enterprises </returns>
		[HttpGet]
		public async Task<IActionResult> GetEnterprises()
		{
			try
			{
				var enterprises = await _enterpriseRepo.GetEnterprises();
				return Ok(enterprises);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}


        /// <summary>
        /// Get an especific Enterprise
        /// </summary>
        /// <param name="id">Id of especific enterprise</param>
        /// <returns>An Enterprise</returns>
        [HttpGet("{id}", Name = "EnterpriseById")]
        public async Task<IActionResult> GetEnterprise(int id)
        {
            try
            {
                var company = await _enterpriseRepo.GetEnterprise(id);
                if (company == null)
                    return NotFound();
                return Ok(company);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);

            }
        }

        /// <summary>
        /// This Endpoint let create an Enterprise
        /// </summary>
        /// <param name="enterprise">EnterpriseForCreationDto</param>
        /// <returns>Enterprise</returns>
        [HttpPost]
        public async Task<IActionResult> CreateEnterprise([FromBody] EnterpriseForCreationDto enterprise )
        {
            try
            {
                var createdEnterprise = await _enterpriseRepo.CreateEnterprise(enterprise);
               
                return Ok(createdEnterprise);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        
        /// <summary>
        /// EndPoint to update an Enterprise
        /// </summary>
        /// <param name="id">int id from header</param>
        /// <param name="enterprise"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnterprise(int id,[FromBody] EnterpriseForUpdateDto enterprise)
        {
            try
            {
                var updatedEnterprise = await _enterpriseRepo.UpdateEnterprise(id, enterprise);

                return Ok(updatedEnterprise);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }




    }
}





