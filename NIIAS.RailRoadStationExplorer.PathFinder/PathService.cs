using NIIAS.RailRoadStationExplorer.Common.DAL.DAO;
using NIIAS.RailRoadStationExplorer.Common.DAL.Repository;
using NIIAS.RailRoadStationExplorer.PathFinder.Graph;

namespace NIIAS.RailRoadStationExplorer.PathFinder
{
    public class PathService : IPathService
    {
        private readonly ITrackRepository trackRepository;
        private readonly IGraphService graphService;

        public PathService(ITrackRepository trackRepository, IGraphService graphService)
        {
            this.trackRepository = trackRepository;
            this.graphService = graphService;
        }

        public bool GetShortestPath(string startTrackName, string finishTrackName, out List<TrackPart> shortestPathResult)
        {
            GraphData graph = GetGraph(startTrackName, finishTrackName);
            return graphService.TryGetShortestPath(graph, out shortestPathResult);
        }

        private GraphData GetGraph(string startTrackName, string finishTrackName)
        {
            TrackPart? startTrack = trackRepository.GetTrackPart(startTrackName);
            TrackPart? finishTrack = trackRepository.GetTrackPart(finishTrackName);

            Point startCenter = new Point((startTrack.Point1.X + startTrack.Point2.X) / 2, (startTrack.Point1.Y + startTrack.Point2.Y) / 2, "StartCenter");
            Point finishCenter = new Point((finishTrack.Point1.X + finishTrack.Point2.X) / 2, (finishTrack.Point1.Y + finishTrack.Point2.Y) / 2, "FinishCenter");

            TrackPart fakeTrackStart1 = new TrackPart("FakeStarting1", startTrack.Point1, startCenter);
            TrackPart fakeTrackStart2 = new TrackPart("FakeStarting2", startCenter, startTrack.Point2);

            TrackPart fakeTrackFinish1 = new TrackPart("FakeFinish1", finishTrack.Point1, finishCenter);
            TrackPart fakeTrackFinish2 = new TrackPart("FakeFinish2", finishCenter, finishTrack.Point2);

            IEnumerable<TrackPart> allTracks = trackRepository.GetAllTrackParts().Where(x => x.Name != startTrackName && x.Name != finishTrackName).Concat([fakeTrackStart1, fakeTrackStart2, fakeTrackFinish1, fakeTrackFinish2]);

            IEnumerable<Point> allPoints = trackRepository.GetAllPoints().Concat([startCenter, finishCenter]);

            Node[] nodes = allPoints.Select(x => new Node() { Point = x }).ToArray();
            Dictionary<int, Node> nodeDict = [];

            for (int i = 0; i < nodes.Count(); i++)
            {
                nodes[i].Id = i;
                nodeDict.Add(i, nodes[i]);
            }

            for (int i = 0; i < nodes.Length; i++)
            {
                IEnumerable<TrackPart> connectedParts = allTracks.Where(x => x.Point1.Name == nodes[i].Point.Name);

                foreach (TrackPart part in connectedParts)
                {
                    Node connectedNode = nodes.First(x => x.Id != i && (x.Point.Name == part.Point2.Name));
                    graphService.ConnectNodesTwoWay(nodes[i], connectedNode, part);
                }
            }

            return new GraphData()
            {
                StartNode = nodes.First(x => x.Point == startCenter),
                FinishNode = nodes.First(x => x.Point == finishCenter),
                FakeParts = [fakeTrackStart1, fakeTrackStart2, fakeTrackFinish1, fakeTrackFinish2],
                Nodes = nodeDict
            };
        }

    }
}
