using BackOffice.Client.Models;
using BackOffice.Client.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BackOffice.Client.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _client;
        private string _accessToken;

        public CategoryService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _accessToken = string.Empty;
        }

        public Category Create(CategoryInputDto model)
        {
            HttpRequestMessage request = new(HttpMethod.Post, "api/categories/");
            var bookAsJson = JsonConvert.SerializeObject(model);
            request.Content = new StringContent(bookAsJson, Encoding.UTF8, "application/json");

          //  _client.SetBearerToken(GetToken());
            var response = _client.Send(request);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = response.Content;
            string responseString = responseContent.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Category>(responseString);
        }

        public bool Delete(Guid id)
        {
        //    _client.SetBearerToken(GetToken());
            var response = _client.Send(new HttpRequestMessage(HttpMethod.Delete, "api/categories/" + id));
            return response.IsSuccessStatusCode;
        }

        public IEnumerable<Category> Get()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/categories/");

       //     _client.SetBearerToken(GetToken());
            var response = _client.Send(request);

            if (!response.IsSuccessStatusCode)
                return new List<Category>();

            var responseContent = response.Content;
            string responseString = responseContent.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<IEnumerable<Category>>(responseString);
        }

        public Category GetById(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/categories/" + id);
          //  _client.SetBearerToken(GetToken());
            var response = _client.Send(request);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = response.Content;
            string responseString = responseContent.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Category>(responseString);
        }

        public bool Update(Guid id, CategoryInputDto model)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "api/categories/" + id);
            var bookAsJson = JsonConvert.SerializeObject(model);
            request.Content = new StringContent(bookAsJson, Encoding.UTF8, "application/json");

         //   _client.SetBearerToken(GetToken());

            var response = _client.Send(request);
            return response.IsSuccessStatusCode;
        }
    }
}
