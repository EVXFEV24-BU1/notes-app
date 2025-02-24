public class MainMenu : Menu
{
    public MainMenu(DependencyProvider dependencyProvider) : base(dependencyProvider)
    {
        this.RegisterCommand(new CreateNoteCommand(dependencyProvider));
        this.RegisterCommand(new ListNotesCommand(dependencyProvider));
        this.RegisterCommand(new ViewNoteCommand(dependencyProvider));
    }

    public override void Display()
    {
        Console.WriteLine("What would you like to do?");
        DisplayCommands();
    }
}
