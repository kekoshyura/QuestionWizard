
using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Server.Data {
    public class DataContext : IdentityDbContext {

        public DataContext(DbContextOptions<DataContext> options) : base(options) {

        }

        public DbSet<SectionModel> Sections { get; set; }
        public DbSet<SurveyModel> Surveys { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<QuestionOptionModel> QuestionOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SurveyModel>(
                entity => {
                    entity.HasOne(surey => surey.Section)
                    .WithMany(section => section.Surveys)
                    .HasForeignKey("SectionId");
                });

            modelBuilder.Entity<QuestionModel>(
                entity => {
                    entity.HasOne(question => question.Survey)
                    .WithMany(survey => survey.Questions)
                    .HasForeignKey("SurveyId");
                });

            modelBuilder.Entity<QuestionOptionModel>(
                entity => {
                    entity.HasOne(QuestionOptions => QuestionOptions.Question)
                    .WithMany(question => question.QuestionOptions)
                    .HasForeignKey("QuestionId");
                });
        }
    }
}
