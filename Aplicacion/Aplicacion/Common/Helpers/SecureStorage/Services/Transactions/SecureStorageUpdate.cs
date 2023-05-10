using Aplicacion.Common.Helpers.SecureStorage.Interfaces.Transactions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.SecureStorage.Services.Transactions
{
    internal class SecureStorageUpdate : ISecureStorageUpdate
    {
        public async Task UpdateAsync<T>(string key, T newValue)
            => await UpdateAsync(key, JsonConvert.SerializeObject(newValue));

        public async Task UpdateAsync(string key, string newValue)
            => await ReplaceAsync(key, newValue);

        private async Task ReplaceAsync(string key, string newValue)
        {
            Xamarin.Essentials.SecureStorage.Remove(key);
           await Xamarin.Essentials.SecureStorage.SetAsync(key, newValue);
        }
    }
}
