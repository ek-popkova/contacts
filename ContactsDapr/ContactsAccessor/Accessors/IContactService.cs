using Accessor.Models;


namespace Accessor.Accessors
{
    public interface IContactService
    {
        public Task<List<Contact>?> GetAllAsync();
        public Task<Contact?> AddContactAsync(Contact contact);
        public Task<List<Contact>?> GetContactByPhoneAsync(string phone);
        public Task<long> DeleteContactByPhoneAsync(string phone);
    }
}
