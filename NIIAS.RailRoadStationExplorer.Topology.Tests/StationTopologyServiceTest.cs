using FluentAssertions;
using Moq;
using NIIAS.RailRoadStationExplorer.Common.DAL.DAO;
using NIIAS.RailRoadStationExplorer.Common.DAL.Repository;

namespace NIIAS.RailRoadStationExplorer.Topology.Tests
{
    public class StationTopologyServiceTest
    {
        [Fact]
        public void GetBorderPoints_When3Points_ReturnItAll() {
            //arrange
            List<Point> points = [
                new Point(1, 1, "A"),
                new Point(2, 1, "B"),
                new Point(2, 2, "C"),
                ];
            var objUnderTest = new StationTopologyService(new Mock<IParkRepository>().Object, new Mock<ITrackRepository>().Object);
            //act
            var borderPoints = objUnderTest.GetBorderPoints(points);
            //assert
            borderPoints.Should().ContainInOrder(new List<Point>()
                {
                points.First(x=>x.Name == "C"),
                points.First(x=>x.Name == "B"),
                points.First(x=>x.Name == "A")
                });
        }


        [Fact]
        public void GetBorderPoints_When5Points_ReturnAppropriate5() {
            //arrange
            List<Point> points = [
                new Point(1, 1, "A"),
                new Point(2, 1, "B"),
                new Point(2, 2, "C"),
                new Point(4, 1, "D"),
                new Point(4, 2, "E"),

                ];
            var objUnderTest = new StationTopologyService(new Mock<IParkRepository>().Object, new Mock<ITrackRepository>().Object);
            //act
            var borderPoints = objUnderTest.GetBorderPoints(points);
            //assert
            borderPoints.Should().ContainInOrder(
                new List<Point>()
                {
                points.First(x=>x.Name == "C"),
                points.First(x=>x.Name == "E"),
                points.First(x=>x.Name == "D"),
                points.First(x=>x.Name == "A")
                });
        }

        [Fact]
        public void GetBorderPoints_When6Points_ReturnAppropriate4() {
            //arrange
            List<Point> points = [
                new Point(1, 1, "A"),
                new Point(2, 1, "B"),
                new Point(2, 2, "C"),
                new Point(4, 1, "D"),
                new Point(4, 2, "E"),
                new Point(3, 3, "F"),
                ];
            var objUnderTest = new StationTopologyService(new Mock<IParkRepository>().Object, new Mock<ITrackRepository>().Object);
            //act
            var borderPoints = objUnderTest.GetBorderPoints(points);
            //assert
            borderPoints.Should().ContainInOrder(
                new List<Point>()
                {
                points.First(x=>x.Name == "C"),
                points.First(x=>x.Name == "E"),
                points.First(x=>x.Name == "D"),
                points.First(x=>x.Name == "A")
                });
        }

        [Fact]
        public void GetBorderPoints_When5Points_ReturnAppropriate4() {
            //arrange
            List<Point> points = [
                new Point(1, 1, "A"),
                new Point(3, 2, "B"),
                new Point(5, 2, "C"),
                new Point(6, 3.5f, "D"),
                new Point(3.5f, 4, "E"),

                ];
            var objUnderTest = new StationTopologyService(new Mock<IParkRepository>().Object, new Mock<ITrackRepository>().Object);
            //act
            var borderPoints = objUnderTest.GetBorderPoints(points);
            //assert
            borderPoints.Should().ContainInOrder(
                new List<Point>()
                {
                points.First(x=>x.Name == "E"),
                points.First(x=>x.Name == "D"),
                points.First(x=>x.Name == "C"),
                points.First(x=>x.Name == "A")
                });
        }
    }
}