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

        IMenuService menuService = Get<IMenuService>();
        IUserService userService = Get<IUserService>();
        try
        {
            User user = userService.Login(username, password);
            Console.WriteLine($"Success! Logged in as user '{user.Name}'");
            menuService.SetMenu(new MainMenu(dependencyProvider));
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error: " + exception.Message);
        }
    }
}
