using DataAccessDDO.ModelsDTO;
using LogicDDO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicDDO.Models;
public class Dream
{
    public int DreamId { get; set; } 
    public string DreamName { get; set; }
    public string DreamText { get; set; }
    public string ReadableBy { get; set; }
    public int DreamerId { get; set; } 
    public Dream(int dreamId, string dreamName, int dreamerId)
    {
        DreamId = dreamId;
        DreamName = dreamName;
        DreamerId = dreamerId;
    }

    public Dream()
    {

    }
}