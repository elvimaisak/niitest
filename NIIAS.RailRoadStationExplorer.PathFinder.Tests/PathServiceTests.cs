using FluentAssertions;
using Moq;
using NIIAS.RailRoadStationExplorer.Common.DAL.DAO;
using NIIAS.RailRoadStationExplorer.Common.DAL.Repository;
using NIIAS.RailRoadStationExplorer.PathFinder.Graph;

namespace NIIAS.RailRoadStationExplorer.PathFinder.Tests
{
    public class PathServiceTests
    {
        private readonly List<Point> points = [
            new Point(2,4,"A"),
            new Point(5,4,"B"),
            new Point(7,2,"C"),
            new Point(11,2,"D"),
            new Point(12,4,"E"),
            new Point(14,4,"F"),
            new Point(8,5,"K")
        ];

        private readonly TrackPart ab, bc, cd, de, ef, be, bk, ke;

        public PathServiceTests()
        {
            ab = new TrackPart("AB", points.First(x => x.Name == "A"), points.First(x => x.Name == "B"));
            bc = new TrackPart("BC", points.First(x => x.Name == "B"), points.First(x => x.Name == "C"));
            cd = new TrackPart("CD", points.First(x => x.Name == "C"), points.First(x => x.Name == "D"));
            de = new TrackPart("DE", points.First(x => x.Name == "D"), points.First(x => x.Name == "E"));
            ef = new TrackPart("EF", points.First(x => x.Name == "E"), points.First(x => x.Name == "F"));
            be = new TrackPart("BE", points.First(x => x.Name == "B"), points.First(x => x.Name == "E"));
            bk = new TrackPart("BK", points.First(x => x.Name == "B"), points.First(x => x.Name == "K"));
            ke = new TrackPart("KE", points.First(x => x.Name == "K"), points.First(x => x.Name == "E"));
        }


        [Fact]
        public void FindShortestPath()
        {
            //arrange
            Mock<ITrackRepository> trackRepo = new Mock<ITrackRepository>();
            List<TrackPart> trackParts = [ab, bc, cd, de, ef];
            trackRepo.Setup(x => x.GetAllPoints()).Returns(points);
            trackRepo.Setup(x => x.GetAllTrackParts()).Returns(trackParts);
            trackRepo.Setup(x => x.GetTrackPart(It.IsAny<string>())).Returns<string>(c => trackParts.FirstOrDefault(x => x.Name == c));

            PathService objUnderTest = new PathService(trackRepo.Object, new GraphService());

            //act
            bool result = objUnderTest.GetShortestPath("AB", "EF", out List<TrackPart> shortestPath);

            //assert
            result.Should().BeTrue();
            shortestPath.Should().ContainInOrder([bc, cd, de]);
        }

        [Fact]
        public void FindShortestPath1()
        {
            //arrange
            Mock<ITrackRepository> trackRepo = new Mock<ITrackRepository>();
            List<TrackPart> trackParts = [ab, bc, cd, de, ef, be];
            trackRepo.Setup(x => x.GetAllPoints()).Returns(points);
            trackRepo.Setup(x => x.GetAllTrackParts()).Returns(trackParts);
            trackRepo.Setup(x => x.GetTrackPart(It.IsAny<string>())).Returns<string>(c => trackParts.FirstOrDefault(x => x.Name == c));

            PathService objUnderTest = new PathService(trackRepo.Object, new GraphService());

            //act
            bool result = objUnderTest.GetShortestPath("AB", "EF", out List<TrackPart> shortestPath);

            //assert
            result.Should().BeTrue();
            shortestPath.Should().ContainInOrder([be]);
        }

        [Fact]
        public void FindShortestPath2()
        {
            //arrange
            Mock<ITrackRepository> trackRepo = new Mock<ITrackRepository>();
            List<TrackPart> trackParts = [ab, bc, cd, de, ef, bk, ke];
            trackRepo.Setup(x => x.GetAllPoints()).Returns(points);
            trackRepo.Setup(x => x.GetAllTrackParts()).Returns(trackParts);
            trackRepo.Setup(x => x.GetTrackPart(It.IsAny<string>())).Returns<string>(c => trackParts.FirstOrDefault(x => x.Name == c));

            PathService objUnderTest = new PathService(trackRepo.Object, new GraphService());

            //act
            bool result = objUnderTest.GetShortestPath("AB", "EF", out List<TrackPart> shortestPath);

            //assert
            result.Should().BeTrue();
            shortestPath.Should().ContainInOrder([bk, ke]);
        }

        [Fact]
        public void FindShortestPath3()
        {
            //arrange
            Mock<ITrackRepository> trackRepo = new Mock<ITrackRepository>();
            List<TrackPart> trackParts = [ab, bc, cd, ef];
            trackRepo.Setup(x => x.GetAllPoints()).Returns(points);
            trackRepo.Setup(x => x.GetAllTrackParts()).Returns(trackParts);
            trackRepo.Setup(x => x.GetTrackPart(It.IsAny<string>())).Returns<string>(c => trackParts.FirstOrDefault(x => x.Name == c));

            PathService objUnderTest = new PathService(trackRepo.Object, new GraphService());

            //act
            bool result = objUnderTest.GetShortestPath("AB", "EF", out List<TrackPart> shortestPath);

            //assert
            result.Should().BeFalse();
            shortestPath.Should().BeEmpty();
        }
    }
}