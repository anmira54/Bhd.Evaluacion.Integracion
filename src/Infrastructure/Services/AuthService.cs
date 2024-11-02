using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Data.Repositories;
using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Bhd.Evaluacion.Integracion.Infrastructure.Services;
public class AuthService(
    IConfiguration configuration,
    IUserRepository userRepository
) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IConfiguration _configuration = configuration;

    public async Task<string> GetUserToken(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.Get(userId, cancellationToken)
            ?? throw new NotFoundException(userId.ToString(), nameof(Domain.Entities.User));

        if (!user.IsActive)
            throw new UnauthorizedAccessException(_configuration["Error:Unauthorized:InactiveUser"]);

        return GenerateToken(user);
    }

    public string GenerateToken(Domain.Entities.User user)
    {
        var securityKey = GetSymmetricSecurityKey();
        var credentials = GetSigningCredentials(securityKey);
        var claims = GetClaims(user);
        var token = CreateJwtToken(credentials, claims);
        return WriteToken(token);
    }

    private SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        var keyBytes = Encoding.UTF8.GetBytes(_configuration["Authentication:Secret"] ?? throw new InvalidOperationException("'Authentication:Secret' is required"));
        return new SymmetricSecurityKey(keyBytes);
    }

    private static SigningCredentials GetSigningCredentials(SymmetricSecurityKey securityKey)
        => new(securityKey, SecurityAlgorithms.HmacSha256);

    private static IEnumerable<Claim> GetClaims(Domain.Entities.User user) =>
        [
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email)
        ];

    private JwtSecurityToken CreateJwtToken(SigningCredentials credentials, IEnumerable<Claim> claims)
        => new(
            issuer: _configuration["Authentication:Issuer"],
            audience: _configuration["Authentication:Audience"],
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Authentication:Expires")),
            signingCredentials: credentials
        );

    private static string WriteToken(JwtSecurityToken token)
        => new JwtSecurityTokenHandler().WriteToken(token);
}
