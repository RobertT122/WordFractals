using WordFractals.Pages;

namespace WordFractals;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(WordPage), typeof(WordPage));
	}
}
