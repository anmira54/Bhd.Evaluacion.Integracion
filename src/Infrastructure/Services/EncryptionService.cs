using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Services;

namespace Bhd.Evaluacion.Integracion.Infrastructure.Services;
public class EncryptionService : IEncryptionService
{
    public string Encrypt(string value)
        => BCrypt.Net.BCrypt.HashPassword(value);
}
