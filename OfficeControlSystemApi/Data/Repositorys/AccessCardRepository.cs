using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace OfficeControlSystemApi.Data.Repositorys
{
    public class AccessCardRepository : IAccessCardRepository
    {
        private readonly AppDbContext _dbContext;

        public AccessCardRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AccessCard GetById(long id)
        {
            return _dbContext.Set<AccessCard>().Find(id);
        }

        public IEnumerable<AccessCard> GetAll()
        {
            return _dbContext.Set<AccessCard>().ToList();
        }

        public void Add(AccessCard entity)
        {
            _dbContext.Set<AccessCard>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(AccessCard entity)
        {
            _dbContext.Set<AccessCard>().Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(AccessCard entity)
        {
            _dbContext.Set<AccessCard>().Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
