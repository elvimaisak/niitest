using NIIAS.RailRoadStationExplorer.Common.DAL.DAO;

namespace NIIAS.RailRoadStationExplorer.PathFinder.Graph
{
    public class GraphData
    {
        public Dictionary<int, Node> Nodes = [];
        public List<TrackPart> FakeParts = [];
        public Node StartNode { get; set; }
        public Node FinishNode { get; set; }
    }
}
