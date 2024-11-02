using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Data;
using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Data.Repositories;
using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Services;
using Bhd.Evaluacion.Integracion.Application.Services.User.DTOs;
using Bhd.Evaluacion.Integracion.Domain.Entities;

namespace Bhd.Evaluacion.Integracion.Infrastructure.Services;
public class UserService(
    IUserRepository userRepository,
    IUserPhoneRepository userPhoneRepository,
    IEncryptionService encryptionService,
    IUnitOfWork unitOfWork
) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserPhoneRepository _userPhoneRepository = userPhoneRepository;
    private readonly IEncryptionService _encryptionService = encryptionService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<User> Create(UserSignUpDto userSignUpDto, CancellationToken cancellationToken = default)
    {
        var user = await AddUser(userSignUpDto, cancellationToken);

        await AddUserPhones(user.Id, userSignUpDto.Phones, cancellationToken);

        return user;
    }

    private async Task<User> AddUser(UserSignUpDto userSignUpDto, CancellationToken cancellationToken = default)
    {
        var user = new User
        {
            Email = userSignUpDto.Email,
            Password = _encryptionService.Encrypt(userSignUpDto.Password),
            Created = DateTime.UtcNow,
            LastLogin = DateTime.UtcNow,
            IsActive = true,
        };

        await _userRepository.AddAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user;
    }

    private async Task<IEnumerable<UserPhone>> AddUserPhones(Guid userId, IEnumerable<UserPhoneDto> phones, CancellationToken cancellationToken = default)
    {
        var userPhones = phones.Select(phone => new UserPhone
        {
            UserId = userId,
            Number = phone.Number,
            CityCode = phone.CityCode,
            CountryCode = phone.CountryCode,
        });

        await _userPhoneRepository.AddRangeAsync(userPhones, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return userPhones;
    }
}
