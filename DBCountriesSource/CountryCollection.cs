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

namespace DBCountriesSource
{
    public class CountryCollection : IEnumerable<ICountryInfo>, IEnumerator<ICountryInfo>, IDisposable
    {
        private readonly MyDBContext context;
        private readonly IEnumerator<ICountryInfo> countries;
        private bool hasGet = false;

        internal CountryCollection(MyDBContext context)
        {
            this.context = context;
            this.countries = ((IEnumerable<ICountryInfo>)context.Countries).GetEnumerator();
        }

        ~CountryCollection() => Dispose();

        public ICountryInfo Current
        {
            get
            {
                hasGet = true;
                return countries.Current;
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            hasGet = true;
            countries.Dispose();
            context.Dispose();
        }

        public IEnumerator<ICountryInfo> GetEnumerator()
        {
            if (hasGet)
                throw new NotSupportedException("GetEnumerator already got!");
            hasGet = true;
            return this;
        }

        public bool MoveNext()
        {
            hasGet = true;
            return countries.MoveNext();
        }

        public void Reset() => countries.Reset();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
