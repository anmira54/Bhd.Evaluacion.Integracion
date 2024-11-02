using Bhd.Evaluacion.Integracion.Application.Common.Interfaces.Data.Repositories;
using Bhd.Evaluacion.Integracion.Domain.Entities;
using Bhd.Evaluacion.Integracion.Infrastructure.Contexts;

namespace Bhd.Evaluacion.Integracion.Infrastructure.Data.Repositories;

public class UserPhoneRepository(ApplicationDbContext context) : BaseRepository<UserPhone>(context), IUserPhoneRepository
{
}
