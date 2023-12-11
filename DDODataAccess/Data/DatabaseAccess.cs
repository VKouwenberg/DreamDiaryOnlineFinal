using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDDO.Data;

public class DatabaseAccess
{
    private readonly MySqlConnection _connection;

    public DatabaseAccess(MySqlConnection connection)
    {
        _connection = connection;
    }
}

