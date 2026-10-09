using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public class ApplicationDbContext : DbContext
    {
            protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Attachement>() 
            .HasOne(a => a.UploadedBy)
            .WithMany()
            .HasForeignKey("UploadedById") 
            .OnDelete(DeleteBehavior.NoAction); 


            modelBuilder.Entity<WorkLog>() 
        .HasOne(w => w.Implementer)  
        .WithMany()
        .HasForeignKey("ImplementerId") 
        .OnDelete(DeleteBehavior.NoAction);  
    }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Attachement> Attachements { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<WorkLog> WorkLogs { get; set; } 
    }
}
