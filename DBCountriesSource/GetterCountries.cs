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
using Microsoft.EntityFrameworkCore;
using UserCountryInterfaces;

namespace DBCountriesSource
{
    public class GetterCountries : IGetterCountries
    {
        private readonly DbContextOptions<MyDBContext> options;

        public GetterCountries(DbContextOptions<MyDBContext> options)
            => this.options = options ?? throw new ArgumentNullException(nameof(options));

        public IEnumerable<ICountryInfo> GetCountries() => new CountryCollection(new MyDBContext(options));
    }
}
