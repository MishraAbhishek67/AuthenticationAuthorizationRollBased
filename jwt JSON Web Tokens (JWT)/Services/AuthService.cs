using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using jwt_JSON_Web_Tokens__JWT_.Context;
using jwt_JSON_Web_Tokens__JWT_.Interface;
using jwt_JSON_Web_Tokens__JWT_.Model_Entity;
using Microsoft.IdentityModel.Tokens;

namespace jwt_JSON_Web_Tokens__JWT_.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtContaxt _context;
        private readonly IConfiguration configuration;

        //private readonly IConfiguration _configuration;
        public AuthService(JwtContaxt contaxt, IConfiguration configuration)
        {
            _context = contaxt;
            this.configuration = configuration;
        }

        public Role Addrole(Role role)
        {
            var addedRole = _context.Roles.Add(role);
            _context.SaveChanges();
            return addedRole.Entity;
        }

        public User AddUser(User user)
        {
            var addedUser = _context.Users.Add(user);
            _context.SaveChanges();
            return addedUser.Entity;
        }

        public bool AssignRoleToUser(AddUserRole obj)
        {
            try
            {
                var addRole = new List<UserRole>();
                var user = _context.Users.SingleOrDefault(s => s.Id == obj.UserId);
                if (user == null)
                    throw new Exception("user is not valid");
                foreach (int role in obj.RoleIds)
                {
                    var UserRole = new UserRole();
                    UserRole.RoleId = role;
                    UserRole.UserId = user.Id;
                    addRole.Add(UserRole);
                }
                _context.UserRoles.AddRange(addRole);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string Login(LoginRequest loginRequest)
        {
            if (!string.IsNullOrEmpty(loginRequest.UserName) && !string.IsNullOrEmpty(loginRequest.Password))
            {
                var user = _context.Users.SingleOrDefault(s => s.UserName == loginRequest.UserName && s.Password == loginRequest.Password);
                if (user != null)
                {
                    var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                new Claim("Id", user.Id.ToString()),
                new Claim("UserName", user.UserName)
            };

                    var userRoles = _context.UserRoles.Where(u => u.UserId == user.Id).ToList();
                    var roleIds = userRoles.Select(s => s.RoleId).ToList();
                    var roles = _context.Roles.Where(r => roleIds.Contains(r.Id)).ToList();

                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        configuration["Jwt:Issuer"],
                        configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10), // Token expiration time (adjust as needed)
                        signingCredentials: signIn
                        
                    );

                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;
                }
                else
                {
                    throw new Exception("User Is Not Valid");
                }
            }

            else
            {
                throw new Exception("credentials is not valid");
            }
        }

    }
}
        
    

