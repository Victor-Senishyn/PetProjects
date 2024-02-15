﻿using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Data.Filters;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Data.Repositorys;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Services
{
    public class VisitHistoryService : IVisitHistoryService, IScopedService
    {
        private readonly VisitHistoryRepository _visitHistoryRepository;
        public string ServiceUniqueIdentifier { get; } = Guid.NewGuid().ToString();

        public VisitHistoryService(AppDbContext context)
        {
            _visitHistoryRepository = new VisitHistoryRepository(context);
        }

        public async Task<VisitHistoryDto> CreateVisitHistoryAsync(AccessCardDto accessCard, CancellationToken cancellationToken = default)
        {
            if (accessCard == null)
                throw new ArgumentException($"AccessCard with id {accessCard} not found");

            var visitHistory = new VisitHistory
            {
                AccessCardId = accessCard.Id,
                VisitDateTime = DateTimeOffset.UtcNow
            };
            
            await _visitHistoryRepository.AddAsync(visitHistory);
            
            return new VisitHistoryDto()
            {
                Id = visitHistory.Id,
                AccessCardId = visitHistory.AccessCardId,
                VisitDateTime = visitHistory.VisitDateTime,
                ExitDateTime = visitHistory.ExitDateTime
            };
        }

        public async Task<VisitHistoryDto> UpdateExitDateTime(long visitHistoryId, CancellationToken cancellationToken = default)
        {
            var visitHistory = (await _visitHistoryRepository.GetAsync(new VisitHistoryFilter() { Id = visitHistoryId })).SingleOrDefault();

            if (visitHistory == null)
                throw new ArgumentException($"AccessHistory with id {visitHistoryId} not found");

            visitHistory.ExitDateTime = DateTimeOffset.UtcNow;

            _visitHistoryRepository.UpdateAsync(visitHistory);

            return new VisitHistoryDto()
            {
                Id = visitHistory.Id,
                AccessCardId = visitHistory.AccessCardId,
                VisitDateTime = visitHistory.VisitDateTime,
                ExitDateTime = visitHistory.ExitDateTime
            };
        }
    }
}
