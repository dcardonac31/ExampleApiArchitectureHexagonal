using AutoMapper;
using PersonasMS.Application.Interfaces;
using PersonasMS.Domain.Dto;
using PersonasMS.Domain.Entities;
using PersonasMS.Domain.Interfaces.Repositories;

namespace PersonasMS.Application.Services
{
    public class GeneroService : IBaseService<GeneroCreateDto, GeneroDto>
    {
        private readonly IBaseRepository<Genero> _baseRepository;
        private readonly IMapper _mapper;

        public GeneroService(IBaseRepository<Genero> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public (bool status, int id) Post(GeneroCreateDto entity)
        {
            var obj = _mapper.Map<Genero>(entity);
            var result = _baseRepository.Insert(obj);
            return (result, obj.Id);
        }

        public async Task<bool> PutAsync(GeneroDto entity, int id)
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

        public async Task<IEnumerable<GeneroDto>> GetAllAsync(int page, int limit, string orderBy, bool ascending = true)
        {
            var result = await _baseRepository.GetAllAsync(page, limit, orderBy, ascending).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<GeneroDto>>(result);
        }

        public async Task<GeneroDto> GetByIdAsync(int id)
        {
            var result = await _baseRepository.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            return _mapper.Map<GeneroDto>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingEntity = await _baseRepository.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if (existingEntity is null) return false;
            return _baseRepository.Delete(existingEntity);
        }
    }
}
