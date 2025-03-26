using NLog;
using System.Reflection;
using System.Text.Json;
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");

// deserialize json from files into List(s)<>
string marioFileName = "mario.json";
List<Mario> marios = [];
string dkFileName = "dk.json";
List<DonkeyKong> donkeyKongs = JsonSerializer.Deserialize<List<DonkeyKong>>(File.ReadAllText(dkFileName))!;
string sf2FileName = "sf2.json";
List<StreetFighterII> streetFighters = JsonSerializer.Deserialize<List<StreetFighterII>>(File.ReadAllText(sf2FileName))!;

// check if file exists
if (File.Exists(marioFileName) && File.Exists(dkFileName) && File.Exists(sf2FileName))
{
  marios = JsonSerializer.Deserialize<List<Mario>>(File.ReadAllText(marioFileName))!;
  logger.Info($"File deserialized {marioFileName}");
  donkeyKongs = JsonSerializer.Deserialize<List<DonkeyKong>>(File.ReadAllText(dkFileName))!;
  logger.Info($"File deserialized {dkFileName}");
  streetFighters = JsonSerializer.Deserialize<List<StreetFighterII>>(File.ReadAllText(sf2FileName))!;
  logger.Info($"File deserialized {sf2FileName}");
  // TODO: Modify the Game Characters assignment. Add menu option(s) to edit existing characters.

  // You will need to provide a way of locating the character to edit (similar to deleting a character)
  // You may find the FindIndex method useful
  // You can / should re-use the static method InputCharacter when editing characters (similar to adding a new character)
  // Make sure you serialize the list into the appropriate json file (similar to deleting a character)
}

