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

namespace UserCountryInterfaces
{
    /// <summary>
    /// Interface of method of get info about country.
    /// </summary>
    public interface IGetterCountry
    {
        /// <summary>
        /// Get current country info by 
        /// </summary>
        /// <param name="countryName">Name of country.</param>
        /// <returns>Instance of info about country.</returns>
        ICountryInfo GetCountryInfo(string countryName);
    }
}
