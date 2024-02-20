using Microsoft.AspNetCore.Mvc;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface ICreateVisitHistoryCommand
    {
        Task<IActionResult> ExecuteAsync(long accessCardId, CancellationToken cancellationToken);
    }
}
