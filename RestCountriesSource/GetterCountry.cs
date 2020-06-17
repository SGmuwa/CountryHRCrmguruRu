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
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using UserCountryInterfaces;

namespace RestCountriesSource
{
    public class GetterCountries : IGetterCountry
    {
        private static readonly HttpClient client = new HttpClient();
        private const string URI = "https://restcountries.eu/rest/v2/name/";

        public ICountryInfo GetCountryInfo(string CountryName)
        {
            var task = GetCountryInfoAwait(CountryName);
            task.Wait(8000);
            return task.IsCompletedSuccessfully
                ? task.Result
                : null;
        }

        public async Task<ICountryInfo> GetCountryInfoAwait(string CountryName)
        {
            if (CountryName == null)
                return null;
            try
            {
                var streamTask = client.GetStreamAsync(URI + Uri.EscapeUriString(CountryName));
                var country = await JsonSerializer.DeserializeAsync<List<CountryInfo>>(await streamTask);
                return country.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
    }
}
