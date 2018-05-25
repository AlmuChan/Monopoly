
/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 24-May-2018: Create class
 */

using Tao.Sdl;
class NumPlayersScreen : Screen
{
    public short NumPlayers { get; set; }
    bool exit;
    Image background;
    Image car, boot, hat, iron, ship;

    public NumPlayersScreen(Hardware hardware) : base(hardware)
    {
        NumPlayers = 2;
        exit = false;
        background = new Image("Images/background.jpg", 1440, 1000);
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
        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(background);
            writeText();
            drawTokens();
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
    }

    private void writeText()
    {
        Font font20 = new Font("Fonts/riffic-bold.ttf", 20);
        Font font25 = new Font("Fonts/comic.ttf", 25);

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
    }
}
