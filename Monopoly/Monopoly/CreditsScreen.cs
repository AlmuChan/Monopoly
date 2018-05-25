/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 21-May-2018: Create class
 * 0.02, 25-May-2018: Display background
 */
using Tao.Sdl;
class CreditsScreen : Screen
{
    Image background;

    public CreditsScreen(Hardware hardware) : base(hardware)
    {
        background = new Image("Images/background.jpg", 1440, 1000);
    }

    public override void Run()
    {
        hardware.ClearScreen();
        hardware.DrawImage(background);
        writeText();
        hardware.ShowHiddenScreen();
        do
        {
            //Wait to press Space
        }
        while (!hardware.KeyPressed(Sdl.SDLK_SPACE));
    }
    private void writeText()
    {
        Font font20 = new Font("Fonts/riffic-bold.ttf", 20);
        Font font40 = new Font("Fonts/comic.ttf", 40);
        Hardware.WriteHiddenText("Credits Screen", 380, 200,
            0xFF, 0x00, 0x00, font40);
        Hardware.WriteHiddenText("Almudena López Sánchez", 390, 400,
            0x00, 0x00, 0x00, font20);
        Hardware.WriteHiddenText("Press Space To Exit!", 400, 500,
            0xFF, 0x00, 0x00, font20);
    }
}

