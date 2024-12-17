using AutoMapper;
using SportMaster.DAL.Interfaces;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Interfaces.Services;

namespace SportMaster.BLL.Services
{
    public class ActionHistoryService : IActionHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActionHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActionHistoryDto>> GetUserActionHistoryAsync(Guid userId, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default)
        {
            var history = await _unitOfWork.ActionHistories.GetByUserIdAsync(userId, cancellationToken);
            
            if (startDate.HasValue && endDate.HasValue)
            {
                history = history.Where(h => h.ActionDate >= startDate.Value && h.ActionDate <= endDate.Value);
            }
            
            return _mapper.Map<IEnumerable<ActionHistoryDto>>(history);
        }
    }
}