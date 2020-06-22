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
using System.Collections.Generic;
using System.Linq;
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
            ICountryInfo country = null;
            while (true)
            {
                Console.Write(
                     $"{(country == null ? "" : $"\n{country}\n\nPress [s] and [Enter] to save in DB.\n")}"
                    + "Press [q] and [Enter] to exit.\n"
                    + "Press [Enter] to get all from DB.\n"
                    + "Type name of country and press [ENTER] to get info about country.\n"
                    + "Country: ");
                string s = Console.ReadLine();
                if (string.IsNullOrEmpty(s))
                {
                    try
                    {
                        using (var countries = getterCountries.GetCountries())
                        {
                            if (countries.Any())
                                Console.WriteLine(string.Join('\n', countries));
                            else
                                Console.WriteLine("List empty.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Can't get list of countries: {e}");
                    }
                    country = null;
                }
                else if (s == "q")
                    break;
                else if (country != null && s == "s")
                {
                    try
                    {
                        Console.WriteLine($"Save: {country.Name}...");
                        saverCountry.SaveCountry(country.Name);
                        Console.WriteLine("Saving completed successfully.");
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine($"Can't save:\n{e}");
                    }
                    country = null;
                }
                else
                {
                    country = getterCountry.GetCountryInfo(s);
                    if (country == null)
                        Console.WriteLine("Country not found.");
                }
            }
        }
    }
}
