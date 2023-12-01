using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dop1
{
    class Program
    {
        static void Main()
        {
            string text = "Вот дом, Который построил Джек. А это пшеница, Которая в темном чулане хранится В доме, Который построил Джек. А это веселая птица­ синица, Которая часто вору­ет пшеницу, Которая в темном чулане хранится В доме, Который построил Джек.";

            Dictionary<string, int> wordFrequency = CountWordFrequency(text);

            Console.WriteLine("Слово\t\tЧастота");
            Console.WriteLine("------------------------");
            foreach (var pair in wordFrequency)
            {
                Console.WriteLine($"{pair.Key}\t\t{pair.Value}");
            }
            Console.ReadKey();
        }

        static Dictionary<string, int> CountWordFrequency(string text)
        {
            string[] words = text.Split(new[] { ' ', '.', ',', '­', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> wordFrequency = new Dictionary<string, int>();

            foreach (var word in words)
            {
                string cleanedWord = word.Trim('­');
                if (wordFrequency.ContainsKey(cleanedWord))
                {
                    wordFrequency[cleanedWord]++;
                }
                else
                {
                    wordFrequency[cleanedWord] = 1;
                }
            }

            return wordFrequency;
        }
    }

}
