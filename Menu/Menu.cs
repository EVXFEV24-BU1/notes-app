// Basklassen för alla menyer
// Den innehåller kommandon som tillhör menyn
public abstract class Menu
{
    private List<Command> commands = new List<Command>();

    // Registrera ett kommandon för menyn, detta görs i menyns constructor (se LoginMenu för ett exempel)
    public void RegisterCommand(Command command)
    {
        this.commands.Add(command);
    }

    // Skriv ut alla kommandon som finns tillgängliga i menyn på ett smidigt sätt
    protected void DisplayCommands()
    {
        foreach (Command command in commands)
        {
            Console.WriteLine($" - {command.Name} - {command.Description}");
        }
    }

    // Försök exekvera ett kommando som finns i menyn
    public void ExecuteCommand(string input)
    {
        foreach (Command command in commands)
        {
            if (command.Name.Equals(input))
            {
                command.Execute();
                return;
            }
        }

        throw new ArgumentException($"No command with name '{input}' was found.");
    }

    // Alla menyer skall "visas upp" och då används denna metod
    public abstract void Display();
}
