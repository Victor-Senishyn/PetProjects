using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Data.Repositorys;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Services
{
    public class AccessCardService : IAccessCardService
    {
        private readonly AppDbContext _dbContext;
        private readonly Repository _AccessCardRepository;

        public AccessCardService(AppDbContext context)
        {
            _dbContext = context;
            _AccessCardRepository = new Repository(context);
        }
    }
}
