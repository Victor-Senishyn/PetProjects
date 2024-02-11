using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.Interface;
using System.Collections.Generic;
using System.Linq;

namespace OfficeControlSystemApi.Data.Repositorys
{
    public class VisitHistoryRepository : IVisitHistoryRepository
    {
        private readonly AppDbContext _dbContext;

        public VisitHistoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<VisitHistory>> GetAsync(Func<VisitHistory, bool> filterCriteria)
        {
            return _dbContext.VisitHistories.Where(filterCriteria);
        }

        public async Task AddAsync(VisitHistory entity)
        {
            await _dbContext.Set<VisitHistory>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(VisitHistory entity)
        {
            _dbContext.Set<VisitHistory>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(VisitHistory entity)
        {
            _dbContext.Set<VisitHistory>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
