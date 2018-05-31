/*
 * Almudena López Sánchez 
 * 
 * 0.01, 25-May-2018: Added enums
 */

using Tao.Sdl;
using System.Collections.Generic;
class MenuScreen : Screen
{
    public enum MenuOption { Menu, Game, Load, Credits,
        ChangeLenguage, Exit };

    private MenuOption chosenOption;
    Image background;
    Dictionary<string,string> lenguages;

    public MenuScreen(Hardware hardware): base(hardware)
    {
        chosenOption = MenuOption.Menu;
        background = new Image("Images/background.jpg", 1440, 1000);
        lenguages = new Dictionary<string, string>();
        lenguages.Add("New Game","Juego Nuevo");
        lenguages.Add("Load Game", "Cargar Juego");
        lenguages.Add("Credits", "Créditos");
        lenguages.Add("Change lenguage", "Cambiar idioma");
        lenguages.Add("Quit", "Salir");
    }

    public override void Run()
    {
        chosenOption = MenuOption.Menu;
        hardware.ClearScreen();
        drawElements();
        do
        {
            checkKeys();
        }
        while (chosenOption == MenuOption.Menu);
    }

    private void drawElements()
    {
        hardware.DrawImage(background);
        writeText();
        hardware.ShowHiddenScreen();
    }

    //Text
    private void writeText()
    {
        Font font18 = new Font("Fonts/comic.ttf", 18);

        short y = 200;
        short count = 1;
        foreach (KeyValuePair<string, string> leng in lenguages)
        {
            string line;
            if (Monopoly.GetLenguage())
                line = leng.Key;
            else
                line = leng.Value;
            Hardware.WriteHiddenText(count+".- "+line, 440, y,
                0x00, 0x00, 0x00, font18);
            y += 50;
            count++;
        }
    }
    //Input
    private void checkKeys()
    {
        if (hardware.KeyPressed(Sdl.SDLK_1))
            chosenOption = MenuOption.Game;

        else if (hardware.KeyPressed(Sdl.SDLK_2))
            chosenOption = MenuOption.Load;

        else if (hardware.KeyPressed(Sdl.SDLK_3))
            chosenOption = MenuOption.Credits;

        else if (hardware.KeyPressed(Sdl.SDLK_4))
            chosenOption = MenuOption.ChangeLenguage;
        else if (hardware.KeyPressed(Sdl.SDLK_5))
            chosenOption = MenuOption.Exit;
    }

    public MenuOption GetChosenOption()
    {
        return chosenOption;
    }
}

