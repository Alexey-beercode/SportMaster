using AutoMapper;
using SportMaster.DAL.Interfaces;
using SportMaster.Domain.Entities;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using SportMaster.BLL.Interfaces.Services;
using SportMaster.Domain.Enums;

namespace SportMaster.BLL.Services
{
    public class FoodService : IFoodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FoodService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FoodLogDto>> GetFoodLogsAsync(Guid userId, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default)
        {
            var logs = await _unitOfWork.FoodLogs.GetByUserIdAsync(userId, cancellationToken);
            if (startDate.HasValue && endDate.HasValue)
            {
                logs = logs.Where(log => log.Date >= startDate.Value && log.Date <= endDate.Value);
            }
            return _mapper.Map<IEnumerable<FoodLogDto>>(logs);
        }

        public async Task AddFoodLogAsync(FoodLogRequestDTO foodLogRequest,
            CancellationToken cancellationToken = default)
        {
            var foodLog = _mapper.Map<FoodLog>(foodLogRequest);

            await _unitOfWork.FoodLogs.CreateAsync(foodLog, cancellationToken);
            
            var action = new ActionHistory()
                {
                    ActionDate = DateTime.UtcNow, ActionType = ActionType.AddFood,
                    Description = $"Вы добавили еду {foodLogRequest.FoodName}", UserId = foodLog.UserId
                };
            await _unitOfWork.ActionHistories.CreateAsync(action, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}