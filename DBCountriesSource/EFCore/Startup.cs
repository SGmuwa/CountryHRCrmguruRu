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
using DBCountriesSource.Tables;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DBCountriesSource
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            Console.WriteLine($"configuration: {configuration}");
            this.configuration = configuration;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }

        public void ConfigureServices(IServiceCollection services)
            => services.AddDbContext<MyDBContext>((dbb) => BuildDbContextOptions(configuration, dbb));

        public static DbContextOptions BuildDbContextOptions(IConfiguration conf = null, DbContextOptionsBuilder builderDb = null)
        {
            if (conf == null)
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
                conf = cb.Build();
            }
            string connectionString = conf.GetConnectionString(nameof(MyDBContext));
            if (builderDb == null)
                builderDb = new DbContextOptionsBuilder();
            if (connectionString == null)
            {
                Console.Error.WriteLine("Warning: You need to set DB options in db.json or in environments for DB.");
                connectionString = "Server=127.0.0.1,1401;Database=Master;User Id=SA;Password=mypassword123!@#;";
            }
            return builderDb
                .UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
        }
    }
}
