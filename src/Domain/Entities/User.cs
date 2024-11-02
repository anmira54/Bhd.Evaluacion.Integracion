namespace Bhd.Evaluacion.Integracion.Domain.Entities;
public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime? Modified { get; set; }
    public DateTime? LastLogin { get; set; }
    public bool IsActive { get; set; }
}
