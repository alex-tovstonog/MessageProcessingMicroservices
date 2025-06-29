using MessageConsumerService.Mpa.Mpapi.Data;
using Microsoft.EntityFrameworkCore;

namespace MessageConsumerService.Mpa.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<WebApplication>>();

            try
            {
                logger.LogInformation("Applying database migrations...");
                db.Database.Migrate();
                logger.LogInformation("Database migrations applied.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while applying database migrations.");
                throw;
            }
        }
    }
}
