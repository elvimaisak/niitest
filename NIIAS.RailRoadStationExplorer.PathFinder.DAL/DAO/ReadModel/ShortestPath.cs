namespace NIIAS.RailRoadStationExplorer.Common.DAL.DAO.ReadModel
{
    public class ShortestPath
    {
        public Guid TrackPartIdStart { get; set; }
        public Guid TrackPartIdEnd { get; set; }

        public double Distance { get; set; }
        public virtual ICollection<TrackPart> TrackParts { get; set; }

    }
}
