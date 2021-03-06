﻿
/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 25-May-2018: Display background
 */

using Tao.Sdl;
using System.Threading;
class LoadScreen : Screen
{
    Image background;

    public LoadScreen(Hardware hardware) : base(hardware)
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
            Thread.Sleep(10);
        }
        while (!hardware.KeyPressed(Sdl.SDLK_ESCAPE));
    }
    private void writeText()
    {
        Font font20 = new Font("Fonts/riffic-bold.ttf", 20);
        Font font40 = new Font("Fonts/comic.ttf", 40);
        Hardware.WriteHiddenText("Load Screen!", 380, 200,
            0xFF, 0x00, 0x00, font40);
        Hardware.WriteHiddenText("Press Space To Select One!", 400, 250,
            0xFF, 0x00, 0x00, font20);
        Hardware.WriteHiddenText("Monopoly -1- 24-05-2018", 390, 400,
            0x00, 0x00, 0x00, font20);
        Hardware.WriteHiddenText("Monopoly -2- 25-05-2018", 390, 420,
            0x00, 0x00, 0x00, font20);
        Hardware.WriteHiddenText("Press Escape To Exit!", 400, 500,
            0xFF, 0x00, 0x00, font20);
    }
}

