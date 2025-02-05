public class LoginCommand : Command
{
    public LoginCommand(DependencyProvider dependencyProvider)
        : base("login", "Login with username and password", dependencyProvider) { }

    public override void Execute()
    {
        Console.Write("Enter a username: ");
        string username = Console.ReadLine()!;

        Console.Write("Enter a password: ");
        string password = Console.ReadLine()!;

        Console.WriteLine("TODO: Logged in");

        IMenuService menuService = Get<IMenuService>();
        menuService.SetMenu(new MainMenu(dependencyProvider));
    }
}
