using NIIAS.RailRoadStationExplorer.Common.DAL.DAO;
using NIIAS.RailRoadStationExplorer.Common.DAL.Repository;

namespace NIIAS.RailRoadStationExplorer.TestDataProvider
{
    public class TrackRepository : ITrackRepository
    {
        private readonly TestDataSource dataSource;

        public TrackRepository(TestDataSource dataSource) {
            this.dataSource = dataSource;
        }

        public IEnumerable<TrackPart> GetAllTrackParts() {
            return dataSource.Parts;
        }

        public IEnumerable<Point> GetAllPoints() {
            return dataSource.Points;
        }

        public TrackPart? GetTrackPart(string name) {
            return dataSource.Parts.FirstOrDefault(x => x.Name == name);
        }
    }
}
