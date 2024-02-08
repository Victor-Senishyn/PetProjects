using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Data.Repositorys;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Services
{
    public class AccessCardService : IAccessCardService
    {
        private readonly AppDbContext _dbContext;
        private readonly Repository _accessCardRepository;

        public AccessCardService(AppDbContext context)
        {
            _dbContext = context;
            _accessCardRepository = new Repository(context);
        }

        public AccessCard CreateNewAccessCard(Employee employee)
        {
            var newAccessCard = new AccessCard
            {
                AccessLevel = AccessLevel.Low,
                VisitHistories = new List<VisitHistory>(),
                Employee = employee
            };

            _dbContext.AccessCards.Add(newAccessCard);
            _dbContext.SaveChanges();

            return newAccessCard;
        }

        public void AddVisitHistory(AccessCard accessCard, VisitHistory visitHistory)
        {
            accessCard.VisitHistories.Add(visitHistory);
            _dbContext.AccessCards.Add(accessCard);
        }
    }
}
