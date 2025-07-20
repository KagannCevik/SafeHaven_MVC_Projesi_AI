using Newtonsoft.Json.Linq;
using Project7DayAndNight.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Project7DayAndNight.Services
{
    public class LinkedinService
    {
        private readonly string apiUrl = "https://linkedin-data-scraper-api1.p.rapidapi.com/profile/detail?username={0}";
        private readonly string apiKey = "apikey";
        private readonly string apiHost = "linkedin-data-scraper-api1.p.rapidapi.com";

        public async Task<LınkedinViewModel> GetUserInfo(string username)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(string.Format(apiUrl, username)),
                };
                request.Headers.Add("x-rapidapi-key", apiKey);
                request.Headers.Add("x-rapidapi-host", apiHost);

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    try
                    {
                        var obj = JObject.Parse(body);
                        var basicInfo = obj["data"]?["basic_info"];

                        return new LınkedinViewModel
                        {
                            Username = username,
                            FollowersCount = (int)(basicInfo?["follower_count"] ?? 0),
                            PublicIdentifier = (string)(basicInfo?["public_identifier"] ?? username)
                        };
                    }
                    catch
                    {
                        return new LınkedinViewModel
                        {
                            Username = username,
                            FollowersCount = 0,
                            PublicIdentifier = username
                        };
                    }
                }
            }
        }
    }
}
