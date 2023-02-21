using PersonasMS.Domain.Dto;

namespace PersonasMS.Application.Interfaces
{
    public interface IPersonaService
    {
        Task<bool> InactivarAsync(PersonaDto persona, int id);
        Task<PersonaDto> GetByCedulaAsync(string cedula);
    }
}
