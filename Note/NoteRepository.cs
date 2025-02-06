public interface INoteRepository
{
    void Save(Note note);
    List<Note> GetByUserId(Guid userId);
}

public class ListNoteRepository : INoteRepository
{
    private List<Note> notes = new List<Note>();

    public void Save(Note note)
    {
        int index = notes.FindIndex(existing => existing.Id.Equals(note.Id));
        if (index == -1)
        {
            notes.Add(note);
        }
        else
        {
            notes[index] = note;
        }
    }

    public List<Note> GetByUserId(Guid userId)
    {
        return notes.Where(note => note.User.Id.Equals(userId)).ToList();
    }
}
