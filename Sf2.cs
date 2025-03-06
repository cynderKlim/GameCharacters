public class StreetFighterII : Character
{
  public List<string> Alias { get; set; } = [];

  public override string Display()
  {
    return $"Id: {Id}\nName: {Name}\nDescription: {Description}\nMoves: {Moves}\nAlias: {string.Join(", ", Alias)}\n";
  }
}