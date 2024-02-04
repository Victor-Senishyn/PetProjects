using OfficeControlSystemApi.Models.Interface;

namespace OfficeControlSystemApi.Data
{
    public interface IRepository
    {
        Task<IEntity> GetByIdAsync(long id);
        Task<IEnumerable<IEntity>> GetAllAsync();
        Task AddAsync(IEntity entity);
        Task UpdateAsync(IEntity entity);
        Task DeleteAsync(IEntity entity);
    }
}
