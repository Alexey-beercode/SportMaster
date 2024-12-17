using SportMaster.DAL.Interfaces;
using SportMaster.DAL.Interfaces.Repositories;
using SportMaster.DAL.Config;

namespace SportMaster.DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        private readonly IFoodLogRepository _foodLogRepository;
        private readonly IExerciseLogRepository _exerciseLogRepository;
        private readonly IGoalRepository _goalRepository;
        private readonly IProgressRepository _progressRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IActionHistoryRepository _actionHistoryRepository;
        private readonly IPersonalDataRepository _personalDataRepository;
        private readonly IRecommendationRepository _recommendationRepository;
        private readonly ICustomGoalRepository _customGoalRepository;
        private readonly IWaterLogRepository _waterLogRepository;
        private readonly IStepLogRepository _stepLogRepository;

        public UnitOfWork(
            IUserRepository userRepository,
            IFoodLogRepository foodLogRepository,
            IExerciseLogRepository exerciseLogRepository,
            IGoalRepository goalRepository,
            IProgressRepository progressRepository,
            INotificationRepository notificationRepository,
            IActionHistoryRepository actionHistoryRepository,
            IPersonalDataRepository personalDataRepository,
            IRecommendationRepository recommendationRepository,
            ICustomGoalRepository customGoalRepository,
            IWaterLogRepository waterLogRepository,
            IStepLogRepository stepLogRepository,
            ApplicationDbContext dbContext)
        {
            _userRepository = userRepository;
            _foodLogRepository = foodLogRepository;
            _exerciseLogRepository = exerciseLogRepository;
            _goalRepository = goalRepository;
            _progressRepository = progressRepository;
            _notificationRepository = notificationRepository;
            _actionHistoryRepository = actionHistoryRepository;
            _personalDataRepository = personalDataRepository;
            _recommendationRepository = recommendationRepository;
            _customGoalRepository = customGoalRepository;
            _waterLogRepository = waterLogRepository;
            _stepLogRepository = stepLogRepository;
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public IUserRepository Users => _userRepository;
        public IFoodLogRepository FoodLogs => _foodLogRepository;
        public IExerciseLogRepository ExerciseLogs => _exerciseLogRepository;
        public IGoalRepository Goals => _goalRepository;
        public IProgressRepository Progresses => _progressRepository;
        public INotificationRepository Notifications => _notificationRepository;
        public IActionHistoryRepository ActionHistories => _actionHistoryRepository;
        public IPersonalDataRepository PersonalData => _personalDataRepository;
        public IRecommendationRepository Recommendations => _recommendationRepository;
        public ICustomGoalRepository CustomGoals => _customGoalRepository;
        public IWaterLogRepository WaterLogs => _waterLogRepository;
        public IStepLogRepository StepLogs => _stepLogRepository;
    }
}
