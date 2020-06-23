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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DBCountriesSource
{
    public static class BuilderDbContextOptions
    {
        private const string fileJson = "appsettings.json";

        public static DbContextOptions BuildDbContextOptions(IConfiguration conf = null, DbContextOptionsBuilder builderDb = null)
        {
            if (conf == null)
            {
                IConfigurationBuilder cb = new ConfigurationBuilder();
                if (File.Exists(fileJson))
                {
                    try
                    {
                        cb.AddJsonFile(fileJson);
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine($"Can't open file {fileJson}: {e}");
                    }
                    cb.AddEnvironmentVariables();
                }
                cb.AddEnvironmentVariables();
                conf = cb.Build();
            }
            string connectionString = conf.GetConnectionString(nameof(MyDBContext));
            if (builderDb == null)
                builderDb = new DbContextOptionsBuilder();
            if (connectionString == null)
            {
                throw new ApplicationException($"You need to set DB options in {fileJson} or in environment \"ConnectionStrings:{nameof(MyDBContext)}\" for DB.\nExample connection string for docker mssql: «Server=127.0.0.1,1433;Database=Master;User Id=SA;Password=mypassword123!@#;»");
            }
            return builderDb
                .UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
        }
    }
}
