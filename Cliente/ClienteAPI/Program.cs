
using ClienteAPI.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ClienteAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClienteAPI", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            builder.Services.Configure<Conexion>(builder.Configuration.GetSection("Conexion"));
            builder.Services.AddSingleton<ClienteData>();


            //Reglas Corq
            var reglasCors = "CorsPolicy";

            builder.Services.AddCors(options =>
            {
              
                options.AddPolicy(reglasCors,
                    builder => builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin());
                        
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            /*
            if (app.Environment.IsDevelopment())
            {

            }
            */

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClienteAPI v1"));

            app.UseCors(reglasCors);

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
