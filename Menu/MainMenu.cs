public class MainMenu : Menu
{
    public MainMenu(DependencyProvider dependencyProvider)
    {
        this.RegisterCommand(new CreateNoteCommand(dependencyProvider));
        this.RegisterCommand(new ListNotesCommand(dependencyProvider));
    }

    public override void Display()
    {
        Console.WriteLine("What would you like to do?");
        DisplayCommands();
    }
}
