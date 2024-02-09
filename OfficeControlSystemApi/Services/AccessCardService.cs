using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Data.Repositorys;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Services
{
    public class AccessCardService : IAccessCardService
    {
        private readonly AppDbContext _dbContext;
        private readonly AccessCardRepository _accessCardRepository;

        public AccessCardService(AppDbContext context)
        {
            _dbContext = context;
            _accessCardRepository = new AccessCardRepository(context);
        }

        public async Task<AccessCard> CreateNewAccessCardAsync(Employee employee)
        {
            var newAccessCard = new AccessCard
            {
                AccessLevel = AccessLevel.Low,
                VisitHistories = new List<VisitHistory>(),
                Employee = employee
            };

            await _accessCardRepository.AddAsync(newAccessCard);

            return newAccessCard;
        }

        public async Task<AccessCard> GetAccessCardById(long id)//Update in Interface
        {
            return await _accessCardRepository.GetByIdAsync(id);
        }

        public void AddVisitHistory(AccessCard accessCard, VisitHistory visitHistory)
        {
            accessCard.VisitHistories.Add(visitHistory);
            _dbContext.AccessCards.Add(accessCard);
            //TODO maybe i should use AddAsync
        }
    }
}
