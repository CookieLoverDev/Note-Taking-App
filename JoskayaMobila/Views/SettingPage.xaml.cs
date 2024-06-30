namespace JoskayaMobila.Views;

public partial class SettingPage : ContentPage
{
	public SettingPage()
	{
		InitializeComponent();
	}

	private void DarkTheme_On(object sender, ToggledEventArgs e)
	{
		if (e.Value)
			Application.Current.UserAppTheme = AppTheme.Dark;
		else
            Application.Current.UserAppTheme = AppTheme.Light;
    }
}