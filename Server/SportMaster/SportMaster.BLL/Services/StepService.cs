using AutoMapper;
using SportMaster.BLL.Dtos.Response;
using SportMaster.BLL.Interfaces.Services;
using SportMaster.DAL.Interfaces;
using SportMaster.Domain.Entities;

namespace SportMaster.BLL.Services;

public class StepService : IStepService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StepService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddStepLogAsync(Guid userId, int steps, CancellationToken cancellationToken)
    {
        var stepLog = new StepLog
        {
            UserId = userId,
            StepsCount = steps,
            Date = DateTime.UtcNow
        };

        await _unitOfWork.StepLogs.CreateAsync(stepLog, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<StepLogDTO>> GetStepLogsAsync(Guid userId, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken)
    {
        var logs = await _unitOfWork.StepLogs.GetByUserIdAsync(userId, cancellationToken);

        if (startDate.HasValue && endDate.HasValue)
        {
            logs = logs.Where(log => log.Date >= startDate && log.Date <= endDate);
        }

        return _mapper.Map<IEnumerable<StepLogDTO>>(logs);
    }
}
