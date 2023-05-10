using Aplicacion.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.SecureStorage.Interfaces.Transactions
{
    public interface ISecureStorageDelete
    {
        Task DeleteAsync(string key);
        Task DeleteAllAsync();
    }
}
