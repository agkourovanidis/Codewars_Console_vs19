using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Codewars_Console_vs19
{
    class Program
    {
        static void Main(string[] args)
        {
            //// 006 - 6kyu (Split Strings) splits the string into pairs of two characters.
            string[] resultArray = SplitStrings_006("abcdef");

            //string[] resultArray = SplitStrings_006("bitcoin take over the world maybe who knows perhaps");

            foreach (string strLine in resultArray)
            {
                Console.WriteLine(strLine);
            }

            #region Done

            //// 006 - 6kyu (Split Strings) splits the string into pairs of two characters.
            ////string[] resultArray = SplitStrings_006("abcdef");

            //string[] resultArray = SplitStrings_006("bitcoin take over the world maybe who knows perhaps");

            //foreach (string strLine in resultArray)
            //{
            //    Console.WriteLine(strLine);
            //}

            ////// 005 - 7kyu (Shortest Word) given a string of words, return the length of the shortest word(s).
            //Console.WriteLine(FindShort("bitcoin take over the world maybe who knows perhaps"));

            //// 004 - 7kyu (Ones and Zeros) convert the equivalent binary value to an integer.
            //Console.WriteLine(binaryArrayToNumber(new int[] { 0, 1, 1, 0 }));

            //// 003 - 7kyu Adds two numbers together and returns their sum in binary
            //Console.WriteLine(AddBinary(5, 8));

            //// 002 - 6kyu Take a Ten Minute Walk
            //Console.WriteLine(IsValidWalk(new string[] { "n", "e", "s", "e", "e", "s", "w", "w", "w", "n" }));

            ////// 001 - 6kyu Decode the Morse code
            ////Console.WriteLine(Decode("  .... . -.--   .--- ..- -.. .  "));
            //Console.WriteLine(Decode("  ···−−−···")); 

            #endregion

        }

        //// 007 - 6kyu (Find the odd int)
        public static int find_it(int[] seq)
        {
            #region My Solution 02 - add an entry to the collection if it does not exist or delete it if it exists - in the end there is only one - the desired entry
            HashSet<int> myNums = new HashSet<int>();
            foreach (int item in seq)
            {
                if (myNums.Contains(item))
                {
                    myNums.Remove(item);
                }
                else
                {
                    myNums.Add(item);
                }
            }
            int result = Int32.Parse(string.Join("", myNums));

            return result;

            #endregion

            #region My Solution 01 Create a dict where we count the number of repetitions for each number

            //Dictionary<int, int> numsDict = new Dictionary<int, int>();

            //foreach (int item in seq)
            //{
            //    if (!numsDict.ContainsKey(item))
            //    {
            //        numsDict.Add(item, 1);
            //    }
            //    else
            //    {
            //        numsDict[item]++;
            //    }
            //}
            //return (from d in numsDict where d.Value % 2 != 0 select d.Key)
            //    .FirstOrDefault();

            #endregion

            #region from Site 02
            //return seq.Aggregate(0, (a, b) => a ^ b);
            #endregion

            #region from Site 01
            //return seq.GroupBy(x => x).Single(g => g.Count() % 2 == 1).Key;
            #endregion
        }

        #region CodeWars tests
        //// 006 - 6kyu (Split Strings) splits the string into pairs of two characters.
        public static string[] SplitStrings_006(string str)
        {
            #region расписал Regex (как с сайта)
            var test = Regex.Matches(str + "_", @"\D{2}");

            var result = (from d in test.OfType<Match>() select d.Value).ToList();

            return result.ToArray();
            #region some regular expressions
            //Рассмотрим вкратце некоторые элементы синтаксиса регулярных выражений:

            //    ^: соответствие должно начинаться в начале строки (например, выражение @"^пр\w*" соответствует слову "привет" в строке "привет мир")

            //    $: конец строки (например, выражение @"\w*ир$" соответствует слову "мир" в строке "привет мир", так как часть "ир" находится в самом конце)

            //    .: знак точки определяет любой одиночный символ (например, выражение "м.р" соответствует слову "мир" или "мор")

            //    *: предыдущий символ повторяется 0 и более раз

            //    +: предыдущий символ повторяется 1 и более раз

            //    ?: предыдущий символ повторяется 0 или 1 раз

            //    \s: соответствует любому пробельному символу

            //    \S: соответствует любому символу, не являющемуся пробелом

            //    \w: соответствует любому алфавитно - цифровому символу

            //    \W: соответствует любому не алфавитно-цифровому символу

            //    \d: соответствует любой десятичной цифре

            //    \D: соответствует любому символу, не являющемуся десятичной цифрой

            //    Это только небольшая часть элементов.Более подробное описание синтаксиса регулярных выражений можно найти на msdn в
            #endregion
            #endregion


            #region My Solution 02

            //List<string> newStrArr = new List<string>();

            //for (int i = 0; i < str.Length; i+=2)
            //{
            //    newStrArr.Add(i + 1 < str.Length ? str.Substring(i, 2) : (str.Substring(i, 1) + "_"));
            //}

            //return newStrArr.ToArray();

            #endregion

            #region My Solution 01

            //int newStrLength = (str.Length % 2 == 1) ? (str.Length / 2 + 1) : (str.Length / 2);

            //string[] newStrArr = new string[newStrLength];

            //for (int j = 0; j < newStrArr.Length;)
            //{
            //    for (int i = 0; i < str.Length; i++)
            //    {
            //        newStrArr[j]= (i + 1 < str.Length) ? (str[i].ToString() + str[i + 1].ToString()) : newStrArr[j] = str[i].ToString() + "_";
            //        #region то же подробно
            //        //if (i + 1 < str.Length)
            //        //{
            //        //    newStrArr[j] = str[i].ToString() + str[i + 1].ToString();
            //        //}
            //        //else
            //        //{
            //        //    newStrArr[j] = str[i].ToString() + "_";
            //        //} 
            //        #endregion

            //        j++;
            //        i++;
            //    }
            //}

            //return newStrArr;

            #endregion

            #region from Site

            ////// так работает только в vs19
            ////return Regex.Matches(str + "_", @"\w{2}").Select(x => x.Value).ToArray();
            ////// так работает и здесь, но удаляет все пробелы
            //return Regex.Matches(str + "_", @"\w{2}").OfType<Match>().Select(x => x.Value).ToArray();

            #endregion
        }

        //// 005 - 7kyu (Shortest Word) given a string of words, return the length of the shortest word(s).
        public static int FindShort(string s)
        {
            #region My Solution
            return (from wa in s.Split(new char[] { ' ' }) orderby wa.Length select wa).FirstOrDefault().Count();
            #endregion

            #region from Site
            //return s.Split(' ').Min(x => x.Length);
            #endregion
        }

        //// 004 - 7kyu (Ones and Zeros) convert the equivalent binary value to an integer.
        public static int binaryArrayToNumber(int[] BinaryArray)
        {
            //// Converting binary string to an Integer
            int binaryToInteger = Convert.ToInt32(string.Join("", BinaryArray), 2);

            return binaryToInteger;
        }

        //// 003 - 7kyu Adds two numbers together and returns their sum in binary
        public static string AddBinary(int a, int b)
        {
            return Convert.ToString((a + b), 2);
        }

        //// 002 - 6kyu Take a Ten Minute Walk
        public static bool IsValidWalk(string[] walk)
        {
            #region My Solution
            bool result = false;

            if (walk.Count() == 10
                &&
                (walk.Where(x => x == "n").Count() == walk.Where(x => x == "s").Count())
                &&
                (walk.Where(x => x == "w").Count() == walk.Where(x => x == "e").Count()))
            {
                result = true;
            }

            return result;
            #endregion

            #region from Web
            //return walk.Count(x => x == "n") == walk.Count(x => x == "s") && walk.Count(x => x == "e") == walk.Count(x => x == "w") && walk.Length == 10;
            #endregion
        }

        //// 001 - 6kyu Decode the Morse code
        public static string Decode(string morseCode)
        {

            #region Creatting a distionary with Morse (Site uses his Own)
            Dictionary<string, string> dict = new Dictionary<string, string>()
            {
                {"a", string.Concat('.', '-')},
                {"b", string.Concat('-', '.', '.', '.')},
                {"c", string.Concat('-', '.', '-', '.')},
                {"d", string.Concat('-', '.', '.')},
                {"e", '.'.ToString()},
                {"f", string.Concat('.', '.', '-', '.')},
                {"g", string.Concat('-', '-', '.')},
                {"h", string.Concat('.', '.', '.', '.')},
                {"i", string.Concat('.', '.')},
                {"j", string.Concat('.', '-', '-', '-')},
                {"k", string.Concat('-', '.', '-')},
                {"l", string.Concat('.', '-', '.', '.')},
                {"m", string.Concat('-', '-')},
                {"n", string.Concat('-', '.')},
                {"o", string.Concat('-', '-', '-')},
                {"p", string.Concat('.', '-', '-', '.')},
                {"q", string.Concat('-', '-', '.', '-')},
                {"r", string.Concat('.', '-', '.')},
                {"s", string.Concat('.', '.', '.')},
                {"t", string.Concat('-')},
                {"u", string.Concat('.', '.', '-')},
                {"v", string.Concat('.', '.', '.', '-')},
                {"w", string.Concat('.', '-', '-')},
                {"x", string.Concat('-', '.', '.', '-')},
                {"y", string.Concat('-', '.', '-', '-')},
                {"z", string.Concat('-', '-', '.', '.')},
                {"0", string.Concat('-', '-', '-', '-', '-')},
                {"1", string.Concat('.', '-', '-', '-', '-')},
                {"2", string.Concat('.', '.', '-', '-', '-')},
                {"3", string.Concat('.', '.', '.', '-', '-')},
                {"4", string.Concat('.', '.', '.', '.', '-')},
                {"5", string.Concat('.', '.', '.', '.', '.')},
                {"6", string.Concat('-', '.', '.', '.', '.')},
                {"7", string.Concat('-', '-', '.', '.', '.')},
                {"8", string.Concat('-', '-', '-', '.', '.')},
                {"9", string.Concat('-', '-', '-', '-', '.')},
                {"SOS", "···−−−···".ToString()}
            };
            #endregion

            #region MY SOLUTION

            string ResultWord = "";

            string[] morseWordArray = morseCode.Trim().Split(new[] { "   " }, StringSplitOptions.None);

            for (int i = 0; i < morseWordArray.Length; i++)
            {
                string[] WordInMorseLetters = morseWordArray[i].Split(new char[] { ' ' });

                foreach (var morseLetter in WordInMorseLetters)
                {
                    //// If using Dictionary from site
                    // string engLetter = MorseCode.Get(morseLetter);
                    string engLetter = (from d in dict where d.Value == morseLetter select d.Key)
                        .FirstOrDefault();

                    ResultWord = ResultWord + engLetter.ToUpper();
                }

                if (i < morseWordArray.Length - 1)
                {
                    ResultWord = ResultWord + " ";
                }
            }

            return ResultWord;

            #region my_v_01
            //string morse = morseCode.Replace("   ", "@");
            //string morseWrdsPre = morse.Replace(" ", "#");
            //string morseWrds = morseWrdsPre.Replace(" ", "");

            //string[] morseArr = morseWrds.Split(new char[] { '@' });

            //for (int i = 0; i < morseArr.Length; i++)
            //{
            //    string[] StrWrds = morseArr[i].Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);

            //    foreach (var wrd in StrWrds)
            //    {
            //        string GotWrd = (from d in dict where d.Value == wrd select d.Key)
            //            .FirstOrDefault()
            //            .ToString();

            //        ResultWord = ResultWord + GotWrd.ToUpper();
            //    }

            //    if (i < morseArr.Length - 1)
            //    {
            //        ResultWord = ResultWord + " ";
            //    }
            //}

            //while (ResultWord.StartsWith(" "))
            //{
            //    ResultWord = ResultWord.Remove(0, 1);
            //}

            //return ResultWord; 
            #endregion

            #endregion

            #region From Web 02 - one line code!!!
            //return string.Concat(morseCode.Trim().Replace("   ", "  ").Split().Select(s => s == "" ? " " : MorseCode.Get(s)));
            #endregion
            #region From Web 01

            //////
            #region Explanation (from site splitted)
            ////// Getting an array splitted by words
            //string AAAwords = morseCode.Trim(); //// remove all unnecessary spaces at the beginning and the end of the string
            //string[] Awords = AAAwords.Split(new[] {"   "}, StringSplitOptions.None); //// splitting the string to array BY STRING - NOT BY CHAR (needs StringSplitOptions)
            //                                                                          /// keeping spaces (not removing unnecessary symbols)

            ////// Splitting Each Word in the above Array into letters
            //var AtranslatedWords = Awords
            //    .Select(word => word.Split(' '))
            //    .ToList();


            ////// To use my morse Dictionary - do this way
            //string ResultWord = "";

            //for (int i = 0; i < AtranslatedWords.Count; i++)
            //{
            //    foreach (var wrd in AtranslatedWords[i])
            //    {
            //        string GotWrd = (from d in dict where d.Value == wrd select d.Key)
            //            .FirstOrDefault()
            //            .ToString();

            //        ResultWord = ResultWord + GotWrd.ToUpper();
            //    }

            //    if (i < AtranslatedWords.Count - 1)
            //    {
            //        ResultWord = ResultWord + " ";
            //    }
            //}

            //return ResultWord; 
            #endregion

            ////////////////////////////////////////////////////////
            //// Getting the array of letters in morse language
            //var words = morseCode.Trim() //// remove all unnecessary spaces at the beginning and the end of the string
            //    .Split(new[] { "   " } //// splitting the string to array BY STRING - NOT BY CHAR
            //        , StringSplitOptions.None //// keeping spaces (not removing unnecessary symbols)
            //        );

            ////// translating the array of words to English
            //var translatedWords = words
            //    .Select(word => word.Split(' '))
            //    .Select(letters => string.Join("", letters.Select(MorseCode.Get)))
            //    .ToList();

            ////// concatenating recieved letters in List, splitting them by space
            //return string.Join(" ", translatedWords);

            #endregion

        }
        #endregion

    }
}
