using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CountriesJson
{
    class CountriesJson
    {
        static void Main(string[] args)
        {
            var countries = JsonConvert.DeserializeObject<List<Country>>(File.ReadAllText(".\\countries.txt"));

            var population = 0;

            var allCurrencies = new List<string>();

            foreach (var country in countries)
            {
                population += country.Population;

                allCurrencies.AddRange(country.Currencies.Select(currency => currency.Name));
            }

            Console.WriteLine("Суммарное население:");
            Console.WriteLine(population);

            Console.WriteLine();

            var uniqueCurrencies = allCurrencies.Distinct();

            Console.WriteLine("Все валюты:");

            foreach (var currency in uniqueCurrencies)
            {
                Console.WriteLine(currency);
            }

            Console.ReadKey();
        }
    }
}
