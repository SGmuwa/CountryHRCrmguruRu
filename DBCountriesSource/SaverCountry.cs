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

using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserCountryInterfaces;

namespace DBCountriesSource
{
    public class SaverCountry : ISaverCountry
    {
        private readonly IGetterCountry gc;
        private readonly DbContextOptions options;

        public SaverCountry(IGetterCountry getterCountry, DbContextOptions options)
        {
            this.gc = getterCountry ?? throw new System.ArgumentNullException(nameof(getterCountry));
            this.options = options ?? throw new System.ArgumentNullException(nameof(options));
        }

        public void SaveCountry(string countryName)
        {
            ICountryInfo countryToBe = gc.GetCountryInfo(countryName)
                ?? throw new System.ArgumentOutOfRangeException(nameof(countryName), $"Not found in {gc}");
            using (var context = new MyDBContext(options))
            {
                var countryIsIs = (from c in context.Countries where c.Name == countryName select c).FirstOrDefault();
                countryIsIs.Name = countryToBe.Name;
                countryIsIs.Area = countryToBe.Area;
                countryIsIs.Code = countryToBe.Code;
                countryIsIs.Population = countryToBe.Population;
                countryIsIs.Capital.Name = countryToBe.Capital;
                countryIsIs.Region.Name = countryToBe.Region;

                context.SaveChanges();
            }
        }
    }
}
