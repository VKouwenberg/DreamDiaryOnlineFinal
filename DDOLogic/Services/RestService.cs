using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.ModelsDTO;
using DataAccessDDO.Repositories;

namespace LogicDDO.Services;

public class RestService
{
    private readonly RestRepo _restRepo;

    public RestService(RestRepo restRepo)
    {
        _restRepo = restRepo;
    }


}
