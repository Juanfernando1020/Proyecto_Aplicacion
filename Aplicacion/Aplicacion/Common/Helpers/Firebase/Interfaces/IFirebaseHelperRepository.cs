using Aplicacion.Common.Helpers.Firebase.Interfaces.Transactions;
using Aplicacion.Common.Specifications;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.Firebase.Interfaces
{
    public interface IFirebaseHelperRepository : IFirebaseCreate, IFirebaseRead, IFirebaseUpdate, IFirebaseDelete
    {
        Task<bool> AnyBySpecificationAsync<T>(SpecificationBase<T> specification)
            where T : class;
    }
}
