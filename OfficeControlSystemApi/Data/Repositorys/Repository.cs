using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Models.Interface;
using OfficeControlSystemApi.Data;

namespace OfficeControlSystemApi.Data.Repositorys
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEntity> GetByIdAsync(long id)
        {
            return await _dbContext.Set<IEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<IEntity>> GetAllAsync()
        {
            return await _dbContext.Set<IEntity>().ToListAsync();
        }

        public async Task AddAsync(IEntity entity)
        {
            await _dbContext.Set<IEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(IEntity entity)
        {
            _dbContext.Set<IEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(IEntity entity)
        {
            _dbContext.Set<IEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
