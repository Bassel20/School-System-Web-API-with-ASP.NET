using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext>options)
            :base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Student> Courses { get; set; }
        public DbSet<Student> Grades { get; set; }
    }
}
