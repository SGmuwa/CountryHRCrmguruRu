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

namespace UserCountryInterfaces
{
    /// <summary>
    /// Interface of gets info about countries.
    /// </summary>
    public interface IGetterCountries
    {
        /// <summary>
        /// Get current country info by 
        /// </summary>
        /// <param name="CountryName">Name of country.</param>
        /// <returns>Instance of info about country.</returns>
        ICountryInfo GetCountryInfo(string CountryName);
        /// <summary>
        /// Get all countries from somewhere.
        /// </summary>
        /// <returns>Enumeration of countries.</returns>
        IEnumerable<ICountryInfo> GetCountries();
    }
}
