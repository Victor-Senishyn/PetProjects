﻿using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace OfficeControlSystemApi.Data.Repositorys
{
    public class AccessCardRepository: IAccessCardRepository
    {
        private readonly AppDbContext _dbContext;

        public AccessCardRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AccessCard>> Get(Func<AccessCard, bool> filterCriteria)
        {
            return _dbContext.AccessCards.Where(filterCriteria);
        }//

        public async Task AddAsync(AccessCard entity)
        {
            await _dbContext.Set<AccessCard>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(AccessCard entity)
        {
            _dbContext.Set<AccessCard>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(AccessCard entity)
        {
            _dbContext.Set<AccessCard>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
