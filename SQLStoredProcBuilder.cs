using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace NotissimusTestProject
{
    class SQLStoredProcBuilder
    {
        public static bool CreateFillDataStoredProc(string connStr, string dbName, string tableName)
        {
            using (Microsoft.Data.SqlClient.SqlConnection cnn = new Microsoft.Data.SqlClient.SqlConnection(connStr))
            {
                try
                {
                    Console.WriteLine($"Creating stored procedure 'sp_AddOffer'...");
                    cnn.Open();
                    ServerConnection sc = new ServerConnection(cnn);
                    sc.Connect();
                    Server srv = new Server(sc);
                    Database db = srv.Databases[dbName];
                    StoredProcedure sp = new StoredProcedure(db, "sp_AddOffer");
                    sp.TextMode = false;
                    sp.AnsiNullsStatus = false;
                    sp.QuotedIdentifierStatus = false;
                    sp.Parameters.Add(new StoredProcedureParameter(sp, "@id", DataType.Int));
                    sp.Parameters.Add(new StoredProcedureParameter(sp, "@type", DataType.NVarChar(50)));
                    sp.Parameters.Add(new StoredProcedureParameter(sp, "@bid", DataType.Int));
                    sp.Parameters.Add(new StoredProcedureParameter(sp, "@available", DataType.Bit));
                    sp.Parameters.Add(new StoredProcedureParameter(sp, "@value", DataType.NVarCharMax));
                    string cmdInsert = $"INSERT INTO {tableName} (id, type, bid, available, value)" +
                                        $" VALUES (@id, @type, @bid, @available, @value)";
                    sp.TextBody = cmdInsert;
                    sp.Create();
                    sp.QuotedIdentifierStatus = true;
                    sp.Alter();
                    Console.WriteLine($"Stored procedure 'sp_AddOffer' created");
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
