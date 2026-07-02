using Microsoft.Extensions.Configuration;
using MiniLms.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniLms.Services
{
    public class AiService : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            // API anahtarını appsettings.json dosyasından güvenle okur
            _apiKey = configuration["apikey"];
        }

        // 1. YAPAY ZEKA ÖZETLEME METODU
        public async Task<string> SummarizeTextAsync(string text)
        {
            if (string.IsNullOrEmpty(text)) return "Özetlenecek metin boş.";

            try
            {
                string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key={_apiKey}";

                var requestBody = new
                {
                    contents = new[]
                    {
                        new { parts = new[] { new { text = $"Lütfen aşağıdaki metni akademik ve anlaşılır bir dilde özetle:\n\n{text}" } } }
                    }
                };

                var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode) return "Yapay zeka servisi şu an yanıt vermiyor.";

                string jsonResponse = await response.Content.ReadAsStringAsync();
                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    return doc.RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();
                }
            }
            catch (Exception ex)
            {
                return $"Özet oluşturulurken teknik bir hata oluştu: {ex.Message}";
            }
        }

        // 2. YAPAY ZEKA QUIZ ÜRETME METODU
        public async Task<string> GenerateQuizAsync(string text, int questionCount = 5)
        {
            if (string.IsNullOrEmpty(text)) return "Quiz üretilecek metin boş.";

            try
            {
                string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key={_apiKey}";

                var requestBody = new
                {
                    contents = new[]
                    {
                        new { parts = new[] { new { text = $"Aşağıdaki metne dayanarak, cevapları net olan {questionCount} adet çoktan seçmeli soru hazırla:\n\n{text}" } } }
                    }
                };

                var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode) return "Quiz üretme servisi şu an yanıt vermiyor.";

                string jsonResponse = await response.Content.ReadAsStringAsync();
                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    return doc.RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();
                }
            }
            catch (Exception ex)
            {
                return $"Quiz oluşturulurken teknik bir hata oluştu: {ex.Message}";
            }
        }

        // 3. YAPAY ZEKA EMBEDDING (VEKTÖRLEŞTİRME) METODU
        public async Task<List<float>> GetEmbeddingAsync(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;

            try
            {
                // Kararlı v1 endpoint'i üzerinden text-embedding-004 model çağrısı
                string url = $"https://generativelanguage.googleapis.com/v1/models/text-embedding-004:embedContent?key={_apiKey}";

                var requestBody = new
                {
                    model = "models/text-embedding-004",
                    content = new { parts = new[] { new { text = text } } }
                };

                string jsonPayload = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                if (!response.IsSuccessStatusCode) return null;

                string jsonResponse = await response.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    var root = doc.RootElement;

                    // JSON hiyerarşisinde güvenli bir şekilde embedding değerlerini okuyoruz
                    if (root.TryGetProperty("embedding", out var embeddingProp) &&
                        embeddingProp.TryGetProperty("values", out var valuesProp))
                    {
                        var embeddingResult = new List<float>();
                        foreach (var val in valuesProp.EnumerateArray())
                        {
                            embeddingResult.Add(val.GetSingle());
                        }
                        return embeddingResult;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}