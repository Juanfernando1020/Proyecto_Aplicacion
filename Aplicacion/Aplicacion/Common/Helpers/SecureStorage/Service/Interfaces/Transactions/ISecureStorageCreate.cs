using Aplicacion.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.SecureStorage.Service.Interfaces.Transactions
{
    public interface ISecureStorageCreate
    {
        Task<ResultBase> SaveAsync<T>(string key, T value);
        Task<ResultBase> SaveAsync(string key, object value);
    }
}
