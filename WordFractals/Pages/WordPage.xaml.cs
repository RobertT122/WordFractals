using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml;
using WordFractals.Helpers;
namespace WordFractals.Pages;

public partial class WordPage : ContentPage, IQueryAttributable
{
	public WordFractal.LetterCombo LetterCombo {get; private set;}
    public string ParentWord { get; private set; }
    public bool Completed { get; private set; } = false;

	public WordPage()
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
		LetterCombo = (WordFractal.LetterCombo) query[nameof(WordFractal.LetterCombo)];
        ParentWord = query["ParentWord"] as string;
        SetLetterComboBinding();
    }

    void SetLetterComboBinding()
    {
        labelParent.Text = ParentWord;
        labelStart.Text = LetterCombo.Start.ToString();
        labelEnd.Text = LetterCombo.End.ToString();
        Completed = WordFractal.Instance.HasSolved(LetterCombo);
        SetVisibility();
    }

    void SetVisibility()
    {
        completedWordStack.Clear();
        if (Completed)
        {
            string text = GetPageWord();
            CreateCompletedWordStack(WordFractal.GetCombos(text));

        }
        editWordEntry.IsVisible = !Completed;
        if (!Completed) editWordEntry.Placeholder = $"{LetterCombo.Start} \t \t {LetterCombo.End}";
    }

    private void CreateCompletedWordStack(List<WordFractal.LetterCombo> letterCombos)
    {
        char lastLetter = '_';
        foreach(var  letterCombo in letterCombos) 
        {
            Label firstLetterLabel = new Label { Text = letterCombo.Start.ToString() };
            completedWordStack.Add(firstLetterLabel);
            Button redirectBtn = new Button();
            redirectBtn.Clicked += (s, e) => NavHelper.ToWordPage(letterCombo, GetPageWord());
            completedWordStack.Add(redirectBtn);
            lastLetter = letterCombo.End;
        }

        completedWordStack.Add( new Label { Text = lastLetter.ToString() } );
    }

    private string GetPageWord() => WordFractal.Instance.GetWord(LetterCombo);

    private void editWordEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = e.NewTextValue.ToLower();
        editWordEntry.Text = text;
        bool isValid = WordFractal.ValidComboWordPair(LetterCombo, text);
        editWordEntry.TextColor = isValid ? Color.Parse("White") : Color.Parse("Grey");
    }

    private void editWordEntry_Completed(object sender, EventArgs e)
    {
        WordFractal.Instance.SolveCombo(LetterCombo, editWordEntry.Text);
        SetLetterComboBinding();
    }

}