do
{
  Console.WriteLine("1) Mario Options");
  Console.WriteLine("2) Donkey Kong Options");
  Console.WriteLine("3) Street Fighter II Options");
  Console.WriteLine("Enter to quit");

  string? choice = Console.ReadLine();
  logger.Info("User choice: {Choice}", choice);

  if (choice == "1")
  {
    // display choices to user
    Console.WriteLine("1) Display Mario Characters");
    Console.WriteLine("2) Add Mario Character");
    Console.WriteLine("3) Remove Mario Character");
    Console.WriteLine("Enter to quit");

    // input selection
    choice = Console.ReadLine();
    logger.Info("User choice: {Choice}", choice);
    if (choice == "1")
    {
      // Display Mario Characters
      foreach (var c in marios)
      {
        Console.WriteLine(c.Display());
      }
    }
    else if (choice == "2")
    {
      // Add Mario Character
      // Generate unique Id
      Mario mario = new()
      {
        Id = marios.Count == 0 ? 1 : marios.Max(c => c.Id) + 1
      };
      InputCharacter(mario);
      // Add Character
      marios.Add(mario);
      File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
      logger.Info($"Character added: {mario.Name}");
    }
    else if (choice == "3")
    {
      // Remove Mario Character
      Console.WriteLine("Enter the Id of the character to remove:");
      if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
      {
        Mario? character = marios.FirstOrDefault(c => c.Id == Id);
        if (character == null)
        {
          logger.Error($"Character Id {Id} not found");
        }
        else
        {
          marios.Remove(character);
          // serialize list<marioCharacter> into json file
          File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
          logger.Info($"Character Id {Id} removed");
        }
      }
      else
      {
        logger.Error("Invalid Id");
      }
    }
    else if (string.IsNullOrEmpty(choice))
    {
      break;
    }
    else
    {
      logger.Info("Invalid choice");
    }
  }
  else if (choice == "2")
  {
    Console.WriteLine("1) Display Donkey Kong Characters");
    Console.WriteLine("2) Add Donkey Kong Character");
    Console.WriteLine("3) Remove Donkey Kong Character");
    Console.WriteLine("Enter to quit");

    // input selection
    choice = Console.ReadLine();
    logger.Info("User choice: {Choice}", choice);

    if (choice == "1")
    {
      // Display Donkey Kong Characters
      foreach (var c in donkeyKongs)
      {
        Console.WriteLine(c.Display());
      }
    }
    else if (choice == "2")
    {
      // Add Donkey Kong Character
      // Generate unique Id
      DonkeyKong donkeyKong = new()
      {
        Id = donkeyKongs.Count == 0 ? 1 : donkeyKongs.Max(c => c.Id) + 1
      };
      // Input Species, Name, Description
      Console.WriteLine("Enter Species: ");
      donkeyKong.Species = Console.ReadLine();
      Console.WriteLine("Enter Name: ");
      donkeyKong.Name = Console.ReadLine();
      Console.WriteLine("Enter Description: ");
      donkeyKong.Description = Console.ReadLine();
    }
    else if (choice == "3")
    {
      // Remove Donkey Kong Character
      Console.WriteLine("Enter the Id of the character to remove:");
      if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
      {
        DonkeyKong? character = donkeyKongs.FirstOrDefault(c => c.Id == Id);
        if (character == null)
        {
          logger.Error($"Character Id {Id} not found");
        }
        else
        {
          donkeyKongs.Remove(character);
          // serialize list<dkCharacter> into json file
          File.WriteAllText(dkFileName, JsonSerializer.Serialize(donkeyKongs));
          logger.Info($"Character Id {Id} removed");
        }
      }
      else
      {
        logger.Error("Invalid Id");
      }
    }
    else if (string.IsNullOrEmpty(choice))
    {
      break;
    }
    else
    {
      logger.Info("Invalid choice");
    }
  }
  else if (choice == "3")
  {
    Console.WriteLine("1) Display Street Fighter II Characters");
    Console.WriteLine("2) Add Street Fighter II Character");
    Console.WriteLine("3) Remove Street Fighter II Character");
    Console.WriteLine("Enter to quit");

    // input selection
    choice = Console.ReadLine();
    logger.Info("User choice: {Choice}", choice);

    if (choice == "1")
    {
      // Display Street Fighter II Characters
      foreach (var c in streetFighters)
      {
        Console.WriteLine(c.Display());
      }
    }
    else if (choice == "2")
    {
      // Add Street Fighter II Character
      // Generate unique Id
      StreetFighterII streetFighter = new()
      {
        Id = streetFighters.Count == 0 ? 1 : streetFighters.Max(c => c.Id) + 1
      };
      // Input Name, Description
      Console.WriteLine("Enter Name: ");
      streetFighter.Name = Console.ReadLine();
      Console.WriteLine("Enter Description: ");
      streetFighter.Description = Console.ReadLine();
      Console.WriteLine("Enter Moves separated by commas: ");
      streetFighter.Moves = Console.ReadLine();
    }
    else if (choice == "3")
    {
      // Remove Street Fighter II Character
      Console.WriteLine("Enter the Id of the character to remove:");
      if (UInt32.TryParse(Console.ReadLine(), out UInt32 Id))
      {
        StreetFighterII? character = streetFighters.FirstOrDefault(c => c.Id == Id);
        if (character == null)
        {
          logger.Error($"Character Id {Id} not found");
        }
        else
        {
          streetFighters.Remove(character);
          // serialize list<sf2Character> into json file
          File.WriteAllText(sf2FileName, JsonSerializer.Serialize(streetFighters));
          logger.Info($"Character Id {Id} removed");
        }
      }
      else
      {
        logger.Error("Invalid Id");
      }
    }
    else if (string.IsNullOrEmpty(choice))
    {
      break;
    }
    else
    {
      logger.Info("Invalid choice");
    }
  }
} while (true);

logger.Info("Program ended");

static void InputCharacter(Character character)
{
  Type type = character.GetType();
  PropertyInfo[] properties = type.GetProperties();
  var props = properties.Where(p => p.Name != "Id");
  foreach (PropertyInfo prop in props)
  {
    if (prop.PropertyType == typeof(string))
    {
      Console.WriteLine($"Enter {prop.Name}:");
      prop.SetValue(character, Console.ReadLine());
    }
    else if (prop.PropertyType == typeof(List<string>))
    {
      List<string> list = [];
      do
      {
        Console.WriteLine($"Enter {prop.Name} or (enter) to quit:");
        string response = Console.ReadLine()!;
        if (string.IsNullOrEmpty(response))
        {
          break;
        }
        list.Add(response);
      } while (true);
      prop.SetValue(character, list);
    }
  }
}