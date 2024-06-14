using NIIAS.RailRoadStationExplorer.Common.DAL.DAO;

namespace NIIAS.RailRoadStationExplorer.Common.DAL.Repository
{
    public interface ITrackRepository
    {
        /// <summary>
        /// в реальном проекте сюда бы в параметрах прилетал id станции например, но мы считаем что станция одна
        /// </summary>
        /// <returns></returns>
        IEnumerable<TrackPart> GetAllTrackParts();
        TrackPart? GetTrackPart(string name);
        IEnumerable<Point> GetAllPoints();
    }
}
