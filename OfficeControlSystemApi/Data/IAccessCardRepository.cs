using OfficeControlSystemApi.Models;

namespace OfficeControlSystemApi.Data
{
    public interface IAccessCardRepository
    {
        AccessCard GetById(long id);
        IEnumerable<AccessCard> GetAll();
        void Add(AccessCard entity);
        void Update(AccessCard entity);
        void Delete(AccessCard entity);
    }
}
