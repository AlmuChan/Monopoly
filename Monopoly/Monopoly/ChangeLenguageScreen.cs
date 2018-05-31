using Tao.Sdl;
using System.Collections.Generic;
class ChangeLenguageScreen : Screen
{
    Image background;
    Dictionary<string, string> lenguages;

    public ChangeLenguageScreen(Hardware hardware) : base(hardware)
    {
        background = new Image("Images/background.jpg", 1440, 1000);
        lenguages = new Dictionary<string, string>();
        lenguages.Add("English", "Inglés");
        lenguages.Add("Spanish", "Castellano");
    }

    public override void Run()
    {
        do
        {
            hardware.ClearScreen();
            hardware.DrawImage(background);
            writeText();
            hardware.ShowHiddenScreen();
        
            if (hardware.KeyPressed(Sdl.SDLK_1))
                Monopoly.SetLenguage(true);
            else if(hardware.KeyPressed(Sdl.SDLK_2))
                Monopoly.SetLenguage(false);

        }
        while (!hardware.KeyPressed(Sdl.SDLK_SPACE));
    }
    private void writeText()
    {
        Font font20 = new Font("Fonts/riffic-bold.ttf", 20);
        Font font40 = new Font("Fonts/comic.ttf", 40);

        string line1;
        if (Monopoly.GetLenguage())
            line1 = "Change lenguage:";
        else
            line1 = "Cambiar idioma:";
        Hardware.WriteHiddenText(line1, 350, 150,
            0xFF, 0x00, 0x00, font40);

        short y = 300;
        short count = 1;
        foreach (KeyValuePair<string, string> leng in lenguages)
        {
            string line;
            if (Monopoly.GetLenguage())
                line = leng.Key;
            else
                line = leng.Value;
            Hardware.WriteHiddenText(count + ".- " + line, 400, y,
                0x00, 0x00, 0x00, font20);
            y += 50;
            count++;
        }

        string line2;
        if (Monopoly.GetLenguage())
            line2 = "Press Space To Select!";
        else
            line2 = "¡Presiona Espacio Para Salir!";
        Hardware.WriteHiddenText(line2, 350, 500,
            0xFF, 0x00, 0x00, font20);

    }
}

