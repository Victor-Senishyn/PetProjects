﻿using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Services;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Controllers
{
    public class VisitHistoryController : Controller
    {
        private readonly IAccessCardService _accessCardService;
        private readonly IVisitHistoryService _visitHistoryService;
        private readonly IScopedService _scoped;

        public VisitHistoryController(
            IAccessCardService accessCardService,
            IVisitHistoryService visitHistoryService,
            IScopedService scoped)
        {
            _accessCardService = accessCardService;
            _visitHistoryService = visitHistoryService;
            _scoped = scoped;
        }

        [HttpPut("exit/{visitHistoryId}")]
        public async Task<IActionResult> UpdateExitDateTimeAsync(long visitHistoryId, CancellationToken cancellationToken)
        {
            try
            {
                var newVisitHistory = await _visitHistoryService.UpdateExitDateTime(visitHistoryId, cancellationToken);
                return Ok(newVisitHistory);
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
            try
            {
                var newAccessCard = await _accessCardService.GetAccessCardByIdAsync(accessCardId, cancellationToken);
                var newVisitHistory = await _visitHistoryService.CreateVisitHistoryAsync(newAccessCard, cancellationToken);

                return Ok(newVisitHistory);
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
    }
}
