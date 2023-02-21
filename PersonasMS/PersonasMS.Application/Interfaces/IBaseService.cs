namespace PersonasMS.Application.Interfaces
{
    public interface IBaseService<TEntityCreateDto, TEntityDto>
    {
        Task<TEntityDto> GetByIdAsync(int id);
        Task<IEnumerable<TEntityDto>> GetAllAsync(int page, int limit, string orderBy, bool ascending = true);
        (bool status, int id) Post(TEntityCreateDto entity);
        Task<bool> PutAsync(TEntityDto entity, int id);
        Task<bool> DeleteAsync(int id);
    }
}
