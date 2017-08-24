using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Import
{
   public class ImportManager
    {
       public string BulkCopy(DataTable dt , string table)
       {
           string Result = "fail";
           try
           {
               using (var conn = new SqlConnection(DB_CONSTANTS.ConnectionString_ERP_CRUZATA))
               {
                   var bulkCopy = new SqlBulkCopy(conn);
                   bulkCopy.DestinationTableName = table;
                   conn.Open();
                   var schema = conn.GetSchema("Columns", new[] { null, null, table, null });
                   foreach (DataColumn sourceColumn in dt.Columns)
                   {
                       foreach (DataRow row in schema.Rows)
                       {
                           if (string.Equals(sourceColumn.ColumnName, (string)row["COLUMN_NAME"], StringComparison.OrdinalIgnoreCase))
                           {
                               bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, (string)row["COLUMN_NAME"]);
                               break;
                           }
                       }
                   }
                   bulkCopy.WriteToServer(dt);                   
               }
               Result = "Success";
           }
           catch (Exception ex)
           {
               BAL.Common.LogManager.LogError("BulkCopy", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Result;
       }
    }
}
