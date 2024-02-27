using OfficeControlSystemApi.Models.Identity;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface IUserService
    {
        Task CreateAdministratorUserAsync(UserCreationModel user, CancellationToken cancellationToken);
    }
}
