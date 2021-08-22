//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace AHTG.Hospital.ObjectModel.Context
//{
//    public static class OptionsFactory
//    {
//        public static DbContextOptions CreateOptions()
//            where T : DbContext
//        {
//            //This creates the SQLite connection string to in-memory database
//            var connectionStringBuilder = new SqliteConnectionStringBuilder
//            { DataSource = ":memory:" };
//            var connectionString = connectionStringBuilder.ToString();

//            //This creates a SqliteConnectionwith that string
//            var connection = new SqliteConnection(connectionString);

//            //The connection MUST be opened here
//            connection.Open();

//            //Now we have the EF Core commands to create SQLite options
//            var builder = new DbContextOptionsBuilder<T>();
//            builder.UseSqlite(connection);

//            return builder.Options;
//        }
//    }
//}
