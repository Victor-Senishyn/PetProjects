using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Data.Filters;
using OfficeControlSystemApi.Data.Repositorys;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Services
{
    public class AccessCardService : IAccessCardService
    {
        private readonly AccessCardRepository _accessCardRepository;

        public AccessCardService(AppDbContext context)
        {
            _accessCardRepository = new AccessCardRepository(context);
        }
    }
}
