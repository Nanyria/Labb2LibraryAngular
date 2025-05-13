
using FinalProjectLibrary.Data;
using FinalProjectLibrary.Repositories;
using FinalProjectLibrary.Services;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.WriteIndented = true; // Optional: For better readability
                });
          
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IBookRepo, BookRepo>();
            builder.Services.AddAutoMapper(typeof(MappingConfig));


            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionToDB")));


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
