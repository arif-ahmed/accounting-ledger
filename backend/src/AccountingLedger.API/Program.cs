using AccountingLedger.API.Utils;
using AccountingLedger.Application;
using AccountingLedger.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AccountingLedger.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<LedgerDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            //builder.Services.AddMediatR(Assembly.Load("AccountingLedger.Application"));
            //builder.Services.AddAutoMapper(Assembly.Load("AccountingLedger.Application"));
            //builder.Services.AddValidatorsFromAssembly(Assembly.Load("AccountingLedger.Application"));

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
            });

            var app = builder.Build();

            // Apply migrations and seed data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<LedgerDbContext>();
                // context.Database.Migrate(); // Apply migrations
                DbInitializer.Initialize(context); // Seed data
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
