using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using NuGet.Protocol;

namespace Marvel.Services.Marvel.Character
{
    public class Character
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string baseUrl = "http://gateway.marvel.com/v1/public";

        public Character()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            _httpClient = new HttpClient();


            _configuration = _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public async Task<string> Get(int? id = null, int? offset = null, int? limit = null)
        {
            string PUBLIC_KEY = _configuration["Marvel:API_KEY:PUBLIC"] ?? "";
            string PRIVATE_KEY = _configuration["Marvel:API_KEY:PRIVATE"] ?? "";
            string timestamp = DateTime.Now.Ticks.ToString();
            string hash = Marvel.Utility.Hash.CreateHash(PUBLIC_KEY, PRIVATE_KEY, timestamp);
            string url = $"{baseUrl}/characters?apikey={PUBLIC_KEY}&hash={hash}&ts={timestamp}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {                        
                    string result = await response.Content.ReadAsStringAsync();
                    Models.Marvel.APIReturn? aPIReturn = System.Text.Json.JsonSerializer.Deserialize<Models.Marvel.APIReturn>(result);
                    Console.WriteLine(aPIReturn.data);
                    return result;
                    
                }
                else
                {
                    return "Error";
                }
            }
            catch (HttpRequestException ex)
            {
                string response = ex.ToString();


                return $"Error\n---\n{url}\n---\n{response}";
            }
            finally
            {
                _httpClient.Dispose();
            }
        }

    }
}
