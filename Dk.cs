public class DonkeyKong : Character
{
  public List<string> Alias { get; set; } = [];

  public override string Display()
  {
    return $"Id: {Id}\nSpecies: {Species}\nName: {Name}\nDescription: {Description}\nAlias: {string.Join(", ", Alias)}\n";
  }
}