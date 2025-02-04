using jwt_JSON_Web_Tokens__JWT_.Interface;
using jwt_JSON_Web_Tokens__JWT_.Model_Entity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace jwt_JSON_Web_Tokens__JWT_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth)
        {
            _auth = auth;   
            
        }
        
        [HttpPost("Login")]
        public string Login([FromBody] LoginRequest obj)
        {
            var Token = _auth.Login(obj);
            return Token;
        }
     
        [HttpPost("AssignRole")]
        public bool AssignRoletoUser([FromBody] AddUserRole userRole )
        {
            var addedUserRole = _auth.AssignRoleToUser(userRole);
            return addedUserRole;
        }

        
        [HttpPost("AddUser")]
        public User AddUser([FromBody] User user)
        {
            var addesuser = _auth.AddUser(user);    
            return addesuser;
        }

       
        [HttpPost("AddRole")]
        public Role AddRole ( [FromBody] Role role)
        {
            var addedRole = _auth.Addrole(role);    
            return addedRole;   

        }

    }
}
