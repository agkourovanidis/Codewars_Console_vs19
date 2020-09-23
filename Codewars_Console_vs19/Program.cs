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
        #region 022 - 3kyu (Battleship field validator) Write a method that takes a field for well-known board game 
        //// "Battleship" as an argument and returns true if it has a valid disposition of ships, false otherwise.

        static void Main(string[] args)
        {
            int[,] field = new int[10, 10]
                     {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
                      {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                      {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
                      {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 1, 0, 1, 0, 0, 0},
                      {0, 0, 0, 0, 1, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 1, 0, 0, 0, 1, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};

            Console.WriteLine(ValidateBattlefield(field));
        }

        #region My Solution 02
        public static bool ValidateBattlefield(int[,] field)
        {
            Dictionary<int, int> mustShips = new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 2 }, { 4, 1 } };

            int shipCells = 0;
            Dictionary<int, int> foundShips = new Dictionary<int, int>();

            bool CheckWrongCorners(int i, int j)
            {
                if (i + 1 <= 9)
                {
                    if ((j - 1 >= 0 && field[i + 1, j - 1] == 1) || (j + 1 <= 9 && field[i + 1, j + 1] == 1)) return true;
                }
                return false;
            }

            int cellsCountByRow(int i, int j)
            {
                if (j > 9 || field[i, j] != 1) return 0;
                if (CheckWrongCorners(i, j)) return -1;

                field[i, j] = -1;
                return 1 + cellsCountByRow(i, ++j);
            }

            int cellsCountByColumn(int i, int j)
            {
                if (i > 9 || field[i, j] != 1) return 0;
                if (CheckWrongCorners(i, j)) return -1;

                field[i, j] = -1;

                return 1 + cellsCountByColumn(++i, j);
            }

            void AddToDict(int cells)
            {
                if (!foundShips.ContainsKey(cells))
                {
                    foundShips.Add(cells, 1);
                }
                else
                {
                    foundShips[cells]++;
                }

                shipCells = 0;
            }

            for (int i = 0; i < field.GetLength(0); i++)
            {
                shipCells = 0;

                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Console.Write($"{field[i, j]} ");

                    if (field[i, j] == 1)
                    {
                        shipCells++;

                        if (CheckWrongCorners(i, j)) return false;

                        int nextRow = i + 1;
                        int nextColumn = j + 1;

                        if (nextColumn != 10 && field[i, nextColumn] == 1)
                        {
                            shipCells += cellsCountByRow(i, nextColumn);
                        }
                        else if (nextRow != 10 && field[nextRow, j] == 1)
                        {
                            shipCells += cellsCountByColumn(nextRow, j);
                        }
                        if (shipCells == 0) return false;

                        AddToDict(shipCells);
                    }
                }
                Console.WriteLine();
            }

            // final check for quantity
            if (mustShips.Except(foundShips).Count() > 0 || foundShips.Except(mustShips).Count() > 0)
            {
                Console.WriteLine("Wrong number of ships");
                return false;
            }

            return true;
        }
        #endregion

        #region from Web 01 - not correct
        //public static bool ValidateBattlefield(int[,] field)
        //{
        //    return !HasContact(field) && GetShips(field) == "1111222334";
        //}

        //private static bool HasContact(int[,] field)
        //{
        //    for (var x = 0; x < field.GetLength(0); x++)
        //        for (var y = 0; y < field.GetLength(1) - 1; y++)
        //            if (field[x, y] == 1)
        //            {
        //                if (x > 0 && field[x - 1, y + 1] == 1)
        //                    return true;
        //                if (x < field.GetLength(0) - 1 && field[x + 1, y + 1] == 1)
        //                    return true;
        //            }
        //    return false;
        //}

        //private static string GetShips(int[,] field)
        //{
        //    var ships = new List<int>();
        //    for (var x = 0; x < field.GetLength(0); x++)
        //        for (var y = 0; y < field.GetLength(1); y++)
        //            if (field[x, y] == 1)
        //            {
        //                var length = 1;
        //                while (x + length < field.GetLength(0) && field[x + length, y] == 1)
        //                    field[x + length++, y] = 0;
        //                while (y + length < field.GetLength(1) && field[x, y + length] == 1)
        //                    field[x, y + length++] = 0;
        //                ships.Add(length);
        //            }
        //    ships.Sort();
        //    return string.Join("", ships);
        //}
        #endregion

        #region My Solution 01
        //public static bool ValidateBattlefield(int[,] field)
        //{
        //    #region пример заполнения из параметра
        //    //for (int i = 0; i < field.GetLength(0); i++)
        //    //{
        //    //    for (int j = 0; j < field.GetLength(1); j++)
        //    //    {
        //    //        Console.Write("{0,2}", field[i, j]);
        //    //    }
        //    //    Console.WriteLine();
        //    //}
        //    //Console.WriteLine(); 
        //    #endregion

        //    ////////////////////////////
        //    int hight = field.GetLength(0);
        //    int width = field.GetLength(1);

        //    int[,] pole = field;

        //    //Dictionary<int, int> shablon = new Dictionary<int, int>();
        //    //shablon.Add(1, 8);
        //    //shablon.Add(2, 3);
        //    //shablon.Add(3, 2);
        //    //shablon.Add(4, 1);
        //    //// или так
        //    Dictionary<int, int> shablon = new Dictionary<int, int>
        //    {
        //    {1, 8},
        //    {2, 3},
        //    {3, 2},
        //    {4, 1}
        //    };

        //    #region Filling Horizontal Ships
        //    Dictionary<int, int> allShipsHorizDict = new Dictionary<int, int>();
        //    for (int i = 0; i < hight; i++)
        //    {
        //        int shipHorizCells = 0;

        //        for (int j = 0; j < width; j++)
        //        {
        //            Console.Write($"{pole[i, j]} ");

        //            //// Если есть
        //            if (pole[i, j] == 1)
        //            {
        //                shipHorizCells++;

        //                #region Если верхний левый
        //                if (i == 0 && j == 0)
        //                {
        //                    #region IF ERRORS
        //                    if (//// if conners ocupied
        //                        (pole[i, j] == pole[i + 1, j + 1])
        //                        ||
        //                        //// if right and down or down and left ocupied                            
        //                        (pole[i, j] == pole[i, j + 1] // right
        //                        &&
        //                        pole[i, j] == pole[i + 1, j])) // down                            
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (pole[i, j + 1] == 0 // right
        //                        &&
        //                        pole[i + 1, j] == 0) // down                            
        //                    {
        //                        if (shipHorizCells > 0)
        //                        {
        //                            if (!allShipsHorizDict.ContainsKey(shipHorizCells))
        //                            {
        //                                allShipsHorizDict.Add(shipHorizCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsHorizDict[shipHorizCells]++;
        //                            }
        //                        }
        //                        shipHorizCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion
        //                #region Если верхний правый
        //                else if (i == 0 && j == 9)
        //                {
        //                    #region IF ERRORS
        //                    if (//// if conners ocupied
        //                        (pole[i, j] == pole[i + 1, j - 1])
        //                        ||
        //                        //// if right and down or down and left ocupied                            
        //                        (pole[i, j] == pole[i, j - 1] // left
        //                        &&
        //                        pole[i, j] == pole[i + 1, j])) // down                            
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (pole[i, j - 1] == 0 // right
        //                        &&
        //                        pole[i + 1, j] == 0) // down                            
        //                    {
        //                        if (shipHorizCells > 0)
        //                        {
        //                            if (!allShipsHorizDict.ContainsKey(shipHorizCells))
        //                            {
        //                                allShipsHorizDict.Add(shipHorizCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsHorizDict[shipHorizCells]++;
        //                            }
        //                        }
        //                        shipHorizCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion
        //                #region Если верхний средний
        //                else if (i == 0)
        //                {
        //                    #region IF ERRORS
        //                    if (
        //                        //// if conners ocupied
        //                        (pole[i, j] == pole[i + 1, j + 1]
        //                        ||
        //                        pole[i, j] == pole[i + 1, j - 1])
        //                        ||
        //                        //// if right and down or down and left ocupied
        //                        ((pole[i, j] == pole[i, j + 1] // right
        //                        &&
        //                        pole[i, j] == pole[i + 1, j]) // down                                                                        
        //                        ||
        //                        (pole[i, j] == pole[i + 1, j] // down                                    
        //                        &&
        //                        pole[i, j] == pole[i, j - 1])) // left
        //                        )
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (
        //                        pole[i, j + 1] == 0 // right
        //                        &&
        //                        pole[i, j - 1] == 0 // left
        //                        &&
        //                        pole[i + 1, j] == 0 // down
        //                        )
        //                    {
        //                        if (shipHorizCells > 0)
        //                        {
        //                            if (!allShipsHorizDict.ContainsKey(shipHorizCells))
        //                            {
        //                                allShipsHorizDict.Add(shipHorizCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsHorizDict[shipHorizCells]++;
        //                            }
        //                        }
        //                        shipHorizCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion

        //                #region Если нижний левый
        //                else if (i == 9 && j == 0)
        //                {
        //                    #region IF ERRORS
        //                    if (//// if conners ocupied
        //                        (pole[i, j] == pole[i - 1, j + 1])
        //                        ||
        //                        //// if right and down or down and left ocupied                            
        //                        (pole[i, j] == pole[i, j + 1] // right
        //                        &&
        //                        pole[i, j] == pole[i - 1, j])) // up                            
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (pole[i, j + 1] == 0 // right
        //                        &&
        //                        pole[i - 1, j] == 0) // up                            
        //                    {
        //                        if (shipHorizCells > 0)
        //                        {
        //                            if (!allShipsHorizDict.ContainsKey(shipHorizCells))
        //                            {
        //                                allShipsHorizDict.Add(shipHorizCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsHorizDict[shipHorizCells]++;
        //                            }
        //                        }
        //                        shipHorizCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion
        //                #region Если нижний правый
        //                else if (i == 9 && j == 9)
        //                {
        //                    #region IF ERRORS
        //                    if (//// if conners ocupied
        //                        (pole[i, j] == pole[i - 1, j - 1])
        //                        ||
        //                        //// if right and down or down and left ocupied                            
        //                        (pole[i, j] == pole[i, j - 1] // left
        //                        &&
        //                        pole[i, j] == pole[i - 1, j])) // up                            
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (pole[i, j - 1] == 0 // left
        //                        &&
        //                        pole[i - 1, j] == 0) // up                            
        //                    {
        //                        if (shipHorizCells > 0)
        //                        {
        //                            if (!allShipsHorizDict.ContainsKey(shipHorizCells))
        //                            {
        //                                allShipsHorizDict.Add(shipHorizCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsHorizDict[shipHorizCells]++;
        //                            }
        //                        }
        //                        shipHorizCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion
        //                #region Если нижний средний
        //                else if (i == 9)
        //                {
        //                    #region IF ERRORS
        //                    if (
        //                        //// if conners ocupied
        //                        (pole[i, j] == pole[i - 1, j - 1]
        //                        ||
        //                        pole[i, j] == pole[i - 1, j + 1])
        //                        ||
        //                        //// if right and down or down and left ocupied
        //                        ((pole[i, j] == pole[i, j + 1] // right
        //                        &&
        //                        pole[i, j] == pole[i - 1, j]) // up                                                                        
        //                        ||
        //                        (pole[i, j] == pole[i - 1, j] // up                                    
        //                        &&
        //                        pole[i, j] == pole[i, j - 1])) // left
        //                        )
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (
        //                        pole[i, j + 1] == 0 // right
        //                        &&
        //                        pole[i, j - 1] == 0 // left
        //                        &&
        //                        pole[i - 1, j] == 0 // up
        //                        )
        //                    {
        //                        if (shipHorizCells > 0)
        //                        {
        //                            if (!allShipsHorizDict.ContainsKey(shipHorizCells))
        //                            {
        //                                allShipsHorizDict.Add(shipHorizCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsHorizDict[shipHorizCells]++;
        //                            }
        //                        }
        //                        shipHorizCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion

        //                #region Если левый средний
        //                else if (j == 0)
        //                {
        //                    #region IF ERRORS
        //                    if (
        //                        //// if conners ocupied
        //                        (pole[i, j] == pole[i - 1, j + 1]
        //                        ||
        //                        pole[i, j] == pole[i + 1, j + 1])
        //                        ||
        //                        //// if right and down or down and left ocupied
        //                        ((pole[i, j] == pole[i, j + 1] // right
        //                        &&
        //                        (pole[i, j] == pole[i + 1, j] // down
        //                        || pole[i, j] == pole[i - 1, j])) // up                                            
        //                        ||
        //                        (pole[i, j] == pole[i + 1, j] // down                                    
        //                        &&
        //                        (pole[i, j] == pole[i, j + 1]))) // right                                
        //                        )
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (
        //                        pole[i, j + 1] == 0 // right
        //                        &&
        //                        pole[i + 1, j] == 0 // down
        //                        &&
        //                        pole[i - 1, j] == 0 // up
        //                        )
        //                    {
        //                        if (shipHorizCells > 0)
        //                        {
        //                            if (!allShipsHorizDict.ContainsKey(shipHorizCells))
        //                            {
        //                                allShipsHorizDict.Add(shipHorizCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsHorizDict[shipHorizCells]++;
        //                            }
        //                        }
        //                        shipHorizCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion

        //                #region Если правый средний
        //                else if (j == 9)
        //                {
        //                    #region IF ERRORS
        //                    if (
        //                        //// if conners ocupied
        //                        (pole[i, j] == pole[i - 1, j - 1]
        //                        ||
        //                        pole[i, j] == pole[i + 1, j - 1])
        //                        ||
        //                        //// if right and down or down and left ocupied
        //                        ((pole[i, j] == pole[i, j - 1] // left
        //                        &&
        //                        (pole[i, j] == pole[i + 1, j] // down
        //                        || pole[i, j] == pole[i - 1, j])) // up                                            
        //                        ||
        //                        (pole[i, j] == pole[i + 1, j] // down                                    
        //                        &&
        //                        (pole[i, j] == pole[i, j - 1]))) // left                                
        //                        )
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (
        //                        pole[i, j - 1] == 0 // left
        //                        &&
        //                        pole[i + 1, j] == 0 // down
        //                        &&
        //                        pole[i - 1, j] == 0 // up
        //                        )
        //                    {
        //                        if (shipHorizCells > 0)
        //                        {
        //                            if (!allShipsHorizDict.ContainsKey(shipHorizCells))
        //                            {
        //                                allShipsHorizDict.Add(shipHorizCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsHorizDict[shipHorizCells]++;
        //                            }
        //                        }
        //                        shipHorizCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion

        //                #region Если центр
        //                else
        //                {
        //                    #region IF ERRORS
        //                    if (
        //                        //// if conners ocupied
        //                        (pole[i, j] == pole[i - 1, j - 1]
        //                        ||
        //                        pole[i, j] == pole[i - 1, j + 1]
        //                        ||
        //                        pole[i, j] == pole[i + 1, j + 1]
        //                        ||
        //                        pole[i, j] == pole[i + 1, j - 1])
        //                        ||
        //                        //// if right and down or down and left ocupied
        //                        ((pole[i, j] == pole[i, j + 1] // right
        //                        &&
        //                        (pole[i, j] == pole[i + 1, j] // down
        //                        || pole[i, j] == pole[i - 1, j])) // up                                            
        //                        ||
        //                        (pole[i, j] == pole[i + 1, j] // down                                    
        //                        &&
        //                        (pole[i, j] == pole[i, j + 1] // right
        //                        || pole[i, j] == pole[i, j - 1]))) // left
        //                        )
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (
        //                        pole[i, j + 1] == 0 // right
        //                        &&
        //                        pole[i, j - 1] == 0 // left
        //                        &&
        //                        pole[i + 1, j] == 0 // down
        //                        &&
        //                        pole[i - 1, j] == 0 // up
        //                        )
        //                    {
        //                        if (shipHorizCells > 0)
        //                        {
        //                            if (!allShipsHorizDict.ContainsKey(shipHorizCells))
        //                            {
        //                                allShipsHorizDict.Add(shipHorizCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsHorizDict[shipHorizCells]++;
        //                            }
        //                        }
        //                        shipHorizCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion
        //            }
        //            //// Если нету, если были корабли, то заносим в словарь и обнуляем счет
        //            else
        //            {
        //                if (shipHorizCells > 1)
        //                {
        //                    if (!allShipsHorizDict.ContainsKey(shipHorizCells))
        //                    {
        //                        allShipsHorizDict.Add(shipHorizCells, 1);
        //                    }
        //                    else
        //                    {
        //                        allShipsHorizDict[shipHorizCells]++;
        //                    }
        //                }
        //                shipHorizCells = 0;
        //            }

        //        }
        //        Console.WriteLine();

        //        //// добавляем если гориз. корабль был по окончании предыдущей
        //        if (shipHorizCells > 1)
        //        {
        //            if (!allShipsHorizDict.ContainsKey(shipHorizCells))
        //            {
        //                allShipsHorizDict.Add(shipHorizCells, 1);
        //            }
        //            else
        //            {
        //                allShipsHorizDict[shipHorizCells]++;
        //            }
        //        }
        //    }
        //    Console.WriteLine();
        //    #endregion

        //    #region Filling Vertical Ships
        //    Dictionary<int, int> allShipsVerticDict = new Dictionary<int, int>();
        //    for (int j = 0; j < width; j++)
        //    {
        //        int shipVerticCells = 0;

        //        for (int i = 0; i < hight; i++)
        //        {
        //            Console.Write($"{pole[i, j]} ");

        //            //// Если есть
        //            if (pole[i, j] == 1)
        //            {
        //                shipVerticCells++;

        //                #region Если верхний левый
        //                if (i == 0 && j == 0)
        //                {
        //                    #region IF ERRORS
        //                    if (//// if conners ocupied
        //                        (pole[i, j] == pole[i + 1, j + 1])
        //                        ||
        //                        //// if right and down or down and left ocupied                            
        //                        (pole[i, j] == pole[i, j + 1] // right
        //                        &&
        //                        pole[i, j] == pole[i + 1, j])) // down                            
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (pole[i, j + 1] == 0 // right
        //                        &&
        //                        pole[i + 1, j] == 0) // down                            
        //                    {
        //                        if (shipVerticCells > 0)
        //                        {
        //                            if (!allShipsVerticDict.ContainsKey(shipVerticCells))
        //                            {
        //                                allShipsVerticDict.Add(shipVerticCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsVerticDict[shipVerticCells]++;
        //                            }
        //                        }
        //                        shipVerticCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion
        //                #region Если нижний левый
        //                else if (i == 9 && j == 0)
        //                {
        //                    #region IF ERRORS
        //                    if (//// if conners ocupied
        //                        (pole[i, j] == pole[i - 1, j + 1])
        //                        ||
        //                        //// if right and down or down and left ocupied                            
        //                        (pole[i, j] == pole[i, j + 1] // right
        //                        &&
        //                        pole[i, j] == pole[i - 1, j])) // up                            
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (pole[i, j + 1] == 0 // right
        //                        &&
        //                        pole[i - 1, j] == 0) // up                            
        //                    {
        //                        if (shipVerticCells > 0)
        //                        {
        //                            if (!allShipsVerticDict.ContainsKey(shipVerticCells))
        //                            {
        //                                allShipsVerticDict.Add(shipVerticCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsVerticDict[shipVerticCells]++;
        //                            }
        //                        }
        //                        shipVerticCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion
        //                #region Если левый средний
        //                else if (j == 0)
        //                {
        //                    #region IF ERRORS
        //                    if (
        //                        //// if conners ocupied
        //                        (pole[i, j] == pole[i - 1, j + 1]
        //                        ||
        //                        pole[i, j] == pole[i + 1, j + 1])
        //                        ||
        //                        //// if right and down or down and left ocupied
        //                        ((pole[i, j] == pole[i, j + 1] // right
        //                        &&
        //                        (pole[i, j] == pole[i + 1, j] // down
        //                        || pole[i, j] == pole[i - 1, j])) // up                                            
        //                        ||
        //                        (pole[i, j] == pole[i + 1, j] // down                                    
        //                        &&
        //                        (pole[i, j] == pole[i, j + 1]))) // right                                
        //                        )
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (
        //                        pole[i, j + 1] == 0 // right
        //                        &&
        //                        pole[i + 1, j] == 0 // down
        //                        &&
        //                        pole[i - 1, j] == 0 // up
        //                        )
        //                    {
        //                        if (shipVerticCells > 0)
        //                        {
        //                            if (!allShipsVerticDict.ContainsKey(shipVerticCells))
        //                            {
        //                                allShipsVerticDict.Add(shipVerticCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsVerticDict[shipVerticCells]++;
        //                            }
        //                        }
        //                        shipVerticCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion

        //                #region Если верхний правый
        //                else if (i == 0 && j == 9)
        //                {
        //                    #region IF ERRORS
        //                    if (//// if conners ocupied
        //                        (pole[i, j] == pole[i + 1, j - 1])
        //                        ||
        //                        //// if right and down or down and left ocupied                            
        //                        (pole[i, j] == pole[i, j - 1] // left
        //                        &&
        //                        pole[i, j] == pole[i + 1, j])) // down                            
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (pole[i, j - 1] == 0 // right
        //                        &&
        //                        pole[i + 1, j] == 0) // down                            
        //                    {
        //                        if (shipVerticCells > 0)
        //                        {
        //                            if (!allShipsVerticDict.ContainsKey(shipVerticCells))
        //                            {
        //                                allShipsVerticDict.Add(shipVerticCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsVerticDict[shipVerticCells]++;
        //                            }
        //                        }
        //                        shipVerticCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion
        //                #region Если нижний правый
        //                else if (i == 9 && j == 9)
        //                {
        //                    #region IF ERRORS
        //                    if (//// if conners ocupied
        //                        (pole[i, j] == pole[i - 1, j - 1])
        //                        ||
        //                        //// if right and down or down and left ocupied                            
        //                        (pole[i, j] == pole[i, j - 1] // left
        //                        &&
        //                        pole[i, j] == pole[i - 1, j])) // up                            
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (pole[i, j - 1] == 0 // left
        //                        &&
        //                        pole[i - 1, j] == 0) // up                            
        //                    {
        //                        if (shipVerticCells > 0)
        //                        {
        //                            if (!allShipsVerticDict.ContainsKey(shipVerticCells))
        //                            {
        //                                allShipsVerticDict.Add(shipVerticCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsVerticDict[shipVerticCells]++;
        //                            }
        //                        }
        //                        shipVerticCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion
        //                #region Если правый средний
        //                else if (j == 9)
        //                {
        //                    #region IF ERRORS
        //                    if (
        //                        //// if conners ocupied
        //                        (pole[i, j] == pole[i - 1, j - 1]
        //                        ||
        //                        pole[i, j] == pole[i + 1, j - 1])
        //                        ||
        //                        //// if right and down or down and left ocupied
        //                        ((pole[i, j] == pole[i, j - 1] // left
        //                        &&
        //                        (pole[i, j] == pole[i + 1, j] // down
        //                        || pole[i, j] == pole[i - 1, j])) // up                                            
        //                        ||
        //                        (pole[i, j] == pole[i + 1, j] // down                                    
        //                        &&
        //                        (pole[i, j] == pole[i, j - 1]))) // left                                
        //                        )
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (
        //                        pole[i, j - 1] == 0 // left
        //                        &&
        //                        pole[i + 1, j] == 0 // down
        //                        &&
        //                        pole[i - 1, j] == 0 // up
        //                        )
        //                    {
        //                        if (shipVerticCells > 0)
        //                        {
        //                            if (!allShipsVerticDict.ContainsKey(shipVerticCells))
        //                            {
        //                                allShipsVerticDict.Add(shipVerticCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsVerticDict[shipVerticCells]++;
        //                            }
        //                        }
        //                        shipVerticCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion

        //                #region Если верхний средний
        //                else if (i == 0)
        //                {
        //                    #region IF ERRORS
        //                    if (
        //                        //// if conners ocupied
        //                        (pole[i, j] == pole[i + 1, j + 1]
        //                        ||
        //                        pole[i, j] == pole[i + 1, j - 1])
        //                        ||
        //                        //// if right and down or down and left ocupied
        //                        ((pole[i, j] == pole[i, j + 1] // right
        //                        &&
        //                        pole[i, j] == pole[i + 1, j]) // down                                                                        
        //                        ||
        //                        (pole[i, j] == pole[i + 1, j] // down                                    
        //                        &&
        //                        pole[i, j] == pole[i, j - 1])) // left
        //                        )
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (
        //                        pole[i, j + 1] == 0 // right
        //                        &&
        //                        pole[i, j - 1] == 0 // left
        //                        &&
        //                        pole[i + 1, j] == 0 // down
        //                        )
        //                    {
        //                        if (shipVerticCells > 0)
        //                        {
        //                            if (!allShipsVerticDict.ContainsKey(shipVerticCells))
        //                            {
        //                                allShipsVerticDict.Add(shipVerticCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsVerticDict[shipVerticCells]++;
        //                            }
        //                        }
        //                        shipVerticCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion

        //                #region Если нижний средний
        //                else if (i == 9)
        //                {
        //                    #region IF ERRORS
        //                    if (
        //                        //// if conners ocupied
        //                        (pole[i, j] == pole[i - 1, j - 1]
        //                        ||
        //                        pole[i, j] == pole[i - 1, j + 1])
        //                        ||
        //                        //// if right and down or down and left ocupied
        //                        ((pole[i, j] == pole[i, j + 1] // right
        //                        &&
        //                        pole[i, j] == pole[i - 1, j]) // up                                                                        
        //                        ||
        //                        (pole[i, j] == pole[i - 1, j] // up                                    
        //                        &&
        //                        pole[i, j] == pole[i, j - 1])) // left
        //                        )
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (
        //                        pole[i, j + 1] == 0 // right
        //                        &&
        //                        pole[i, j - 1] == 0 // left
        //                        &&
        //                        pole[i - 1, j] == 0 // up
        //                        )
        //                    {
        //                        if (shipVerticCells > 0)
        //                        {
        //                            if (!allShipsVerticDict.ContainsKey(shipVerticCells))
        //                            {
        //                                allShipsVerticDict.Add(shipVerticCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsVerticDict[shipVerticCells]++;
        //                            }
        //                        }
        //                        shipVerticCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion

        //                #region Если центр
        //                else
        //                {
        //                    #region IF ERRORS
        //                    if (
        //                        //// if conners ocupied
        //                        (pole[i, j] == pole[i - 1, j - 1]
        //                        ||
        //                        pole[i, j] == pole[i - 1, j + 1]
        //                        ||
        //                        pole[i, j] == pole[i + 1, j + 1]
        //                        ||
        //                        pole[i, j] == pole[i + 1, j - 1])
        //                        ||
        //                        //// if right and down or down and left ocupied
        //                        ((pole[i, j] == pole[i, j + 1] // right
        //                        &&
        //                        (pole[i, j] == pole[i + 1, j] // down
        //                        || pole[i, j] == pole[i - 1, j])) // up                                            
        //                        ||
        //                        (pole[i, j] == pole[i + 1, j] // down                                    
        //                        &&
        //                        (pole[i, j] == pole[i, j + 1] // right
        //                        || pole[i, j] == pole[i, j - 1]))) // left
        //                        )
        //                    {
        //                        return false;
        //                    }
        //                    #endregion
        //                    #region LONIES
        //                    else if (
        //                        pole[i, j + 1] == 0 // right
        //                        &&
        //                        pole[i, j - 1] == 0 // left
        //                        &&
        //                        pole[i + 1, j] == 0 // down
        //                        &&
        //                        pole[i - 1, j] == 0 // up
        //                        )
        //                    {
        //                        if (shipVerticCells > 0)
        //                        {
        //                            if (!allShipsVerticDict.ContainsKey(shipVerticCells))
        //                            {
        //                                allShipsVerticDict.Add(shipVerticCells, 1);
        //                            }
        //                            else
        //                            {
        //                                allShipsVerticDict[shipVerticCells]++;
        //                            }
        //                        }
        //                        shipVerticCells = 0;
        //                    }
        //                    #endregion
        //                }
        //                #endregion
        //            }
        //            //// Если нету, то если быди корабли, то заносим в слоаврь и обнуляем счет
        //            else
        //            {
        //                if (shipVerticCells > 1)
        //                {
        //                    if (!allShipsVerticDict.ContainsKey(shipVerticCells))
        //                    {
        //                        allShipsVerticDict.Add(shipVerticCells, 1);
        //                    }
        //                    else
        //                    {
        //                        allShipsVerticDict[shipVerticCells]++;
        //                    }
        //                }
        //                shipVerticCells = 0;
        //            }
        //        }
        //        Console.WriteLine();

        //        //// добавляем если гориз. корабль был по окончании предыдущей
        //        if (shipVerticCells > 1)
        //        {
        //            if (!allShipsVerticDict.ContainsKey(shipVerticCells))
        //            {
        //                allShipsVerticDict.Add(shipVerticCells, 1);
        //            }
        //            else
        //            {
        //                allShipsVerticDict[shipVerticCells]++;
        //            }
        //        }
        //    }

        //    foreach (var item in allShipsVerticDict)
        //    {
        //        if (!allShipsHorizDict.ContainsKey(item.Key))
        //        {
        //            allShipsHorizDict.Add(item.Key, item.Value);
        //        }
        //        else
        //        {
        //            allShipsHorizDict[item.Key] += item.Value;
        //        }
        //    }
        //    Console.WriteLine();
        //    #endregion

        //    foreach (var item in shablon)
        //    {
        //        if (allShipsHorizDict.ContainsKey(item.Key))
        //        {
        //            if (allShipsHorizDict[item.Key] == item.Value)
        //            {
        //                continue;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}
        #endregion
        #endregion

        #region CodeWars tests
        #region 021 - 4kyu (parseInt() reloaded) Convert a string into an integer. The strings simply represent the numbers in words.

        //static void Main(string[] args)
        //{
        //    Console.WriteLine(ParseInt("five million six hundred eighty nine thousand four hundred twenty-seven"));
        //    //Console.WriteLine(ParseInt("five million six lakh eighty nine thousand four hundred twenty-seven"));

        //    //Console.WriteLine(ParseInt("thirty-five million six hundred sixty-six thousand six hundred sixty-six"));
        //    //Console.WriteLine(ParseInt("six hundred sixty six thousand six hundred sixty-six"));
        //}

        #region from Web 02 - RegEx
        //private static Dictionary<string, int> numberTable =
        //new Dictionary<string, int>
        //{{"zero",0},{"one",1},{"two",2},{"three",3},{"four",4},
        //    {"five",5},{"six",6},{"seven",7},{"eight",8},{"nine",9},
        //    {"ten",10},{"eleven",11},{"twelve",12},{"thirteen",13},
        //    {"fourteen",14},{"fifteen",15},{"sixteen",16},
        //    {"seventeen",17},{"eighteen",18},{"nineteen",19},{"twenty",20},
        //    {"thirty",30},{"forty",40},{"fifty",50},{"sixty",60},
        //    {"seventy",70},{"eighty",80},{"ninety",90},{"hundred",100},
        //    {"thousand",1000},{"million",1000000}};

        //public static int ParseInt(string s)
        //{
        //    var numbers = Regex.Matches(s, @"\w+").Cast<Match>()
        //         .Select(m => m.Value.ToLowerInvariant())
        //         .Where(v => numberTable.ContainsKey(v))
        //         .Select(v => numberTable[v]);
        //    int acc = 0, total = 0;
        //    foreach (var n in numbers)
        //    {
        //        if (n >= 1000)
        //        {
        //            total += (acc * n);
        //            acc = 0;
        //        }
        //        else if (n >= 100)
        //        {
        //            acc *= n;
        //        }
        //        else acc += n;
        //    }
        //    return (total + acc);
        //}
        #endregion

        #region from Web 01
        //static Dictionary<string, int> numbers = new Dictionary<string, int> { {"zero", 0 }, { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 },
        // { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 }, { "ten", 10 }, { "eleven", 11 }, { "twelve", 12 }, { "thirteen", 13 }, { "fourteen", 14  }, { "fifteen", 15 },
        // { "sixteen", 16}, { "seventeen", 17 }, { "eighteen", 18 }, { "nineteen", 19 }, { "twenty", 20 }, { "thirty", 30 }, { "forty", 40 }, { "fifty", 50 },
        // { "sixty", 60 }, { "seventy", 70 }, { "eighty", 80 }, { "ninety", 90 } };

        //static Dictionary<string, int> multipliers = new Dictionary<string, int> { { "hundred", 100 }, { "thousand", 1000 }, { "million", 1000000 } };

        //public static int ParseInt(string s)
        //{
        //    s = s.Replace(" and ", " ").Replace("-", " ");
        //    int result = 0;
        //    var list = s.Split(' ');
        //    int multiplier = 1;
        //    for (int i = list.Length - 1; i >= 0; i--)
        //    {
        //        if (numbers.ContainsKey(list[i]))
        //            result += (numbers[list[i]] * multiplier);
        //        else if (multipliers.ContainsKey(list[i]))
        //        {
        //            if (multiplier < multipliers[list[i]])
        //                multiplier = multipliers[list[i]];
        //            else
        //                multiplier *= multipliers[list[i]];
        //        }
        //    }
        //    return result;
        //}
        #endregion

        #region My Solution
        //public static int ParseInt(string s)
        //{
        //    #region Creatting a dictionary with Numbers and Letters
        //    Dictionary<string, int> dict = new Dictionary<string, int>()
        //    {
        //        {"zero", 0},
        //        {"one", 1},
        //        {"two", 2},
        //        {"three", 3},
        //        {"four", 4},
        //        {"five", 5},
        //        {"six", 6},
        //        {"seven", 7},
        //        {"eight", 8},
        //        {"nine", 9},
        //        {"ten", 10},

        //        {"eleven",11},
        //        {"twelve",12},
        //        {"thirteen",13},
        //        {"fourteen",14},
        //        {"fifteen",15},
        //        {"sixteen",16},
        //        {"seventeen",17},
        //        {"eighteen",18},
        //        {"nineteen",19},

        //        {"twenty",20},
        //        {"thirty",30},
        //        {"forty",40},
        //        {"fifty",50},
        //        {"sixty",60},
        //        {"seventy",70},
        //        {"eighty",80},
        //        {"ninety",90},

        //        {"hundred",100},

        //        {"thousand",1000},

        //        {"lakh",100000},

        //        {"million",1000000}
        //    };
        //    #endregion

        //    s = s.Replace(" and", "");

        //    //// 01 splitting into parts
        //    Dictionary<string, string> splitted = new Dictionary<string, string>();
        //    for (int i = 0; i < s.Length; i++)
        //    {
        //        if (s.Contains("million"))
        //        {

        //            splitted.Add("million", s.Split("million")[0]+ "million".Trim());
        //            s = s.Split("million")[1].Trim();
        //        }
        //        else if (s.Contains("thousand"))
        //        {
        //            splitted.Add("thousand", s.Split("thousand")[0] + "thousand".Trim());
        //            s = s.Split("thousand")[1].Trim();
        //        }
        //        else if (s.Contains("hundred"))
        //        {
        //            splitted.Add("hundred", s.Split("hundred")[0] + "hundred".Trim());
        //            s = s.Split("hundred")[1].Trim();
        //        }
        //        else
        //        {
        //            splitted.Add("dcds", s.Trim() + " one");
        //            s = "";
        //        }

        //    }

        //    //// 02 continue
        //    List<string> numList = new List<string>();
        //    foreach (var item in splitted)
        //    {
        //        numList.Add(item.Value);
        //    }

        //    var ssss = numList.Select(x => x.Split(" ")).ToList();

        //    //// getting the array in numbers
        //    List<int> numsArray = new List<int>();
        //    foreach (var item in ssss)
        //    {
        //        List<int> tmp = new List<int>();

        //        for (int i = 0; i < item.Count(); i++)
        //        {
        //            if (item[i].Contains("-"))
        //            {
        //                string[] tmpArray = item[i].Split("-");
        //                string frstNum = dict.Where(x => x.Key.ToUpper() == tmpArray[0].ToUpper()).Select(a => a.Value).First().ToString().Remove(1);
        //                tmp.Add(Int32.Parse(frstNum + dict.Where(x => x.Key.ToUpper() == tmpArray[1].ToUpper()).Select(a => a.Value).First().ToString()));
        //            }
        //            else
        //            {
        //                tmp.Add(Int32.Parse(dict.Where(x => x.Key.ToUpper() == item[i].ToUpper()).Select(a => a.Value).First().ToString()));
        //            }
        //        }

        //        List<int> TmpList = new List<int>();
        //        for (int i = 0; i < tmp.Count-1; i++)
        //        {
        //            //// если последняя запись
        //            if (i == tmp.Count() - 2)
        //            {
        //                TmpList.Add(tmp[i]);
        //            }
        //            //// если в записи 1 цифра
        //            else if (tmp[i + 1].ToString().Length == 1)
        //            {
        //                var newNum = (tmp[i] + tmp[i + 1]);
        //                TmpList.Add(newNum);
        //                i++;
        //            }
        //            else
        //            {
        //                var newNum = tmp[i] * tmp[i + 1];
        //                TmpList.Add(newNum);
        //                i++;
        //            }
        //        }

        //        numsArray.Add(TmpList.Sum() * tmp.Last());
        //    }

        //    return numsArray.Sum();
        //}
        #endregion
        #endregion

        #region 020 - 4kyu (Strip Comments) Complete the solution so that it strips all text that follows any of a set of comment markers passed in.

        //static void Main(string[] args)
        //{
        //    Console.WriteLine(StripComments("apples, pears # and bananas\ngrapes\nbananas !apples", new string[] { "#", "!" }));
        //    //Console.WriteLine(StripComments("a #b\nc\nd $e f g", new string[] { "#", "$" }));
        //}

        #region from web 03 - интересно как находит символы в строке (linq)
        public static string StripComments(string text, string[] commentSymbols)
        {
            if (string.IsNullOrWhiteSpace(text) || commentSymbols.Length == 0) return string.Empty;
            var result = new List<string>();
            var rows = text.Split('\n');

            foreach (var row in rows)
            {
                var str = row.TrimEnd();
                foreach (var symbol in commentSymbols.Where(row.Contains)) //интересно как находит символы в строке (linq)
                {
                    var id = str.IndexOf(symbol, StringComparison.Ordinal);
                    if (id == -1) continue;
                    str = str.Substring(0, id).Trim();
                }

                result.Add(str);
            }

            return string.Join("\n", result);
        }
        #endregion

        #region from Web 02 - Regex
        //public static string StripComments(string text, string[] commentSymbols)
        //{ // [ \t]*([#].*|$)
        //    string regex = @"[ \t]*([" + string.Join("", commentSymbols) + @"].*|$)";
        //    return Regex.Replace(text, regex, "", RegexOptions.Multiline);
        //}
        #endregion

        #region from Web 01
        //public static string StripComments(string text, string[] commentSymbols)
        //{
        //    string[] texts = text.Split("\n");

        //    string[] lines = text.Split(new[] { "\n" }, StringSplitOptions.None);
        //    //// делит список сиволами из commentSymbols на подсписки и выбирает первую запись
        //    lines = lines.Select(x => x.Split(commentSymbols, StringSplitOptions.None).First().TrimEnd()).ToArray();
        //    return string.Join("\n", lines);
        //}
        #endregion

        #region My Solution
        //public static string StripComments(string text, string[] commentSymbols)
        //{
        //    List<string> beginList = text.Split(new[] { "\n" }, StringSplitOptions.None).ToList();
        //    List<string> newList = new List<string>();

        //    foreach (var item in beginList)
        //    {
        //        int FoundSymbols = 0;
        //        foreach (string symbol in commentSymbols)
        //        {
        //            int symbolIndex = item.IndexOf(symbol);
        //            if (symbolIndex != -1)
        //            {
        //                newList.Add(item.Remove(symbolIndex).TrimEnd());
        //                FoundSymbols++;
        //            }
        //        }
        //        if (FoundSymbols==0)
        //        {
        //            newList.Add(item.TrimEnd());
        //        }                
        //    }

        //    string result = string.Join("\n", newList);

        //    return result;
        //}
        #endregion

        #endregion

        #region 019 - 5kyu (Directions Reduction) Write a function dirReduc which will take an array of strings and 
        //// returns an array of strings with the needless directions removed (W<->E or S<->N side by side).

        //static void Main(string[] args)
        //{
        //    string[] a = new string[] { "NORTH", "SOUTH", "SOUTH", "SOUTH", "EAST", "WEST", "NORTH", "NORTH", "WEST" };
        //    //string[] a = new string[] { "NORTH", "SOUTH", "SOUTH", "EAST", "WEST", "NORTH", "WEST" };
        //    //string[] a = new string[] { "SOUTH", "NORTH", "WEST" };
        //    //string[] a = new string[] { "WEST" };

        //    //string[] a = new string[] { "NORTH", "WEST", "SOUTH", "EAST" };

        //    foreach (var item in dirReduc(a))
        //    {
        //        Console.WriteLine(item);
        //    }
        //}

        #region from Web 03 (Kozhanov)
        public static string[] dirReduc(string[] arr)
        {
            var direction = string.Join(" ", arr);

            for (var i = 0; i < arr.Length; i++)
            {
                direction = Regex.Replace(direction, @"NORTH\s+SOUTH|SOUTH\s+NORTH|EAST\s+WEST|WEST\s+EAST", "");
            }

            return direction.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion

        #region from Web 02
        //public static string[] dirReduc(String[] arr)
        //{
        //    Dictionary<string, string> oppositeOf = new Dictionary<string, string>()
        //    {
        //        {"NORTH", "SOUTH"},
        //        {"SOUTH", "NORTH"},
        //        {"EAST", "WEST"},
        //        {"WEST", "EAST"}
        //    };

        //    List<string> betterDirections = new List<string>();
        //    foreach (var direction in arr)
        //    {
        //        var ss = oppositeOf[direction];

        //        if (betterDirections.LastOrDefault() == oppositeOf[direction])
        //        {
        //            betterDirections.RemoveAt(betterDirections.Count - 1);
        //        }
        //        else
        //        {
        //            betterDirections.Add(direction);
        //        }
        //    }
        //    return betterDirections.ToArray();
        //}
        #endregion

        #region from Web 01
        //public static String[] dirReduc(String[] arr)
        //{
        //    Stack<String> stack = new Stack<String>();

        //    foreach (String direction in arr)
        //    {
        //        String lastElement = stack.Count > 0 ? stack.Peek().ToString() : null;

        //        switch (direction)
        //        {
        //            case "NORTH": if ("SOUTH".Equals(lastElement)) { stack.Pop(); } else { stack.Push(direction); } break;
        //            case "SOUTH": if ("NORTH".Equals(lastElement)) { stack.Pop(); } else { stack.Push(direction); } break;
        //            case "EAST": if ("WEST".Equals(lastElement)) { stack.Pop(); } else { stack.Push(direction); } break;
        //            case "WEST": if ("EAST".Equals(lastElement)) { stack.Pop(); } else { stack.Push(direction); } break;
        //        }
        //    }
        //    String[] result = stack.ToArray();
        //    Array.Reverse(result);

        //    return result;
        //}
        #endregion

        #region My Solution
        //public static string[] dirReduc(String[] arr)
        //{
        //    List<string> newRecs = new List<string>();

        //    string[] newarr = arr;

        //    for (int a = 0; a < newarr.Length; a++)
        //    {
        //        newRecs.Clear();

        //        if (newarr.Length == 1)
        //        {
        //            newRecs.Add(newarr[0]);
        //        }
        //        else
        //        {
        //            for (int i = 0; i < newarr.Length - 1; i++)
        //            {
        //                if (
        //                    newarr[i] == "NORTH" && newarr[i + 1] == "SOUTH"
        //                    ||
        //                    newarr[i] == "SOUTH" && newarr[i + 1] == "NORTH"
        //                    ||
        //                    newarr[i] == "EAST" && newarr[i + 1] == "WEST"
        //                    ||
        //                    newarr[i] == "WEST" && newarr[i + 1] == "EAST"
        //                    )
        //                {
        //                    i++;
        //                }
        //                else
        //                {
        //                    newRecs.Add(newarr[i]);
        //                }

        //                if (i == newarr.Length - 2)
        //                {
        //                    newRecs.Add(newarr[i + 1]);
        //                }
        //            }
        //        }

        //        newarr= newRecs.ToArray();                
        //    }

        //    return newRecs.ToArray();
        //}
        #endregion
        #endregion

        #region 018 - 4kyu (Catching Car Mileage Numbers)

        //static void Main(string[] args)
        //{
        //    //int isInterest = (IsInteresting(999, new List<int>() { 1337, 256 })); // 0
        //    //int isInterest = (IsInteresting(3, new List<int>() { 1337, 256 }));     // 0
        //    int isInterest = (IsInteresting(102, new List<int>() { 1337, 256 }));  // 1
        //    //int isInterest = (IsInteresting(1337, new List<int>() { 1337, 256 }));  // 2
        //    //int isInterest = (IsInteresting(11209, new List<int>() { 1337, 256 })); // 1
        //    //int isInterest = (IsInteresting(11211, new List<int>() { 1337, 256 })); // 2
        //    //int isInterest = (IsInteresting(8901, new List<int>() { 1337, 256 }));

        //    switch (isInterest)
        //    {
        //        case 2:
        //            Console.WriteLine($"HEY, LOOK!!! YOU'VE GOT AN INTERESTING NUMBER ON TABLO");
        //            break;
        //        case 1:
        //            Console.WriteLine($"WARNING!!! You are close to an interesting number");
        //            break;
        //        default:
        //            Console.WriteLine($"Nothing interesting");
        //            break;
        //    }
        //}

        #region from Web 02
        public static int IsInteresting(int number, List<int> awesomePhrases)
        {
            int score = 2;

            for (int i = 0; i < 3; i++)
            {
                if (number > 99)
                {
                    if (awesomePhrases.Contains(number)) return score;
                    if (AllEqual(number)) return score;
                    if (AllFollowingAreZeros(number)) return score;
                    if (IsPalindrome(number)) return score;
                    if (IsIncrementing(number)) return score;
                    if (IsDecrementing(number)) return score;
                }
                score = 1;
                number++;
            }
            return 0;
        }

        private static bool IsDecrementing(int number) => "09876543210".Contains(number.ToString());
        private static bool IsIncrementing(int number) => "01234567890".Contains(number.ToString());
        private static bool AllEqual(int number) => number.ToString().Distinct().Count() == 1;
        private static bool AllFollowingAreZeros(int number) => number.ToString().Skip(1).All(x => x == '0');
        private static bool IsPalindrome(int number) => number.ToString() == string.Concat(number.ToString().Reverse());

        #endregion

        #region from Web 01
        //public static int IsInteresting(int number, List<int> awesomePhrases)
        //{
        //    return Enumerable.Range(number, 3)
        //      .Where(x => Interesting(x, awesomePhrases))
        //      .Select(x => (number - x + 4) / 2)
        //      .FirstOrDefault();
        //}

        //private static bool Interesting(int num, List<int> awesome)
        //{
        //    if (num < 100) return false;
        //    var s = num.ToString();
        //    return awesome.Contains(num)
        //      || s.Skip(1).All(c => c == '0')
        //      || s.Skip(1).All(c => c == s[0])
        //      || "1234567890".Contains(s)
        //      || "9876543210".Contains(s)
        //      || s.SequenceEqual(s.Reverse());
        //}
        #endregion

        #region My Solution
        //public static int IsInteresting(int number, List<int> awesomePhrases)
        //{
        //    int isInterest = 0;

        //    int currentMillage = number;

        //    int tmp = 1;

        //    int curNumber = number;

        //    #region 06 The digits match one of the values in the awesomePhrases array
        //    for (int j = 0; j <= 2; j++)
        //    {
        //        var awesome = awesomePhrases.Where(x => x == curNumber);

        //        if (awesome.Count() != 0 && j == 0)
        //        {
        //            isInterest = 2;
        //            break;
        //        }
        //        else if (awesome.Count() != 0 && j != 0)
        //        {
        //            isInterest = 1;
        //            break;
        //        }

        //        curNumber++;
        //    }
        //    curNumber = 0;

        //    if (isInterest==2)
        //    {
        //        return isInterest;
        //    }
        //    #endregion

        //    #region 05 The digits are a palindrome: 1221 or 73837

        //    for (int j = 0; j <= 2; j++)
        //    {
        //        tmp = 1;

        //        int[] numsArray = new int[(number + j).ToString().Length];
        //        for (int i = 0; i < (number + j).ToString().Length; i++)
        //        {
        //            numsArray[i] = Int32.Parse((number + j).ToString()[i].ToString());
        //        }


        //        for (int i = 0; i < numsArray.Length - 1; i++)
        //        {
        //            if (numsArray[i] == numsArray[numsArray.Length - 1 - i])
        //            {
        //                tmp++;
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }

        //        if (number > 99 && tmp == numsArray.Length && j == 0)
        //        {
        //            isInterest = 2;
        //            break;
        //        }
        //        else if (number > 99 && tmp == numsArray.Length && j != 0)
        //        {
        //            isInterest = 1;
        //            break;
        //        }
        //    }

        //    if (isInterest == 2)
        //    {
        //        return isInterest;
        //    }
        //    #endregion

        //    #region 04 The digits are sequential, decrementing‡: 4321

        //    for (int j = 0; j <= 2; j++)
        //    {
        //        tmp = 1;

        //        int[] numsArray = new int[(number + j).ToString().Length];
        //        for (int i = 0; i < (number + j).ToString().Length; i++)
        //        {
        //            numsArray[i] = Int32.Parse((number + j).ToString()[i].ToString());
        //        }

        //        for (int i = 0; i < numsArray.Length - 1; i++)
        //        {
        //            if (numsArray[i] == numsArray[i + 1] + 1)
        //            {
        //                tmp++;
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }

        //        if (number > 99 && tmp == numsArray.Length && j == 0)
        //        {
        //            isInterest = 2;
        //            break;
        //        }
        //        else if (number > 99 && tmp == numsArray.Length && j != 0)
        //        {
        //            isInterest = 1;
        //            break;
        //        }
        //    }

        //    if (isInterest == 2)
        //    {
        //        return isInterest;
        //    }
        //    #endregion

        //    #region 03 The digits are sequential, incementing: 1234
        //    //////// For incrementing sequences, 0 should come after 9, and not before 1, as in 7890

        //    for (int j = 0; j <= 2; j++)
        //    {
        //        tmp = 1;

        //        int[] numsArray = new int[(number + j).ToString().Length];
        //        for (int i = 0; i < (number + j).ToString().Length; i++)
        //        {
        //            numsArray[i] = Int32.Parse((number + j).ToString()[i].ToString());
        //        }

        //        for (int i = 0; i < numsArray.Length - 1; i++)
        //        {
        //            if (i == (numsArray.Length - 2) && numsArray[i + 1] == 0)
        //            {
        //                numsArray[i + 1] = 10;
        //            }

        //            if (numsArray[i] == numsArray[i + 1] - 1)
        //            {
        //                tmp++;
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }

        //        if (number > 99 && tmp == numsArray.Length && j == 0)
        //        {
        //            isInterest = 2;
        //            break;
        //        }
        //        else if (number > 99 && tmp == numsArray.Length && j != 0)
        //        {
        //            isInterest = 1;
        //            break;
        //        }
        //    }
        //    if (isInterest == 2)
        //    {
        //        return isInterest;
        //    }
        //    #endregion

        //    #region 02 Every digit is the same number: 1111
        //    for (int j = 0; j <= 2; j++)
        //    {
        //        tmp = 1;

        //        int[] numsArray = new int[(number + j).ToString().Length];
        //        for (int i = 0; i < (number + j).ToString().Length; i++)
        //        {
        //            numsArray[i] = Int32.Parse((number + j).ToString()[i].ToString());
        //        }

        //        for (int i = 0; i < numsArray.Length - 1; i++)
        //        {
        //            if (numsArray[i] == numsArray[i + 1])
        //            {
        //                tmp++;
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }

        //        if (number > 99 && tmp == numsArray.Length && j == 0)
        //        {
        //            isInterest = 2;
        //            break;
        //        }
        //        else if (number > 99 && tmp == numsArray.Length && j != 0)
        //        {
        //            isInterest = 1;
        //            break;
        //        }
        //    }
        //    if (isInterest == 2)
        //    {
        //        return isInterest;
        //    }
        //    #endregion

        //    #region 01 Any digit followed by all zeros: 100, 90000
        //    //// getting the number with nulls-as much as number's length - 1
        //    int numWithNulls = 1;
        //    for (int i = 0; i < number.ToString().Length - 1; i++)
        //    {
        //        numWithNulls = numWithNulls * 10;
        //    }
        //    int interestingNumberExact = (Int32.Parse(number.ToString().First().ToString()) * numWithNulls);
        //    int interestingNumberClose = ((Int32.Parse(number.ToString().First().ToString()) + 1) * numWithNulls);

        //    if (number > 99 && number == interestingNumberExact)
        //    {
        //        isInterest = 2;
        //    }
        //    else if (number > 97 && (number + 1 == interestingNumberClose || number + 2 == interestingNumberClose))
        //    {
        //        isInterest = 1;
        //    }

        //    if (isInterest == 2)
        //    {
        //        return isInterest;
        //    }
        //    #endregion

        //    return isInterest;
        //}
        #endregion

        #endregion

        #region 017 - 5kyu (Valid Parentheses) takes a string of parentheses, and determines if the order of the parentheses is valid

        //static void Main(string[] args)
        //{
        //    //Console.WriteLine(ValidParentheses("()"));
        //    //Console.WriteLine(ValidParentheses(")(((("));
        //    //Console.WriteLine(ValidParentheses(")(()))"));
        //    //Console.WriteLine(ValidParentheses("(())((()())())"));
        //    //Console.WriteLine(ValidParentheses(")())((()())()("));
        //    //Console.WriteLine(ValidParentheses(""));
        //    Console.WriteLine(ValidParentheses("iuew647usef7w4%^%&e76337234>{<<{{{{]"));
        //}

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
#region ПРИМЕРЫ
#region цикл внутри метода - вызывает сам себя
//int cellsCountByColumn(int i, int j)
//{
//    //// когда одно из условий соблюдено - останавливается цикл
//    if (field[i, j] != 1 || i > 9) return 0;
//    if (CheckWrongCorners(i, j)) return -1;

//    //// проверенной клетке с единицей присваиваем значение -1
//    field[i, j] = -1;

//    //// после того как цикл остановился (выше) - возвращается последнее полученное значение до окончания цикла
//    return 1 + cellsCountByColumn(++i, j);
//}
#endregion
#region Sorting Dictionary (Dictionary не имеет метода сортировки)
//List<KeyValuePair<string, string>> myList = aDictionary.ToList();

//myList.Sort(
//    delegate (KeyValuePair<string, string> pair1,
//    KeyValuePair<string, string> pair2)
//    {
//        return pair1.Value.CompareTo(pair2.Value);
//    }
//);

//////или так после .NET 2.0
//var myList = aDictionary.ToList();
//myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
#endregion

#region Dictionary - Словари (Заполнение)
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

////// ЕЩЕ
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
#endregion

#region пример заполнения двумерного массива циклом
//int hight = 10;
//int width = 10;

//int[,] pole = new int[hight, width];

//for (int i = 0; i < hight; i++)
//{
//    for (int j = 0; j < width; j++)
//    {
//        pole[i, j] = 0;
//    }
//} 

////// еще
////    Console.Write("Введите высоту: ");
////    int hight = Convert.ToInt32(Console.ReadLine());
////    Console.Write("Введите ширину: ");
////    int width = Convert.ToInt32(Console.ReadLine());

////    int[,] array = new int[hight, width];
////    for (int i = 0; i < hight; i++)
////    {
////        for (int j = 0; j < width; j++)
////        {
////            array[i, j] = (i * hight) + j + 1;
////        }
////    }

////    for (int i = 0; i < hight; i++)
////    {
////        for (int j = 0; j < width; j++)
////        {
////            Console.Write("{0,4}", array[i, j]);
////        }
////        Console.WriteLine();
////    }
////    Console.WriteLine();
#endregion

#region строку string проще делить так:
//string[] texts = text.Split("\n");

////это стандартный вариант:
//string[] texts = text.Split(new[] { "\n" }, StringSplitOptions.None))
#endregion

#region заменить запись в списке (если в цикле, то ломается коллекция и получаем исключение)
//beginList[beginList.FindIndex(ind=>ind.Equals(item))] = item.Remove(symbolIndex).Trim();
#endregion

#region добавляю-убираю записи
//HashSet<string> myStrings = new HashSet<string>();
//foreach (string item in arr)
//{
//    if (item == "NORTH" && myStrings.Contains("SOUTH"))
//    {
//        myStrings.Remove("SOUTH");
//    }
//    else if (item == "SOUTH" && myStrings.Contains("NORTH"))
//    {
//        myStrings.Remove("NORTH");
//    }
//    else if (item == "EAST" && myStrings.Contains("WEST"))
//    {
//        myStrings.Remove("WEST");
//    }
//    else if (item == "WEST" && myStrings.Contains("EAST"))
//    {
//        myStrings.Remove("EAST");
//    }
//    else
//    {
//        myStrings.Add(item);
//    }
//}
////int result = Int32.Parse(string.Join("", myStrings)); 
#endregion

#region пример группировки с подсчетом
//var groupped = arr
//        .GroupBy(d => d)
//        .Select(g => new { name = g.Key, count = g.Count() })
//        .ToList();
#endregion

#endregion