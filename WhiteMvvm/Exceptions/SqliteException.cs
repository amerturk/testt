using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteMvvm.Exceptions
{
    public class SqliteException :Exception
    {
        public SqliteException()
        {
            Source = "ApiService";
        }
        public SqliteException(string message) : base($"Sqlite Error: {message}")
        {
            Source = "ApiService";
        }
        public SqliteException(string message, Exception exception) : base($"Sqlite Error: {message}", exception)
        {
            Source = "ApiService";
        }
    }
}
