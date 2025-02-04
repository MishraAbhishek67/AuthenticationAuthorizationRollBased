using jwt_JSON_Web_Tokens__JWT_.Model_Entity;
using Microsoft.EntityFrameworkCore;

namespace jwt_JSON_Web_Tokens__JWT_.Context
{
    public class JwtContaxt:DbContext
    {
        public JwtContaxt(DbContextOptions<JwtContaxt>options):base(options) 
        {
            
        }
        public DbSet<User> Users { get; set; }  
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

    }
}
