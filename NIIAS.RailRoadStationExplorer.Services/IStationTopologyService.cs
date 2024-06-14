using NIIAS.RailRoadExplorer.Topology.DAL.DAO.ReadModel;
using NIIAS.RailRoadStationExplorer.Topology.ViewModels;

namespace NIIAS.RailRoadStationExplorer.Services
{
    public interface IStationTopologyService
    {
        IEnumerable<ParkViewModel> GetAllParks();
        IEnumerable<TrackPartViewModel> GetTrackParts();
        TrackBorder GetBorder(string parkName);
    }
}
