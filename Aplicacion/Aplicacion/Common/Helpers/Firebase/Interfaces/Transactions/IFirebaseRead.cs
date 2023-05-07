using Aplicacion.Common.Specifications;
using Firebase.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.Firebase.Interfaces.Transactions
{
    public interface IFirebaseRead
    {
        Task<IEnumerable<FirebaseObject<T>>> GetAllFirebaseAsync<T>();
        Task<IEnumerable<T>> GetAllAsync<T>();
        Task<T> GetByKeyAsync<T>(string key);
        Task<T> GetBySpecificationAsync<T>(SpecificationBase<T> specification)
            where T : class;
        Task<IEnumerable<T>> GetAllBySpecificationAsync<T>(SpecificationBase<T> specification)
            where T : class;
    }
}