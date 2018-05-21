﻿/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 15-May-2018: Create class and constructor
 */
using System;
using System.IO;
using System.Collections.Generic;

class Square
{
    public short Num { get; set; }
    public short X { get; set; }
    public short Y { get; set; }
    public string Name { get; set; }

    public Square(short Num, short X, short Y, string Name)
    {
        this.Num = Num;
        this.Y = Y;
        this.X = X;
        this.Name = Name;
    }

    //Read files of squares and add to list of squares
    public static List<Square> ReadSquares()
    {
        List<Square> squares = new List<Square>();

        if (!File.Exists("Files/Squares.txt"))
            Console.WriteLine("File not exists");
        else
        {
            try
            {

                StreamReader sw = new StreamReader("Files/Squares.txt");
                string line;
                //Initialize x and y positions for square
                short x = 530;
                short y = 530;
                do
                {
                    line = sw.ReadLine();
                    if (line != null)
                    {

                        string[] words = line.Split('-');
                        int i = Convert.ToInt32(words[0]);
                        string name = words[1];
                        Square s = new Square((short)(i), x, y, name);

                        switch (words.Length)
                        {
                            case 4:
                                s = new Property((short)(i), x, y, name + " P",
                                    words[2], words[3]);
                                break;
                            case 3:
                                if (words[2] != "CC" && words[2] != "C")
                                    s = new Tax((short)(i), x, y, name + " T",
                                        Convert.ToInt32(words[2]));
                                else
                                    s = new Card((short)(i), x, y, name + " C",
                                        words[2]);
                                break;
                        }
                        squares.Add(s);

                        //Calculate x and y of squares
                        if (i < 11)
                            x -= 50;
                        else if (i < 21)
                            y -= 50;
                        else if (i < 31)
                            x += 50;
                        else
                            y += 50;
                    }
                }
                while (line != null);
                sw.Close();
                return squares;
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("Path too long");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not accessible");
            }
            catch (IOException e)
            {
                Console.WriteLine("I/O error: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Oooops... " + e.Message);
            }
        }
        return squares;
    }
}
