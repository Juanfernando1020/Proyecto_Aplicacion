using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.Firebase.Interfaces.Transactions
{
    public interface IFirebaseDelete
    {
        Task DeleteAsync(string key);
    }
}
