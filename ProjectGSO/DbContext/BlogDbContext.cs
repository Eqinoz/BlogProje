using Microsoft.EntityFrameworkCore;
using ProjectGSO.Models;
using ProjectGSO.Models.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectGSO.DbContext
{
    public class BlogDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source = YOSHI\\YOSHI; Database = ProjectGSO; Trusted_Connection = true; TrustServerCertificate = true");
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Content> Content { get; set; }
    }
}
