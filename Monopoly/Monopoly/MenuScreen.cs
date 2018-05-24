using Tao.Sdl;

class MenuScreen : Screen
{
    private bool exit;

    public MenuScreen(Hardware hardware): base(hardware)
    {
        exit = false;
    }

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
        writeText();
        hardware.ShowHiddenScreen();
    }

    //Text
    private void writeText()
    {
        string option1 = " 1.-New Game";
        string option2 = " 2.-Load Game";
        string option3 = " 3.-Credits";
        string option4 = " 4.-Quit";

        Font font18 = new Font("Fonts/comic.ttf", 18);

        Hardware.WriteHiddenText(option1, 440, 200,
            0xFF, 0xFA, 0x00, font18);
        Hardware.WriteHiddenText(option2, 440, 250,
            0xFF, 0xFA, 0x00, font18);
        Hardware.WriteHiddenText(option3, 440, 300,
            0xFF, 0xFA, 0x00, font18);
        Hardware.WriteHiddenText(option4, 440, 350,
            0xFF, 0xFA, 0x00, font18);
    }

    //Input
    private void checkKeys()
    {
        if (hardware.KeyPressed(Sdl.SDLK_1))
        {
            NumPlayersScreen nps = new NumPlayersScreen(hardware);
            nps.Run();
            GameScreen game = new GameScreen(hardware, nps.NumPlayers);
            game.Run();
        }
            
        else if (hardware.KeyPressed(Sdl.SDLK_2))
        {
            //To do
        }
        
        else if (hardware.KeyPressed(Sdl.SDLK_3))
        {
            //To do
        }

        else if (hardware.KeyPressed(Sdl.SDLK_4))
            exit = true;
    }
}

