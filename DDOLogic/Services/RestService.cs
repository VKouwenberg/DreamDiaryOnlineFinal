using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.Models;
using LogicDDO.Services.DataAccessRepositoriesInterfaces;

namespace LogicDDO.Services;

public class RestService : LogicInterfaces.IRestService
{
    private readonly IRestRepository _restRepo;

    public RestService(IRestRepository restRepo)
    {
        _restRepo = restRepo;
    }


}
