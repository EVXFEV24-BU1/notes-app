public class MainMenu : Menu
{
    public MainMenu(DependencyProvider dependencyProvider) { }

    public override void Display()
    {
        Console.WriteLine("What would you like to do?");
        DisplayCommands();
    }
}
