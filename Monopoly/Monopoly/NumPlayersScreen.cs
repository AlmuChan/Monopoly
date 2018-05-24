
/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 24-May-2018: Create class
 */

using Tao.Sdl;
class NumPlayersScreen : Screen
{
    public int NumPlayers { get; set; }
    bool exit;

    public NumPlayersScreen(Hardware hardware) : base(hardware)
    {
        NumPlayers = 2;
        exit = false;
    }

    public override void Run()
    {
        do
        {
            hardware.ClearScreen();
            writeText();
            if (hardware.KeyPressed(Sdl.SDLK_2))
                NumPlayers = 2;
            if (hardware.KeyPressed(Sdl.SDLK_3))
                NumPlayers = 3;
            if (hardware.KeyPressed(Sdl.SDLK_4))
                NumPlayers = 4;
            if (hardware.KeyPressed(Sdl.SDLK_SPACE))
                exit = true;
        }
        while (!exit);
        
    }

    private void writeText()
    {
        string text = "Number of Players?";
        Font font20 = new Font("Fonts/riffic-bold.ttf", 20);
        Hardware.WriteHiddenText(text, 400, 150,
            0xFF, 0x00, 0x00, font20);

        Font font25 = new Font("Fonts/comic.ttf", 25);
        Hardware.WriteHiddenText("- "+NumPlayers+ " -", 420, 220,
            0xFF, 0x00, 0x00, font25);

        Hardware.WriteHiddenText("Press Space To Select!", 400, 500,
            0xFF, 0x00, 0x00, font20);
        hardware.ShowHiddenScreen();
    }
}
