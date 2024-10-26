// AutoreporterData.cs
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


    public class AutoreporterData
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }

        [JsonPropertyName("MainCategory")]
        public string MainCategory { get; set; }

        [JsonPropertyName("SubCategory")]
        public string SubCategory { get; set; }

        [JsonPropertyName("CountryCode")]
        public string CountryCode { get; set; }

        [JsonPropertyName("City")]
        public string City { get; set; }
    }

    public class AutoreporterResponse
    {
        [JsonPropertyName("@odata.context")]
        public string OdataContext { get; set; }

        [JsonPropertyName("value")]
        public List<AutoreporterData> Value { get; set; }
    }

    public class AutoreporterService
    {
        private static readonly HttpClient client = new HttpClient();
        private const string ApiUrl = "https://opendata.traficom.fi/api/v13/Autoreporter";

        public async Task<AutoreporterResponse> FetchAutoreporterDataAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(ApiUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                AutoreporterResponse data = JsonSerializer.Deserialize<AutoreporterResponse>(responseBody);
                return data;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"HTTP request failed: {e.Message}");
            }
            catch (JsonException e)
            {
                Console.WriteLine($"JSON parsing failed: {e.Message}");
            }
            return null;
        }
    }
