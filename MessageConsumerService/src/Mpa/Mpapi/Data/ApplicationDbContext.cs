using MessageConsumerService.Mpa.Mpapi.Models;
using Microsoft.EntityFrameworkCore;

namespace MessageConsumerService.Mpa.Mpapi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}
