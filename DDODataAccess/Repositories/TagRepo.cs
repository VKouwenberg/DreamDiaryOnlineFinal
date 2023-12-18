using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.DatabaseSettings;
using DataAccessDDO.ModelsDTO;
using Microsoft.Extensions.Options;

namespace DataAccessDDO.Repositories;

public class TagRepo
{
    private readonly DatabaseSettings.DatabaseSettings _databaseSettings;

    public TagRepo(IOptions<DatabaseSettings.DatabaseSettings> databaseSettings)
    {
        _databaseSettings = databaseSettings.Value;
    }
}
