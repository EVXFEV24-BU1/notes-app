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

        Console.Write("Enter your birth date: ");
        string birthString = Console.ReadLine()!;

        DateTime birthDate = DateTime.Parse(birthString);
        birthDate = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);

        IUserService userService = Get<IUserService>();
        try
        {
            User user = userService.Register(username, password, birthDate);
            Console.WriteLine($"Success! Regisered user with name '{user.Name}'");
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error: " + exception.Message);
            if (exception.InnerException != null)
            {
                Console.WriteLine("Inner exception: " + exception.InnerException.Message);
            }
        }
    }
}
