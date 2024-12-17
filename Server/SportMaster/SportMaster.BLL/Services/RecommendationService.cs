using AutoMapper;
using SportMaster.DAL.Interfaces;
using SportMaster.Domain.Entities;
using SportMaster.BLL.Dtos.Response;
using SportMaster.BLL.Interfaces.Services;
using SportMaster.Domain.Enums;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SportMaster.BLL.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RecommendationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RecommendationResponseDTO> GetRecommendationsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var today = DateTime.UtcNow.Date;

            // Получаем пользователя, цели и данные за сегодня
            var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
            if (user == null)
                return new RecommendationResponseDTO { RecommendationText = "Пользователь не найден." };

            var goals = await _unitOfWork.Goals.GetActiveGoalsByUserIdAsync(userId, cancellationToken);
            var foodLogs = await _unitOfWork.FoodLogs.GetByUserIdAsync(userId, cancellationToken);
            var exerciseLogs = await _unitOfWork.ExerciseLogs.GetByUserIdAsync(userId, cancellationToken);
            var stepLogs = await _unitOfWork.StepLogs.GetByUserIdAsync(userId, cancellationToken);
            var waterLogs = await _unitOfWork.WaterLogs.GetByUserIdAsync(userId, cancellationToken);

            // Собираем данные за сегодня
            var todayFoodCalories = foodLogs.Where(f => f.Date.Date == today).Sum(f => f.Calories);
            var todayExerciseCalories = exerciseLogs.Where(e => e.Date.Date == today).Sum(e => e.CaloriesBurned);
            var todaySteps = stepLogs.Where(s => s.Date.Date == today).Sum(s => s.StepsCount);
            var todayWater = waterLogs.Where(w => w.Date.Date == today).Sum(w => w.GlassesDrunk);

            var calorieBalance = todayFoodCalories - todayExerciseCalories;

            // Генерируем итоговую рекомендацию
            var recommendation = new StringBuilder();

            // Генерация рекомендаций по целям
            var goalRecommendation = GenerateGoalRecommendations(goals, calorieBalance);
            if (!string.IsNullOrEmpty(goalRecommendation))
                recommendation.AppendLine(goalRecommendation);

            // Рекомендация по шагам
            var stepRecommendation = GenerateStepRecommendation(user, todaySteps);
            if (!string.IsNullOrEmpty(stepRecommendation))
                recommendation.AppendLine(stepRecommendation);

            // Рекомендация по воде
            var waterRecommendation = GenerateWaterRecommendation(user, todayWater);
            if (!string.IsNullOrEmpty(waterRecommendation))
                recommendation.AppendLine(waterRecommendation);

            return new RecommendationResponseDTO
            {
                RecommendationText = recommendation.ToString().Trim()
            };
        }

        private string GenerateGoalRecommendations(IEnumerable<Goal> goals, decimal calorieBalance)
        {
            var recommendations = goals
                .Select(goal => GenerateGoalRecommendation(goal, calorieBalance))
                .Where(text => !string.IsNullOrEmpty(text))
                .Distinct()
                .ToList();

            return recommendations.Any() ? string.Join(" ", recommendations) : string.Empty;
        }

        private string GenerateGoalRecommendation(Goal goal, decimal calorieBalance)
        {
            return goal.GoalType switch
            {
                GoalType.WeightLoss when calorieBalance > 0 =>
                    "Для похудения уменьшите потребление калорий или увеличьте активность.",
                GoalType.MuscleGain when calorieBalance < 0 =>
                    "Для набора мышечной массы увеличьте потребление калорий.",
                GoalType.Maintenance when calorieBalance == 0 =>
                    "Ваш калорийный баланс в норме. Отличная работа!",
                GoalType.Maintenance =>
                    "Небольшие отклонения в балансе калорий. Проверьте питание и активность.",
                _ => string.Empty
            };
        }

        private string GenerateStepRecommendation(User user, int todaySteps)
        {
            return todaySteps >= user.DailyStepGoal
                ? "Вы достигли дневной нормы по шагам. Отличная работа!"
                : $"До нормы по шагам осталось пройти {user.DailyStepGoal - todaySteps} шагов.";
        }

        private string GenerateWaterRecommendation(User user, int todayWater)
        {
            return todayWater >= user.DailyWaterGoal
                ? "Вы выполнили дневную норму по воде. Продолжайте в том же духе!"
                : $"Осталось выпить {user.DailyWaterGoal - todayWater} стаканов воды для выполнения нормы.";
        }
    }
}
