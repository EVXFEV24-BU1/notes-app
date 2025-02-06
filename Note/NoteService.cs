public interface INoteService
{
    Note CreateNote(string title, string content, User user);
    List<Note> GetByUserId(Guid userId);

    bool ValidateNoteTitle(string title)
    {
        return !string.IsNullOrWhiteSpace(title);
    }
}

public class DefaultNoteService : INoteService
{
    private INoteRepository noteRepository;
    private IUserRepository userRepository;

    public DefaultNoteService(DependencyProvider dependencyProvider)
    {
        this.noteRepository = dependencyProvider.Get<INoteRepository>();
        this.userRepository = dependencyProvider.Get<IUserRepository>();
    }

    public Note CreateNote(string title, string content, User user)
    {
        INoteService self = this;
        if (!self.ValidateNoteTitle(title))
        {
            throw new ArgumentException("Title may not be null or empty");
        }

        Note note = new Note(title, content, user);
        user.Notes.Add(note);
        noteRepository.Save(note);
        userRepository.Save(user);
        return note;
    }

    public List<Note> GetByUserId(Guid userId)
    {
        return noteRepository.GetByUserId(userId);
    }
}
