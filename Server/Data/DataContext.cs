
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Server.Data {
    public class DataContext : DbContext {

        public DataContext(DbContextOptions<DataContext> options) : base(options) {

        }

        public DbSet<SectionModel> Sections { get; set; }
        public DbSet<SurveyModel> Surveys { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<QuestionOptionModel> QuestionOptions { get; set; }
    }
}
