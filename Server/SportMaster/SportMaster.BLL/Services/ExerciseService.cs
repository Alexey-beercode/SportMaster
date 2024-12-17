using AutoMapper;
using SportMaster.DAL.Interfaces;
using SportMaster.Domain.Entities;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using SportMaster.BLL.Interfaces.Services;
using SportMaster.Domain.Enums;

namespace SportMaster.BLL.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExerciseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExerciseLogDto>> GetExerciseLogsAsync(Guid userId, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default)
        {
            var logs = await _unitOfWork.ExerciseLogs.GetByUserIdAsync(userId, cancellationToken);
            if (startDate.HasValue && endDate.HasValue)
            {
                logs = logs.Where(log => log.Date >= startDate.Value && log.Date <= endDate.Value);
            }
            return _mapper.Map<IEnumerable<ExerciseLogDto>>(logs);
        }

        public async Task AddExerciseLogAsync(ExerciseLogRequestDTO exerciseLogRequest, CancellationToken cancellationToken = default)
        {
            var exerciseLog = _mapper.Map<ExerciseLog>(exerciseLogRequest);
            
            await _unitOfWork.ExerciseLogs.CreateAsync(exerciseLog, cancellationToken);
            var action = new ActionHistory()
            {
                ActionDate = DateTime.UtcNow, ActionType = ActionType.AddExercise,
                Description = $"Вы добавили упражнение продолжительностью {exerciseLogRequest.Duration}", UserId = exerciseLogRequest.UserId
            };
            await _unitOfWork.ActionHistories.CreateAsync(action, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}