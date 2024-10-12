using Microsoft.EntityFrameworkCore;
using Praktika.Db;

namespace Praktika
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContext<ApiDbContext>(options=>options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllerRoute(
                
                name:"default",
                pattern:"{controller=Home}/{action=Index}/{Id?}"
                );

            

            app.Run();
        }
    }
}
