using NIIAS.RailRoadStationExplorer.Common.DAL.DAO;
using System.Numerics;

namespace NIIAS.RailRoadStationExplorer.PathFinder.Graph
{
    public class GraphService : IGraphService
    {
        public void ConnectNodesTwoWay(Node node1, Node node2, TrackPart trackPart)
        {
            node1.Edges.Add(new Edge
            {
                TrackPart = trackPart,
                SecondNode = node2,
                Weight = GetEdgeWeight(trackPart)
            });

            if (!node2.Edges.Any(x => x.SecondNode == node1))
            {
                node2.Edges.Add(new Edge
                {
                    TrackPart = trackPart,
                    SecondNode = node1,
                    Weight = GetEdgeWeight(trackPart)
                });
            }
        }

        public void PrepareNode(Node current, Dictionary<int, float> pathToNodeValue, Dictionary<int, List<Edge>> shortestPath)
        {
            foreach (Edge? edge in current.Edges)
            {
                Node nextNode = edge.SecondNode;

                if (pathToNodeValue[nextNode.Id] >= pathToNodeValue[current.Id] + edge.Weight)
                {
                    shortestPath[nextNode.Id] = shortestPath[current.Id].Concat([edge]).ToList();
                    nextNode.Gone = false;
                }

                pathToNodeValue[nextNode.Id] = Math.Min(pathToNodeValue[nextNode.Id], pathToNodeValue[current.Id] + edge.Weight);
            }
            current.Gone = true;

            foreach (Edge? edge in current.Edges)
            {
                Node nextNode = edge.SecondNode;

                if (!nextNode.Gone)
                {
                    PrepareNode(nextNode, pathToNodeValue, shortestPath);
                }
            }
        }

        public bool TryGetShortestPath(GraphData graph, out List<TrackPart> shortestPathResult)
        {
            Dictionary<int, float> pathToNodeValue = [];
            Dictionary<int, List<Edge>> shortestPath = [];

            foreach (KeyValuePair<int, Node> node in graph.Nodes)
            {
                pathToNodeValue.Add(node.Key, float.MaxValue);
            }

            Node current = graph.StartNode;
            shortestPath[current.Id] = [];
            pathToNodeValue[current.Id] = 0;

            foreach (Edge edge in graph.StartNode.Edges)
            {
                pathToNodeValue[edge.SecondNode.Id] = edge.Weight;
                shortestPath[edge.SecondNode.Id] = [edge];
            }

            PrepareNode(current, pathToNodeValue, shortestPath);

            shortestPathResult = [];
            if (!shortestPath.TryGetValue(graph.FinishNode.Id, out List<Edge> resultEdges))
            {
                return false;
            }

            shortestPathResult = GetOnlyRealPartsFromEdges(graph, resultEdges);

            return true;
        }

        private List<TrackPart> GetOnlyRealPartsFromEdges(GraphData graph, IEnumerable<Edge> edgesToSelect)
        {
            return edgesToSelect.Select(x => x.TrackPart).Where(x => !graph.FakeParts.Contains(x)).ToList();
        }

        private float GetEdgeWeight(TrackPart trackPart)
        {
            return Vector2.Distance(new Vector2(trackPart.Point1.X, trackPart.Point1.Y), new Vector2(trackPart.Point2.X, trackPart.Point2.Y));
        }
    }

}
