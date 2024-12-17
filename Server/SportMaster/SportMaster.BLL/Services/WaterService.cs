using AutoMapper;
using SportMaster.BLL.Dtos.Response;
using SportMaster.BLL.Interfaces.Services;
using SportMaster.DAL.Interfaces;
using SportMaster.Domain.Entities;

namespace SportMaster.BLL.Services;

public class WaterService : IWaterService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WaterService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddWaterLogAsync(Guid userId, int glasses, CancellationToken cancellationToken)
    {
        var waterLog = new WaterLog
        {
            UserId = userId,
            GlassesDrunk = glasses,
            Date = DateTime.UtcNow
        };

        await _unitOfWork.WaterLogs.CreateAsync(waterLog, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<WaterLogDTO>> GetWaterLogsAsync(Guid userId, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken)
    {
        var logs = await _unitOfWork.WaterLogs.GetByUserIdAsync(userId, cancellationToken);

        if (startDate.HasValue && endDate.HasValue)
        {
            logs = logs.Where(log => log.Date >= startDate && log.Date <= endDate);
        }

        return _mapper.Map<IEnumerable<WaterLogDTO>>(logs);
    }
}
