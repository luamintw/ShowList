using System;
using System.Collections.Generic;
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
    public class VoteService : IVoteService
    {
        private readonly HttpClient _httpClient;
        private readonly ShowServiceConfig _showServiceConfig;

        public VoteService(HttpClient httpClient, IOptions<ShowServiceConfig> showServiceConfig)
        {
            _httpClient = httpClient;
            _showServiceConfig = showServiceConfig.Value;
        }

        public async Task VoteShow(VoteRequest voteRequest)
        {
            var requestToJson = new StringContent(JsonConvert.SerializeObject(voteRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(
                new Uri(_showServiceConfig.BaseAddress, $"{_showServiceConfig.Routes.Vote}/voteshow"), requestToJson);
            if (!response.IsSuccessStatusCode)
            {
                
            }
        }

        public async Task<IEnumerable<VoteResponse>> GetVoteResult()
        {
           var response = await _httpClient.GetStringAsync(new Uri(_showServiceConfig.BaseAddress, $"{_showServiceConfig.Routes.Vote}/result"));
           return JsonConvert.DeserializeObject<IEnumerable<VoteResponse>>(response);
        }
    }
}