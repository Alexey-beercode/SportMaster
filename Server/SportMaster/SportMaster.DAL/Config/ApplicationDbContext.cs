using Microsoft.EntityFrameworkCore;
using SportMaster.DAL.Config.Database;
using SportMaster.Domain.Entities;

namespace SportMaster.DAL.Config;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<FoodLog> FoodLogs { get; set; }
    public DbSet<ExerciseLog> ExerciseLogs { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Progress> Progresses { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<ActionHistory> ActionHistories { get; set; }
    public DbSet<PersonalData> PersonalData { get; set; }
    public DbSet<Recommendation> Recommendations { get; set; }
    public DbSet<CustomGoal> CustomGoals { get; set; }
    public DbSet<WaterLog> WaterLogs { get; set; }
    public DbSet<StepLog> StepLogs { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new FoodLogConfiguration());
        modelBuilder.ApplyConfiguration(new ExerciseLogConfiguration());
        modelBuilder.ApplyConfiguration(new GoalConfiguration());
        modelBuilder.ApplyConfiguration(new ProgressConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());
        modelBuilder.ApplyConfiguration(new ActionHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new PersonalDataConfiguration());
        modelBuilder.ApplyConfiguration(new RecommendationConfiguration());
        modelBuilder.ApplyConfiguration(new CustomGoalConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}