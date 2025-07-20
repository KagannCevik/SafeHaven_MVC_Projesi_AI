using Newtonsoft.Json;
using Project7DayAndNight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project7DayAndNight.Controllers
{
    public class ImageController : Controller
    {
        private readonly string _apiKey = "apikey"; // Buraya kendi Hugging Face API anahtarını gir

        [HttpGet]
        public ActionResult Generate()
        {
            return View(new PromtViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Generate(PromtViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Prompt))
            {
                ModelState.AddModelError("", "Lütfen bir prompt giriniz.");
                return View(model);
            }

            model.ImageBase64 = await GenerateImageFromPrompt(model.Prompt);
            return View(model);
        }

        private async Task<string> GenerateImageFromPrompt(string prompt)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                var data = new { inputs = prompt };
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api-inference.huggingface.co/models/black-forest-labs/FLUX.1-dev", content);
                var responseBytes = await response.Content.ReadAsByteArrayAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var errorMsg = Encoding.UTF8.GetString(responseBytes);
                    throw new Exception("API başarısız: " + errorMsg);
                }

                // Eğer response JSON ise parse et, değilse doğrudan base64 döndür
                try
                {
                    var responseString = Encoding.UTF8.GetString(responseBytes);
                    dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
                    string base64Image = json[0]?.generated_image ?? json.generated_image ?? null;
                    if (!string.IsNullOrEmpty(base64Image))
                        return base64Image;
                }
                catch
                {
                    // Eğer JSON parse edilemiyorsa, doğrudan base64 döndür
                    return Convert.ToBase64String(responseBytes);
                }

                throw new Exception("Beklenmeyen API cevabı.");
            }
        }
    }
}