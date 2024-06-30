namespace JoskayaMobila.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
    private string appDataPath = FileSystem.AppDataDirectory;

    public string ItemId
    {
        set { LoadNote(value); }
    }

    public NotePage()
	{
		InitializeComponent();
		
		string randomFileName = $"{Path.GetRandomFileName()}.txt";

		LoadNote(Path.Combine(appDataPath, randomFileName));
	}

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
        {
            File.Delete(note.Filename);

            string newFileName = Path.Combine(appDataPath, $"{note.Title}.txt");
            note.Filename = newFileName;

            File.WriteAllText(note.Filename, TextEditor.Text);
        }
        
        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
        {
            if (File.Exists(note.Filename))
                File.Delete(note.Filename);
        }

        await Shell.Current.GoToAsync("..");
    }

    private void LoadNote(string fileName)
	{
		Models.Note noteModel = new Models.Note();
		noteModel.Filename = fileName;

		if (File.Exists(fileName))
		{
			noteModel.Date = File.GetCreationTime(fileName);
			noteModel.Text = File.ReadAllText(fileName);
            noteModel.Title = Path.GetFileNameWithoutExtension(fileName);
		}

		BindingContext = noteModel;
	}
}