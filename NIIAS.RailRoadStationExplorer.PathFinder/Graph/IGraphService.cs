using NIIAS.RailRoadStationExplorer.Common.DAL.DAO;

namespace NIIAS.RailRoadStationExplorer.PathFinder.Graph
{
    public interface IGraphService
    {
        void ConnectNodesTwoWay(Node node1, Node node2, TrackPart trackPart);
        bool TryGetShortestPath(GraphData graph, out List<TrackPart> shortestPathResult);
    }
}
