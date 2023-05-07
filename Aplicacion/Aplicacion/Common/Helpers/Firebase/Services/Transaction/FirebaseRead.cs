using Aplicacion.Common.Helpers.Firebase.Interfaces.Transactions;
using Aplicacion.Common.Specifications;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.Firebase.Services.Transaction
{
    internal class FirebaseRead : IFirebaseRead
    {
        private readonly FirebaseClient _firebaseClient;
        private readonly string _node;

        public FirebaseRead(FirebaseClient firebaseClient, string node)
        {
            _firebaseClient = firebaseClient;
            _node = node;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            IEnumerable<FirebaseObject<T>> data = await GetAllFirebaseAsync<T>();
            List<T> list = new List<T>();

            foreach (FirebaseObject<T> item in data)
            {
                list.Add(item.Object);
            }

            return list;
        }

        public async Task<IEnumerable<T>> GetAllBySpecificationAsync<T>(SpecificationBase<T> specification) where T : class
        {
            IEnumerable<T> list = await GetAllAsync<T>();

            return list.Where(specification.ToExpression().Compile()).ToList();
        }

        public async Task<IEnumerable<FirebaseObject<T>>> GetAllFirebaseAsync<T>()
        {
            IReadOnlyCollection<FirebaseObject<T>> list = await _firebaseClient
            .Child(_node)
            .OnceAsync<T>();

            return list.ToList();
        }

        public async Task<T> GetByKeyAsync<T>(string key)
        {
            return await _firebaseClient
            .Child(_node)
            .Child(key)
            .OnceSingleAsync<T>();
        }

        public async Task<T> GetBySpecificationAsync<T>(SpecificationBase<T> specification) where T : class
        {
            IEnumerable<T> list = await GetAllAsync<T>();

            return list.FirstOrDefault(specification.ToExpression().Compile());
        }
    }
}
