using AuthorizationApi.Application;
using AuthorizationApi.Application.Options;
using AuthorizationApi.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //Options
            builder.Services.Configure<JwtOptions>(
                builder.Configuration.GetSection("JwtOptions"));
            //Infrastructure Layer
            builder.Services.AddInfrastructureLayer();
            //Application Layer
            builder.Services.AddApplicationLayer();
            //Authentication
            JwtOptions jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>()!;
            builder.Services.AddAuthenticationLayer(jwtOptions);

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Db services
            builder.Services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("AuthDb")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
