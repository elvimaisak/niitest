using NIIAS.RailRoadStationExplorer.Common.DAL.DAO;

namespace NIIAS.RailRoadStationExplorer.TestDataProvider
{
    public class TestDataBuilder
    {
        private readonly TestDataSource dataSource = new TestDataSource();

        public TestDataBuilder WithPoints()
        {
            dataSource.Points.AddRange(
                [
                    new Point(2, 5.5f, "A"),
                    new Point(3, 5.5f, "B"),
                    new Point(5, 5.5f, "C"),
                    new Point(7, 5.5f, "D"),
                    new Point(9, 5.5f, "E"),
                    new Point(10.5f, 5.5f, "F"),
                    new Point(10.75f, 5.5f, "G"),
                    new Point(12.75f, 5.5f, "H"),
                    new Point(3.5f, 7, "K"),
                    new Point(7, 7, "L"),
                    new Point(8, 8, "M"),
                    new Point(10.5f, 8, "N"),
                    new Point(11.5f, 7, "O"),
                    new Point(12.3f, 7, "P"),
                    new Point(12.5f, 6, "Q"),
                    new Point(6, 6.5f, "R"),
                    new Point(9.5f, 6.5f, "S"),
                    new Point(11, 6, "T"),
                    new Point(5, 4, "U"),
                    new Point(10, 4, "V"),
                    new Point(11.5f, 4, "W"),
                    new Point(7.5f, 4.5f, "X"),
                    new Point(9.5f, 4.5f, "Y"),
                    new Point(12, 6.5f, "Z")
                    ]
                );
            return this;
        }

        public TestDataBuilder WithTrackParts()
        {
            if (!dataSource.Points.Any())
            {
                WithPoints();
            }

            dataSource.Parts.AddRange([
                GenerateTrackPart("A", "B"),
                GenerateTrackPart("B", "C"),
                GenerateTrackPart("C", "R"),
                GenerateTrackPart("C", "D"),
                GenerateTrackPart("D", "E"),
                GenerateTrackPart("E", "F"),
                GenerateTrackPart("F", "G"),
                GenerateTrackPart("G", "H"),
                GenerateTrackPart("G", "T"),
                GenerateTrackPart("T", "Q"),
                GenerateTrackPart("A", "K"),
                GenerateTrackPart("K", "L"),
                GenerateTrackPart("L", "M"),
                GenerateTrackPart("M", "N"),
                GenerateTrackPart("N", "O"),
                GenerateTrackPart("O", "P"),
                GenerateTrackPart("P", "Q"),
                GenerateTrackPart("Q", "H"),
                GenerateTrackPart("L", "O"),
                GenerateTrackPart("E", "S"),
                GenerateTrackPart("S", "Z"),
                GenerateTrackPart("O", "Z"),
                GenerateTrackPart("D", "X"),
                GenerateTrackPart("X", "Y"),
                GenerateTrackPart("Y", "F"),
                GenerateTrackPart("B", "U"),
                GenerateTrackPart("U", "V"),
                GenerateTrackPart("V", "W"),
                GenerateTrackPart("W", "H")
             ]);
            return this;
        }

        public TestDataBuilder WithTracks()
        {
            if (!dataSource.Parts.Any())
            {
                WithTrackParts();
            }

            dataSource.Tracks.AddRange([
                GenerateTrack("Путь А", ["AB", "BC", "CD", "DE", "EF", "FG", "GH"]),
                GenerateTrack("Путь Б", ["AK", "KL"]),
                GenerateTrack("Путь В", ["BU", "UV"]),
                GenerateTrack("Путь Г", ["DX", "XY", "YF"]),
                GenerateTrack("Путь Д", ["ES", "SZ"]),
                GenerateTrack("Путь Е", ["LO", "OP", "PQ", "QH"]),
                GenerateTrack("Путь Ж", ["LM", "MN", "NO"]),
                GenerateTrack("Путь З", ["VW"]),
                GenerateTrack("Путь И", ["CR"])
                ]);
            return this;
        }

        public TestDataBuilder WithParks()
        {
            if (!dataSource.Tracks.Any())
            {
                WithTracks();
            }

            dataSource.Parks.AddRange([
                    GeneratePark("Парк П1", ["Путь Б", "Путь В", "Путь Д"]),
                    GeneratePark("Парк П2", ["Путь А", "Путь Е", "Путь Г"]),
                    GeneratePark("Парк П3", ["Путь З"]),
                    GeneratePark("Парк П4", ["Путь Ж", "Путь И"]),
                ]);

            return this;
        }

        public TestDataSource Build()
        {
            return dataSource;
        }

        private TrackPart GenerateTrackPart(string point1, string point2)
        {
            Point pointStart = dataSource.Points.First(x => x.Name == point1);
            Point pointEnd = dataSource.Points.First(x => x.Name == point2);

            return new TrackPart($"{point1}{point2}", pointStart, pointEnd);
        }

        private Track GenerateTrack(string name, List<string> parts)
        {
            Track result = new Track(name) { Parts = [] };

            foreach (string part in parts)
            {
                result.Parts.Add(dataSource.Parts.First(x => x.Name == part));
            }

            return result;
        }

        private Park GeneratePark(string parkName, List<string> tracks)
        {
            Park result = new Park(parkName) { Tracks = [] };

            foreach (string track in tracks)
            {
                result.Tracks.Add(dataSource.Tracks.First(x => x.Name == track));
            }
            return result;
        }
    }
}
