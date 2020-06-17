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

using UserCountryInterfaces;
using System.Text.Json.Serialization;
using System;

namespace RestCountriesSource
{
    public class CountryInfo : ICountryInfo
    {
        public CountryInfo(string name) => Name = name;

        public CountryInfo(string name, string code, string capital, double? area, ulong? population, string region)
            => (Name, Code, Capital, Area, Population, Region) = (name, code, capital, area, population, region);

        private string name;

        [JsonPropertyName("name")]
        public string Name { get => name; set => name = value ?? throw new ArgumentNullException(nameof(Name)); }

        [JsonPropertyName("alpha2Code")]
        public string Code { get; set; }

        [JsonPropertyName("capital")]
        public string Capital { get; set; }

        [JsonPropertyName("area")]
        public double? Area { get; set; }

        [JsonPropertyName("population")]
        public ulong? Population { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }
    }
}
