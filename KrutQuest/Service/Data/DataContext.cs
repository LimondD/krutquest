using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KrutQuest.Service.Models;

namespace KrutQuest.Service.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        {
            Database.EnsureCreated();

            PrePopulateDb();
        }

        private void PrePopulateDb()
        {
            var adminUser = TechUsers.FirstOrDefault(user => user.Login == "Admin");
            if (adminUser == null)
            {
                TechUsers.Add(new TechUser
                {
                    Id = Guid.NewGuid(),
                    Login = "Admin",
                    Password = "RedGreen2002",
                    ContactInfo = "контактная информация"
                });

                SaveChanges();
            }
        }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TechUser> TechUsers { get; set; }

        public DbSet<QuestionGroup> QuestionGroups { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Quest> Quests { get; set; }

        public DbSet<TeamAnswer> TeamAnswers { get; set; }

        public DbSet<ServerImage> ServerImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Quest)
                .WithMany(q => q.Teams)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.TeamAnswers)
                .WithOne(ta => ta.Team)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.TeamAnswers)
                .WithOne(ta => ta.Question)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Group)
                .WithMany(g => g.Questions)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Picture)
                .WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<Quest>()
                .HasOne(q => q.FinishPicture)
                .WithMany(p => p.Quests)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<QuestionGroup>()
                .HasOne(q => q.Quest)
                .WithMany(p => p.Groups)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<QuestionGroup>()
                .HasOne(g => g.MapPicture)
                .WithMany(p => p.Groups)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        }       
    }
}