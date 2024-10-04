
using Labb2LibraryAngular.Data;
using Labb2LibraryAngular.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Labb2LibraryAngular
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
            builder.Services.AddAutoMapper(typeof(MappingConfig));

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionToDB")));

            builder.Services.AddScoped<IBookRepo, BookRepo>();
            builder.Services.AddCors((setup) => setup.AddPolicy("default", (options) =>
            {
                options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
            }));
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("default");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
