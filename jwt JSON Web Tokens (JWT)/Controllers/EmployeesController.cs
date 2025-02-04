using jwt_JSON_Web_Tokens__JWT_.Interface;
using jwt_JSON_Web_Tokens__JWT_.Model_Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace jwt_JSON_Web_Tokens__JWT_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Authorize (Roles ="User , Admin")]
        public List <Employee>GetEmployees()
        {
            return _employeeService.GetEmployeeDetails();

        }


        [HttpPost]
        public Employee AddEmployee([FromBody] Employee emp)

        {
            var employee = _employeeService.AddEmployee(emp);   
            return employee;    
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
