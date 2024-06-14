namespace NIIAS.RailRoadStationExplorer.Common.DAL.DAO
{
    public class Park
    {
        public Park()
        {
        }

        public Park(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public ICollection<Track> Tracks { get; set; }
    }
}
