namespace NIIAS.RailRoadStationExplorer.Common.DAL.DAO
{
    public class Track
    {
        public Track() {
        }

        public Track(string name) {
            Name = name;
        }

        public string Name { get; set; }

        public ICollection<TrackPart> Parts { get; set; }

    }
}
