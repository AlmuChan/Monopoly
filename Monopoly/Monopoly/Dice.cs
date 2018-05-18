/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 14-May-2018: Create class and method Throw
 */

using System;

class Dice
{
    public Image dice;
    int[] faces;
    public short X { get; set; }
    public short Y { get; set; }
    public const short WIDTH = 80;
    public const short HEIGHT = 80;
    Random rnd = new Random();
    public short numDice;

    public Dice()
    {
        dice = new Image("Images/dice.png", 450, 240);
        faces = new int[6];
        numDice = 1;
        //Initialize position of X of all faces of the dice
        for (int i = 0, j = 0; i < 6; i++, j += WIDTH)
            faces[i] = j;
        Y = 80;
        X = (short)faces[0];
        
    }

    //Calculate a new random number and change position X
    public void Throw()
    {
        numDice = (short)rnd.Next(1, 7);
        X = (short)faces[numDice -1];
    }
}

