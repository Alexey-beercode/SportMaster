using AutoMapper;
using SportMaster.DAL.Interfaces;
using SportMaster.Domain.Entities;
using SportMaster.BLL.Dtos;
using SportMaster.BLL.Dtos.Request;
using System;
using System.Threading;
using System.Threading.Tasks;
using SportMaster.BLL.Interfaces.Services;

namespace SportMaster.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
            if (user == null || user.IsDeleted)
            {
                throw new KeyNotFoundException("User not found.");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserAsync(Guid userId, UpdateUserRequestDTO updateUserRequest, CancellationToken cancellationToken = default)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
            if (user == null || user.IsDeleted)
            {
                throw new KeyNotFoundException("User not found.");
            }
            
            _mapper.Map(updateUserRequest, user);

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}