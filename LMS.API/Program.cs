
using FluentValidation.AspNetCore;
using FluentValidation;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using LMS.Core.Interfaces.Repositories;
using LMS.Infrastructure.Repositories;
using LMS.Core.Interfaces.Services;
using LMS.Infrastructure.Services;
using LMS.Infrastructure.Mappings;

namespace LMS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            //builder.Services.AddAutoMapper(typeof(Program).Assembly);

            builder.Services.AddAutoMapper(typeof(MappingProfile));


            builder.Services.AddScoped<IUserRepository, UsersRepository>();
            builder.Services.AddScoped<ILoanRepository, LoansRepository>();
            builder.Services.AddScoped<IBookRepository, BooksRepository>();

            builder.Services.AddScoped<IUsersService,UserService>();
            builder.Services.AddScoped<ILoansService, LoansService>();
            builder.Services.AddScoped<IBooksService, BookService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
