public class RegisterUserCommand : Command
{
    public RegisterUserCommand(DependencyProvider dependencyProvider)
        : base("register", "Register a new user with username and password", dependencyProvider) { }

    public override void Execute()
    {
        Console.Write("Enter a username: ");
        string username = Console.ReadLine()!;

        Console.Write("Enter a password: ");
        string password = Console.ReadLine()!;

        Console.WriteLine("TODO: Registered user with name and password");
        IUserService userService = Get<IUserService>();
        userService.Register();
    }
}
