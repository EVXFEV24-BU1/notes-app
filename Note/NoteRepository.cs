// Basklassen för alla note repositories. Fördelen med detta är att vi enkelt
// kan bygga ut med andra repositories, t.ex för databaser som vi kommer att göra vecka 2.
using Microsoft.EntityFrameworkCore;

public interface INoteRepository
{
    void Save(Note note, Guid userId);
    List<Note> GetByUserId(Guid userId);
}

// Vår nuvarande NoteRepository sparar notes i en enkel lista.
public class ListNoteRepository : INoteRepository
{
    private List<Note> notes = new List<Note>();

    public void Save(Note note, Guid userId)
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

public class PostgresNoteRepository : INoteRepository
{
    public List<Note> GetByUserId(Guid userId)
    {
        using AppContext context = new AppContext();
        return context
            .Notes.Include(n => n.User)
            .Where(note => note.User.Id.Equals(userId))
            .ToList();
    }

    public void Save(Note note, Guid userId)
    {
        // TODO: Do not add if already exists
        using AppContext context = new AppContext();
        note.User = context.Users.Find(userId)!;
        context.Notes.Add(note);
        context.SaveChanges();
    }
}
