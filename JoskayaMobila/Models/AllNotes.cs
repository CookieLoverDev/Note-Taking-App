using System.Collections.ObjectModel;

namespace JoskayaMobila.Models;

internal class AllNotes
{
    public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();
    private List<Note> allNotes = new List<Note>();

    public AllNotes() =>
        LoadNotes();

    public void LoadNotes()
    {
        Notes.Clear();

        string appDataPath = FileSystem.AppDataDirectory;

        allNotes = Directory

                                    .EnumerateFiles(appDataPath, "*.txt")

                                    .Select(filename => new Note()
                                    {
                                        Filename = filename,
                                        Title = Path.GetFileNameWithoutExtension(filename),
                                        Text = File.ReadAllText(filename),
                                        Date = File.GetLastWriteTime(filename)
                                    })

                                    .OrderByDescending(note => note.Date)
                                    .ToList();

        foreach (Note note in allNotes)
            Notes.Add(note);
    }

    public void FilterNotes(string query)
    {
        if (string.IsNullOrWhiteSpace(query)) LoadNotes();
        else
        {
            var filteredNotes = allNotes.Where(note => note.Title.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

            Notes.Clear();

            foreach (var note in filteredNotes)
                Notes.Add(note);
        }
    }
}