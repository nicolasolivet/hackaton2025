using APIhackaton.Services;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using APIhackaton.Data;
using Microsoft.Extensions.Hosting;
namespace APIhackaton
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Allow the web app (served from ctrlcctrlv) to call this API during development
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowWeb",
                    policy => policy.WithOrigins("http://localhost:5149").AllowAnyHeader().AllowAnyMethod());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register EF Core DbContext (SQLite)
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=app.db")
            );

            builder.Services.AddHttpClient<OllamaService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:11434/"); // Cambia la URL si tu instancia de Ollama est� en otro lugar
                // Increase timeout to accommodate longer model generation times (default is 100s)
                client.Timeout = TimeSpan.FromMinutes(10);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enable CORS for the web app origin
            app.UseCors("AllowWeb");

            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();

        }
    }
}
