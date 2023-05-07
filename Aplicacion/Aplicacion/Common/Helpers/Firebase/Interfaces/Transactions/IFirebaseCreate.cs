using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.Firebase.Interfaces.Transactions
{
    public interface IFirebaseCreate
    {
        Task<T> CreateDataAsync<T>(T entity);
    }
}
