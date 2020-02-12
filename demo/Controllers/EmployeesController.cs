using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using demo.Models;
using demo.Repository;

namespace demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IDataRepository<Employee> _dataRepository;

        public EmployeesController(IDataRepository<Employee> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            IEnumerable<Employee> employees = _dataRepository.GetAll();
            return Ok(employees);
        }

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            var employee =  _dataRepository.Get(id);

            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(long id, [FromBody]Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Oops employee is null");
            }

            Employee employeeToUpdate = _dataRepository.Get(id);
            if(employeeToUpdate == null)
            {
                return NotFound("The Emplyee record could not be found");
            }
            _dataRepository.Update(employeeToUpdate, employee);
            return NoContent();
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            if(employee == null)
            {
                return BadRequest("Employee is null");
            }
            _dataRepository.Add(employee);
            return CreatedAtRoute("Get", new { Id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(long id)
        {
            Employee employee = _dataRepository.Get(id);
            if (employee == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }
            _dataRepository.Delete(employee);
            return NoContent();
        }
    }
}
