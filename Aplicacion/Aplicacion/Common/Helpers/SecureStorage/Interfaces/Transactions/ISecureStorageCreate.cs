using Aplicacion.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.SecureStorage.Interfaces.Transactions
{
    public interface ISecureStorageCreate
    {
        Task SaveAsync<T>(string key, T value);
        Task SaveAsync(string key, object value);
    }
}
