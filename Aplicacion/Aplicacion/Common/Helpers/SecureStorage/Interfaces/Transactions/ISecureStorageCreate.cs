using Aplicacion.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.SecureStorage.Interfaces.Transactions
{
    public interface ISecureStorageCreate
    {
        Task SaveAsync(string key, Guid value);
        Task SaveAsync(string key, double value);
        Task SaveAsync(string key, decimal value);
        Task SaveAsync(string key, int value);
    }
}
