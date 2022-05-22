using Microsoft.EntityFrameworkCore;
using Task_Portal.Models;

namespace Task_Portal.Database
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<UserRegistrationViewModel> UserRegistration { get; set; }
        public DbSet<LoginViewModel> LoginUserData { get; set; }
    }
}
