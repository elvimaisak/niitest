using NIIAS.RailRoadStationExplorer.Common.DAL.DAO;

namespace NIIAS.RailRoadStationExplorer.Common.DAL.Repository
{
    public interface IParkRepository
    {
        //в реальном проекте - если станция не одна, то тут прилетает id станции
        IEnumerable<Park> GetAllParksFromStation();
        Park? Get(string name);
    }
}
