using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Models.Interface;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace OfficeControlSystemApi.Data.Repositorys
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Employee> GetByIdAsync(long id)
        {
            return await _dbContext.Set<Employee>().FirstOrDefaultAsync(ah => ah.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbContext.Set<Employee>().ToListAsync();
        }

        public async Task AddAsync(Employee entity)
        {
            await _dbContext.Set<Employee>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee entity)
        {
            _dbContext.Set<Employee>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee entity)
        {
            _dbContext.Set<Employee>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
