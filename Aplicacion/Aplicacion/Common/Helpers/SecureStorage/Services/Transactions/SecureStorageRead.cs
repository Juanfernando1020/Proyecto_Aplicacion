using Aplicacion.Common.Helpers.SecureStorage.Interfaces.Transactions;
using Aplicacion.Common.Result;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.SecureStorage.Services.Transactions
{
    internal class SecureStorageRead : ISecureStorageRead
    {
        public async Task<T> ReadAsync<T>(string key)
            => JsonConvert.DeserializeObject<T>(await ReadAsync(key));

        public async Task<string> ReadAsync(string key)
            => await GetAsync(key);

        private async Task<string> GetAsync(string key)
        {
            return await Xamarin.Essentials.SecureStorage.GetAsync(key);
        }
    }
}
