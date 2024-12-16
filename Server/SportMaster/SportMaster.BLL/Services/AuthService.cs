using AuthService.BLL.Exceptions;
using AuthService.BLL.Interfaces.Services;
using AutoMapper;
using EventMaster.BLL.Exceptions;
using SportMaster.BLL.Dtos.Request;
using SportMaster.BLL.Dtos.Request.Auth;
using SportMaster.BLL.Dtos.Response;
using SportMaster.BLL.Helpers;
using SportMaster.BLL.Interfaces.Services;
using SportMaster.DAL.Interfaces;
using SportMaster.Domain.Entities;

namespace SportMaster.BLL.Services;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public AuthService(IMapper mapper, IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<AuthReponseDTO> RegisterAsync(
        UserRequestDTO registerDto, CancellationToken cancellationToken = default)
    {
        var userFromDb = await _unitOfWork.Users.GetByEmailAsync(registerDto.Email, cancellationToken);
        if (userFromDb != null)
        {
            throw new AlreadyExistsException("User");
        }

        var newUser = _mapper.Map<User>(registerDto);
        newUser.PasswordHash = PasswordHelper.HashPassword(registerDto.Password);

        await _unitOfWork.Users.CreateAsync(newUser, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var user = await _unitOfWork.Users.GetByEmailAsync(registerDto.Email);

        var token = _tokenService.GenerateAccessToken(user);
        return new AuthReponseDTO() { AccessToken = token, UserId = user.Id };
    }

    public async Task<AuthReponseDTO> LoginAsync(LoginDTO loginDto, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(loginDto.Email, cancellationToken);

        if (user == null)
        {
            throw new EntityNotFoundException($"User with email {loginDto.Email} does not exist");
        }
        
        var token = _tokenService.GenerateAccessToken(user);
        return new AuthReponseDTO() { AccessToken = token, UserId = user.Id };
    }
}