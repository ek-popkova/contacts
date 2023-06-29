using Accessor.Models;


namespace Accessor.Accessors
{
    public interface IContactService
    {
        public Task<List<Contact>?> GetAllAsync();
        public Task<Contact?> AddContactAsync(Contact contact);
    }
}
