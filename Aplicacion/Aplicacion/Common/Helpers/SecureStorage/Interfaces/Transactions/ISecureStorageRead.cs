using Aplicacion.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.SecureStorage.Interfaces.Transactions
{
    public interface ISecureStorageRead
    {
        Task<string> ReadAsync(string key);
    }
}
