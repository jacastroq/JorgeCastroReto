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
    public class EmployeesController : ControllerBase
    {

        private readonly IEmployeeRepository _employeRepo;

        public EmployeesController(IEmployeeRepository employeRepo)
        {
            _employeRepo = employeRepo;
        }

        /// <summary>
        /// GET ALL EMPLOYEES
        /// </summary>
        /// <returns>List about all employees </returns>
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _employeRepo.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Get an especific Employee
        /// </summary>
        /// <param name="id">Id of especific employee</param>
        /// <returns>An Employee</returns>
        [HttpGet("{id}", Name = "EmployeeById")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                var company = await _employeRepo.GetEmployee(id);
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
        /// This Endpoint let create an Employee
        /// </summary>
        /// <param name="employee">EmployeeForCreationDto</param>
        /// <returns>Employee</returns>
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeForCreationDto employee)
        {
            try
            {
                var createdEmployee = await _employeRepo.CreateEmployee(employee);

                return Ok(createdEmployee);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// EndPoint to update an Employee
        /// </summary>
        /// <param name="id">int id from header</param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeForUpdateDto employee)
        {
            try
            {
                var updatedEmployee = await _employeRepo.UpdateEmployee(id, employee);

                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }







    }
}
