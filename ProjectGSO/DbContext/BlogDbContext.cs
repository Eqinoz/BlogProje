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
                @"Server=mtkc.c9sq2as4s1oa.eu-central-1.rds.amazonaws.com;Database=ProjectGSO;User Id=admin;Password=Sinan7980;TrustServerCertificate=True;");
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<Members> Members { get; set; }
    }
}
