using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessDDO.DatabaseSettings;

public class DatabaseSettings
{
    public string ConnectionString { get; }
    public DatabaseSettings() 
    {
        ConnectionString = "SERVER=localhost;DATABASE=ddodb;UID=root;";
	}
}