using NIIAS.RailRoadStationExplorer.Common.DAL.DAO;

namespace NIIAS.RailRoadStationExplorer.PathFinder.Graph
{
    public class Node
    {
        public int Id { get; set; }
        public Point Point { get; set; } = null!;
        public bool Gone { get; set; }
        public List<Edge> Edges { get; set; } = [];

        public override string ToString() {
            return Point.Name;
        }
    }

    public class Edge//<T>
    {
        //TODO в реальном проекте можно было бы заморочиться дженериками и сделать структуру, максимально абстрактную и оторванную от данных полностью. начала было так делать и для Node и для Edge, потом подумала, что уйдет больше времени.

        //private readonly T value;
        //public T Value => value;
        public Node SecondNode { get; set; } = null!;

        public float Weight { get; set; }
        public TrackPart TrackPart { get; set; } = null!;
        public override string ToString() {
            return TrackPart.Name;
        }
        //public Edge(T value)
        //{
        //    this.value = value;
        //}
    }

}
