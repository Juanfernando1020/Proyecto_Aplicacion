using Aplicacion.Common.Helpers.SecureStorage.Interfaces;
using Aplicacion.Common.Helpers.SecureStorage.Interfaces.Transactions;
using Aplicacion.Common.Helpers.SecureStorage.Services.Transactions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Common.Helpers.SecureStorage.Service
{
    internal class SecureStorageService : ISecureStorageService
    {
        ISecureStorageCreate _createService;
        ISecureStorageRead _readService;
        ISecureStorageUpdate _updateService;
        ISecureStorageDelete _deleteService;
        public SecureStorageService()
        {
            _createService = new SecureStorageCreate();
            _readService = new SecureStorageRead();
            _updateService = new SecureStorageUpdate();
            _deleteService = new SecureStorageDelete();
        }

        public async Task DeleteAllAsync() => await _deleteService.DeleteAllAsync();

        public async Task DeleteAsync(string key) => await _deleteService.DeleteAsync(key);

        public async Task<T> ReadAsync<T>(string key) => await _readService.ReadAsync<T>(key);

        public async Task<string> ReadAsync(string key) => await _readService.ReadAsync(key);

        public async Task SaveAsync<T>(string key, T value) => await _createService.SaveAsync(key, value);

        public async Task SaveAsync(string key, object value) => await _createService.SaveAsync(key, value);

        public async Task UpdateAsync<T>(string key, T newValue) => await _updateService.UpdateAsync(key, newValue);

        public async Task UpdateAsync(string key, string newValue) => await _updateService.UpdateAsync(key, newValue);
    }
}
