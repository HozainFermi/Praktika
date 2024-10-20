using Microsoft.EntityFrameworkCore;
using Praktika.Contracts;
using Praktika.Db;
using Praktika.Interfaces;
using Praktika.Services;


namespace Praktika
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IParseService, ParseService>();
            builder.Services.AddScoped<IExportService, CsvExportService>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApiDbContext>(options=>options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
           
            app.MapControllers();
            

            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.Run();
        }
    }
}
