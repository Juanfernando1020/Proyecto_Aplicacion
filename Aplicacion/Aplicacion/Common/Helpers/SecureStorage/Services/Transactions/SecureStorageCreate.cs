using Aplicacion.Common.Helpers.SecureStorage.Interfaces.Transactions;
using Aplicacion.Common.Result;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.SecureStorage.Services.Transactions
{
    internal class SecureStorageCreate : ISecureStorageCreate
    {
        public async Task SaveAsync<T>(string key, T value)
            => await SaveAsync(key, value);

        public async Task SaveAsync(string key, object value)
            => await InsertAsync(key, JsonConvert.SerializeObject(value));

        private async Task InsertAsync(string key, string value)
        {
            await Xamarin.Essentials.SecureStorage.SetAsync(key, value);
        }
    }
}
