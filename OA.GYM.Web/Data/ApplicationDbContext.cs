using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OA.GYM.Entities;

namespace OA.GYM.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<TrainingClass> TrainingClasses { get; set; }
        public DbSet<ClassType> ClassTypes { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}