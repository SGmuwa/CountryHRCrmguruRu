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

using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using UserCountryInterfaces;

namespace DBCountriesSource
{
    public class GetterCountries : IGetterCountries
    {
        private readonly DbContextOptions options;

        public GetterCountries(DbContextOptions options) => this.options = options;

        public GetterCountries()
        {
            DbContextOptionsBuilder dbb = new DbContextOptionsBuilder();
            var connectionInfo = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
            if(connectionInfo == null)
                connectionInfo = "Data Source=(local); Database=ArticlesSite; Persist Security Info=false; "
                    + "MultipleActiveResultSets=True; Trusted_Connection=True; Initial Catalog=Countries";
            dbb.UseSqlServer(connectionInfo);
            options = dbb.Options;
        }

        public IEnumerable<ICountryInfo> GetCountries()
        {
            using (var context = new MyDBContext(options))
            {
                return context.Countries;
            }
        }
    }
}
