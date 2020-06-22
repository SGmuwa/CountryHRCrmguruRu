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
using System.Diagnostics.CodeAnalysis;

namespace UserCountryInterfaces
{
    /// <summary>
    /// Abstract class of info country.
    /// </summary>
    public abstract class AbstractCountryInfo : ICountryInfo
    {
        private string name;
        [NotNull] public virtual string Name { get => name; set => name = value ?? throw new ArgumentNullException(nameof(Name)); }
        public abstract string Code { get; set; }
        public abstract string Capital { get; set; }
        public abstract double? Area { get; set; }
        public abstract ulong? Population { get; set; }
        public abstract string Region { get; set; }

        public static bool Equals(ICountryInfo that, object obj)
            => obj is ICountryInfo info
            && that.Name == info.Name
            && that.Code == info.Code
            && that.Capital == info.Capital
            && that.Area == info.Area
            && that.Population == info.Population
            && that.Region == info.Region;

        public static int GetHashCode(ICountryInfo that)
            => HashCode.Combine(that.Name, that.Code, that.Capital, that.Area, that.Population, that.Region);

        public static string ToString(ICountryInfo that)
            => $"{nameof(Name)}: {that.Name}, "
            + $"{nameof(Code)}: {that.Code}, "
            + $"{nameof(Capital)}: {that.Capital}, "
            + $"{nameof(Area)}: {that.Area}, "
            + $"{nameof(Population)}: {that.Population}, "
            + $"{nameof(Region)}: {that.Region}.";

        public override bool Equals(object obj) => Equals(this, obj);

        public override int GetHashCode() => GetHashCode(this);

        public override string ToString() => ToString(this);
    }
}
