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
                Tables.Region regionIsIs = (from r in context.Regions where r.Name == countryToBe.Region select r).FirstOrDefault();
                if (regionIsIs == null)
                {
                    regionIsIs = new Tables.Region();
                    context.Add(regionIsIs);
                }
                regionIsIs.Name = countryToBe.Region;
                Tables.City capitalIsIs = (from r in context.Cities where r.Name == countryToBe.Capital select r).FirstOrDefault();
                if (capitalIsIs == null)
                {
                    capitalIsIs = new Tables.City();
                    context.Add(capitalIsIs);
                }
                capitalIsIs.Name = countryToBe.Capital;
                Tables.Country countryIsIs = (from c in context.Countries where c.Name == countryToBe.Name select c).FirstOrDefault();
                if (countryIsIs == null)
                {
                    countryIsIs = new Tables.Country();
                    context.Add(countryIsIs);
                }
                countryIsIs.Name = countryToBe.Name;
                countryIsIs.Area = countryToBe.Area;
                countryIsIs.Code = countryToBe.Code;
                countryIsIs.Population = (long?)countryToBe.Population;
                countryIsIs.Region = regionIsIs;
                countryIsIs.Capital = capitalIsIs;
                context.SaveChanges();
            }
        }
    }
}
