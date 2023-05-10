using Aplicacion.Common.Helpers.SecureStorage.Interfaces.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Common.Helpers.SecureStorage.Interfaces
{
    public interface ISecureStorageService : ISecureStorageCreate, ISecureStorageRead, ISecureStorageUpdate, ISecureStorageDelete
    {
    }
}
