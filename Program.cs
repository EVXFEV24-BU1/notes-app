using System.Runtime.Intrinsics.Arm;

class Program
{
    static void Main(string[] args)
    {
        // Skapa en dependency provider som håller koll på alla dependencies
        DependencyProvider dependencyProvider = new DependencyProvider();

        // Registrera en ny menu service
        dependencyProvider.Register(new DefaultMenuService());

        // Registrera andra dependencies vi behöver i programmet
        dependencyProvider.Register(new ListUserRepository());
        dependencyProvider.Register(new DefaultUserService(dependencyProvider));
        dependencyProvider.Register(new ListNoteRepository());
        dependencyProvider.Register(new DefaultNoteService(dependencyProvider));

        // Hämta ut menu service implementationen och sätt start menyn till LoginMenu
        IMenuService menuService = dependencyProvider.Get<IMenuService>();
        // Skicka med 'dependencyProvider' till LoginMenu så att kommandon senare
        // kan få tillgång till alla dependencies
        menuService.SetMenu(new LoginMenu(dependencyProvider));

        while (true)
        {
            string command = Console.ReadLine()!;

            try
            {
                // Försök exekvera ett kommando som finns i den aktuella menyn
                menuService.GetCurrentMenu().ExecuteCommand(command);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}

// Håller koll på alla dependencies (services, repositories m.m) som vi har i applikationen.
public class DependencyProvider
{
    // Vi kan spara vad som helst, oavsett vilken typ.
    private List<object> dependencies = new List<object>();

    // Registrera dependencies, som services och repositories
    // Register(new DefaultMenuService()) exempelvis
    public void Register(object dependency)
    {
        this.dependencies.Add(dependency);
    }

    // Hämta ut ett visst dependency, som en service eller repository
    // Get<IMenuService> exempelvis
    public T Get<T>()
    {
        Type type = typeof(T);
        foreach (object dependency in dependencies)
        {
            if (type.IsAssignableFrom(dependency.GetType()))
            {
                return (T)dependency;
            }
        }

        throw new ArgumentException("Dependency has not been registered");
    }
}
