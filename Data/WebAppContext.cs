using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web_App.Models;

namespace Web_App.Data
{
    public class WebAppContext : IdentityDbContext<User>
    {
        public WebAppContext (DbContextOptions<WebAppContext> options)
            : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; } = default!;
        public DbSet<Message> Messages { get; set; } = default!;
    }
}

