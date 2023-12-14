using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Server.Data {
    public class AppDbContext : DbContext {
        public DbSet<QuestionModel> Questions { get; set; }
    }
}
