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

        public Employee GetById(long id)
        {
            return _dbContext.Set<Employee>().Find(id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbContext.Set<Employee>().ToList();
        }

        public void Add(Employee entity)
        {
            _dbContext.Set<Employee>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Employee entity)
        {
            _dbContext.Set<Employee>().Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Employee entity)
        {
            _dbContext.Set<Employee>().Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
