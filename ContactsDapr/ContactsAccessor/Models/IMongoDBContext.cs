using MongoDB.Driver;

namespace Accessor.Models
{
    public interface IMongoDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
