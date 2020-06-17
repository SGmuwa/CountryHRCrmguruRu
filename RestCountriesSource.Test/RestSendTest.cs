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
using UserCountryInterfaces;
using Xunit;

namespace RestCountriesSource.Test
{
    public class RestSendTest
    {
        private GetterCountries getter = new GetterCountries();

        private List<ICountryInfo> infosExpect = new List<ICountryInfo>()
        {
            new CountryInfo("Russian Federation", "RU", "Moscow", 1.7124442E7, 146599183, "Europe")
        };

        [Theory]
        [InlineData(0, "russia")]
        public void Test1(int i, string rest)
        {
            Assert.Equal(infosExpect[i], getter.GetCountryInfo(rest));
        }
    }
}
