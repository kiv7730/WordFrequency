using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordFrequency
{
    public class WordFrequencyGame
    {
        private const string Pattern = @"\s+";

        public string GetResult(string inputStr)
        {
            if (Regex.Split(inputStr, Pattern).Length == 1)
            {
                return inputStr + " 1";
            }
            else
            {
                List<WordCount> wordCountList = ToWordCounts(inputStr);
                Dictionary<string, List<WordCount>> map = GetListMap(wordCountList);
                List<WordCount> reducedWordCountList = map.Select(entry => new WordCount(entry.Key, entry.Value.Count)).ToList();
                reducedWordCountList.Sort((w1, w2) => w2.Count - w1.Count);
                return ToFormattedStringResult(reducedWordCountList);
            }
        }

        private string ToFormattedStringResult(List<WordCount> reducedWordCountList)
        {
            List<string> formattedWordCountList = reducedWordCountList.Select(wordCount => wordCount.Word + " " + wordCount.Count).ToList();
            return string.Join("\n", formattedWordCountList);
        }

        private static List<WordCount> ToWordCounts(string inputStr)
        {
            string[] words = Regex.Split(inputStr, Pattern);
            List<WordCount> inputList = words.Select(word => new WordCount(word, 1)).ToList();
            return inputList;
        }

        private Dictionary<string, List<WordCount>> GetListMap(List<WordCount> wordCountList)
        {
            return wordCountList.GroupBy(wc => wc.Word)
                .ToDictionary(group => group.Key, group => group.ToList());
        }

    }
}
