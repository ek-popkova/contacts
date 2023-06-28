using ContactsDapr.Models;


namespace ContactsDapr.Accessors
{
    public interface IContactService
    {
        public Task<List<Contact>?> GetAllAsync();
    }
}
