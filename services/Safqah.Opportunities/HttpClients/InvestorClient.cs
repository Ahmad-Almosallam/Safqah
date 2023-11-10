using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Safqah.Shared.Settings;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Safqah.Opportunities.HttpClients
{
    public class InvestorClient : IInvestorClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<InvestorClient> _logger;

        public InvestorClient(HttpClient httpClient,
                              IHttpContextAccessor httpContextAccessor,
                              IOptions<HttpClientsSettings> httpClientsSettings,
                              ILogger<InvestorClient> logger)
        {
            _httpClient = httpClient;
            string token = httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer", string.Empty));
            _httpClient.BaseAddress = new Uri(httpClientsSettings.Value.InvestorUrl);
            _logger = logger;
        }

        public async Task<decimal> GetBalance(string investorId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Investor/{investorId}/balance");
                var contentResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<decimal>(contentResponse);
                }

                _logger.LogError($"Status Code {response.StatusCode}, content {contentResponse}");
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected exception {ex}", ex);
                throw;
            }
        }
    }
}
