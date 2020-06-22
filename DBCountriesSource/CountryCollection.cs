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
using System.Collections;
using System.Collections.Generic;
using UserCountryInterfaces;
using System.Linq;
using DBCountriesSource.Tables;

namespace DBCountriesSource
{
    internal class CountryCollection : IEnumerableDisposable<ICountryInfo>
    {
        private readonly MyDBContext context1;
        private readonly MyDBContext context2;

        internal CountryCollection(MyDBContext context1, MyDBContext context2)
        {
            this.context1 = context1 ?? throw new ArgumentNullException(nameof(context1));
            this.context2 = context2 ?? throw new ArgumentNullException(nameof(context2));
        }

        ~CountryCollection() => Dispose();

        public void Dispose() => context1.Dispose();

        public IEnumerator<ICountryInfo> GetEnumerator()
        {
            using (var en = ((IEnumerable<Country>)context1.Countries).GetEnumerator())
            {
                while (en.MoveNext())
                {
                    en.Current.Capital = (from city in context2.Cities where city.Id == en.Current.CapitalId select city).FirstOrDefault();
                    en.Current.Region = (from reg in context2.Regions where reg.Id == en.Current.RegionId select reg).FirstOrDefault();
                    yield return en.Current;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
