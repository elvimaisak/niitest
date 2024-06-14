using NIIAS.RailRoadStationExplorer.PathFinder;
using NIIAS.RailRoadStationExplorer.Services;
using Sharprompt;

namespace NIIAS.RailRoadStationExplorer
{
    internal class ConsoleCommandService
    {
        private readonly IStationTopologyService stationTopologyService;
        private readonly IPathService pathService;

        public ConsoleCommandService(IStationTopologyService stationTopologyService, IPathService pathService) {
            this.stationTopologyService = stationTopologyService;
            this.pathService = pathService;
        }

        public IEnumerable<ConsoleCommand> GetMainCommands() {
            return
            [
               new ConsoleCommand{Name = "view park borders", Action = new Action(ShowAllParksToSelectForBorders) }, //с возможностью выбора парка для дальнейших действий
               new ConsoleCommand{Name = "get shortest path", Action = new Action(ShowAllStationTrackPartsToSelectForShortestPath) },
            ];
        }

        private void ShowAllParksToSelectForBorders() {
            var parkCommands = stationTopologyService.GetAllParks()
                .Select(x => new ConsoleCommand() { Name = x.Name, Action = new Action(() => WriteBorderToConsole(x.Name)) });
            var selectedPark = Prompt.Select("please select the park", parkCommands);
            selectedPark.Action();
        }

        private void WriteBorderToConsole(string parkName) {
            var borders = stationTopologyService.GetBorder(parkName);
            var i = 1;
            foreach (var item in borders.BorderPoints) {
                Console.WriteLine($"{i}: {item.Name}");
            }
        }
        private void ShowAllStationTrackPartsToSelectForShortestPath() {
            var trackPartsCommands = stationTopologyService.GetTrackParts()
                .Select(x => x.Name);
            var selectedParkStart = Prompt.Select<string>("please select track part for start", trackPartsCommands);
            var selectedParkEnd = Prompt.Select<string>($"Start track part:{selectedParkStart}. Please select track for finish", trackPartsCommands);

            if (pathService.GetShortestPath(selectedParkStart, selectedParkEnd, out var shortestPath)) {
                foreach (var path in shortestPath) {
                    Console.WriteLine(path.Name);
                }
            }
            else {
                Console.WriteLine("No way!");
            }
        }
    }
}
