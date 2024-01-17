using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataAccessDDO.DatabaseConfiguration;

public class DatabaseContext
{
    private readonly string _connectionString;

    public DatabaseContext()
    {
		_connectionString = "Server=localhost;Database=ddodb;User Id=root;Password=;";
	}

    public MySqlConnection GetConnection()
    {
        MySqlConnection connection = new MySqlConnection(_connectionString);
        try
        {
            connection.Open();
            return connection;
        }
        catch (Exception ex)
        {
            throw new Exception("Error cannot connect to database", ex);
        }
    }
}

