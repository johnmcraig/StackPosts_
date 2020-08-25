using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackPosts_.Client.Contracts;

namespace StackPosts_.Client.Services
{
    public class RepositoryService<T> : IRepositoryService<T> where T : class
    {
        private readonly HttpClient _client;
        private readonly ILogger<RepositoryService<T>> _logger;

        protected RepositoryService(HttpClient client, ILogger<RepositoryService<T>> logger)
        {
            _client = client;
            _logger = logger;
        }
        
        public async Task<IList<T>> GetAll(string url)
        {
            try
            {
                var response = await _client.GetFromJsonAsync<IList<T>>(url);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            } 
        }

        public async Task<T> GetSingle(string url, int id)
        {
            try
            {
                var response = await _client.GetFromJsonAsync<T>(url + id);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            } 
        }

        public async Task<T> Create(string url, T entity)
        {
            var response = await _client.PostAsJsonAsync<T>(url, entity);
            
            if (response.StatusCode == System.Net.HttpStatusCode.Created) 
                return entity;

            return null; 
        }

        public async Task<T> Update(string url, T entity, int id)
        {
            if (entity == null) 
                return null;

            var response = await _client.PutAsJsonAsync(url + id, entity);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent) 
                return entity;

            return null;
        }

        public async Task<bool> Delete(string url, int id)
        {
            if (id < 1) 
                return false;

            var response = await _client.DeleteAsync(url + id);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return true;

            return false;
        }
    }
}