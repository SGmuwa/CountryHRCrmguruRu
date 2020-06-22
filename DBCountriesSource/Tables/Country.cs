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
using System.ComponentModel.DataAnnotations;
using UserCountryInterfaces;

namespace DBCountriesSource.Tables
{
    public class Country : ICountryInfo
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Code { get; set; }

        public Guid? CapitalId { get; set; }

        public virtual City Capital { get; set; }

        public double? Area { get; set; }

        public long? Population { get; set; }

        public Guid? RegionId { get; set; }

        public virtual Region Region { get; set; }

        string ICountryInfo.Capital { get => Capital.Name; set => Capital.Name = value; }

        ulong? ICountryInfo.Population { get => (ulong?)Population; set => Population = (long?)value; }

        string ICountryInfo.Region { get => Region.Name; set => Region.Name = value; }
    }
}
