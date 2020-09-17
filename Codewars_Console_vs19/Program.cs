using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Codewars_Console_vs19
{
    class Program
    {
        #region 014 - 4kyu (Sum of Intervals) The class is designed to take in an array of values and (NOT WORKING IN VS_15)

        static void Main(string[] args)
        {
            //(int, int)[] Interval = new (int, int)[] { (11, 15), (1, 2), (6, 10) };
            //(int, int)[] Interval = new (int, int)[] { (1, 5), (10, 20), (7, 13), (16, 19), (5, 11) }; 
            (int, int)[] Interval = new (int, int)[] { (1, 5), (10, 20), (1, 6), (16, 19), (5, 11) }; // 19

            //(int, int)[] Interval = new (int, int)[] { (-2, -1), (-1, 0), (0, 21) };
            //(int, int)[] Interval = new (int, int)[] { (1, 2), (2, 3), (3, 24) };

            //(int, int)[] Interval = new (int, int)[] { (5, 9), (4, 10) };

            //(int, int)[] Interval = new (int, int)[] { (3, 7), (8, 10), (2, 11) };
            //(int, int)[] Interval = new (int, int)[] { (6, 10), (11, 13), (2, 4), (14, 17), (5, 18) };

            //(int, int)[] Interval = new (int, int)[] { (-3, 0), (1, 3), (-4, 2) };
            //(int, int)[] Interval = new (int, int)[] { (6, 8), (-6, -1), (0, 3), (-7, 1) };

            //(int, int)[] Interval = new (int, int)[] { (3041, 8282), (-8428, -799), (-597, 224), (8602, 8932), (5636, 9459), (-1535, 8763), (-9534, -7743), (-6874, 3781), (-9280, 3989), (-6426, -1132), (-241, 2619) }; //18993
            //(int, int)[] Interval = new (int, int)[] { (3, 6), (7, 8), (4, 9) }; //6

            //(int, int)[] Interval = new (int, int)[] { (0, int.MaxValue) };
            Console.WriteLine($"The sum of all intervals = {SumIntervals(Interval)}");
        }

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


    }
}
