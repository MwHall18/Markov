namespace markov;

class MarkovEntry
{
    private SortedDictionary<string, int> nextWords;
    private string root;
    private int count;

    public MarkovEntry(string root)
    {
        this.root = root;
        this.count = 0;
        this.nextWords = new SortedDictionary<string, int>();
    }

    public void Add(string word)
    {
        //if it already contains the next word just add it to itself
        if (nextWords.ContainsKey(word))
        {
            nextWords[word]++;
        }
        //else, add it and set the count = 1
        else
        {
            nextWords[word] = 1;
            count++;
        }
    }

    public string RandomWord(Random rand)
    {
        int total = nextWords.Values.Sum();
        int r = rand.Next(total);
        int sum = 0;
        foreach (var item in nextWords)
        {
            sum += item.Value;
            if (r < sum)
                return item.Key;
        }
        return "";
    }

}