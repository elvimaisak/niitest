namespace NIIAS.RailRoadStationExplorer.Common.DAL.DAO
{
    public class Station
    {
        /// <summary>
        /// в реальном проекте во всех DAO-объектах был бы уникальный Id, т.к. вероятно что Name не уникальное поле в некоторых случаях.
        /// для данного проекта примем, что станция у нас одна, и все Name уникальны в пределах одной станции
        /// </summary>
        //public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<TrackPart> TrackParts { get; set; }
    }
}
