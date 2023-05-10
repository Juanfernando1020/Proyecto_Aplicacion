using Aplicacion.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.SecureStorage.Service.Interfaces.Transactions
{
    public interface ISecureStorageRead
    {
        Task<ResultBase<T>> ReadAsync<T>(string key);
        Task<ResultBase<string>> ReadAsync(string key);
    }
}
