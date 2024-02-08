using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.Interface;
using System.Collections.Generic;
using System.Linq;

namespace OfficeControlSystemApi.Data.Repositorys
{
    public class VisitHistoryRepository
    {
        private readonly AppDbContext _dbContext;

        public VisitHistoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public VisitHistory GetById(long id)
        {
            return _dbContext.Set<VisitHistory>().Find(id);
        }

        public IEnumerable<VisitHistory> GetAll()
        {
            return _dbContext.Set<VisitHistory>().ToList();
        }

        public async Task AddAsync(VisitHistory entity)
        {
            await _dbContext.Set<VisitHistory>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void Update(VisitHistory entity)
        {
            _dbContext.Set<VisitHistory>().Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(VisitHistory entity)
        {
            _dbContext.Set<VisitHistory>().Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
