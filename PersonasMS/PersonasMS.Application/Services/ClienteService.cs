using AutoMapper;
using PersonasMS.Application.Interfaces;
using PersonasMS.Domain.Dto;
using PersonasMS.Domain.Entities;
using PersonasMS.Domain.Interfaces.Repositories;

namespace PersonasMS.Application.Services
{
    public class ClienteService : IBaseService<ClienteCreateDto, ClienteDto>
    {
        private readonly IBaseRepository<Cliente> _baseRepository;
        private readonly IMapper _mapper;

        public ClienteService(IBaseRepository<Cliente> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public (bool status, int id) Post(ClienteCreateDto entity)
        {
            var obj = _mapper.Map<Cliente>(entity);
            var result = _baseRepository.Insert(obj);
            return (result, obj.Id);
        }

        public async Task<bool> PutAsync(ClienteDto entity, int id)
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

        public async Task<IEnumerable<ClienteDto>> GetAllAsync(int page, int limit, string orderBy, bool ascending = true)
        {
            var result = await _baseRepository.GetAllAsync(page, limit, orderBy, ascending).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<ClienteDto>>(result);
        }

        public async Task<ClienteDto> GetByIdAsync(int id)
        {
            var result = await _baseRepository.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            return _mapper.Map<ClienteDto>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingEntity = await _baseRepository.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (existingEntity is null) return false;
            return _baseRepository.Delete(existingEntity);
        }
    }
}
