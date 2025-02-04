using jwt_JSON_Web_Tokens__JWT_.Model_Entity;

namespace jwt_JSON_Web_Tokens__JWT_.Interface
{
    public interface IEmployeeService
    {
        public List<Employee> GetEmployeeDetails();
        public Employee AddEmployee(Employee employee);
    }

}
