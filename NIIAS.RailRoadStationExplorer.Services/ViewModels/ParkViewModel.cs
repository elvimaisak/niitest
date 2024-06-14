public struct ParkViewModel
{
    public string Name { get; set; }
    public IEnumerable<string> Tracks { get; set; }

    public override string ToString()
    {
        return Name;
    }
}