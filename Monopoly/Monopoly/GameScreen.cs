/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 14-May-2018: Created method DrawElements, constructor and loop of Run
 * 0.02, 15-May-2018: Initialized list of squared, 
 *      add player and add method RollDices
 * 0.03, 16-May-2018: Add method readSquares (read file) and mehod
 *      writeSquare
 */

using System;
using Tao.Sdl;
using System.Collections.Generic;
using System.IO;

class GameScreen : Screen
{
    Image board;
    Dice dice1, dice2;
    Token token;
    Player player;
    bool exit;
    bool isRollDices;
    List<Square> squares;

    public GameScreen(Hardware hardware): base(hardware)
    {
        board = new Image("Images/board.jpg",600, 600);
        dice1 = new Dice();
        dice2 = new Dice();
        token = new Token();
        player = new Player(1);
        squares = new List<Square>();
        
        isRollDices = false;
        exit = false;
        readSquares();
    }
    
    public override void Run()
    {
        do
        {
            hardware.ClearScreen();
            drawElements();
            ckeckKeys();
        }
        while (!exit);
    }

    private void drawElements()
    {
        hardware.DrawImage(board);
        hardware.DrawImage(token.tokenImg);
        player.ShowMenu();
        writeSquare();
        drawDices();
        
        hardware.ShowHiddenScreen();
    }

    private void drawDices()
    {
        hardware.DrawSprite(dice1.dice, 650, 10,
            dice1.X, dice1.Y, Dice.WIDTH, Dice.HEIGHT);
        hardware.DrawSprite(dice2.dice, 750, 10,
            dice2.X, dice2.Y, Dice.WIDTH, Dice.HEIGHT);
    }

    //Display information about square where player is
    private void writeSquare()
    {
        Font font18 = new Font("Fonts/riffic-bold.ttf", 18);
        Hardware.WriteHiddenText(squares[player.Pos].Num + " - "+
            squares[player.Pos].Name, 650, 400,
            0xFF, 0xFA, 0x00, font18);
    }

    //Check keys pressed
    private void ckeckKeys()
    {
        if (hardware.KeyPressed(Sdl.SDLK_ESCAPE))
            exit = true;

        if (hardware.KeyPressed(Sdl.SDLK_1))
            if(!isRollDices)
            {
                rollDices();
            }    

        if (hardware.KeyPressed(Sdl.SDLK_3))
            isRollDices = false;
    }

    //Calculate new player's position and move token
    private void rollDices()
    {
        //Throw dices while user press "1"
        do
        {
            dice1.Throw();
            dice2.Throw();
            drawDices();
            hardware.ShowHiddenScreen();
        }
        while (hardware.KeyPressed(Sdl.SDLK_1));

        player.Pos += (short)(dice1.numDice + dice2.numDice);
        if (player.Pos >= 40)
            player.Pos -= 40;

        //Move token to new coordinates
        token.tokenImg.MoveTo(squares[player.Pos].X,
            squares[player.Pos].Y);

        isRollDices = true;
    }

    //Read files of squares and add to list of squares
    private void readSquares()
    {
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
                        if (words.Length == 4)
                        {
                            Property s = new Property((short)(i), x, y, name);
                            squares.Add(s);
                        }
                        else
                        {
                            Property s = new Property((short)(i), x, y, name);
                            squares.Add(s);
                        }
                        
                       
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
    }
}
