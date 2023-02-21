using AutoMapper;
using PersonasMS.Application.Interfaces;
using PersonasMS.Domain.Dto;
using PersonasMS.Domain.Entities;
using PersonasMS.Domain.Interfaces.Repositories;

namespace PersonasMS.Application.Services
{
    public class DepartamentoService : IBaseService<DepartamentoCreateDto, DepartamentoDto>
    {
        private readonly IBaseRepository<Departamento> _baseRepository;
        private readonly IMapper _mapper;

        public DepartamentoService(IBaseRepository<Departamento> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public (bool status, int id) Post(DepartamentoCreateDto entity)
        {
            var obj = _mapper.Map<Departamento>(entity);
            var result = _baseRepository.Insert(obj);
            return (result, obj.Id);
        }

        public async Task<bool> PutAsync(DepartamentoDto entity, int id)
        {
            var existingEntity = await _baseRepository.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if(existingEntity is null)
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

        public async Task<IEnumerable<DepartamentoDto>> GetAllAsync(int page, int limit, string orderBy, bool ascending = true)
        {
            var result = await _baseRepository.GetAllAsync(page, limit, orderBy, ascending).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<DepartamentoDto>>(result);
        }

        public async Task<DepartamentoDto> GetByIdAsync(int id)
        {
            var result = await _baseRepository.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            return _mapper.Map<DepartamentoDto>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingEntity = await _baseRepository.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            if(existingEntity is null) return false;
            return _baseRepository.Delete(existingEntity);
        }
    }
}
