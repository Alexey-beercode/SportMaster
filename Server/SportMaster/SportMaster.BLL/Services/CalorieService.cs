using AutoMapper;
using SportMaster.BLL.Dtos.Response;
using SportMaster.BLL.Interfaces.Services;
using SportMaster.DAL.Interfaces;
using SportMaster.Domain.Entities;
using SportMaster.Domain.Enums;

namespace SportMaster.BLL.Services;

public class CalorieService : ICalorieService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CalorieService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserCaloriesDTO> CalculateDailyCaloriesAsync(Guid userId, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow.Date;

        var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken)
                   ?? throw new KeyNotFoundException("User not found.");

        // Расчет нормы калорий
        var bmr = CalculateBMR(user);
        var calorieNorm = bmr * GetActivityCoefficient(user.ActivityLevel);

        // Получаем логи еды и упражнений за сегодня
        var foodLogs = await _unitOfWork.FoodLogs.GetByUserIdAsync(userId, cancellationToken);
        var exerciseLogs = await _unitOfWork.ExerciseLogs.GetByUserIdAsync(userId, cancellationToken);
        var steps = await _unitOfWork.StepLogs.GetByUserIdAsync(userId, cancellationToken);

        var caloriesConsumed = foodLogs.Where(f => f.Date.Date == today).Sum(f => f.Calories);
        var caloriesBurned = exerciseLogs.Where(e => e.Date.Date == today).Sum(e => e.CaloriesBurned)
                             + steps.Where(s => s.Date.Date == today).Sum(s => s.StepsCount / 25.0m); // 400 калорий на 10k шагов

       
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UserCaloriesDTO
        {
            CaloriesNorm = calorieNorm,
            CaloriesConsumed = caloriesConsumed,
            CaloriesBurned = caloriesBurned
        };
    }
    
    private decimal CalculateBMR(User user)
    {
        return user.Gender == Gender.Male
            ? 9.99m * user.Weight + 6.25m * user.Height - 4.92m * user.Age + 5
            : 9.99m * user.Weight + 6.25m * user.Height - 4.92m * user.Age - 161;
    }

    private decimal GetActivityCoefficient(string activityLevel)
    {
        return activityLevel switch
        {
            "Не очень подвижный" => 1.2m,
            "Малоподвижный" => 1.4m,
            "Активный" => 1.7m,
            "Очень подвижный" => 1.9m,
            _ => 1.2m
        };
    }
}
