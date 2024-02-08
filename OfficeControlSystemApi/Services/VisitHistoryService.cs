using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Data.Repositorys;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Services
{
    public class VisitHistoryService : IVisitHistoryService
    {
        private readonly AppDbContext _dbContext;
        private readonly VisitHistoryRepository _visitHistoryRepository;

        public VisitHistoryService(AppDbContext context)
        {
            _dbContext = context;
            _visitHistoryRepository = new VisitHistoryRepository(context);
        }

        public async Task<VisitHistory> CreateVisitHistoryAsync(long accessCardId)
        {
            var accessCard = _dbContext.AccessCards.FirstOrDefault(e => e.Id == accessCardId);

            if (accessCard == null)
                throw new ArgumentException($"AccessCard with id {accessCardId} not found");

            var newVisitHistory = new VisitHistory
            {
                AccessCardId = accessCard.Id,
                VisitDateTime = DateTimeOffset.UtcNow
            };

            await _visitHistoryRepository.AddAsync(newVisitHistory);
            return newVisitHistory;
        }

        public VisitHistory UpdateExitDateTime(long visitHistoryId)
        {
            var visitHistory = _dbContext.VisitHistories.FirstOrDefault(ah => ah.Id == visitHistoryId);

            if (visitHistory == null)
                throw new ArgumentException($"AccessHistory with id {visitHistoryId} not found");

            visitHistory.ExitDateTime = DateTimeOffset.UtcNow;
            _dbContext.SaveChanges();

            return visitHistory;
        }
    }
}
