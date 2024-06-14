// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using NIIAS.RailRoadStationExplorer;
using NIIAS.RailRoadStationExplorer.Common.DAL.Repository;
using NIIAS.RailRoadStationExplorer.PathFinder;
using NIIAS.RailRoadStationExplorer.PathFinder.Graph;
using NIIAS.RailRoadStationExplorer.Services;
using NIIAS.RailRoadStationExplorer.TestDataProvider;
using NIIAS.RailRoadStationExplorer.Topology;
using Sharprompt;


var dataSource = new TestDataBuilder()
                .WithPoints()
                .WithTrackParts()
                .WithTracks()
                .WithParks()
                .Build();

var serviceProvider = new ServiceCollection()
  .AddLogging()
  .AddSingleton(dataSource)
  .AddTransient<IParkRepository, ParkRepository>()
  .AddTransient<ITrackRepository, TrackRepository>()
  .AddTransient<IStationTopologyService, StationTopologyService>()
  .AddTransient<IGraphService, GraphService>()
  .AddTransient<IPathService, PathService>()
  .AddTransient<ConsoleCommandService>()
  .BuildServiceProvider();

var cmdService = serviceProvider.GetService<ConsoleCommandService>();



while (true) {
    var commands = cmdService.GetMainCommands();
    var selectedCommand = Prompt.Select("what would you like to do?", commands);
    selectedCommand.Action();
}




