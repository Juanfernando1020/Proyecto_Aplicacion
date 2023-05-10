using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.SecureStorage.Service
{
    internal class SecureStorageService
    {
        #region Save
        public async Task SaveAsync<T>(string key, T value)
            => await SaveAsync(key, value);
        public async Task SaveAsync(string key, object value)
            => await InsertAsync(key, JsonConvert.SerializeObject(value));
        private async Task InsertAsync(string key, string value)
        {
            await Xamarin.Essentials.SecureStorage.SetAsync(key, value);
        }
        #endregion
        
        #region Get
        public async Task<T> ReadAsync<T>(string key)
            => JsonConvert.DeserializeObject<T>(await ReadAsync(key));
        public async Task<string> ReadAsync(string key)
            => await GetAsync(key);
        private async Task<string> GetAsync(string key)
        {
            return await Xamarin.Essentials.SecureStorage.GetAsync(key);
        }
        #endregion
    }
}
