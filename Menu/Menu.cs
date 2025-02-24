using System.Reflection;

// Basklassen för alla menyer
// Den innehåller kommandon som tillhör menyn
public abstract class Menu
{
    private List<Command> commands = new List<Command>();

    public Menu(DependencyProvider dependencyProvider)
    {
        // AppDomain och Assembly är representationer av program.
        // 'notes-app' är ett av dem (för det finns fler som inbyggda C# saker exempelvis)
        AppDomain domain = AppDomain.CurrentDomain;
        Assembly[] assemblies = domain.GetAssemblies();

        // Vi behöver egentligen bara vår egen assembly (program) men vi vet inte vilken den är
        // så vi loopar igenom alla
        foreach (Assembly assembly in assemblies)
        {
            // Hämta alla klasser, interfaces och andra typer (Type) i vårt program
            // Typerna är exempelvis: Command, Note, User, IUserService och så vidare
            Type[] types = assembly.GetTypes();

            // Loopa igenom alla typer
            foreach (Type type in types)
            {
                // Kolla om typen (klassen) är ett kommando
                // genom att se om typen ärver av 'Command'
                if (type.IsSubclassOf(typeof(Command)))
                {
                    // Skapa en instans av kommando klassen genom reflection
                    // och skicka in dependency providern som argument
                    Command command = (Command)Activator.CreateInstance(type, dependencyProvider);

                    // Registrera kommandot (automatiskt)
                    RegisterCommand(command);
                }
            }
        }
    }

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
