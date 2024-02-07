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
        public Employee AddEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentException("Invalid input data");

            var newAccessCard = new AccessCard
            {
                AccessLevel = AccessLevel.Low,
                VisitHistories = new List<VisitHistory>(),
                Employee = employee
            };

            var newVisitHistory = new VisitHistory
            {
                VisitDateTime = DateTimeOffset.UtcNow
            };

            newAccessCard.VisitHistories.Add(newVisitHistory);

            _dbContext.Employees.Add(employee);
            _dbContext.AccessCards.Add(newAccessCard);
            _dbContext.VisitHistories.Add(newVisitHistory);
            _dbContext.SaveChanges();

            return employee;
        }

    }
}
