namespace JoskayaMobila.Views;

public partial class AllNotes : ContentPage
{
	public AllNotes()
	{
		InitializeComponent();

		BindingContext = new Models.AllNotes();
        
	}

    private void OnTextChanged(object sender, EventArgs e)
    {
        var viewModel = (Models.AllNotes)BindingContext;
        viewModel.FilterNotes(searchBar.Text);
        
    }

    protected override void OnAppearing()
    {
        ((Models.AllNotes)BindingContext).LoadNotes();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NotePage));
    }

    private async void NotesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            var note = (Models.Note)e.CurrentSelection[0];

            await Shell.Current.GoToAsync($"{nameof(NotePage)}?{nameof(NotePage.ItemId)}={note.Filename}");

            notesCollection.SelectedItem = null;
        }
    }
}