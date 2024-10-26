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
                    // Tulostetaan näkyviin rajapinnasta merkittävimmät tiedot, eli ID, kohdekaupunki maakoodin kanssa,
                    // haittaohjelman pääkategoria ja sen alikategoria tarkentamaan haittaohjelmatyyppiä.
                    Console.WriteLine($"ID: {item.id}, City: {item.city}, {item.countryCode}\nMainCategory: {item.mainCategory}, SubCategory: {item.subCategory}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Failed to retrieve data.");
            }
        }
    }