using NIIAS.RailRoadExplorer.Topology.DAL.DAO.ReadModel;
using NIIAS.RailRoadStationExplorer.PathFinder;
using NIIAS.RailRoadStationExplorer.Services;
using Sharprompt;

namespace NIIAS.RailRoadStationExplorer
{
    internal class ConsoleCommandService
    {
        private readonly IStationTopologyService stationTopologyService;
        private readonly IPathService pathService;

        public ConsoleCommandService(IStationTopologyService stationTopologyService, IPathService pathService)
        {
            this.stationTopologyService = stationTopologyService;
            this.pathService = pathService;
        }

        public IEnumerable<ConsoleCommand> GetMainCommands()
        {
            return
            [
               new ConsoleCommand{Name = "view park borders", Action = new Action(ShowAllParksToSelectForBorders) }, //с возможностью выбора парка для дальнейших действий
               new ConsoleCommand{Name = "get shortest path", Action = new Action(ShowAllStationTrackPartsToSelectForShortestPath) },
            ];
        }

        private void ShowAllParksToSelectForBorders()
        {
            IEnumerable<ConsoleCommand> parkCommands = stationTopologyService.GetAllParks()
                .Select(x => new ConsoleCommand() { Name = x.Name, Action = new Action(() => WriteBorderToConsole(x.Name)) });
            ConsoleCommand selectedPark = Prompt.Select("please select the park", parkCommands);
            selectedPark.Action();
        }

        private void WriteBorderToConsole(string parkName)
        {
            TrackBorder borders = stationTopologyService.GetBorder(parkName);
            int i = 1;
            foreach (Common.DAL.DAO.Point item in borders.BorderPoints)
            {
                Console.WriteLine($"{i}: {item.Name}");
            }
        }
        private void ShowAllStationTrackPartsToSelectForShortestPath()
        {
            IEnumerable<string> trackPartsCommands = stationTopologyService.GetTrackParts()
                .Select(x => x.Name);
            string selectedParkStart = Prompt.Select<string>("please select track part for start", trackPartsCommands);
            string selectedParkEnd = Prompt.Select<string>($"Start track part:{selectedParkStart}. Please select track for finish", trackPartsCommands);

            if (pathService.GetShortestPath(selectedParkStart, selectedParkEnd, out List<Common.DAL.DAO.TrackPart> shortestPath))
            {
                foreach (Common.DAL.DAO.TrackPart path in shortestPath)
                {
                    Console.WriteLine(path.Name);
                }
            }
            else
            {
                Console.WriteLine("No way!");
            }
        }
    }
}
