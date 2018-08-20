using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SQLCRUDAssn
{
    class Repository
    {
        public static string ConnectionString { get; private set; }

        public Repository()
        {
            IConfiguration configBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
                .AddJsonFile("appsettings.debug.json")
#else
                .AddJsonFile("appsettings.release.json")
#endif
                .Build();

            ConnectionString = configBuilder.GetConnectionString("DefaultConnection");

            
        }

        
    }
}
