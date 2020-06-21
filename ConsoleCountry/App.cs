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

        public App(IGetterCountry getterCountry)
            => this.getterCountry = getterCountry ?? throw new ArgumentNullException(nameof(getterCountry));

        public void Run()
        {
            Console.WriteLine("Press [Enter] to exit.\n"
                + "Type name of country and press [ENTER] to get info about country.");
            while (true)
            {
                Console.Write("Country: ");
                string s = Console.ReadLine();
                if (string.IsNullOrEmpty(s))
                    return;
                var country = getterCountry.GetCountryInfo(s);
                Console.WriteLine(country == null ? "Not found." : country.ToString());
            }
        }
    }
}
