using System;
using System.Diagnostics;

namespace markov;

class Program
{
    static void Main()
    {
        //take input and split it into 3 variables
        string dir = Console.ReadLine()!;
        string[] inputs = dir.Split(' ');

        int N = int.Parse(inputs[1]); //length of phrases
        int L = int.Parse(inputs[2]); //length of story

        // *** SortedDictionary ***

        Stopwatch s1 = new Stopwatch(); //stopwatch
        s1.Start();

        SortedDictionary<string, MarkovEntry> entries1 = new SortedDictionary<string, MarkovEntry>(); //MarkovEntries

        foreach (string file in Directory.EnumerateFiles(inputs[0])) //goes through each file in folder, took a lot of looking through .NET page to find Directory.EnumerateFiles
        {
            string text = File.ReadAllText(file);
            string[] words = text.Split(' ', '\n'); //array for each individual file

            //create combination array for phrases based on N length
            string[] combine = new string[N];
            Array.Copy(words, 0, combine, 0, N);

            //cycle through loop to add phrases to MarkovEntry
            for (int i = 0; i < words.Length; i++)
            {
                string key = string.Join(" ", combine); //key becomes each combined phrase
                if (!entries1.ContainsKey(key))
                {
                    entries1[key] = new MarkovEntry(key); //create entry
                }
                entries1[key].Add(words[i]); //adds next word
                Array.Copy(combine, 1, combine, 0, N - 1); //creates next combined phrase
                combine[N - 1] = words[i]; //sets up next word
            }

        }

        Random rand1 = new Random(); //create random object

        //base story on first two words from the first text in the directory
        string[] files1 = Directory.GetFiles(inputs[0]);
        string[] wordsList1 = File.ReadAllText(files1[0]).Split(' ');
        int idx1 = rand1.Next(wordsList1.Length);

        List<string> story1 = new List<string>(); //thought Microsoft List would be error free compared to my own linkedlist
        story1.Add(wordsList1[idx1]);
        story1.Add(wordsList1[idx1+1]);

        // Generate the rest of the story
        while (story1.Count < L)
        {
            // Get the last N words from the story
            string key = string.Join(" ", story1.GetRange(story1.Count - N, N));

            // If the key doesn't exist in the dictionary, break out of the loop
            if (!entries1.ContainsKey(key))
            {
                break;
            }

            // Add a random word for the key to the story
            story1.Add(entries1[key].RandomWord(rand1));
        }
        s1.Stop();
        TimeSpan ts1 = s1.Elapsed;

        // Print the story
        Console.WriteLine(string.Join(" ", story1));
        Console.WriteLine($"Time elapsed for SortedDictionary: {ts1} for {L} words");
        Console.WriteLine();


        // *** TreeLibrary ***

        Stopwatch s2 = new Stopwatch(); //stopwatch
        s2.Start();

        TreeSymbolTable<string, MarkovEntry> entries2 = new TreeSymbolTable<string, MarkovEntry>(); //MarkovEntries

        foreach (string file in Directory.EnumerateFiles(inputs[0])) //goes through each file in folder, took a lot of looking through .NET page to find Directory.EnumerateFiles
        {
            string text = File.ReadAllText(file);
            string[] words = text.Split(' ', '\n'); //array for each individual file

            //create combination array for phrases based on N length
            string[] combine = new string[N];
            Array.Copy(words, 0, combine, 0, N);

            //cycle through loop to add phrases to MarkovEntry
            for (int i = 0; i < words.Length; i++)
            {
                string key = string.Join(" ", combine); //key becomes each combined phrase
                if (!entries2.ContainsKey(key))
                {
                    entries2[key] = new MarkovEntry(key); //create entry
                }
                entries2[key].Add(words[i]); //adds next word
                Array.Copy(combine, 1, combine, 0, N - 1); //creates next combined phrase
                combine[N - 1] = words[i]; //sets up next word
            }

        }

        Random rand2 = new Random(); //create random object

        //base story on first two words from the first text in the directory
        string[] files2 = Directory.GetFiles(inputs[0]);
        string[] wordsList2 = File.ReadAllText(files2[0]).Split(' ');
        int idx2 = rand2.Next(wordsList2.Length);

        List<string> story2 = new List<string>(); //thought Microsoft List would be error free compared to my own linkedlist
        story2.Add(wordsList2[idx2]);
        story2.Add(wordsList2[idx2 + 1]);

        // Generate the rest of the story
        while (story2.Count < L)
        {
            // Get the last N words from the story
            string key = string.Join(" ", story2.GetRange(story2.Count - N, N));

            // If the key doesn't exist in the dictionary, break out of the loop
            if (!entries2.ContainsKey(key))
            {
                break;
            }

            // Add a random word for the key to the story
            story2.Add(entries2[key].RandomWord(rand2));
        }
        s2.Stop();
        TimeSpan ts2 = s2.Elapsed;

        // Print the story
        Console.WriteLine(string.Join(" ", story2));
        Console.WriteLine($"Time elapsed for TreeLibrary: {ts2} for {L} words");
        Console.WriteLine();


        // *** SymbolTable ***

        Stopwatch s3 = new Stopwatch(); //stopwatch
        s3.Start();

        ListSymbolTable<string, MarkovEntry> entries3 = new ListSymbolTable<string, MarkovEntry>(); //MarkovEntries

        foreach (string file in Directory.EnumerateFiles(inputs[0])) //goes through each file in folder, took a lot of looking through .NET page to find Directory.EnumerateFiles
        {
            string text = File.ReadAllText(file);
            string[] words = text.Split(' ', '\n'); //array for each individual file

            //create combination array for phrases based on N length
            string[] combine = new string[N];
            Array.Copy(words, 0, combine, 0, N);

            //cycle through loop to add phrases to MarkovEntry
            for (int i = 0; i < words.Length; i++)
            {
                string key = string.Join(" ", combine); //key becomes each combined phrase
                if (!entries2.ContainsKey(key))
                {
                    entries2[key] = new MarkovEntry(key); //create entry
                }
                entries2[key].Add(words[i]); //adds next word
                Array.Copy(combine, 1, combine, 0, N - 1); //creates next combined phrase
                combine[N - 1] = words[i]; //sets up next word
            }

        }

        Random rand3 = new Random(); //create random object

        //base story on first two words from the first text in the directory
        string[] files3 = Directory.GetFiles(inputs[0]);
        string[] wordsList3 = File.ReadAllText(files2[0]).Split(' ');
        int idx3 = rand3.Next(wordsList2.Length);

        List<string> story3 = new List<string>(); //thought Microsoft List would be error free compared to my own linkedlist
        story3.Add(wordsList2[idx3]);
        story3.Add(wordsList2[idx3 + 1]);

        // Generate the rest of the story
        while (story3.Count < L)
        {
            // Get the last N words from the story
            string key = string.Join(" ", story3.GetRange(story3.Count - N, N));

            // If the key doesn't exist in the dictionary, break out of the loop
            if (!entries3.ContainsKey(key))
            {
                break;
            }

            // Add a random word for the key to the story
            story3.Add(entries2[key].RandomWord(rand2));
        }
        s3.Stop();
        TimeSpan ts3 = s3.Elapsed;

        // Print the story
        Console.WriteLine(string.Join(" ", story3));
        Console.WriteLine($"Time elapsed for ListSymbolTable: {ts3} for {L} words");
        Console.WriteLine();

    }
}
