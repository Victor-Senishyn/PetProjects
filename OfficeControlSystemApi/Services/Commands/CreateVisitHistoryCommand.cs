using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Services.Interaces;
using OfficeControlSystemApi.Data.Filters;
using OfficeControlSystemApi.Models;

namespace OfficeControlSystemApi.Services.Commands
{
    public class CreateVisitHistoryCommand : Command
    {
        private readonly AppDbContext _dbContext;
        private readonly IAccessCardRepository _accessCardRepository;


        public CreateVisitHistoryCommand(
            AppDbContext dbContext,
            IAccessCardRepository accessCardRepository
            )
        {
            _dbContext = dbContext;
            _accessCardRepository = accessCardRepository;
        }

        public async Task<IActionResult> ExecuteAsync(long accessCardId, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var accessCard = (await _accessCardRepository.GetAsync(new AccessCardFilter() { Id = accessCardId })).SingleOrDefault();

                if (accessCard == null)
                    throw new ArgumentException($"Access Card by Id {accessCardId} not found");

                var visitHistory = new VisitHistory
                {
                    AccessCardId = accessCard.Id,
                    VisitDateTime = DateTimeOffset.UtcNow
                };

                accessCard.VisitHistories.Add( visitHistory );
                await _accessCardRepository.AddAsync( accessCard );
                await _accessCardRepository.CommitAsync();

                return new OkObjectResult(new VisitHistoryDto
                {
                    Id = visitHistory.Id,
                    AccessCardId = visitHistory.AccessCardId,
                    VisitDateTime = visitHistory.VisitDateTime,
                    ExitDateTime = visitHistory.ExitDateTime
                });
            }
            catch (ArgumentException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            catch (Exception ex) when (ex is OperationCanceledException or TaskCanceledException)
            {
                await transaction.RollbackAsync(cancellationToken);
                return new BadRequestObjectResult("Request canceled due to user action or timeout.");
            }
        }
    }
}
