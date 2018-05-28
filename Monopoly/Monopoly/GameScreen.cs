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
 * 0.06, 24-May-2018: Rent streets
 * 0.07, 28-May-2018: Added SaveGame
 */
using System;
using Tao.Sdl;
using System.Threading;
using System.IO;

class GameScreen : Screen
{
    Image board;
    Dice dice1, dice2;
    Token token;
    Square actualSquare;
    Player[] players;
    int numActualPlayer;
    bool exit;
    bool isRollDices;
    bool isTax;
    bool isProperty;
    Square[] squares;
    string[] menu = { "1. Roll Dices", "2. Show properties",
        "3. Finish turn" };

    public GameScreen(Hardware hardware,short numPlayers): base(hardware)
    {
        board = new Image("Images/board.jpg",600, 600);
        dice1 = new Dice();
        dice2 = new Dice();
        token = new Token();

        //Create num of players have been selected
        players = new Player[numPlayers];
        for( short i = 0; i < players.Length; i++)
        {
            Player p = new Player(i);
            players[i] = p;
        }
        numActualPlayer = 0;

        squares = Square.ReadSquares();
        actualSquare = squares[0];
        
        isRollDices = false;
        exit = false;
        
    }
    
    public override void Run()
    {
        drawElements();
        do
        {
            Thread.Sleep(10);
        }
        while (hardware.KeyPressed(Sdl.SDLK_1));
        do
        {
            ckeckImput();
        }
        while (!exit);
        saveGame();
    }

    //Display all elements in the screen
    private void drawElements()
    {
        hardware.ClearScreen();

        hardware.DrawImage(board);
        showPlayerMenu();
        drawDices();

        //Update square
        actualSquare = squares[players[numActualPlayer].Pos];
        writeSquare();

        //Draw totem in a determinate square
        token.tokenImg.MoveTo(actualSquare.X,
            actualSquare.Y);
        hardware.DrawImage(token.tokenImg);

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
        Hardware.WriteHiddenText(actualSquare.Num 
            + " - "+ actualSquare.Name, 650, 400,
            0xFF, 0xFA, 0x00, font18);

        string line2 = " ";
        isProperty = false;
        isTax = false;
        if (actualSquare is Tax) ;

        switch (actualSquare.GetType().ToString())
        {
            case "Tax":
                line2 = "Price: " + 
                    ((Tax)actualSquare).Price;
                isTax = true;
                break;
            case "Property":
                isProperty = true;
                line2 = "Price: " + 
                    ((Property)actualSquare).Price +
                    "   Colour: "+ 
                    ((Property)actualSquare).Colour;
                break;
            case "Card":
                line2 = "Type: " + 
                    ((Card)actualSquare).Type;
                break;
        }
        Hardware.WriteHiddenText(line2, 650, 450,
            0xFF, 0xFA, 0x00, font16);
        hardware.ShowHiddenScreen();


        if(isProperty && ((Property)actualSquare).Sold)
        {
            if(numActualPlayer != ((Property)actualSquare).NumPropietary)
                Hardware.WriteHiddenText("This street has Owner!", 650, 500,
                        0xFF, 0x00, 0x00, font16);
            else
                Hardware.WriteHiddenText("This street is yours!", 650, 500,
                        0xFF, 0x00, 0x00, font16);
            hardware.ShowHiddenScreen();
        }

    }

    //Check keys pressed
    private void ckeckImput()
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
            drawElements();
            Thread.Sleep(800); // To Change
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

        isRollDices = true;
        drawElements();
        checkSquare();
    }

    public void checkSquare()
    {
        //If square is a property and it isnt buy...
        if (isProperty)
        {
            short numOwner = ((Property)actualSquare).
                NumPropietary;

            if (numOwner == -1)
                menuToBuy();
            else
            {
                short rent = ((Property)actualSquare).
                    Rent;
                players[numActualPlayer].DecreaseMoney(rent);
                players[numOwner].IncreaseMoney(rent);
            }
        }
        else if(isTax)
        {
            players[numActualPlayer].DecreaseMoney(
                ((Tax)actualSquare).Price);
        }
        drawElements();
    }

    //Display the menu of each player
    private void showPlayerMenu()
    {
        Font font30 = new Font("Fonts/riffic-bold.ttf", 30);
        Font font18 = new Font("Fonts/riffic-bold.ttf", 18);
        Hardware.WriteHiddenText("Player " +
            (players[numActualPlayer].Num+1), 650, 100,
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
        Thread.Sleep(100); // Provisional
        //Wait to player to choose one option
        hardware.ShowHiddenScreen();
        bool exit = false;
        do
        {
            if (hardware.KeyPressed(Sdl.SDLK_1))
            {
                //To add one street to a player's list of properties
                //in a method "Buy" in a Property's class
                ((Property)actualSquare).
                    Buy(players[numActualPlayer],
                        (Property)actualSquare);
                exit = true;
            }
            if (hardware.KeyPressed(Sdl.SDLK_2))
            {
                exit = true;
                Thread.Sleep(50);
            }
        }
        while (!exit);
        
    }

    //Finish turn and change player
    private void changePlayer()
    {
        numActualPlayer++;
        if (numActualPlayer >= players.Length)
            numActualPlayer = 0;
        drawElements();
    }

    private void saveGame()
    {
        try
        {
            StreamWriter sw = new StreamWriter("Files/Saved.txt");
            string line;
            for(int i = 0; i < players.Length; i++)
            {
                line = players[i].Num.ToString() + "-" +
                    players[i].Pos.ToString() + "-" +
                    players[i].Money.ToString() + "-" +
                    players[i].InJail + "-";
                for(int j = 0; j < players[i].properties.Count; j++)
                { 
                    line += players[i].properties[j].Name;
                    line += ";";
                }
                sw.WriteLine(line);
            }
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
