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
            var countries = JsonConvert.DeserializeObject<List<Country>>(File.ReadAllText("countries.txt"));

            var population = countries.Sum(c => c.Population);

            Console.WriteLine("Суммарное население:");
            Console.WriteLine(population);

            Console.WriteLine();

            var uniqueCurrencies = countries.SelectMany(c => c.Currencies)
                .Where(c => !string.IsNullOrEmpty(c.Code))
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .Where(c => !string.IsNullOrEmpty(c))
                .Distinct()
                .ToList();

            Console.WriteLine("Все валюты:");

            foreach (var currency in uniqueCurrencies)
            {
                Console.WriteLine(currency);
            }

            Console.ReadKey();
        }
    }
}
