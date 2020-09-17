using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Codewars_Console_vs19
{
    class Program
    {
        #region 017 - 5kyu (Valid Parentheses) takes a string of parentheses, and determines if the order of the parentheses is valid
        
        static void Main(string[] args)
        {
            //Console.WriteLine(ValidParentheses("()"));
            //Console.WriteLine(ValidParentheses(")(((("));
            //Console.WriteLine(ValidParentheses(")(()))"));
            //Console.WriteLine(ValidParentheses("(())((()())())"));
            //Console.WriteLine(ValidParentheses(")())((()())()("));
            //Console.WriteLine(ValidParentheses(""));
            Console.WriteLine(ValidParentheses("iuew647usef7w4%^%&e76337234>{<<{{{{]"));
        }

        #region My Solution
        public static bool ValidParentheses(string input)
        {
            #region Because in these cases must return true - these cases are not needed any more
            //if (!input.Contains("(") && !input.Contains(")"))
            //{
            //    return true;
            //}

            //if (input == "")
            //{
            //    return true;
            //} 
            #endregion

            int count = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    count++;
                }
                else if (input[i] == ')')
                {
                    count--;
                }
                if (count < 0)
                {
                    return false;
                }
            }

            if (count == 0)
            {
                return true;
            }

            return false;

        } 
        #endregion
        #endregion

        #region CodeWars tests
        #region 016 - 5kyu (Greed is Good) Your mission is to score a throw according to these rules
        // Three 1's => 1000 points
        // Three 6's =>  600 points
        // Three 5's =>  500 points
        // Three 4's =>  400 points
        // Three 3's =>  300 points
        // Three 2's =>  200 points
        // One   1   =>  100 points
        // One   5   =>   50 point

        //static void Main(string[] args)
        //{
        //    //Console.WriteLine($"The Score is {Score(new int[] { 2, 3, 4, 6, 2 })}"); //"Should be 0 :-(");
        //    //Console.WriteLine($"The Score is {Score(new int[] { 4, 4, 4, 3, 3 })}"); // "Should be 400");
        //    //Console.WriteLine($"The Score is {Score(new int[] { 2, 4, 4, 5, 4 })}"); // "Should be 450");
        //    //Console.WriteLine($"The Score is {Score(new int[] { 5, 1, 3, 4, 1 })}"); // "Should be 250");
        //    //Console.WriteLine($"The Score is {Score(new int[] { 1, 1, 1, 3, 1 })}"); // "Should be 1100");
        //    Console.WriteLine($"The Score is {Score(new int[] { 2, 4, 4, 5, 4 })}"); // "Should be 450");
        //}

        #region from Web 02
        public static int Score(int[] dice)
        {
            int[] tripleValue = { 0, 1000, 200, 300, 400, 500, 600 };
            int[] singleValue = { 0, 100, 0, 0, 0, 50, 0 };

            int value = 0;
            for (int dieSide = 1; dieSide <= 6; dieSide++)
            {
                int countRolls = dice.Where(outcome => outcome == dieSide).Count();
                value += tripleValue[dieSide] * (countRolls / 3) + singleValue[dieSide] * (countRolls % 3);
            }
            return value;
        }
        #endregion

        #region from Web 01
        //public static int Score(int[] dice)
        //{
        //    #region Original
        //    //return dice
        //    //    .GroupBy(d => d)
        //    //    .Select(g => Points(g.Key, g.Count()))
        //    //    .Sum(); 
        //    #endregion

        //    //// Getting Grouped dies
        //    var groupped = dice
        //      .GroupBy(d => d)
        //      .ToList();

        //    foreach (var item in groupped)
        //    {
        //        Console.WriteLine($"record with index: {groupped.IndexOf(item)}: die: {item.Key}, appeares {item.Count()} times");
        //    }
        //    Console.WriteLine();

        //    //// From Another Method Getting values for each item in list
        //    /// вызов другого метода внутри linq через лямбда выражение
        //    var scores = groupped
        //        .Select(g => Points(g.Key, g.Count()))
        //        .ToList();

        //    foreach (var item in scores)
        //    {
        //        Console.WriteLine($"record with index: {scores.IndexOf(item)}: : {item}");
        //    }

        //    //// Summing the Got Scores
        //    int result = scores.Sum();
        //    Console.WriteLine();

        //    return result;
        //}

        ////// Function that counts scores for each die
        //private static int Points(int die, int count)
        //{
        //    switch (die)
        //    {
        //        case 1:
        //            return (count / 3) * 1000 + (count % 3) * 100;
        //        case 5:
        //            return (count / 3) * 500 + (count % 3) * 50;
        //        default:
        //            return (count / 3) * die * 100;
        //    }
        //} 
        #endregion

        #region My Solution
        //public static int Score(int[] dice)
        //{
        //    int score = 0;

        //    if (dice.Where(x => x == 1).Count() >= 3)
        //    {
        //        score += 1000;
        //    }
        //    else if (dice.Where(x => x == 6).Count() >= 3)
        //    {
        //        score += 600;
        //    }
        //    else if (dice.Where(x => x == 5).Count() >= 3)
        //    {
        //        score += 500;
        //    }
        //    else if (dice.Where(x => x == 4).Count() >= 3)
        //    {
        //        score += 400;
        //    }
        //    else if (dice.Where(x => x == 3).Count() >= 3)
        //    {
        //        score += 300;
        //    }
        //    else if (dice.Where(x => x == 2).Count() >= 3)
        //    {
        //        score += 200;
        //    }

        //    int OnesCount = dice.Where(x => x == 1).Count();
        //    if (OnesCount < 3)
        //    {
        //        score = score + 100 * OnesCount;
        //    }
        //    else if (OnesCount > 3)
        //    {
        //        score = score + 100 * (OnesCount - 3);
        //    }

        //    int FivesCount = dice.Where(x => x == 5).Count();
        //    if (FivesCount < 3)
        //    {
        //        score = score + 50 * FivesCount;
        //    }
        //    else if (FivesCount > 3)
        //    {
        //        score = score + 50 * (FivesCount - 3);
        //    }

        //    return score;
        //} 
        #endregion
        #endregion

        #region 015 - 4kyu (Adding Big Numbers) Write a function that returns the sum of two numbers. 
        ////// The input numbers are strings and the function must return a string.
        /// Idea is that you are given two strings. Both of these strings represent numbers, which can be potentially very big 
        /// (like, for example, 1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890), 
        /// so they do not fit into any numeric data type. You have to add them up somehow, however you like, but the addition 
        /// has to be exact. Then, you return the result also as string, because it will be too big to fit into any numeric type.
        //// (Should Also change the Test in the buttom to pass.)

        //static void Main(string[] args)
        //{
        //    //Console.WriteLine(Add("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890", "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")); // "444"
        //    Console.WriteLine(Add("123", "321"));
        //}

        public static string Add(string a, string b)
        {
            return (BigInteger.Parse(a) + BigInteger.Parse(b)).ToString();
        }
        #endregion

        #region 014 - 4kyu (Sum of Intervals) The class is designed to take in an array of values and (NOT WORKING IN VS_15)

        //static void Main(string[] args)
        //{
        //    //(int, int)[] Interval = new (int, int)[] { (11, 15), (1, 2), (6, 10) };
        //    //(int, int)[] Interval = new (int, int)[] { (1, 5), (10, 20), (7, 13), (16, 19), (5, 11) }; 
        //    (int, int)[] Interval = new (int, int)[] { (1, 5), (10, 20), (1, 6), (16, 19), (5, 11) }; // 19

        //    //(int, int)[] Interval = new (int, int)[] { (-2, -1), (-1, 0), (0, 21) };
        //    //(int, int)[] Interval = new (int, int)[] { (1, 2), (2, 3), (3, 24) };

        //    //(int, int)[] Interval = new (int, int)[] { (5, 9), (4, 10) };

        //    //(int, int)[] Interval = new (int, int)[] { (3, 7), (8, 10), (2, 11) };
        //    //(int, int)[] Interval = new (int, int)[] { (6, 10), (11, 13), (2, 4), (14, 17), (5, 18) };

        //    //(int, int)[] Interval = new (int, int)[] { (-3, 0), (1, 3), (-4, 2) };
        //    //(int, int)[] Interval = new (int, int)[] { (6, 8), (-6, -1), (0, 3), (-7, 1) };

        //    //(int, int)[] Interval = new (int, int)[] { (3041, 8282), (-8428, -799), (-597, 224), (8602, 8932), (5636, 9459), (-1535, 8763), (-9534, -7743), (-6874, 3781), (-9280, 3989), (-6426, -1132), (-241, 2619) }; //18993
        //    //(int, int)[] Interval = new (int, int)[] { (3, 6), (7, 8), (4, 9) }; //6

        //    //(int, int)[] Interval = new (int, int)[] { (0, int.MaxValue) };
        //    Console.WriteLine($"The sum of all intervals = {SumIntervals(Interval)}");
        //}

        #region from Web 02
        public static int SumIntervals((int, int)[] intervals)
        {
            //// сначала создаем отсортированные список интервалоа
            var orderedInterval = (from interval in intervals
                                   orderby interval.Item1, interval.Item2
                                   select interval//;
                                  ).ToList();

            int sum = 0;
            //// и началу и концу присваиваем минимальное значение для переменной
            int begin = Int32.MinValue;
            int end = Int32.MinValue;

            foreach (var item in orderedInterval)
            {
                //// если начало интервала больше последнего минимального конца
                bool newBegin = item.Item1 > end;
                if (newBegin)
                {
                    //// то к результату прибавляем значение равное последний конец - последнее начало
                    /// (при первом круге цикла = 0)
                    sum += end - begin;
                    //// а также передвигаем последнее начало
                    /// (при первом круге цикла = минимальному Item1 из массива)
                    begin = item.Item1;
                }

                //// передвигаем конец, присвоив ему максимальное текущее значение
                end = item.Item2 > end ? item.Item2 : end;
            }
            //// после окончания цикла считаем результат, прибавив к нему значение равное последний конец - последнее начало
            sum += end - begin;
            return sum;
        }
        #endregion

        #region from Web 01 (Зависает при больших числах напр. (int, int)[] Interval = new (int, int)[] { (0, int.MaxValue) };
        //public static int SumIntervals((int, int)[] intervals)
        //{
        //    //// 01. Из массива берем Диапазон каждого интервала (Range с длиной item2 - item1)
        //    //// 02. Убираем повторяющиеся значения (Distinct)
        //    //// 03. считаем количество оставшихся записей
        //    return intervals
        //      .SelectMany(i => Enumerable.Range(i.Item1, i.Item2 - i.Item1))
        //      .Distinct()
        //      .Count();
        //}
        #endregion

        #region My Solution 01
        //public class myRecord
        //{
        //    public int? Item1 { get; set; }
        //    public int? Item2 { get; set; }
        //}

        //public static int SumIntervals((int, int)[] intervals)
        //{
        //    List<myRecord> FilledIntervals = new List<myRecord>();

        //    int SumIntervals = 0;

        //    for (int i = 0; i < intervals.Length; i++)
        //    {
        //        myRecord currentLeft = new myRecord();
        //        if (FilledIntervals.Where(x => intervals[i].Item1 <= x.Item2 && intervals[i].Item1 >= x.Item1).Count() > 0)
        //        {
        //            currentLeft = FilledIntervals.Where(x => intervals[i].Item1 <= x.Item2 && intervals[i].Item1 >= x.Item1).First();
        //        }

        //        myRecord currentRight = new myRecord();
        //        if (FilledIntervals.Where(x => intervals[i].Item2 <= x.Item2 && intervals[i].Item2 >= x.Item1).Count() > 0)
        //        {
        //            currentRight = FilledIntervals.Where(x => intervals[i].Item2 <= x.Item2 && intervals[i].Item2 >= x.Item1).First();
        //        }

        //        int? minLeft = null;
        //        int? maxRight = null;

        //        if (FilledIntervals.Where(x => x.Item1 > intervals[i].Item1).Count() > 0)
        //        {
        //            minLeft = (from arr in FilledIntervals where arr.Item1 > intervals[i].Item1 orderby arr.Item1 select arr.Item1).First();
        //        }
        //        if (minLeft != null)
        //        {
        //            if (FilledIntervals.Where(x => x.Item2 < intervals[i].Item2).Count() > 0)
        //            {
        //                maxRight = (from arr in FilledIntervals where arr.Item2 < intervals[i].Item2 orderby arr.Item2 descending select arr.Item2).First();
        //            }
        //        }

        //        if (intervals[i].Item1 < minLeft && intervals[i].Item2 > maxRight && maxRight > minLeft && currentLeft.Item1 != null && currentRight.Item1 == null)
        //        {
        //            var existing = FilledIntervals
        //                          .Where(x => x.Item1 >= minLeft && x.Item2 <= maxRight)
        //                          .ToList();


        //            ////// создаю временную запись с последними данными
        //            myRecord tmpFilledIntervals = new myRecord
        //            {
        //                Item1 = intervals[i].Item1,
        //                Item2 = intervals[i].Item2
        //            };

        //            ////// из моего списка удаляю найденные выше записи
        //            foreach (var item in existing)
        //            {
        //                FilledIntervals.Remove(item);
        //            }

        //            ////// вместо удаленных добавляю новую с последними данными
        //            FilledIntervals.Add(tmpFilledIntervals);

        //            //// левый
        //            ////удаляю из моего списка запись с таким же интервалами что и текущая
        //            var toRemove = FilledIntervals
        //                          .Where(x => x.Item1 == intervals[i].Item1 && x.Item2 == intervals[i].Item2)
        //                          .First();
        //            FilledIntervals.Remove(toRemove);

        //            currentLeft.Item2 = intervals[i].Item2;
        //        }
        //        else if (intervals[i].Item1 < minLeft && intervals[i].Item2 > maxRight && maxRight > minLeft && currentLeft.Item1 == null && currentRight.Item1 != null)
        //        {
        //            //// внутренний
        //            var existing = FilledIntervals
        //                          .Where(x => x.Item1 >= minLeft && x.Item2 <= maxRight)
        //                          .ToList();


        //            ////// создаю временную запись с последними данными
        //            myRecord tmpFilledIntervals = new myRecord
        //            {
        //                Item1 = intervals[i].Item1,
        //                Item2 = intervals[i].Item2
        //            };

        //            ////// из моего списка удаляю найденные выше записи
        //            foreach (var item in existing)
        //            {
        //                FilledIntervals.Remove(item);
        //            }

        //            ////// вместо удаленных добавляю новую с последними данными
        //            FilledIntervals.Add(tmpFilledIntervals);

        //            //// правый
        //            ////удаляю из моего списка запись с таким же интервалами что и текущая
        //            var toRemove = FilledIntervals
        //                          .Where(x => x.Item1 == intervals[i].Item1 && x.Item2 == intervals[i].Item2)
        //                          .First();
        //            FilledIntervals.Remove(toRemove);
        //            //////// в моем списке обновляю найденную выше запись
        //            currentRight.Item1 = intervals[i].Item1;
        //        }
        //        else if (intervals[i].Item1 < minLeft && intervals[i].Item2 > maxRight && maxRight > minLeft && currentLeft.Item1 != null && currentRight.Item1 != null)
        //        {
        //            //// внутренний
        //            var existing = FilledIntervals
        //                          .Where(x => x.Item1 >= minLeft && x.Item2 <= maxRight)
        //                          .ToList();

        //            ////// создаю временную запись с последними данными
        //            myRecord tmpFilledIntervals = new myRecord
        //            {
        //                Item1 = intervals[i].Item1,
        //                Item2 = intervals[i].Item2
        //            };

        //            ////// из моего списка удаляю найденные выше записи
        //            foreach (var item in existing)
        //            {
        //                FilledIntervals.Remove(item);
        //            }

        //            ////// вместо удаленных добавляю новую с последними данными
        //            FilledIntervals.Add(tmpFilledIntervals);
        //            //}

        //            //// правый
        //            ////удаляю из моего списка запись с таким же интервалами что и текущая
        //            var toRemove = FilledIntervals
        //                          .Where(x => x.Item1 == intervals[i].Item1 && x.Item2 == intervals[i].Item2)
        //                          .First();
        //            FilledIntervals.Remove(toRemove);

        //            ////// создаю временную запись с последними данными
        //            myRecord tmpFilledIntervalsNew = new myRecord
        //            {
        //                Item1 = currentLeft.Item1,
        //                Item2 = currentRight.Item2
        //            };

        //            ////// из моего списка удаляю найденные выше записи
        //            FilledIntervals.Remove(currentLeft);
        //            FilledIntervals.Remove(currentRight);

        //            ////// вместо удаленных добавляю новую с последними данными
        //            FilledIntervals.Add(tmpFilledIntervalsNew);
        //        }
        //        else if (intervals[i].Item1 < minLeft && intervals[i].Item2 > maxRight && maxRight > minLeft)
        //        {
        //            var existing = FilledIntervals
        //                          .Where(x => x.Item1 >= minLeft && x.Item2 <= maxRight)
        //                          .ToList();

        //            ////// создаю временную запись с последними данными
        //            myRecord tmpFilledIntervals = new myRecord
        //            {
        //                Item1 = intervals[i].Item1,
        //                Item2 = intervals[i].Item2
        //            };

        //            ////// из моего списка удаляю найденные выше записи
        //            foreach (var item in existing)
        //            {
        //                FilledIntervals.Remove(item);
        //            }

        //            ////// вместо удаленных добавляю новую с последними данными
        //            FilledIntervals.Add(tmpFilledIntervals);
        //        }
        //        else if (currentLeft.Item1 == null && currentRight.Item1 == null)
        //        {
        //            FilledIntervals.Add(new myRecord
        //            {
        //                Item1 = intervals[i].Item1,
        //                Item2 = intervals[i].Item2
        //            });
        //        }
        //        else if (currentLeft.Item1 != null && currentRight.Item1 == null)
        //        {
        //            int commonLeft = ((int)currentLeft.Item2 - intervals[i].Item1);

        //            int numToAdd = (intervals[i].Item2 - intervals[i].Item1) - commonLeft;

        //            if (numToAdd > 0)
        //            {
        //                //////// в моем списке обновляю найденную выше запись
        //                currentLeft.Item2 = intervals[i].Item2;
        //            }
        //        }
        //        else if (currentLeft.Item1 == null && currentRight.Item1 != null)
        //        {
        //            int commonRight = (intervals[i].Item2 - (int)currentRight.Item1);

        //            int numToAdd = (intervals[i].Item2 - intervals[i].Item1) - commonRight;

        //            if (numToAdd > 0)
        //            {
        //                //////// в моем списке обновляю найденную выше запись
        //                currentRight.Item1 = intervals[i].Item1;
        //            }
        //        }
        //        else if (currentLeft.Item1 != null && currentRight.Item1 != null)
        //        {
        //            int commonLeft = ((int)currentLeft.Item2 - intervals[i].Item1);
        //            int commonRight = (intervals[i].Item2 - (int)currentRight.Item1);

        //            int numToAdd = (intervals[i].Item2 - intervals[i].Item1)
        //                            - commonLeft - commonRight;

        //            if (numToAdd > 0)
        //            {
        //                ////// создаю временную запись с последними данными
        //                myRecord tmpFilledIntervals = new myRecord
        //                {
        //                    Item1 = currentLeft.Item1,
        //                    Item2 = currentRight.Item2
        //                };

        //                ////// из моего списка удаляю найденные выше записи
        //                FilledIntervals.Remove(currentLeft);
        //                FilledIntervals.Remove(currentRight);

        //                ////// вместо удаленных добавляю новую с последними данными
        //                FilledIntervals.Add(tmpFilledIntervals);
        //            }
        //        }
        //    }

        //    foreach (var item in FilledIntervals)
        //    {
        //        Console.WriteLine($"interval from {item.Item1} to {item.Item2}");

        //        SumIntervals = SumIntervals + ((int)item.Item2 - (int)item.Item1);
        //    }

        //    return SumIntervals;
        //}
        #endregion


        #endregion

        #region 013 - 5kyu (PaginationHelper) The class is designed to take in an array of values and 
        //// an integer indicating how many items will be allowed per each page.

        //static void Main(string[] args)
        //{
        //    var helper = new PagnationHelper<int>(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }, 12);
        //    Console.WriteLine($"totalPages: {helper.PageCount}");
        //    Console.WriteLine($"totalItems: {helper.ItemCount}");
        //    Console.WriteLine();
        //    Console.WriteLine($"Items on Page 0 with index -1: {helper.PageItemCount(-1)}");

        //    Console.WriteLine($"Items on Page 1 with index 0: {helper.PageItemCount(0)}");
        //    Console.WriteLine($"Items on Page 2 with index 1: {helper.PageItemCount(1)}");
        //    Console.WriteLine($"Items on Page 3 with index 2: {helper.PageItemCount(2)}");
        //    Console.WriteLine($"Items on Page 4 with index 3: {helper.PageItemCount(3)}");
        //    Console.WriteLine();
        //    Console.WriteLine($"Item 0 with index -1 is on Page {helper.PageIndex(-1) + 1} with index: {helper.PageIndex(-1)}");
        //    Console.WriteLine($"Item 1 with index 0 is on Page {helper.PageIndex(0) + 1} with index: {helper.PageIndex(0)}");
        //    Console.WriteLine($"Item 4 with index 3 is on Page {helper.PageIndex(3) + 1} with index: {helper.PageIndex(3)}");
        //    Console.WriteLine($"Item 5 with index 4 is on Page {helper.PageIndex(4) + 1} with index: {helper.PageIndex(4)}");

        //    Console.WriteLine($"Item 6 with index 5 is on Page {helper.PageIndex(5) + 1} with index: {helper.PageIndex(5)}");
        //    Console.WriteLine($"Item 3 with index 2 is on Page {helper.PageIndex(2) + 1} with index: {helper.PageIndex(2)}");
        //    Console.WriteLine($"Item 21 with index 20 is on Page {helper.PageIndex(20) + 1} with index: {helper.PageIndex(20)}");
        //    Console.WriteLine($"Item -9 with index -10 is on Page {helper.PageIndex(-10) + 1} with index: {helper.PageIndex(-10)}");

        //    Console.WriteLine($"Item 12 with index 11 is on Page: {helper.PageIndex(11)}");


        //    //var helper = new PagnationHelper<char>(new List<char> { 'a', 'b', 'c', 'd', 'e', 'f' }, 12);

        //    //Console.WriteLine($"totalPages: {helper.PageCount}"); //should == 2
        //    //Console.WriteLine($"totalItems: {helper.ItemCount}"); //should == 6
        //    //Console.WriteLine();
        //    //Console.WriteLine($"Items on Page 1 with index 0: {helper.PageItemCount(0)}");
        //    //Console.WriteLine($"Items on Page 2 with index 1: {helper.PageItemCount(1)}");
        //    //Console.WriteLine($"Items on Page 3 with index 2: {helper.PageItemCount(2)}");
        //    //Console.WriteLine($"Items on Page 4 with index 3: {helper.PageItemCount(3)}");
        //    //Console.WriteLine();
        //    //Console.WriteLine($"Item 0 with index -1 is on Page: {helper.PageIndex(-1)}");
        //    //Console.WriteLine($"Item 1 with index 0 is on Page: {helper.PageIndex(0)}");
        //    //Console.WriteLine($"Item 4 with index 3 is on Page: {helper.PageIndex(3)}");
        //    //Console.WriteLine($"Item 5 with index 4 is on Page: {helper.PageIndex(4)}");

        //    //Console.WriteLine($"Item 6 with index 5 is on Page: {helper.PageIndex(5)}");
        //    //Console.WriteLine($"Item 3 with index 2 is on Page: {helper.PageIndex(2)}");
        //    //Console.WriteLine($"Item 21 with index 20 is on Page: {helper.PageIndex(20)}");
        //    //Console.WriteLine($"Item -9 with index -10 is on Page: {helper.PageIndex(-10)}");

        //    //Console.WriteLine($"Item 13 with index 12 is on Page: {helper.PageIndex(12)}");
        //}

        public class PagnationHelper<T>
        {
            // TODO: Complete this class

            private IList<T> _collection { get; set; }
            int _itemsPerPage { get; set; }


            /// <summary>
            /// Constructor, takes in a list of items and the number of items that fit within a single page
            /// </summary>
            /// <param name="collection">A list of items</param>
            /// <param name="itemsPerPage">The number of items that fit within a single page</param>
            public PagnationHelper(IList<T> collection, int itemsPerPage)
            {
                _collection = collection;
                _itemsPerPage = itemsPerPage;
            }

            /// <summary>
            /// The number of items within the collection
            /// </summary>
            public int ItemCount
            {
                get
                {
                    return _collection.Count;
                }
            }

            /// <summary>
            /// The number of pages
            /// </summary>
            public int PageCount
            {
                get
                {
                    if (ItemCount % _itemsPerPage == 0)
                    {
                        return ItemCount / _itemsPerPage;
                    }
                    else
                    {
                        return (ItemCount / _itemsPerPage) + 1;
                    }
                }
            }

            /// <summary>
            /// Returns the number of items in the page at the given page index 
            /// </summary>
            /// <param name="pageIndex">The zero-based page index to get the number of items for</param>
            /// <returns>The number of items on the specified page or -1 for pageIndex values that are out of range</returns>
            public int PageItemCount(int pageIndex)
            {
                if (
                    (ItemCount % _itemsPerPage == 0 && pageIndex + 1 <= PageCount && pageIndex + 1 > 0)
                    ||
                    (ItemCount % _itemsPerPage != 0 && pageIndex + 1 < PageCount && pageIndex + 1 > 0)
                    )
                {
                    return _itemsPerPage;
                }
                else if (ItemCount % _itemsPerPage != 0 && pageIndex + 1 == PageCount)
                {
                    return ItemCount % _itemsPerPage;
                }
                else
                {
                    return -1;
                }
            }

            /// <summary>
            /// Returns the PAGE INDEX of the page containing the item at the given item index.
            /// </summary>
            /// <param name="itemIndex">The zero-based index of the item to get the pageIndex for</param>
            /// <returns>The zero-based page index of the page containing the item at the given item index or -1 if the item index is out of range</returns>
            public int PageIndex(int itemIndex)
            {
                if (itemIndex + 1 < 1 || itemIndex + 1 > ItemCount)
                {
                    return -1;
                }
                else if ((itemIndex + 1) % _itemsPerPage == 0)
                {
                    return ((itemIndex + 1) / _itemsPerPage) - 1;
                }
                else
                {
                    return (itemIndex + 1) / _itemsPerPage;
                }

            }
        }
        #endregion

        #region 012 - 6kyu (Find the missing letter) Write a method that takes an array of consecutive (increasing) letters as input and that returns the missing letter in the array.
        //static void Main(string[] args)
        //{
        //    Console.WriteLine(FindMissingLetter(new[] { 'a', 'b', 'c', 'd', 'f' }));
        //}

        public static char FindMissingLetter(char[] array)
        {
            #region from Web
            for (int i = 0; i < array.Length - 1; i++)
            {
                #region for clarify
                char aa = array[i];
                char bb = array[i + 1];
                int cc = bb - aa;
                int dd = aa + 1;

                //// int to char
                char ee = (char)dd;
                #endregion


                if (array[i + 1] - array[i] > 1)
                {
                    return (char)(array[i] + 1);
                }
            }

            return ' ';

            #endregion

            #region My Solution 01
            //#region Creating an alphabet
            //List<char> alphabet = new List<char>()
            //{
            //    'a',
            //    'b',
            //    'c',
            //    'd',
            //    'e',
            //    'f',
            //    'g',
            //    'h',
            //    'i',
            //    'j',
            //    'k',
            //    'l',
            //    'm',
            //    'n',
            //    'o',
            //    'p',
            //    'q',
            //    'r',
            //    's',
            //    't',
            //    'u',
            //    'v',
            //    'w',
            //    'x',
            //    'y',
            //    'z',
            //    'A',
            //    'B',
            //    'C',
            //    'D',
            //    'E',
            //    'F',
            //    'G',
            //    'H',
            //    'I',
            //    'J',
            //    'K',
            //    'L',
            //    'M',
            //    'N',
            //    'O',
            //    'P',
            //    'Q',
            //    'R',
            //    'S',
            //    'T',
            //    'U',
            //    'V',
            //    'W',
            //    'X',
            //    'Y',
            //    'Z'
            //};
            //#endregion

            //int startIndex = alphabet.IndexOf(array.First());

            //List<char> allChars = alphabet.Skip(startIndex).Take(array.Length + 1).ToList();

            //return allChars.Except(array.ToList()).First();

            #endregion
        }
        #endregion

        #region 011 - 6kyu (Sort the odd) Your task is to sort ascending odd numbers but even numbers must be on their places
        //static void Main(string[] args)
        //{
        //    foreach (var item in SortArray(new int[] { 5, 3, 1, 8, 0 }))
        //    {
        //        Console.WriteLine(item);
        //    }
        //}

        public static int[] SortArray(int[] array)
        {
            #region My Solution 01
            int[] OddSorted = array.Where(x => x % 2 == 1).OrderBy(x => x).ToArray();

            int[] newArr = new int[array.Length];

            int currentOdd = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] % 2 == 0)
                {
                    newArr[i] = array[i];
                }
                else
                {
                    newArr[i] = OddSorted[currentOdd];
                    currentOdd++;
                }
            }

            return newArr;

            #endregion

        }
        #endregion

        #region 010 - 6kyu (Build Tower) Build Tower by the following given argument: number of floors(integer and always greater than 0)
        //////[
        //////'     *     ',
        //////'    ***    ',
        //////'   *****   ',
        //////'  *******  ',
        //////' ********* ',
        //////'***********'
        //////]

        //static void Main(string[] args)
        //{
        //    foreach (var item in TowerBuilder(50))
        //    {
        //        Console.WriteLine(item);
        //    }
        //}

        public static string[] TowerBuilder(int nFloors)
        {
            #region My Solution 01

            List<string> building = new List<string>();

            for (int i = nFloors; i > 0; i--)
            {
                var currFloor = "";

                for (int k = 0; k < nFloors - i; k++)
                {
                    currFloor = currFloor + " ";
                }
                for (int l = 0; l < i * 2 - 1; l++)
                {
                    currFloor = currFloor + "*";
                }
                for (int m = 0; m < nFloors - i; m++)
                {
                    currFloor = currFloor + " ";
                }

                building.Add(currFloor);
            }

            return building.OrderBy(x => x).ToArray();

            #endregion

        }
        #endregion

        #region 009 - 6kyu (Delete occurrences of an element if it occurs more than n times)
        //static void Main(string[] args)
        //{
        //    foreach (var item in DeleteNth(new int[] { 1, 1, 3, 3, 7, 2, 2, 2, 2 }, 3))
        //    {
        //        Console.WriteLine(item);
        //    }
        //}

        public static int[] DeleteNth(int[] arr, int x)
        {
            #region My Solution 02

            List<int> collection = new List<int>();

            for (int i = 0; i < arr.Length; i++)
            {
                if (collection.Where(a => a == arr[i]).Count() < x)
                {
                    collection.Add(arr[i]);
                }
            }

            return collection.ToArray();

            #endregion

            #region My Solution 01 - not exactly what they want (here I keep only records where the count of that value is less than "x")

            //Dictionary<int, int> numsCount = new Dictionary<int, int>();

            //for (int i = 0; i < arr.Length; i++)
            //{
            //    if (!numsCount.ContainsKey(arr[i]))
            //    {
            //        numsCount.Add(arr[i], 1);
            //    }
            //    else
            //    {
            //        numsCount[arr[i]]++;
            //    }
            //}

            //List<int> ToDelete = numsCount
            //    .Where(a => a.Value == x)
            //    .Select(a => a.Key)
            //    .ToList();

            //int[] result = new int[3];
            //result = arr.Where(val => !ToDelete.Contains(val)).ToArray();

            //return result;

            #endregion

            #region from Web
            //List<int> collection = new List<int>();
            //collection = arr.Where((t, i) => arr.Take(i + 1).Count(s => s == t) <= x).ToList();

            //return collection.ToArray();
            #endregion
        }
        #endregion

        #region 008 - 5kyu (Moving Zeros To The End) Write an algorithm that takes an array and moves all of the zeros to the end, preserving the order of the other elements.
        //static void Main(string[] args)
        //{
        //    foreach (var item in MoveZeroes(new int[] {1, 2, 0, 1, 0, 1, 0, 3, 0, 1}))
        //    {
        //        Console.WriteLine(item);
        //    }
        //}

        public static int[] MoveZeroes(int[] arr)
        {
            #region My Solution
            int[] unsorted = arr.Where(x => x != 0).ToArray();
            int[] newArr = new int[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                newArr[i] = (i < unsorted.Length) ? unsorted[i] : 0;
            }

            return newArr;
            #endregion

            #region from Web
            //return arr.OrderBy(x => x == 0).ToArray();
            #endregion
        }
        #endregion

        #region 007 - 6kyu (Find the odd int) Given an array of integers, find the one that appears an odd number of times.
        //static void Main(string[] args)
        //{
        //    Console.WriteLine(find_it(new int[] { 20, 1, -1, 2, -2, 3, 3, 5, 5, 1, 2, 4, 20, 4, -1, -2, 5 }));
        //}

        public static int find_it(int[] seq)
        {
            #region My Solution 02 - add an entry to the collection if it does not exist, or delete it if it exists - in the end there is only one - the desired entry
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

            #region from Web 02
            //return seq.Aggregate(0, (a, b) => a ^ b);
            #endregion

            #region from Web 01
            //return seq.GroupBy(x => x).Single(g => g.Count() % 2 == 1).Key;
            #endregion
        }
        #endregion

        #region 006 - 6kyu (Split Strings) splits the string into pairs of two characters.
        //static void Main(string[] args)
        //{
        //    ////string[] resultArray = SplitStrings_006("abcdef");

        //    string[] resultArray = SplitStrings_006("bitcoin take over the world maybe who knows perhaps");

        //    foreach (string strLine in resultArray)
        //    {
        //        Console.WriteLine(strLine);
        //    }
        //}

        public static string[] SplitStrings_006(string str)
        {
            #region Regex (from Web)
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

            #region from Web

            ////// так работает только в vs19
            ////return Regex.Matches(str + "_", @"\w{2}").Select(x => x.Value).ToArray();
            ////// так работает и здесь, но удаляет все пробелы
            //return Regex.Matches(str + "_", @"\w{2}").OfType<Match>().Select(x => x.Value).ToArray();

            #endregion
        }
        #endregion

        #region 005 - 7kyu (Shortest Word) given a string of words, return the length of the shortest word(s).
        //static void Main(string[] args)
        //{
        //    Console.WriteLine(FindShort("bitcoin take over the world maybe who knows perhaps"));
        //}

        public static int FindShort(string s)
        {
            #region My Solution
            return (from wa in s.Split(new char[] { ' ' }) orderby wa.Length select wa).FirstOrDefault().Count();
            #endregion

            #region from Web
            //return s.Split(' ').Min(x => x.Length);
            #endregion
        }
        #endregion

        #region 004 - 7kyu (Ones and Zeros) convert the equivalent binary value to an integer.
        //static void Main(string[] args)
        //{
        //    Console.WriteLine(binaryArrayToNumber(new int[] { 0, 1, 1, 0 }));
        //}

        public static int binaryArrayToNumber(int[] BinaryArray)
        {
            //// Converting binary string to an Integer
            int binaryToInteger = Convert.ToInt32(string.Join("", BinaryArray), 2);

            return binaryToInteger;
        }
        #endregion

        #region 003 - 7kyu (Binary Addition) - Adds two numbers together and returns their sum in binary
        //static void Main(string[] args)
        //{
        //    Console.WriteLine(AddBinary(5, 8));
        //}

        public static string AddBinary(int a, int b)
        {
            return Convert.ToString((a + b), 2);
        }

        #endregion

        #region 002 - 6kyu (Take a Ten Minute Walk)
        //static void Main(string[] args)
        //{
        //    Console.WriteLine(IsValidWalk(new string[] { "n", "e", "s", "e", "e", "s", "w", "w", "w", "n" }));
        //}
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
        #endregion

        #region 001 - 6kyu (Decode the Morse code)
        //static void Main(string[] args)
        //{
        //    Console.WriteLine(Decode("  .... . -.--   .--- ..- -.. .  "));
        //    //Console.WriteLine(Decode("  ···−−−···")); 
        //}

        public static string Decode(string morseCode)
        {

            #region Creating a dictionary with Morse - for testing here (Site uses his Own)
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

            #region MY SOLUTION 02

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

            #endregion

            #region  MY SOLUTION 01
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

            #region From Web 02 - one line code!!!
            //return string.Concat(morseCode.Trim().Replace("   ", "  ").Split().Select(s => s == "" ? " " : MorseCode.Get(s)));
            #endregion

            #region From Web 01

            //////
            #region Explanation (from Web splitted)
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
        #endregion
    }
}
