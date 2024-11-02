namespace Bhd.Evaluacion.Integracion.Application.Services.User.DTOs;

public class UserSignUpDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public IEnumerable<UserPhoneDto> Phones { get; set; } = [];
}
