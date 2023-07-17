using WordFractals.Pages;
namespace WordFractals.Helpers
{
    public static class NavHelper
    {
        public static async void ToWordPage(WordFractal.LetterCombo letterCombo, string parentWord="")
        {

            await Shell.Current.GoToAsync(nameof(WordPage), true, new Dictionary<string, object>
            {
                { nameof(WordFractal.LetterCombo), letterCombo },
                {"ParentWord", parentWord }
            });
        }        

        public static async void ToHome()
        {
            
            await Shell.Current.GoToAsync(nameof(MainPage), true);
        }
    }
}
