using Aplicacion.Common.Helpers.SecureStorage.Interfaces.Transactions;
using System;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.SecureStorage.Services.Transactions
{
    internal class SecureStorageDelete : ISecureStorageDelete
    {
        public async Task DeleteAllAsync()
            => await RemoveAllAsync();

        public async Task DeleteAsync(string key)
            => await RemoveAsync(key);

        private async Task RemoveAsync(string key)
        {
            await Task.Run(() => Xamarin.Essentials.SecureStorage.Remove(key));
        }
        private async Task RemoveAllAsync()
        {
            await Task.Run(() => Xamarin.Essentials.SecureStorage.RemoveAll());
        }
    }
}
