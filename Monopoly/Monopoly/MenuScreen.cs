

using Tao.Sdl;

class MenuScreen : Screen
{
    public enum MenuOption { Menu, Game, Load, Credits, Exit };
    private MenuOption chosenOption;
    Image background;

    private string[] optionsText = {" 1.-New Game"," 2.-Load Game",
        " 3.-Credits", " 4.-Quit"};

    public MenuScreen(Hardware hardware): base(hardware)
    {
        chosenOption = MenuOption.Menu;
        background = new Image("Images/background.jpg", 1440, 1000);
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

        for(short i = 0, y = 200; i < optionsText.Length; i++,y += 50)
        {
            Hardware.WriteHiddenText(optionsText[i], 440, y,
                0x00, 0x00, 0x00, font18);
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
            chosenOption = MenuOption.Exit;
    }

    public MenuOption GetChosenOption()
    {
        return chosenOption;
    }
}

