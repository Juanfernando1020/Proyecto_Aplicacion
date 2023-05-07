using Aplicacion.Common.Helpers.Firebase.Interfaces;
using Aplicacion.Common.Helpers.Firebase.Interfaces.Transactions;
using Aplicacion.Common.Helpers.Firebase.Services.Transaction;
using Aplicacion.Common.Specifications;
using Firebase.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.Firebase.Services
{
    internal class FirebaseHelperService : IFirebaseHelperService
    {
        private readonly IFirebaseCreate _firebaseCreate;
        private readonly IFirebaseRead _firebaseRead;
        private readonly IFirebaseUpdate _firebaseUpdate;
        private readonly IFirebaseDelete _firebaseDelete;

        public FirebaseHelperService(FirebaseClient firebaseClient, string node)
        {
            _firebaseCreate = new FirebaseCreate(firebaseClient, node);
            _firebaseRead = new FirebaseRead(firebaseClient, node);
            _firebaseUpdate = new FirebaseUpdate(firebaseClient, node);
            _firebaseDelete = new FirebaseDelete(firebaseClient, node);
        }

        public async Task<bool> AnyBySpecificationAsync<T>(SpecificationBase<T> specification) where T : class
        {
            IEnumerable<T> list = await GetAllAsync<T>();

            return list.Any(specification.ToExpression().Compile());
        }

        public async Task<T> CreateDataAsync<T>(T entity)
        {
            return await _firebaseCreate.CreateDataAsync<T>(entity);
        }

        public async Task DeleteAsync(string key)
        {
            await _firebaseDelete.DeleteAsync(key);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await _firebaseRead.GetAllAsync<T>();
        }

        public async Task<IEnumerable<T>> GetAllBySpecificationAsync<T>(SpecificationBase<T> specification) where T : class
        {
            return await _firebaseRead.GetAllBySpecificationAsync(specification);
        }

        public async Task<IEnumerable<FirebaseObject<T>>> GetAllFirebaseAsync<T>()
        {
            return await _firebaseRead.GetAllFirebaseAsync<T>();
        }

        public async Task<T> GetByKeyAsync<T>(string key)
        {
            return await _firebaseRead.GetByKeyAsync<T>(key);
        }

        public async Task<T> GetBySpecificationAsync<T>(SpecificationBase<T> specification) where T : class
        {
            return await _firebaseRead.GetBySpecificationAsync(specification);
        }

        public async Task UpdateDataAsync<T>(T newObj)
        {
            await _firebaseUpdate.UpdateDataAsync(newObj);
        }
    }
}
