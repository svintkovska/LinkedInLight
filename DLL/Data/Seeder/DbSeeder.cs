using Bogus;
using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                context.Database.Migrate();
                var faker = new Faker();

                if (!context.Countries.Any())
                {
					string basePath = AppDomain.CurrentDomain.BaseDirectory;
					string filePath = Path.Combine(basePath, "Data", "Seeder", "countries.json");

					var json = File.ReadAllText(filePath);
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
