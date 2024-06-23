using NIIAS.RailRoadStationExplorer.Common.DAL.DAO;

namespace NIIAS.RailRoadStationExplorer.PathFinder.Graph
{
    public class GraphData
    {
        public Dictionary<int, Node> Nodes { get; set; } = [];
        public List<TrackPart> FakeParts { get; set; } = [];
        public Node StartNode { get; set; }
        public Node FinishNode { get; set; }
    }
}
