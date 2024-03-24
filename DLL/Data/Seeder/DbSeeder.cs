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

				if (!context.Industries.Any())
				{
					var industries = new List<string>
					{
						"Accounting",
						"Airlines/Aviation",
						"Alternative Dispute Resolution",
						"Alternative Medicine",
						"Animation",
						"Apparel & Fashion",
						"Architecture & Planning",
						"Arts & Crafts",
						"Automotive",
						"Aviation & Aerospace",
						"Banking",
						"Biotechnology",
						"Broadcast Media",
						"Building Materials",
						"Business Supplies & Equipment",
						"Capital Markets",
						"Chemicals",
						"Civic & Social Organization",
						"Civil Engineering",
						"Commercial Real Estate",
						"Computer & Network Security",
						"Computer Games",
						"Computer Hardware",
						"Computer Networking",
						"Computer Software",
						"Construction",
						"Consumer Electronics",
						"Consumer Goods",
						"Consumer Services",
						"Cosmetics",
						"Dairy",
						"Defense & Space",
						"Design",
						"Education Management",
						"E-learning",
						"Electrical & Electronic Manufacturing",
						"Entertainment",
						"Environmental Services",
						"Events Services",
						"Executive Office",
						"Facilities Services",
						"Farming",
						"Financial Services",
						"Fine Art",
						"Fishery",
						"Food & Beverages",
						"Food Production",
						"Fundraising",
						"Furniture",
						"Gambling & Casinos",
						"Glass, Ceramics & Concrete",
						"Government Administration",
						"Government Relations",
						"Graphic Design",
						"Health, Wellness & Fitness",
						"Higher Education",
						"Hospital & Health Care",
						"Hospitality",
						"Human Resources",
						"Import & Export",
						"Individual & Family Services",
						"Industrial Automation",
						"Information Services",
						"Information Technology & Services",
						"Insurance",
						"International Affairs",
						"International Trade & Development",
						"Internet",
						"Investment Banking",
						"Investment Management",
						"Judiciary",
						"Law Enforcement",
						"Law Practice",
						"Legal Services",
						"Legislative Office",
						"Leisure & Travel",
						"Libraries",
						"Logistics & Supply Chain",
						"Luxury Goods & Jewelry",
						"Machinery",
						"Management Consulting",
						"Maritime",
						"Market Research",
						"Marketing & Advertising",
						"Mechanical Or Industrial Engineering",
						"Media Production",
						"Medical Device",
						"Medical Practice",
						"Mental Health Care",
						"Military",
						"Mining & Metals",
						"Motion Pictures & Film",
						"Museums & Institutions",
						"Music",
						"Nanotechnology",
						"Newspapers",
						"Nonprofit Organization Management",
						"Oil & Energy",
						"Online Media",
						"Outsourcing/Offshoring",
						"Package/Freight Delivery",
						"Packaging & Containers",
						"Paper & Forest Products",
						"Performing Arts",
						"Pharmaceuticals",
						"Philanthropy",
						"Photography",
						"Plastics",
						"Political Organization",
						"Primary/Secondary Education",
						"Printing",
						"Professional Training & Coaching",
						"Program Development",
						"Public Policy",
						"Public Relations & Communications",
						"Public Safety",
						"Publishing",
						"Railroad Manufacture",
						"Ranching",
						"Real Estate",
						"Recreational Facilities & Services",
						"Religious Institutions",
						"Renewables & Environment",
						"Research",
						"Restaurants",
						"Retail",
						"Security & Investigations",
						"Semiconductors",
						"Shipbuilding",
						"Sporting Goods",
						"Sports",
						"Staffing & Recruiting",
						"Supermarkets",
						"Telecommunications",
						"Textiles",
						"Think Tanks",
						"Tobacco",
						"Translation & Localization",
						"Transportation/Trucking/Railroad",
						"Utilities",
						"Venture Capital & Private Equity",
						"Veterinary",
						"Warehousing",
						"Wholesale",
						"Wine & Spirits",
						"Wireless",
						"Writing & Editing"
					};
					foreach (var industryName in industries)
					{
						context.Industries.Add(new Industry { Name = industryName });
					}
					context.SaveChanges();
				}
				if (!context.Languages.Any())
				{
					var languages = new List<string>
					{
						"Afrikaans",
						"Albanian",
						"Amharic",
						"Arabic",
						"Armenian",
						"Azerbaijani",
						"Basque",
						"Belarusian",
						"Bengali",
						"Bosnian",
						"Bulgarian",
						"Catalan",
						"Cebuano",
						"Chichewa",
						"Chinese",
						"Corsican",
						"Croatian",
						"Czech",
						"Danish",
						"Dutch",
						"English",
						"Esperanto",
						"Estonian",
						"Filipino",
						"Finnish",
						"French",
						"Frisian",
						"Galician",
						"Georgian",
						"German",
						"Greek",
						"Gujarati",
						"Haitian Creole",
						"Hausa",
						"Hawaiian",
						"Hebrew",
						"Hindi",
						"Hmong",
						"Hungarian",
						"Icelandic",
						"Igbo",
						"Indonesian",
						"Irish",
						"Italian",
						"Japanese",
						"Javanese",
						"Kannada",
						"Kazakh",
						"Khmer",
						"Korean",
						"Kurdish (Kurmanji)",
						"Kyrgyz",
						"Lao",
						"Latin",
						"Latvian",
						"Lithuanian",
						"Luxembourgish",
						"Macedonian",
						"Malagasy",
						"Malay",
						"Malayalam",
						"Maltese",
						"Maori",
						"Marathi",
						"Mongolian",
						"Myanmar (Burmese)",
						"Nepali",
						"Norwegian",
						"Pashto",
						"Persian",
						"Polish",
						"Portuguese",
						"Punjabi",
						"Romanian",
						"Russian",
						"Samoan",
						"Scots Gaelic",
						"Serbian",
						"Sesotho",
						"Shona",
						"Sindhi",
						"Sinhala",
						"Slovak",
						"Slovenian",
						"Somali",
						"Spanish",
						"Sundanese",
						"Swahili",
						"Swedish",
						"Tajik",
						"Tamil",
						"Telugu",
						"Thai",
						"Turkish",
						"Ukrainian",
						"Urdu",
						"Uzbek",
						"Vietnamese",
						"Welsh",
						"Xhosa",
						"Yiddish",
						"Yoruba",
						"Zulu"
					};


					foreach (var languageName in languages)
					{
						context.Languages.Add(new Language { Name = languageName });
					}

					context.SaveChanges();
				}
			}
        }



    }
}
