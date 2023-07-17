using WordFractals.Helpers;
namespace WordFractals.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();
	}

    private void StartBtn_Clicked(object sender, EventArgs e)
    {

        string startingWord = "bee";

        WordFractal.Start(startingWord);
        WordFractal.LetterCombo lc = WordFractal.GetParentCombo(startingWord);
        NavHelper.ToWordPage(lc);
    }

}

