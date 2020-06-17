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

namespace UserCountryInterfaces
{
    /// <summary>
    /// Implement of info country.
    /// </summary>
    public class CountryInfoSample : ICountryInfo
    {
        public CountryInfoSample(string name) => Name = name;

        private string name;

        [SetNotNull]
        public string Name { get => name; set => name = value ?? throw new ArgumentNullException(nameof(Name)); }
        public string Code { get; set; }
        public string Capital { get; set; }
        public double? Area { get; set; }
        public ulong? Population { get; set; }
        public string Region { get; set; }


        public override bool Equals(object obj)
            => obj is ICountryInfo info
            && Name == info.Name
            && Code == info.Code
            && Capital == info.Capital
            && Area == info.Area
            && Population == info.Population
            && Region == info.Region;

        public override int GetHashCode() => HashCode.Combine(Name, Code, Capital, Area, Population, Region);
    }
}
