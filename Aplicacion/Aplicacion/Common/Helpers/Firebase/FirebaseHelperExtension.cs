using Aplicacion.Common.Helpers.Firebase.Interfaces;
using Aplicacion.Common.Specifications;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.Firebase
{
    public static class FirebaseHelperExtensions
    {
        public static async Task<bool> AnyBySpecificationAsync<TEntity>(this IFirebaseHelperService service, SpecificationBase<TEntity> specification)
            where TEntity : class
        {
            return await service.AnyBySpecificationAsync(specification);
        }

        public static async Task DeleteAsync(this IFirebaseHelperService service, string key)
        {
            await service.DeleteAsync(key);
        }

        public static async Task<TEntity> GetByKeyAsync<TEntity>(this IFirebaseHelperService service, string key)
            where TEntity : class
        {
            return await service.GetByKeyAsync<TEntity>(key);
        }

        public static async Task<TEntity> GetBySpecificationAsync<TEntity>(this IFirebaseHelperService service, SpecificationBase<TEntity> specification)
            where TEntity : class
        {
            return await service.GetBySpecificationAsync(specification);
        }

        public static async Task<IEnumerable<TEntity>> GetAllBySpecificationAsync<TEntity>(this IFirebaseHelperService service, SpecificationBase<TEntity> specification)
            where TEntity : class
        {
            return await service.GetAllBySpecificationAsync(specification);
        }

        public static async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(this IFirebaseHelperService service)
            where TEntity : class
        {
            return await service.GetAllAsync<TEntity>();
        }

        public static async Task<IEnumerable<FirebaseObject<TEntity>>> GetAllFirebaseAsync<TEntity>(this IFirebaseHelperService service)
            where TEntity : class
        {
            return await service.GetAllFirebaseAsync<TEntity>();
        }
    }
}
