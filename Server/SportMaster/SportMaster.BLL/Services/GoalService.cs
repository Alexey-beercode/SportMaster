using AutoMapper;
using SportMaster.DAL.Interfaces;
using SportMaster.Domain.Entities;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using SportMaster.BLL.Dtos.Response;
using SportMaster.BLL.Interfaces.Services;

namespace SportMaster.BLL.Services
{
    public class GoalService : IGoalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GoalService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GoalWithProgressDTO>> GetUserGoalsWithProgressesAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            // Получаем все цели пользователя
            var goals = await _unitOfWork.Goals.GetActiveGoalsByUserIdAsync(userId, cancellationToken);

            // Получаем все прогрессы пользователя
            var progresses = await _unitOfWork.Progresses.GetProgressHistoryByUserIdAsync(userId, cancellationToken);

            // Объединяем цели с соответствующими прогрессами
            var result = goals.Select(goal => new GoalWithProgressDTO
            {
                GoalId = goal.Id,
                GoalType = goal.GoalType,
                TargetWeight = goal.TargetWeight,
                DailyCalorieIntake = goal.DailyCalorieIntake,
                DailyCalorieBurn = goal.DailyCalorieBurn,
                CreatedDate = goal.CreatedDate,
                Progresses = progresses
                    .Where(p => p.Date >= goal.CreatedDate) // Привязываем прогресс к цели по дате создания
                    .Select(p => new ProgressDto
                    {
                        Id = p.Id,
                        UserId = p.UserId,
                        Date = p.Date,
                        Weight = p.Weight,
                        CaloriesConsumed = p.CaloriesConsumed,
                        CaloriesBurned = p.CaloriesBurned
                    })
                    .ToList()
            });

            return result;
        }


        public async Task AddGoalAsync(CreateGoalRequestDTO goalRequest, CancellationToken cancellationToken = default)
        {
            var goal = _mapper.Map<Goal>(goalRequest);
            goal.CreatedDate = DateTime.UtcNow;
            goal.UserId = goalRequest.UserId;
            
            await _unitOfWork.Goals.CreateAsync(goal, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<ProgressDto> GetProgressAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var today = DateTime.UtcNow.Date;

            var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
            // Get all exercises for today
            var exercises = await _unitOfWork.ExerciseLogs.GetByUserIdAsync(userId, cancellationToken);
            var todayExercises = exercises.Where(e => e.Date.Date == today);
            var caloriesBurned = todayExercises.Sum(e => e.CaloriesBurned);

            // Get all food logs for today
            var foodLogs = await _unitOfWork.FoodLogs.GetByUserIdAsync(userId, cancellationToken);
            var todayFoodLogs = foodLogs.Where(f => f.Date.Date == today);
            var caloriesConsumed = todayFoodLogs.Sum(f => f.Calories);

            // Get user's latest progress
            var latestProgress = await _unitOfWork.Progresses.GetLatestProgressByUserIdAsync(userId, cancellationToken);

            return new ProgressDto
            {
                Id = latestProgress?.Id ?? Guid.NewGuid(),
                UserId = userId,
                Date = today,
                Weight = user.Weight,
                CaloriesConsumed = caloriesConsumed,
                CaloriesBurned = caloriesBurned
            };
        }

        public async Task<IEnumerable<CustomGoalDto>> GetCustomGoalsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var customGoals = await _unitOfWork.CustomGoals.GetByUserIdAsync(userId, cancellationToken);
            return _mapper.Map<IEnumerable<CustomGoalDto>>(customGoals);
        }

        public async Task AddCustomGoalAsync(CreateCustomGoalRequestDTO customGoalRequest, CancellationToken cancellationToken = default)
        {
            var customGoal = _mapper.Map<CustomGoal>(customGoalRequest);
            customGoal.CreatedDate = DateTime.UtcNow;
            customGoal.UserId = customGoalRequest.UserId;

            await _unitOfWork.CustomGoals.CreateAsync(customGoal, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<GoalDto>> GetUserGoalsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var goals = await _unitOfWork.Goals.GetActiveGoalsByUserIdAsync(userId, cancellationToken);
            return _mapper.Map<IEnumerable<GoalDto>>(goals);
        }
    }
}
