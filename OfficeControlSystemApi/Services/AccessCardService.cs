using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Data.Filters;
using OfficeControlSystemApi.Data.Repositorys;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Services
{
    public class AccessCardService : IAccessCardService, IScopedService
    {
        private readonly AccessCardRepository _accessCardRepository;
        public string ServiceUniqueIdentifier { get; } = Guid.NewGuid().ToString();

        public AccessCardService(AppDbContext context)
        {
            _accessCardRepository = new AccessCardRepository(context);
        }

        public async Task<AccessCardDto> CreateAccessCardAsync(EmployeeDto employee, CancellationToken cancellationToken = default)
        {
            var newAccessCard = new AccessCard
            {
                AccessLevel = AccessLevel.Low,
                EmployeeId = employee.Id
            };

            await _accessCardRepository.AddAsync(newAccessCard);

            return new AccessCardDto(){
                AccessLevel = AccessLevel.Low,
                Id = newAccessCard.Id
            };
        }

        public async Task<AccessCardDto> GetAccessCardByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var accessCard = (await _accessCardRepository.GetAsync(new AccessCardFilter() { Id = id} )).SingleOrDefault();

            if (accessCard == null)
                throw new ArgumentException($"Access Card by Id {id} not found");

            return new AccessCardDto()
            {
                Id = accessCard.Id,
                AccessLevel = accessCard.AccessLevel
            };
        }
    }
}
