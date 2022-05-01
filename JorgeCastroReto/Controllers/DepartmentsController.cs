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
    public class DepartmentsController : ControllerBase
    {

        private readonly IDepartmentRepository _departmentRepo;

        public DepartmentsController(IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

		/// <summary>
		/// GET ALL DEPARTMENTS
		/// </summary>
		/// <returns>List about all departments </returns>
		[HttpGet]
		public async Task<IActionResult> GetDepartments()
		{
			try
			{
				var departments = await _departmentRepo.GetDepartments();
				return Ok(departments);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}


        /// <summary>
        /// Get an especific Department
        /// </summary>
        /// <param name="id">Id of especific department</param>
        /// <returns>An Department</returns>
        [HttpGet("{id}", Name = "DepartmentById")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            try
            {
                var company = await _departmentRepo.GetDepartment(id);
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
        /// This Endpoint let create an Department
        /// </summary>
        /// <param name="department">DepartmentForCreationDto</param>
        /// <returns>Department</returns>
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentForCreationDto department )
        {
            try
            {
                var createdDepartment = await _departmentRepo.CreateDepartment(department);
               
                return Ok(createdDepartment);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        
        /// <summary>
        /// EndPoint to update an Department
        /// </summary>
        /// <param name="id">int id from header</param>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id,[FromBody] DepartmentForUpdateDto department)
        {
            try
            {
                var updatedDepartment = await _departmentRepo.UpdateDepartment(id, department);

                return Ok(updatedDepartment);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }



    }
}
