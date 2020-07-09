using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;


namespace NotissimusTestProject
{
    class SQLTableBuilder
    {
        public static bool CreateTable(string connStr, string dbName, string tableName)
        {
            using (SqlConnection cnn = new SqlConnection(connStr))
            {
                string createCmd = $"USE {dbName} CREATE TABLE {tableName}(" +
                   $"id INT PRIMARY KEY," +
                   $"type NVARCHAR(50) NOT NULL," +
                   $"bid INT," +
                   $"available BIT," +
                   $"value XML)";
                SqlCommand cmdCreateTable = new SqlCommand(createCmd, cnn);
                try
                {
                    cnn.Open();
                    Console.WriteLine($"Creating table '{tableName}'...");
                    cmdCreateTable.ExecuteNonQuery();
                    Console.WriteLine($"Table '{tableName}' created");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return true;
        }

        public static bool FillDataTable(string connStr, string dbName, IEnumerable<XElement> fieldsOffer)
        {
            Console.WriteLine("Writing data to a table using a stored procedure...");
            using (SqlConnection cnn = new SqlConnection(connStr))
            {

                SqlCommand cmdStoredProc = new SqlCommand("sp_AddOffer", cnn);
                cmdStoredProc.CommandType = CommandType.StoredProcedure;
                try
                {
                    cnn.Open();
                    cnn.ChangeDatabase(dbName);
                    foreach (XElement el in fieldsOffer)
                    {
                        cmdStoredProc.Parameters.AddWithValue("@id", el.Attribute("id").Value);
                        cmdStoredProc.Parameters.AddWithValue("@type", el.Attribute("type").Value);
                        cmdStoredProc.Parameters.AddWithValue("@bid", el.Attribute("bid").Value);
                        cmdStoredProc.Parameters.AddWithValue("@available", el.Attribute("available").Value);
                        cmdStoredProc.Parameters.AddWithValue("@value", el.Value);
                        cmdStoredProc.ExecuteNonQuery();
                        cmdStoredProc.Parameters.Clear();
                    }
                    Console.WriteLine($"Writing data to a table is completed");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return true;
        }
    }
}
