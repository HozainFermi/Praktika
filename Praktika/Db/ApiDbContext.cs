namespace Praktika.Db
{
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.EntityFrameworkCore;
    using Praktika.Models.Entitys;

    public class ApiDbContext: DbContext

    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TasksEntity> Tasks { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }




    }
}
