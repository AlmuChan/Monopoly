/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 18-May-2018: Create class
 * 0.02, 28-May-2018: Added struct
 */

using System.IO;
using System;
public struct CardStruct
{
    public string text;
    public string action;
    public short num;
}

class Card : Square
{
    public string Type { get; set; }
    CardStruct[] card;

    public Card(short Num, short X, short Y, string Name, string Type) 
        : base(Num,X,Y,Name)
    {
        this.Type = Type;
        card = new CardStruct[16];
    }

    public void ReadCards()
    {
        if (!File.Exists("Files/chance.txt"))
            Console.WriteLine("File not exists");
        else
        {
            try
            {
                StreamReader sr = new StreamReader("Files/chance.txt");
                string line;
                do
                {
                    line = sr.ReadLine();
                    if (line != null)
                    {
                        string[] words = line.Split('-');

                        for(int j = 0; j < card.Length;j++)
                        {
                            card[j].text = words[1];
                            card[j].action = words[2];
                            card[j].num = Convert.ToInt16(words[3]);
                        }
                    }
                }
                while (line != null);
                sr.Close();
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

        for(int i = 0; i < card.Length; i++)
        {
            Console.WriteLine(card[i].text);
        }
    }
}
