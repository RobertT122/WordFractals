namespace WordFractals.Helpers;

public class WordFractal
{
    public static WordFractal Instance;

    public static void Start(string word)
    {
        WordFractal.Instance = new WordFractal(word);
    }

    public Dictionary<LetterCombo, string> ComboWordPairs { get; private set; }

    public WordFractal(string startingWord)
    {
        ComboWordPairs = new Dictionary<LetterCombo, string>();
        var startingCombo = GetParentCombo(startingWord);
        AddCombo(startingCombo);
        SolveCombo(startingCombo, startingWord);
    }


    // Public instance methods
    public void Reset()
    {
        ComboWordPairs.Clear();
    }

    public string GetWord(LetterCombo combo)
    {
        return ComboWordPairs[combo];
    }

    public bool HasSolved(LetterCombo combo)
    {
        return HasSeen(combo) && !String.IsNullOrEmpty( ComboWordPairs[combo] );
    }

    public void SolveCombo(LetterCombo combo, string word)
    {
        bool hasSeen = HasSeen(combo);
        bool hasNotSolved = !HasSolved(combo);
        bool isValid = ValidComboWordPair(combo, word);

        if (hasSeen && hasNotSolved && isValid)
        {
            ComboWordPairs[combo] = word;
            var combos = GetCombos(word);
            UpdateCombos(combos);
            foreach (var newCombo in GetCombos(word)) { AddCombo(newCombo); }
        }
    }

    //Private instance methods

    bool HasSeen(LetterCombo combo)
    {
        return ComboWordPairs.ContainsKey(combo);
    }

    void AddCombo(LetterCombo combo)
    {
        if (!HasSeen(combo)) ComboWordPairs.Add(combo, null);
    }

    void UpdateCombos(List<LetterCombo> combos)
    {
        foreach (LetterCombo combo in combos )
        {
            AddCombo(combo);
        }
    }

    //Public static helper methods

    static public LetterCombo GetParentCombo(string word)
    {
        char start = word[0];
        char end = word[word.Length - 1];
        return new LetterCombo { Start = start, End = end };
    }

    static public bool ValidComboWordPair(LetterCombo combo, string word)
    {
        if (string.IsNullOrEmpty(word)) return false;
        return word.Length > 2 && combo.Equals(GetParentCombo(word));
    }


    static public List<LetterCombo> GetCombos(string word)
    {
        var list = new List<LetterCombo>();
        for (int i = 0; i < word.Length - 1; i++)
        {
            char start = word[i];
            char end = word[i + 1];
            list.Add( new LetterCombo{ End = end, Start = start } );
        }

        return list;
    }

    // Structs

    public struct LetterCombo
    {
        public char Start;
        public char End;
    }
}
