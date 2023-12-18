using DataAccessDDO.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.DatabaseSettings;
using Microsoft.Extensions.Options;

namespace DataAccessDDO.Repositories;

public class RestRepo
{
    private readonly DatabaseSettings.DatabaseSettings _databaseSettings;

    public RestRepo(IOptions<DatabaseSettings.DatabaseSettings> databaseSettings)
    {
        _databaseSettings = databaseSettings.Value;
    }
}
