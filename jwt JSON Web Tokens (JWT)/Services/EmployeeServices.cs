using jwt_JSON_Web_Tokens__JWT_.Context;
using jwt_JSON_Web_Tokens__JWT_.Interface;
using jwt_JSON_Web_Tokens__JWT_.Model_Entity;

namespace jwt_JSON_Web_Tokens__JWT_.Services
{
    public class EmployeeServices : IEmployeeService
    {
        private readonly JwtContaxt _contaxt;
        public EmployeeServices(JwtContaxt contaxt)
        {
            _contaxt = contaxt;
            
            
        }
        public Employee AddEmployee(Employee employee)
        {
            var emp = _contaxt.Employees.Add(employee); 
            _contaxt.SaveChanges();
            return emp.Entity;
        }

        public List<Employee> GetEmployeeDetails()
        {
            var employee = _contaxt.Employees.ToList();
            return employee;
        }
    }
}
