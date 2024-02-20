﻿using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Services;
using OfficeControlSystemApi.Services.Commands;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Controllers
{
    public class VisitHistoryController : Controller
    {
        private readonly IVisitHistoryService _visitHistoryService;
        private readonly ICreateVisitHistoryCommand _createVisitHistoryCommand;
        private readonly AppDbContext _dbContext;

        public VisitHistoryController(
            IVisitHistoryService visitHistoryService,
            ICreateVisitHistoryCommand createVisitHistoryCommand,
            AppDbContext dbContext)
        {
            _visitHistoryService = visitHistoryService;
            _createVisitHistoryCommand = createVisitHistoryCommand;
            _dbContext = dbContext;
        }

        [HttpPatch("exit/{visitHistoryId}")]
        public async Task<IActionResult> UpdateExitDateTimeAsync(long visitHistoryId, CancellationToken cancellationToken)
        {
            try
            {
                var visitHistory = await _visitHistoryService.UpdateExitDateTime(visitHistoryId, cancellationToken);
                await _dbContext.SaveChangesAsync();

                return Ok(visitHistory);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) when (ex is OperationCanceledException or TaskCanceledException)
            {
                return BadRequest("Request canceled due to user action or timeout.");
            }
        }

        [HttpPost("visit/{accessCardId}")]
        public async Task<IActionResult> AddVisitHistory(long accessCardId, CancellationToken cancellationToken)
        {
            return await _createVisitHistoryCommand.ExecuteAsync(accessCardId, cancellationToken);
        }
    }
}
