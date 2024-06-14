namespace NIIAS.RailRoadStationExplorer.Common.DAL.DAO
{
    public class Point
    {
        public Point() {
        }

        public Point(float x, float y, string name) {
            X = x;
            Y = y;
            Name = name;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public string Name { get; set; }

    }
}
