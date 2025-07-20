using Newtonsoft.Json;
using Project7DayAndNight.Models;
using Project7DayAndNight.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project7DayAndNight.Controllers
{
    public class GenerateFaqController : Controller
    {

        private readonly string apiKey = "apikey";
        private readonly string apiUrl = "https://chatgpt-42.p.rapidapi.com/aitohuman";

        public ActionResult Index()
        {
            return View(); // input için form
        }

        [HttpPost]
        public async Task<ActionResult> Index(string prompt)
        {
            string fullPrompt = $"{prompt} Buna göre 3 adet sık sorulan soru üret, sadece soruları sırayla ver.";

            string responseText = await GenerateFromApi(fullPrompt);
            var questions = ExtractQuestions(responseText);
            SaveQuestionsToDb(questions);

            ViewBag.Questions = questions;
            ViewBag.RawText = responseText;
            return View();
        }

        private async Task<string> GenerateFromApi(string prompt)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(apiUrl),
                        Headers =
                {
                    { "x-rapidapi-key", apiKey },
                    { "x-rapidapi-host", "chatgpt-42.p.rapidapi.com" },
                },
                        Content = new StringContent(JsonConvert.SerializeObject(new { text = prompt }))
                        {
                            Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                        }
                    };

                    var response = await client.SendAsync(request);
                    var body = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        return $"API Hatası: {response.StatusCode}\n{body}";
                    }

                    return body;
                }
            }
            catch (Exception ex)
            {
                return $"İstisna oluştu: {ex.Message}";
            }
        }

       
        public List<string> ExtractQuestions(string text)
        {
            
            if (text.Trim().StartsWith("{"))
            {
                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(text);
                text = obj.result[0];
            }

            
            return text
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(q => q.Trim())
                .Where(q => q.Length > 10 && !q.ToLower().Contains("hey")) // gereksiz satırları at
                .Select(q => System.Text.RegularExpressions.Regex.Replace(q, @"^(n\d+\.|\d+\.)\s*", "")) // baştaki n2., 2. gibi numaraları sil
                .ToList();
        }
        private void SaveQuestionsToDb(List<string> questions)
        {
            using (var db = new DayNightDbEntities())
            {
                foreach (var question in questions)
                {
                    db.SSS.Add(new SSS { Question = question });
                }

                db.SaveChanges();
            }
        }
    }
}