/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 14-May-2018: Created method DrawElements, constructor and loop of Run
 * 0.02, 15-May-2018: Initialized list of squared, 
 *      add player and add method RollDices
 * 0.03, 16-May-2018: Add method readSquares (read file) and mehod
 *      writeSquare
 * 0.04, 18-May-2018: Classify types of squares add method checkSquares
 */
using System;
using Tao.Sdl;
using System.Collections.Generic;

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
        squares = Square.ReadSquares();
        
        isRollDices = false;
        exit = false;
        
    }
    
    public override void Run()
    {
        drawElements();
        do
        {
            ckeckKeys();
        }
        while (!exit);
    }

    //Display all elements in the screen
    private void drawElements()
    {
        hardware.ClearScreen();

        hardware.DrawImage(board);
        hardware.DrawImage(token.tokenImg);
        player.ShowMenu();
        drawDices();
        writeSquare();
        
        hardware.ShowHiddenScreen();
    }

    //Draw two dices in a determinate position
    private void drawDices()
    {
        hardware.DrawSprite(dice1.dice, 650, 10,
            dice1.X, dice1.Y, Dice.WIDTH, Dice.HEIGHT);
        hardware.DrawSprite(dice2.dice, 750, 10,
            dice2.X, dice2.Y, Dice.WIDTH, Dice.HEIGHT);
    }

    //Display information about square where player 
    //and do action of the square
    private void writeSquare()
    {
        Font font16 = new Font("Fonts/riffic-bold.ttf", 16);
        Font font18 = new Font("Fonts/riffic-bold.ttf", 18);
        Hardware.WriteHiddenText(squares[player.Pos].Num + " - "+
            squares[player.Pos].Name, 650, 400,
            0xFF, 0xFA, 0x00, font18);

        string line2 = " ";
        switch(squares[player.Pos].GetType().ToString())
        {
            case "Tax":
                line2 = "Price: " + ((Tax)squares[player.Pos]).Price;
                player.DecreaseMoney(((Tax)squares[player.Pos]).Price);
                break;
            case "Property":
                line2 = "Price: " + ((Property)squares[player.Pos]).Price +
                    "   Colour: "+ ((Property)squares[player.Pos]).Colour;
                break;
            case "Card":
                line2 = "Type: " + ((Card)squares[player.Pos]).Type;
                break;
        }
        Hardware.WriteHiddenText(line2, 650, 450,
            0xFF, 0xFA, 0x00, font16);
        hardware.ShowHiddenScreen();
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
        if (hardware.KeyPressed(Sdl.SDLK_2))
        {
            hardware.ClearRightPart();
            player.ShowProperties();
            hardware.ShowHiddenScreen();
            do
            {
                //while user press 2, user can see his properties
            }
            while (hardware.KeyPressed(Sdl.SDLK_2));
            drawElements();
            /*
            //To change
            foreach(Square s in squares)
            {
                if(s.GetType().ToString() == "Property")
                    if (((Property)s).NumPropietary == player.Num)
                        player.ShowProperties();
            }
            */
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
        //When player come back to start square
        if (player.Pos >= 40)
        {
            player.Pos -= 40;
            player.IncreaseMoney(200);
        }

        //Move token to new coordinates
        token.tokenImg.MoveTo(squares[player.Pos].X,
            squares[player.Pos].Y);

        isRollDices = true;
        drawElements();
    }
}
