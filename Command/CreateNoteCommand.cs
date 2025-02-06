public class CreateNoteCommand : Command
{
    public CreateNoteCommand(DependencyProvider dependencyProvider)
        : base("create-note", "Create and save a new note.", dependencyProvider) { }

    public override void Execute()
    {
        INoteService noteService = Get<INoteService>();
        IUserService userService = Get<IUserService>();
        User? user = userService.GetLoggedInUser();
        if (user == null)
        {
            Console.WriteLine("You must log in to do this.");
            return;
        }

        Console.Write("Enter a title:");
        string title = Console.ReadLine()!;
        if (!noteService.ValidateNoteTitle(title))
        {
            Console.WriteLine("Title may not be null or empty.");
            return;
        }

        Console.Write("Enter content:");
        string content = Console.ReadLine()!;

        try
        {
            Note note = noteService.CreateNote(title, content, user);
            Console.WriteLine($"Saved note with title '{note.Title}'");
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error: " + exception.Message);
        }
    }
}
