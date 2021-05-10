using BackOffice.Client.Models;
using BackOffice.Client.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BackOffice.Client.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        private string _accessToken;

        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public Product Create(ProductInputDto model)
        {
            HttpRequestMessage request = new(HttpMethod.Post, "api/products/");
            var modelAsJson = JsonConvert.SerializeObject(model);
            request.Content = new StringContent(modelAsJson, Encoding.UTF8, "application/json");

            //  _client.SetBearerToken(GetToken());
            var response = _client.Send(request);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = response.Content;
            string responseString = responseContent.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Product>(responseString);
        }

        public bool Delete(Guid id)
        {
            //    _client.SetBearerToken(GetToken());
            var response = _client.Send(new HttpRequestMessage(HttpMethod.Delete, "api/products/" + id));
            return response.IsSuccessStatusCode;
        }

        public IEnumerable<Product> Get()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/products/");

            //     _client.SetBearerToken(GetToken());
            var response = _client.Send(request);

            if (!response.IsSuccessStatusCode)
                return new List<Product>();

            var responseContent = response.Content;
            string responseString = responseContent.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<IEnumerable<Product>>(responseString);
        }

        public Product GetById(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/products/" + id);
            //  _client.SetBearerToken(GetToken());
            var response = _client.Send(request);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = response.Content;
            string responseString = responseContent.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Product>(responseString);
        }

        public bool Update(Guid id, ProductInputDto model)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "api/products/" + id);
            var modelAsJson = JsonConvert.SerializeObject(model);
            request.Content = new StringContent(modelAsJson, Encoding.UTF8, "application/json");

            //   _client.SetBearerToken(GetToken());

            var response = _client.Send(request);
            return response.IsSuccessStatusCode;
        }
    }
}
