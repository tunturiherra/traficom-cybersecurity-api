// Program.cs
using System;
using System.Threading.Tasks;
    class Program
    {
        static async Task Main(string[] args) 
        {
            // Kutsutaan datanhakijaa sekä sen käsittelijää
            AutoreporterService service = new AutoreporterService();
            AutoreporterResponse response = await service.FetchAutoreporterDataAsync();

            if (response != null && response.Value != null)
            {
                Console.WriteLine("Data fetched from Traficom Autoreporter API:");
                foreach (var item in response.Value)
                {
                    Console.WriteLine($"ID: {item.ID}, City: {item.City}, CountryCode: {item.CountryCode}\nMainCategory: {item.MainCategory}, SubCategory: {item.SubCategory}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Failed to retrieve data.");
            }
        }
    }