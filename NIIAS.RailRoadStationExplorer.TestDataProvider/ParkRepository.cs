using NIIAS.RailRoadStationExplorer.Common.DAL.DAO;
using NIIAS.RailRoadStationExplorer.TestDataProvider;

namespace NIIAS.RailRoadStationExplorer.Common.DAL.Repository
{
    public class ParkRepository : IParkRepository
    {
        private readonly TestDataSource dataSource;
        public ParkRepository(TestDataSource dataSource) {
            this.dataSource = dataSource;
        }

        public IEnumerable<Park> GetAllParksFromStation() {
            return dataSource.Parks;
        }

        public Park? Get(string name) {
            return dataSource.Parks.FirstOrDefault(p => p.Name == name);
        }
    }
}
