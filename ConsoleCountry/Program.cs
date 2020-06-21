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
using System.IO;
using DBCountriesSource;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestCountriesSource;
using UserCountryInterfaces;

namespace ConsoleCountry
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IGetterCountry gc = new GetterCountry();
            DbContextOptions context = BuildDbContextOptions();
            IGetterCountries gcs = new GetterCountries(context);
            ISaverCountry sc = new SaverCountry(gc, context);
            new App(gc, gcs, sc).Run();
        }

        public static DbContextOptions BuildDbContextOptions()
        {
            IConfigurationBuilder cb = new ConfigurationBuilder();
            cb.AddEnvironmentVariables("COUNTRY_");
            if (File.Exists("db.json"))
            {
                try
                {
                    cb.AddJsonFile("db.json");
                }
                catch { }
            }
            IConfigurationRoot conf = cb.Build();
            string DB_select = conf.GetValue<string>("DB_select");
            if (DB_select == null)
                throw new NullReferenceException("You need to set type DB options in db.json or in environments for DB.");
            string connectionString = conf.GetConnectionString(DB_select);
            if (connectionString == null)
                throw new NullReferenceException("You need to set DB options in db.json or in environments for DB.");
            return new DbContextOptionsBuilder()
                .UseSqlServer(connectionString, providerOptions=>providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
        }
    }
}
