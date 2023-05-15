using Aplicacion.Common.Helpers.SecureStorage.Interfaces.Transactions;
using Aplicacion.Common.Result;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.SecureStorage.Services.Transactions
{
    internal class SecureStorageCreate : ISecureStorageCreate
    {
        public async Task SaveAsync(string key, Guid value) => await InsertAsync(key, value.ToString());

        public async Task SaveAsync(string key, double value) => await InsertAsync(key, value.ToString());

        public async Task SaveAsync(string key, decimal value) => await InsertAsync(key, value.ToString());

        public async Task SaveAsync(string key, int value) => await InsertAsync(key, value.ToString());

        private async Task InsertAsync(string key, string value)
        {
            await Xamarin.Essentials.SecureStorage.SetAsync(key, value);
        }
    }
}
