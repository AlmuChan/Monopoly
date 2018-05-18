/*
 * v0.04 Angel Rebollo Berna, 17/05/2018 - Created the WelcomeScreen class.
 */

using System.Threading;
using Tao.Sdl;

class WelcomeScreen : Screen
{

    private bool exit;
    private long frames;
    Image welcome;

    public WelcomeScreen(Hardware hardware) : base(hardware)
    {
        exit = false;
        welcome = new Image("Images/welcomeScreen.jpg", 650, 449);
        frames = 0;
    }

    //Main loop of the class
    public override void Run()
    {
        do
        {
            hardware.ClearScreen();
            drawElements();
            checkKeys();
        }
        while (!exit);
    }

    private void drawElements()
    {
        
        hardware.DrawImage(welcome);
        if (frames%2 == 0)
            writeText();
        hardware.ShowHiddenScreen();
        frames++;
        Thread.Sleep(100);
    }

    //Text
    private void writeText()
    {
        string text = "Press Space to Begin!";
        Font font18 = new Font("Fonts/riffic-bold.ttf", 18);

        Hardware.WriteHiddenText(text, 400, 500,
            0xFF, 0xFA, 0x00, font18);
    }

    //Input
    private void checkKeys()
    {
        if (hardware.KeyPressed(Sdl.SDLK_SPACE))
            exit = true;
    }

}

