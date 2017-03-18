using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void usp_Fruit_GET()
    {
        // Put your code here
        using (SqlConnection connection = new SqlConnection("context connection=true"))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.Fruit", connection);
            SqlContext.Pipe.ExecuteAndSend(command);
        }
    }
}
