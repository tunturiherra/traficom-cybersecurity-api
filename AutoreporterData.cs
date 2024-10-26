using System.Text.Json;
using System.Text.Json.Serialization;

    // Tässä luokasta määritetään, mitä haetaan rajapinnasta
    public class AutoreporterData
    {
        [JsonPropertyName("ID")]
        public int id { get; set; }

        [JsonPropertyName("MainCategory")]
        public string mainCategory { get; set; }

        [JsonPropertyName("SubCategory")]
        public string subCategory { get; set; }

        [JsonPropertyName("CountryCode")]
        public string countryCode { get; set; }

        [JsonPropertyName("City")]
        public string city { get; set; }
    }

// Autoreporterin rajapinnasta saapuva vastaus, kun lähetetään pyyntöä
public class AutoreporterResponse
{
    [JsonPropertyName("@odata.context")]
    public string OdataContext { get; set; }
    
    [JsonPropertyName("value")]
    public List<AutoreporterData> Value { get; set; }
}

// Vastaa Autoreporterin tietojen hakemisesta rajapinnasta. Sisältää itse osoitteen, HTTP-pyynnöt ja
// vastaukset rajapinnasta. Täältä myös data tuodaan käytettäväksi.
public class AutoreporterService
{
    private static readonly HttpClient client = new HttpClient();
    
    // Rajapinnan osoite tässä
    private const string ApiUrl = "https://opendata.traficom.fi/api/v13/Autoreporter";

    // Asynkroninen metodi, joka hakee Autoreporterin tiedot rajapinnasta.
    public async Task<AutoreporterResponse> FetchAutoreporterDataAsync()
    {
        try
        {
            // Tehdään HTTP GET -pyyntö API:iin.
            // Luetaan vastaus ja deserialisoidaan se objektiksi AutoreporterResponse.
            HttpResponseMessage response = await client.GetAsync(ApiUrl);
            
            response.EnsureSuccessStatusCode();
            
            string responseBody = await response.Content.ReadAsStringAsync();
            AutoreporterResponse data = JsonSerializer.Deserialize<AutoreporterResponse>(responseBody);
            
            // Palautetaan haettu data.
            return data;
        }
        // Käsitellään mahdolliset HTTP-pyynnön virheet.
        catch (HttpRequestException e)
        {
            Console.WriteLine($"HTTP request failed: {e.Message}");
        }
        catch (JsonException e)
        {
            Console.WriteLine($"JSON parsing failed: {e.Message}");
        }
        // Palautetaan null, jos tietojen hakeminen jostain syystä epäonnistuu.
        return null;
    }
}
