using AccountingLedger.API.Utils;
using AccountingLedger.Application;
using AccountingLedger.Application.Common.Behaviors;
using AccountingLedger.Application.Interfaces;
using AccountingLedger.Application.Mappings;
using AccountingLedger.Application.Validators;
using AccountingLedger.Infrastructure.Data;
using AccountingLedger.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


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

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateAccountCommandValidator>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000") // frontend port
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Accounting Ledger API",
                    Version = "v1"
                });
            });



            //builder.Services.AddMediatR(Assembly.Load("AccountingLedger.Application"));
            //builder.Services.AddAutoMapper(Assembly.Load("AccountingLedger.Application"));
            //builder.Services.AddValidatorsFromAssembly(Assembly.Load("AccountingLedger.Application"));

            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddTransient<IAccountRepository, AccountRepository>();
            builder.Services.AddTransient<IJournalEntryRepository, JournalEntryRepository>();
            builder.Services.AddTransient<ITrialBalanceRepository, TrialBalanceRepository>();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ledger API V1");
                });
            }

            // Apply migrations and seed data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<LedgerDbContext>();
                // context.Database.Migrate(); // Apply migrations
                DbInitializer.Initialize(context); // Seed data
            }

            // Configure the HTTP request pipeline.

            app.UseCors("AllowFrontend");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
