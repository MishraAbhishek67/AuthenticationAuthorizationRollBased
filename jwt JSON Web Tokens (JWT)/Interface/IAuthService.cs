


using jwt_JSON_Web_Tokens__JWT_.Model_Entity;

namespace jwt_JSON_Web_Tokens__JWT_.Interface
{
    public interface IAuthService
    {
        User AddUser(User user);
        string Login(LoginRequest loginRequest);

        Role Addrole(Role role);    
        bool AssignRoleToUser (AddUserRole obj);    
    }
}
