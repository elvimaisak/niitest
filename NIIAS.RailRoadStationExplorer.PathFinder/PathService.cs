using NIIAS.RailRoadStationExplorer.Common.DAL.DAO;
using NIIAS.RailRoadStationExplorer.Common.DAL.Repository;
using NIIAS.RailRoadStationExplorer.PathFinder.Graph;

namespace NIIAS.RailRoadStationExplorer.PathFinder
{
    public class PathService : IPathService
    {
        private readonly ITrackRepository trackRepository;
        private readonly IGraphService graphService;

        public PathService(ITrackRepository trackRepository, IGraphService graphService) {
            this.trackRepository = trackRepository;
            this.graphService = graphService;
        }

        public bool GetShortestPath(string startTrackName, string finishTrackName, out List<TrackPart> shortestPathResult) {
            var graph = GetGraph(startTrackName, finishTrackName);
            return graphService.TryGetShortestPath(graph, out shortestPathResult);
        }

        private GraphData GetGraph(string startTrackName, string finishTrackName) {
            var startTrack = trackRepository.GetTrackPart(startTrackName);
            var finishTrack = trackRepository.GetTrackPart(finishTrackName);

            var startCenter = new Point((startTrack.Point1.X + startTrack.Point2.X) / 2, (startTrack.Point1.Y + startTrack.Point2.Y) / 2, "StartCenter");
            var finishCenter = new Point((finishTrack.Point1.X + finishTrack.Point2.X) / 2, (finishTrack.Point1.Y + finishTrack.Point2.Y) / 2, "FinishCenter");

            var fakeTrackStart1 = new TrackPart("FakeStarting1", startTrack.Point1, startCenter);
            var fakeTrackStart2 = new TrackPart("FakeStarting2", startCenter, startTrack.Point2);

            var fakeTrackFinish1 = new TrackPart("FakeFinish1", finishTrack.Point1, finishCenter);
            var fakeTrackFinish2 = new TrackPart("FakeFinish2", finishCenter, finishTrack.Point2);

            var allTracks = trackRepository.GetAllTrackParts().Where(x => x.Name != startTrackName && x.Name != finishTrackName).Concat([fakeTrackStart1, fakeTrackStart2, fakeTrackFinish1, fakeTrackFinish2]);

            var allPoints = trackRepository.GetAllPoints().Concat([startCenter, finishCenter]);

            var nodes = allPoints.Select(x => new Node() { Point = x }).ToArray();
            Dictionary<int, Node> nodeDict = [];

            for (var i = 0; i < nodes.Count(); i++) {
                nodes[i].Id = i;
                nodeDict.Add(i, nodes[i]);
            }

            for (var i = 0; i < nodes.Length; i++) {
                var connectedParts = allTracks.Where(x => x.Point1.Name == nodes[i].Point.Name);

                foreach (var part in connectedParts) {
                    var connectedNode = nodes.First(x => x.Id != i && (x.Point.Name == part.Point2.Name));
                    graphService.ConnectNodesTwoWay(nodes[i], connectedNode, part);
                }
            }

            return new GraphData() {
                StartNode = nodes.First(x => x.Point == startCenter),
                FinishNode = nodes.First(x => x.Point == finishCenter),
                FakeParts = [fakeTrackStart1, fakeTrackStart2, fakeTrackFinish1, fakeTrackFinish2],
                Nodes = nodeDict
            };
        }

    }
}
