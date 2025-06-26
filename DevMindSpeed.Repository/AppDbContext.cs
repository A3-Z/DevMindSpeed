using DevMindSpeed.Entity.Aggregates.GameAggregate;
using DevMindSpeed.Entity.Aggregates.QuestionAggregate;
using DevMindSpeed.Common.Db.Abstraction;
using Microsoft.EntityFrameworkCore;


namespace DevMindSpeed.DataAccessLayer
{
    public class AppDbContext : DbBaseContext
    {
        public AppDbContext(DbContextOptions dbContextOptions)
           : base(dbContextOptions)
        {
        }
        public DbSet<Game> Games {  get; set; }
        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>()
                .HasMany(g => g.Questions)
                .WithOne(q => q.Game) 
                .HasForeignKey(q => q.GameId)
                .OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<Question>()
            //   .HasOne(q => q.Game)
            //   .WithMany(g => g.Questions)
            //   .HasForeignKey(q => q.GameId)
            //   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
