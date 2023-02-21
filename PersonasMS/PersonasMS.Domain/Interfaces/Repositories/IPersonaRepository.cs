using PersonasMS.Domain.Entities;

namespace PersonasMS.Domain.Interfaces.Repositories
{
    public interface IPersonaRepository
    {
        bool InactivarAsync(Persona persona);
    }
}
