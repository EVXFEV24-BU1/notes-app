[MenuCommand(typeof(MainMenu))]
public class ViewNoteCommand : Command
{
    public ViewNoteCommand(DependencyProvider dependencyProvider)
        : base("view-note", "View the details and content of a note.", dependencyProvider) { }

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

        Console.Write("Enter a title: ");
        string title = Console.ReadLine()!;
        if (!noteService.ValidateNoteTitle(title))
        {
            Console.WriteLine("Title may not be null or empty.");
            return;
        }

        try
        {
            Note? note = noteService.GetByTitleAndUserId(title, user.Id);
            if (note == null)
            {
                Console.WriteLine($"No such note exists.");
            }
            else
            {
                Console.WriteLine($"Note: {note.Title}");
                Console.WriteLine($"Owner: {note.User.Name}");
                Console.WriteLine(
                    $"Update dates: {string.Join(", ", note.UpdateHistoryDates.Select(date => date.ToShortDateString()))}"
                );
                Console.WriteLine($"\n{note.Content}");
            }
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
