using NIIAS.RailRoadStationExplorer.Common.DAL.DAO;

namespace NIIAS.RailRoadStationExplorer.PathFinder
{
    public interface IPathService
    {
        bool GetShortestPath(string startTrackName, string finishTrackName, out List<TrackPart> shortestPathResult);
    }
}
