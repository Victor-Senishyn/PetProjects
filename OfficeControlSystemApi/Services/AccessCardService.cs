using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Data;
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

        public async Task<AccessCardDto> CreateAccessCardDtoAsync(Employee employee)
        {
            var newAccessCard = new AccessCard
            {
                AccessLevel = AccessLevel.Low,
                VisitHistories = new List<VisitHistory>(),
                Employee = employee
            };

            await _accessCardRepository.AddAsync(newAccessCard);

            return new AccessCardDto(){
                Id = newAccessCard.Id,
                AccessLevel = AccessLevel.Low,
            };
        }

        public async Task<AccessCardDto> GetAccessCardByIdAsync(long id)
        {
            var accessCard = (await _accessCardRepository.Get(accessCard => accessCard.Id == id)).SingleOrDefault();

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
