using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotissimusTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var xmlCatalog = XMLObjectModel.GetXmlData("http://partner.market.yandex.ru/pages/help/YML.xml");
            if (xmlCatalog == null)
            {
                ProgramExitMsg();
                return;
            }
            var offers = XMLObjectModel.GetOffers(xmlCatalog);
            Console.WriteLine("\nEnter SQLServer data source connection string:");
            var connStr = Console.ReadLine();
            Console.WriteLine("\nEnter name of database to be created:");
            var dbName = Console.ReadLine();
            var tableName = "Offers";
            if (!SQLDBBuilder.CreateDatabase(connStr, dbName))
            {
                ProgramExitMsg();
                return;
            }
            if (!SQLTableBuilder.CreateTable(connStr, dbName, tableName))
            {
                ProgramExitMsg();
                return;
            };
            if (!SQLStoredProcBuilder.CreateFillDataStoredProc(connStr, dbName, tableName))
            {
                ProgramExitMsg();
                return;
            }
            if (!SQLTableBuilder.FillDataTable(connStr, dbName, offers))
            {
                ProgramExitMsg();
                return;
            }
            ProgramExitMsg();
        }
        private static void ProgramExitMsg()
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
