using System.Collections.Generic;
using Xunit;

namespace RestCountriesSource.Test
{
    public class RestSendTest
    {
        private GetterCountries getter = new GetterCountries();

        private List<CountryInfo> infosExpect = new List<CountryInfo>()
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
