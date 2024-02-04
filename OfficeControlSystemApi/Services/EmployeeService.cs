using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Data.Repositorys;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _dbContext;
        private readonly Repository _employeeRepository;
        public EmployeeService(AppDbContext context)
        {
            _dbContext = context;
            _employeeRepository = new Repository(context);
        }
        public Employee AddEmployee(Employee employeeInput, int accessLevel)
        {
            if (employeeInput == null)
                throw new ArgumentException("Invalid input data");

            var newEmployee = new Employee
            {
                FirstName = employeeInput.FirstName,
                LastName = employeeInput.LastName,
            };

            var newAccessCard = new AccessCard
            {
                EmployeeId = newEmployee.Id,
                AccessLevel = (AccessLevel)accessLevel
            };

            var newVisitHistory = new VisitHistory
            {
                VisitDateTime = DateTimeOffset.UtcNow
            };

            _dbContext.Employees.Add(newEmployee);
            _dbContext.AccessCards.Add(newAccessCard);

            _dbContext.SaveChanges();

            newVisitHistory.AccessCardId = newAccessCard.Id;
            _dbContext.VisitHistories.Add(newVisitHistory);
            _dbContext.SaveChanges();

            return newEmployee;
        }
    }
}
