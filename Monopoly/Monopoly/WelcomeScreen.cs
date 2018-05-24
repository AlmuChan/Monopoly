/*
 * v0.04 Angel Rebollo Berna, 17/05/2018 - Created the WelcomeScreen class.
 */

using System.Threading;
using Tao.Sdl;

class WelcomeScreen : Screen
{

    private bool exit;
    Image title;
    Image houses;
    Image background;

    public WelcomeScreen(Hardware hardware) : base(hardware)
    {
        exit = false;
        background = new Image("Images/background.jpg", 1440, 1000);
        title = new Image("Images/tittle.png", 460, 100);
        title.MoveTo(300, 220);
        houses = new Image("Images/houses.png",1920,200);
        houses.MoveTo(-100, 400);
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
        hardware.DrawImage(background);
        hardware.DrawImage(houses);
        hardware.DrawImage(title);
        
        writeText();
        hardware.ShowHiddenScreen();
        Thread.Sleep(100);
    }

    //Text
    private void writeText()
    {
        string text = "Press Space to Begin!";
        Font font18 = new Font("Fonts/riffic-bold.ttf", 18);
        Hardware.WriteHiddenText(text, 400, 350,
            0x00, 0x00, 0x00, font18);
    }

    //Input
    private void checkKeys()
    {
        if (hardware.KeyPressed(Sdl.SDLK_SPACE))
            exit = true;
    }

}

