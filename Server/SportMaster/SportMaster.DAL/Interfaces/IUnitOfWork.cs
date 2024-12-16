using SportMaster.DAL.Interfaces.Repositories;

namespace SportMaster.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IUserRepository Users { get; }
        IFoodLogRepository FoodLogs { get; }
        IExerciseLogRepository ExerciseLogs { get; }
        IGoalRepository Goals { get; }
        IProgressRepository Progresses { get; }
        INotificationRepository Notifications { get; }
        IActionHistoryRepository ActionHistories { get; }
        IPersonalDataRepository PersonalData { get; }
        IRecommendationRepository Recommendations { get; }
        ICustomGoalRepository CustomGoals { get; }
    }
}