using NIIAS.RailRoadExplorer.Topology.DAL.DAO.ReadModel;
using NIIAS.RailRoadStationExplorer.Common.DAL.DAO;
using NIIAS.RailRoadStationExplorer.Common.DAL.Repository;
using NIIAS.RailRoadStationExplorer.Services;
using NIIAS.RailRoadStationExplorer.Topology.ViewModels;
using System.Numerics;

namespace NIIAS.RailRoadStationExplorer.Topology
{
    public class StationTopologyService : IStationTopologyService
    {
        private readonly IParkRepository parkRepository;
        private readonly ITrackRepository trackRepository;
        public StationTopologyService(IParkRepository parkRepository, ITrackRepository trackRepository) {
            this.parkRepository = parkRepository;
            this.trackRepository = trackRepository;
        }

        public IEnumerable<ParkViewModel> GetAllParks() {
            return parkRepository.GetAllParksFromStation().Select(x => new ParkViewModel() { Name = x.Name, Tracks = x.Tracks.Select(c => c.Name) });
        }

        public IEnumerable<TrackPartViewModel> GetTrackParts() {
            return trackRepository.GetAllTrackParts().Select(x => new TrackPartViewModel() { Name = x.Name });
        }

        public TrackBorder GetBorder(string parkName) {
            var park = parkRepository.Get(parkName);
            var points = GetBorderPoints(park.Tracks.SelectMany(x => x.Parts).SelectMany(x => new List<Point>() { x.Point1, x.Point2 }));
            return new TrackBorder() { BorderPoints = points };
        }


        public IList<Point> GetBorderPoints(IEnumerable<Point> points) {
            var leftPoint = points.MinBy(x => x.X);
            List<Point> resultPoints = [];

            var current = new PointEval(leftPoint, new Vector2(-1, 0));
            do {
                var allAngles = GetAnglesToPoints(current, points.Except(resultPoints));
                current = GetNext(allAngles);
                resultPoints.Add(current.Point);
            }
            while (current.Point != leftPoint);

            return resultPoints;
        }


        private Dictionary<Point, PointEval> GetAnglesToPoints(PointEval current, IEnumerable<Point> points) {
            Dictionary<Point, PointEval> result = [];

            foreach (var point in points) {
                if (point != current.Point) {
                    result.Add(point, GetAngleValuation(current, point));
                }
            }

            return result;
        }

        private PointEval GetNext(Dictionary<Point, PointEval> allAngles) {
            return allAngles.MaxBy(x => x.Value.Cos).Value;
        }

        private PointEval GetAngleValuation(PointEval p1, Point p2) {
            var vector = Vector2.Normalize(new Vector2(p2.X - p1.Point.X, p2.Y - p1.Point.Y));

            var result = new PointEval(p2, vector) {
                Cos = Vector2.Dot(p1.NormalizedVectorToPoint, vector)
            };

            return result;
        }

        private struct PointEval
        {
            public Point Point { get; set; }
            public Vector2 NormalizedVectorToPoint { get; set; }

            public float Cos { get; set; }

            public PointEval(Point point, Vector2 vector) {
                Point = point;
                NormalizedVectorToPoint = vector;
            }
        }
    }
}
