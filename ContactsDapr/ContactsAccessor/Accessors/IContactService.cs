using Accessor.Models;


namespace Accessor.Accessors
{
    public interface IContactService
    {
        public Task<List<Contact>?> GetAllAsync();
    }
}
