namespace NIIAS.RailRoadStationExplorer.Common.DAL.DAO
{
    public class TrackPart
    {
        public TrackPart()
        {

        }

        public TrackPart(string name, Point point1, Point point2)
        {
            Name = name;
            Point1 = point1;
            Point2 = point2;
        }

        public string Name { get; set; }
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
    }
}
