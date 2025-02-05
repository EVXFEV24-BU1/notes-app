public class LoginMenu : Menu
{
    public LoginMenu(DependencyProvider dependencyProvider)
    {
        this.RegisterCommand(new LoginCommand(dependencyProvider));
        this.RegisterCommand(new RegisterUserCommand(dependencyProvider));
    }

    public override void Display()
    {
        Console.WriteLine("Welcome! Please register a user or login.");
        DisplayCommands();
    }
}
