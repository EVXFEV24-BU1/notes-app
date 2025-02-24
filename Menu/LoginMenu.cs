public class LoginMenu : Menu
{
    public LoginMenu(DependencyProvider dependencyProvider) : base(dependencyProvider)
    {
    }

    public override void Display()
    {
        Console.WriteLine("Welcome! Please register a user or login.");
        DisplayCommands();
    }
}
