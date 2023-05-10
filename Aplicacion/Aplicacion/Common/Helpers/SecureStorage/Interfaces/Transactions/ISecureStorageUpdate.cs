using Aplicacion.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.SecureStorage.Interfaces.Transactions
{
    public interface ISecureStorageUpdate
    {
        Task UpdateAsync<T>(string key, T newValue);
        Task UpdateAsync(string key, string newValue);
    }
}
