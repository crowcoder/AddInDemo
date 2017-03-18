using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ModelGenWizard
{
    public class GoCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Window window = parameter as Window;
            ViewModel vm = window.DataContext as ViewModel;

            StringBuilder classBody = new StringBuilder();
            foreach (TableMetaData tblmeta in GetTableMetaData(vm))
            {
                classBody.AppendLine($"    public {tblmeta.DotNetType} {tblmeta.ColumnName} {{ get; set; }}");
            }

            vm.GeneratedCode = classBody.ToString();

            window.Close();
        }

        private IEnumerable<TableMetaData> GetTableMetaData(ViewModel vm)
        {
            SqlConnectionStringBuilder bldr = new SqlConnectionStringBuilder();
            bldr.DataSource = vm.ServerName;
            bldr.InitialCatalog = vm.DatabaseName;
            bldr.IntegratedSecurity = true;

            using (SqlConnection con = new SqlConnection(bldr.ConnectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = cmdtext;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = vm.TableName;

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    yield return new TableMetaData
                    {
                        ColumnName = rdr.GetString(0),
                        DotNetType = rdr.GetString(1)
                    };
                }
            }
        }

        private readonly string cmdtext = @"SELECT c.[name] AS [ColumnName],
	CASE 
		WHEN t.[system_type_id] IN(175,239,99,231,231,35,167) THEN 'string'
		WHEN t.[system_type_id] IN(56, 52, 48) THEN 'int'
		WHEN t.[system_type_id] IN(104) THEN 'bool'
		WHEN t.[system_type_id] IN(127) THEN 'long'
		WHEN t.[system_type_id] IN(40, 61,42,43) THEN 'DateTime'
		WHEN t.[system_type_id] IN(106, 62,60,108,59,122) THEN 'decimal'
		WHEN t.[system_type_id] IN(36) THEN 'Guid'
		ELSE 'Unknown'
	END AS DotNetType
FROM sys.columns c
INNER JOIN sys.types t
 ON c.[user_type_id] = t.[user_type_id]
WHERE object_id = (SELECT [object_id] FROM sys.tables WHERE [name] = @TableName);";
    }
}
