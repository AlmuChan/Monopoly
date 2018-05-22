/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 14-May-2018: Created method DrawElements, constructor and loop of Run
 * 0.02, 15-May-2018: Initialized list of squared, 
 *      add player and add method RollDices
 * 0.03, 16-May-2018: Add method readSquares (read file) and mehod
 *      writeSquare
 * 0.04, 18-May-2018: Classify types of squares add method checkSquares
 * 0.05, 22-May-2018: some changes and added methods ChangePlayer
 *      and menuToBuy
 */
using System;
using Tao.Sdl;
using System.Collections.Generic;

class GameScreen : Screen
{
    Image board;
    Dice dice1, dice2;
    Token token;
    List<Player> players;
    int numActualPlayer;
    bool exit;
    bool isRollDices;
    Square[] squares;
    string[] menu = { "1. Roll Dices", "2. Show propierties",
        "3. Finish turn" };

    public GameScreen(Hardware hardware): base(hardware)
    {
        board = new Image("Images/board.jpg",600, 600);
        dice1 = new Dice();
        dice2 = new Dice();
        token = new Token();

        //Initialize list of players and add two player
        //To test (provisional)
        players = new List<Player>();
        Player p1 = new Player(1);
        Player p2 = new Player(2);
        players.Add(p1);
        players.Add(p2);
        numActualPlayer = players.Count;

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
        showPlayerMenu();
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
        Hardware.WriteHiddenText(squares[players[numActualPlayer].Pos].Num 
            + " - "+ squares[players[numActualPlayer].Pos].Name, 650, 400,
            0xFF, 0xFA, 0x00, font18);

        string line2 = " ";
        switch(squares[players[numActualPlayer].Pos].GetType().ToString())
        {
            case "Tax":
                line2 = "Price: " + 
                    ((Tax)squares[players[numActualPlayer].Pos]).Price;
                    players[numActualPlayer].DecreaseMoney(
                    ((Tax)squares[players[numActualPlayer].Pos]).Price);
                break;
            case "Property":
                line2 = "Price: " + 
                    ((Property)squares[players[numActualPlayer].Pos]).Price +
                    "   Colour: "+ 
                    ((Property)squares[players[numActualPlayer].Pos]).Colour;
                menuToBuy();
                break;
            case "Card":
                line2 = "Type: " + 
                    ((Card)squares[players[numActualPlayer].Pos]).Type;
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
            players[numActualPlayer].ShowProperties();
            hardware.ShowHiddenScreen();
            do
            {
                //while user press 2, user can see his properties
            }
            while (hardware.KeyPressed(Sdl.SDLK_2));
            drawElements();
        }
            
        if (hardware.KeyPressed(Sdl.SDLK_3))
        {
            isRollDices = false;
            changePlayer();
        } 
    }

    //Calculate new players's position and move token
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

        players[numActualPlayer].Pos += (short)(dice1.numDice + dice2.numDice);
        //When player come back to start square
        if (players[numActualPlayer].Pos >= 40)
        {
            players[numActualPlayer].Pos -= 40;
            players[numActualPlayer].IncreaseMoney(200);
        }

        //Move token to new coordinates
        token.tokenImg.MoveTo(squares[players[numActualPlayer].Pos].X,
            squares[players[numActualPlayer].Pos].Y);

        isRollDices = true;
        drawElements();
    }

    //Display the menu of each player
    private void showPlayerMenu()
    {
        Font font30 = new Font("Fonts/riffic-bold.ttf", 30);
        Font font18 = new Font("Fonts/riffic-bold.ttf", 18);
        Hardware.WriteHiddenText("Player " + 
            players[numActualPlayer].Num, 650, 100,
            0xFF, 0x00, 0x00, font30);

        Hardware.WriteHiddenText("Money: " + 
            players[numActualPlayer].Money, 700, 140,
            0xFF, 0x00, 0x00, font18);

        for (short i = 0, y = 200; i < menu.Length; i++)
        {
            Hardware.WriteHiddenText(menu[i], 650, y,
                0xFF, 0xFA, 0x00, font18);
            y += 50;
        }
    }

    //Menu for buy properties
    private void menuToBuy()
    {
        Font font18 = new Font("Fonts/riffic-bold.ttf", 18);

        Hardware.WriteHiddenText("Do you want to buy?", 650, 500,
             0xFF, 0x00, 0x00, font18);
        Hardware.WriteHiddenText("1.- YES", 670, 540,
             0xFF, 0x00, 0x00, font18);
        Hardware.WriteHiddenText("2.- NO", 670, 570,
             0xFF, 0x00, 0x00, font18);

        //Wait to player to choose one option
        bool exit = false;
        do
        {
            if (hardware.KeyPressed(Sdl.SDLK_1))
            {
                //To add one street to a player's list of properties
                //in a method "Buy" in a Property's class
                ((Property)squares[players[numActualPlayer].Pos]).
                    Buy(players[numActualPlayer],
                    (Property)squares[players[numActualPlayer].Pos]);
                exit = true;
            }
            if (hardware.KeyPressed(Sdl.SDLK_2))
                exit = true;
        }
        while (!exit);

    }

    //Finish turn and change player
    private void changePlayer()
    {
        numActualPlayer++;
        if (numActualPlayer >= players.Count)
            numActualPlayer--;
    }
}
