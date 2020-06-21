/*
    CountryHRCrmguruRu.
    Copyright (C) 2020
    Mikhail Pavlovich Sidorenko (motherlode.muwa@gmail.com)

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using UserCountryInterfaces;

namespace ConsoleCountry
{
    class App
    {
        private readonly IGetterCountry getterCountry;
        private readonly IGetterCountries getterCountries;
        private readonly ISaverCountry saverCountry;

        public App(IGetterCountry getterCountry, IGetterCountries getterCountries, ISaverCountry saverCountry)
        {
            this.getterCountry = getterCountry ?? throw new ArgumentNullException(nameof(getterCountry));
            this.getterCountries = getterCountries ?? throw new ArgumentNullException(nameof(getterCountries));
            this.saverCountry = saverCountry ?? throw new ArgumentNullException(nameof(saverCountry));
        }

        public void Run()
        {
            Console.WriteLine("Press [Enter] to exit.\n"
                + "Type name of country and press [ENTER] to get info about country.");
            ICountryInfo country = null;
            while (true)
            {
                Console.Write(
                     $"{(country == null ? "" : "Press [s] and [Enter] to save in DB.\n")}"
                    + "Press [q] and [Enter] to exit.\n"
                    + "Press [Enter] to get all from DB.\n"
                    + "Type name of country and press [ENTER] to get info about country.\n"
                    + "Country: ");
                string s = Console.ReadLine();
                if (string.IsNullOrEmpty(s))
                {
                    Console.WriteLine(string.Join('\n', getterCountries.GetCountries()));
                    country = null;
                    continue;
                }
                if (country != null && s == "s")
                {
                    try
                    {
                        Console.WriteLine($"Save: {country.Name}...");
                        saverCountry.SaveCountry(country.Name);
                        Console.WriteLine("Saved success.");
                    }
                    catch(Exception e)
                    {
                        Console.Error.WriteLine($"Can't save: {e.Message}\n{e.StackTrace}\n{e}");
                    }
                    country = null;
                    continue;
                }
                country = getterCountry.GetCountryInfo(s);
                if (country == null)
                {
                    Console.WriteLine("Not found.");
                    continue;
                }
                Console.WriteLine(country);
            }
        }
    }
}
