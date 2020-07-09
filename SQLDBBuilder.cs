using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace NotissimusTestProject
{
    class SQLDBBuilder
    {
        public static bool CreateDatabase(string connStr, string dbName)
        {
            using (SqlConnection cnn = new SqlConnection(connStr))
            {
                string createCmd = $"CREATE DATABASE {dbName}";
                SqlCommand cmdCreateDB = new SqlCommand(createCmd, cnn);
                try
                {
                    cnn.Open();
                    Console.WriteLine("\nConnected successfully");
                    Console.WriteLine($"Creating database '{dbName}'...");
                    cmdCreateDB.ExecuteNonQuery();
                    Console.WriteLine($"Database '{dbName}' created");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        
    }
}
