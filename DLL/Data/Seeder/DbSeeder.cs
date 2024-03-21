using Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Data.Seeder
{
    public static class DbSeeder
    {
        public static void SeedCountriesAndCities(ApplicationDbContext context)
        {
            if (context.Countries.Any())
            {
                return;
            }

			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string filePath = Path.Combine(basePath, "Data", "countries.json");


			var json = File.ReadAllText("countries.json");
            var data = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);

            foreach (var (countryName, cities) in data)
            {
                var country = new Country { Name = countryName };
                context.Countries.Add(country);

                foreach (var cityName in cities)
                {
                    var city = new City { Name = cityName, Country = country };
                    context.Cities.Add(city);
                }
            }

            context.SaveChanges();
        }
    }
}
