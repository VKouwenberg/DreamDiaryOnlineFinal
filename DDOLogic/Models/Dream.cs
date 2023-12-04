using DataAccessDDO.ModelsDTO;
using LogicDDO.Models;

namespace DDOLogic.Models;
public class Dream
{
    public int DreamId { get; set; } 
    public string DreamName { get; set; }
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