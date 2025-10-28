
using ITIApi.BL.Interfaces;
using ITIApi.BL.UnitOfWorks;
using ITIApi.Configuration;
using ITIApi.DAL.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


namespace ITIApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var MyAllowSpecificOrigins = "_origins";
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            #region AutoMapper Options
            builder.Services.AddAutoMapper((op) => op.AddProfile<MapperConfig>());
            #endregion

            #region Services Injection

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            #region Databases Injection

            builder.Services.AddDbContext<ApiContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApiDb"));
            });
            builder.Services.AddDbContext<ITIContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ItiDb"));
            });

            #endregion


            #region Cors Policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            #endregion


            builder.Services.AddEndpointsApiExplorer();

            #region Swagger Options
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1",
                    Description = "Example ASP.NET Core 9 API"
                });
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors(MyAllowSpecificOrigins);

            app.MapControllers();

            app.Run();
        }
    }
}
