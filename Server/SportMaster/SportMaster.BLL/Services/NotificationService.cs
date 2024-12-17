using AutoMapper;
using SportMaster.DAL.Interfaces;
using SportMaster.DAL.Interfaces.Repositories;
using SportMaster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventMaster.BLL.Exceptions;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Interfaces.Services;

namespace SportMaster.BLL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificationDto>> GetNotificationsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException("User", userId);
            }
            
            var notifications = await _unitOfWork.Notifications.GetUnreadNotificationsByUserIdAsync(userId, cancellationToken);
            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }

        public async Task MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken = default)
        {
            var notification = await _unitOfWork.Notifications.GetByIdAsync(notificationId, cancellationToken);
            if (notification is null)
            {
                throw new EntityNotFoundException(
                    "Notification ",notificationId);
            }

            notification.IsRead = true;
            _unitOfWork.Notifications.Update(notification);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}