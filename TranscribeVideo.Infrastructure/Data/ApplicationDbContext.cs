using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TranscribeVideo.Core.Entities;

namespace TranscribeVideo.Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<RefreshTokens> RefreshTokens { get; set; }
        public DbSet<Videos> Videos { get; set; }
        public DbSet<ProcessingJobs> ProcessingJobs { get; set; }
        public DbSet<Transcript> Transcripts { get; set; }
        public DbSet<Summaries> Summaries { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Users>()
        //        .Property(x => x.CreatedAt);
        //}
    }
}
