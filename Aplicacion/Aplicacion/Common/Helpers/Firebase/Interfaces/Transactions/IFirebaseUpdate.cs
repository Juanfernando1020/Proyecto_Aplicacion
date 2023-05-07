using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.Firebase.Interfaces.Transactions
{
    public interface IFirebaseUpdate
    {
        Task UpdateDataAsync<T>(T newObj);
    }
}
