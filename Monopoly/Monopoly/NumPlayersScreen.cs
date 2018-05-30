
/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 24-May-2018: Create class
 * 0.02. 29-May-2018: Added move and draw arrow
 */

using Tao.Sdl;
class NumPlayersScreen : Screen
{
    public short NumPlayers { get; set; }
    public short[] NumsToken { get; set; }
    bool exit;
    Image background;
    Image arrow;
    short xArrow;
    Image car, boot, hat, iron, ship;
    Font font20;
    Font font25;


    public NumPlayersScreen(Hardware hardware) : base(hardware)
    {
        //Fonts
        font20 = new Font("Fonts/riffic-bold.ttf", 20);
        font25 = new Font("Fonts/comic.ttf", 25);

        NumPlayers = 2;
        exit = false;
        background = new Image("Images/background.jpg", 1440, 1000);
        arrow = new Image("Images/arrowRed.png",19,100);
        xArrow = 360;
        arrow.MoveTo(xArrow, 400);
        
        //Future array
        iron = new Image("Images/iron.png", 50, 50);
        iron.MoveTo(340, 350);
        ship = new Image("Images/ship.png", 50, 52);
        ship.MoveTo(420, 350);
        boot = new Image("Images/boot.png", 50, 52);
        boot.MoveTo(500, 350);
        car = new Image("Images/car.png", 50, 38);
        car.MoveTo(580, 350);
        hat = new Image("Images/hat.png", 67, 50);
        hat.MoveTo(660, 350);
    }

    public override void Run()
    {
        //To choose num players
        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(background);
            writeText();
            
            hardware.ShowHiddenScreen();

            if (hardware.KeyPressed(Sdl.SDLK_2))
                NumPlayers = 2;
            if (hardware.KeyPressed(Sdl.SDLK_3))
                NumPlayers = 3;
            if (hardware.KeyPressed(Sdl.SDLK_4))
                NumPlayers = 4;
            if (hardware.KeyPressed(Sdl.SDLK_SPACE))
                exit = true;
            //if (hardware.KeyPressed(Sdl.SDLK_ESCAPE)) TO DO  
        }
        while (!exit);
        //To choose token
        exit = false;
        NumsToken = new short[NumPlayers];
        byte numPlayer = 1;
        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(background);
            writeText();
            Hardware.WriteHiddenText("Player "+numPlayer, 50, 400,
                0xFF, 0x00, 0x00, font20);
            drawTokens();
            hardware.ShowHiddenScreen();

            int key = hardware.KeyPressed();
            if (key == Hardware.KEY_SPACE)
            {
                if (numPlayer > NumPlayers)
                    exit = true;
                else
                {
                    //To know num of token with position of arrow
                    short token = 0; 
                    switch(xArrow)
                    {
                        case 360:
                            token = 1;
                            break;
                        case 440:
                            token = 2;
                            break;
                        case 520:
                            token = 3;
                            break;
                        case 600:
                            token = 4;
                            break;
                        case 680:
                            token = 5;
                            break;
                    }
                    NumsToken[numPlayer - 1] = token;
                    numPlayer++;
                }
            }
            
            //To move arrow 
            else if (key == Hardware.KEY_RIGHT)
            {
                if (xArrow < 640)
                    xArrow += 80;
            }
            else if (key == Hardware.KEY_LEFT)
            {
                if (xArrow > 360)
                    xArrow -= 80;
            }
            //if (key == Hardware.KEY_ESC) TO DO 
        }
        while (!exit);
    }

    private void writeText()
    {
        Hardware.WriteHiddenText("Number of Players?", 400, 150,
            0xFF, 0x00, 0x00, font20);

        Hardware.WriteHiddenText("- "+NumPlayers+ " -", 450, 220,
            0x00, 0x00, 0x00, font25);

        Hardware.WriteHiddenText("Press Space To Select!", 400, 500,
            0xFF, 0x00, 0x00, font20);
    }
   private void drawTokens()
    {
        hardware.DrawImage(boot);
        hardware.DrawImage(hat);
        hardware.DrawImage(car);
        hardware.DrawImage(iron);
        hardware.DrawImage(ship);
        hardware.DrawImage(arrow);
        arrow.MoveTo(xArrow, 400);
    }
}
