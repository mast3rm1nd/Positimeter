using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Positimeter
{
    class TextHelper
    {
        public static Dictionary<string, int> GetWordsStatistics(string text)
        {
            var statistics = new Dictionary<string, int>();

            var isInWord = false;

            var startCopyPos = 0;
            

            for (int index = 0; index < text.Length; index++)
            {
                if(!char.IsLetter(text[index]) && isInWord) // here is word end
                {
                    isInWord = false;

                    var word = text.Substring(startCopyPos, index - startCopyPos).ToLower();

                    if (statistics.ContainsKey(word))
                        statistics[word]++;
                    else
                        statistics[word] = 1;

                    continue;                    
                }

                if(char.IsLetter(text[index]) && !isInWord) // word start
                {
                    isInWord = true;

                    startCopyPos = index;
                }
            }

            return statistics.OrderByDescending(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
