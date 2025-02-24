[MenuCommand(typeof(MainMenu))]
public class ListNotesCommand : Command
{
    public ListNotesCommand(DependencyProvider dependencyProvider)
        : base("list-notes", "List all saved note titles", dependencyProvider) { }

    public override void Execute()
    {
        INoteService noteService = Get<INoteService>();
        IUserService userService = Get<IUserService>();
        // Kolla om användaren är inloggad
        User? user = userService.GetLoggedInUser();
        if (user == null)
        {
            Console.WriteLine("You must log in to do this.");
            return;
        }

        Console.WriteLine("Your notes:");
        List<Note> notes = noteService.GetByUserId(user.Id);
        foreach (Note note in notes)
        {
            Console.WriteLine($" - {note.Title} ({note.Content.Length} chars)");
        }
    }
}
