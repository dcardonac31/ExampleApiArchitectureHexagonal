using AutoMapper;
using PersonasMS.Application.Interfaces;
using PersonasMS.Domain.Dto;
using PersonasMS.Domain.Entities;
using PersonasMS.Domain.Interfaces.Repositories;

namespace PersonasMS.Application.Services
{
    public class PersonaService : IBaseService<PersonaCreateDto, PersonaDto>, IPersonaService
    {
        private readonly IBaseRepository<Persona> _baseRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IMapper _mapper;

        public PersonaService(IBaseRepository<Persona> baseRepository, IPersonaRepository personaRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _personaRepository = personaRepository;
            _mapper = mapper;
        }
        public (bool status, int id) Post(PersonaCreateDto entity)
        {
            var obj = _mapper.Map<Persona>(entity);
            var result = _baseRepository.Insert(obj);
            return (result, obj.Id);
        }

        public async Task<bool> PutAsync(PersonaDto entity, int id)
        {
            var existingEntity = await _baseRepository.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (existingEntity is null)
            {
                return false;
            }

            var fechaCreacion = existingEntity.FechaCreacion;
            var usuarioCreacion = existingEntity.UsuarioCreacion;

            _mapper.Map(entity, existingEntity);

            existingEntity.FechaCreacion = fechaCreacion;
            existingEntity.UsuarioCreacion = usuarioCreacion;

            return _baseRepository.Update(existingEntity);
        }

        public async Task<IEnumerable<PersonaDto>> GetAllAsync(int page, int limit, string orderBy, bool ascending = true)
        {
            var result = await _baseRepository.GetAllAsync(page, limit, orderBy, ascending).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<PersonaDto>>(result);
        }

        public async Task<PersonaDto> GetByIdAsync(int id)
        {
            var result = await _baseRepository.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            return _mapper.Map<PersonaDto>(result);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var existingEntity = await _baseRepository.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (existingEntity is null) return false;
            return _baseRepository.Delete(existingEntity);
        }

        public async Task<bool> InactivarAsync(PersonaDto persona, int id)
        {
            var existingEntity = await _baseRepository.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (existingEntity is null)
            {
                return false;
            }

            var fechaCreacion = existingEntity.FechaCreacion;
            var usuarioCreacion = existingEntity.UsuarioCreacion;

            _mapper.Map(persona, existingEntity);

            existingEntity.FechaCreacion = fechaCreacion;
            existingEntity.UsuarioCreacion = usuarioCreacion;

            return _personaRepository.InactivarAsync(existingEntity);
        }

        public async Task<PersonaDto> GetByCedulaAsync(string cedula)
        {
            var result = await _baseRepository.FirstOrDefaultAsync(x => x.Cedula == cedula).ConfigureAwait(false);
            return _mapper.Map<PersonaDto>(result);
        }
    }
}
