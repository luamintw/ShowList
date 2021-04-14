using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ShowList.Api.Configuration;
using ShowList.Api.Model;
using ShowList.Api.Services.Interfaces;

namespace ShowList.Api.Services
{
    public class ShowService : IShowService
    {
        private readonly HttpClient _httpClient;
        private readonly ShowServiceConfig _showServiceConfig;

        public ShowService(HttpClient httpClient, IOptions<ShowServiceConfig> showServiceConfig)
        {
            _httpClient = httpClient;
            _showServiceConfig = showServiceConfig.Value;
        }

        public async Task<IEnumerable<ShowResponse>> GetShowList()
        {
            var responseString = await _httpClient.GetStringAsync(new Uri(_showServiceConfig.BaseAddress,
                $"{_showServiceConfig.Routes.Show}/list"));
            return JsonConvert.DeserializeObject<IEnumerable<ShowResponse>>(responseString);
        }

        public async Task AddShow(ShowRequest showRequest)
        {
            var showToJson = new StringContent(JsonConvert.SerializeObject(showRequest), Encoding.UTF8,
                "application/json"); 
            var response = await _httpClient.PostAsync(new Uri(_showServiceConfig.BaseAddress,
                $"{_showServiceConfig.Routes.Show}/add"), showToJson );
            if (!response.IsSuccessStatusCode)
            {
                //throw exception
            }
        }

        public async Task EditShow(ShowRequest showRequest)
        { 
            var showToJson = new StringContent(JsonConvert.SerializeObject(showRequest), Encoding.UTF8,
                "application/json"); 
            var response = await _httpClient.PostAsync(new Uri(_showServiceConfig.BaseAddress,
                $"{_showServiceConfig.Routes.Show}/edit"), showToJson );
            if (!response.IsSuccessStatusCode)
            {
                //throw exception
            }
        }

        public async Task DeleteShow(int id)
        {
            var response = await _httpClient.DeleteAsync(new Uri(_showServiceConfig.BaseAddress,
                $"{_showServiceConfig.Routes.Show}/delete?id={id}" ) );
            if (!response.IsSuccessStatusCode)
            {
                //throw exception
            }
        }
    }
}