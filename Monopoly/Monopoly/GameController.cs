/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 14-May-2018: Create class
 * 0.04, 17/05/2018: Modified to include the menu and welcome screens
 *                      The game method is moved to the menu class
 * 
 */

class GameController
{
    public void Run()
    {
        Hardware hardware = new Hardware(1000, 600, 24, false);
        WelcomeScreen welcome = new WelcomeScreen(hardware);
        MenuScreen menu = new MenuScreen(hardware);
        welcome.Run();
        bool exit = false;
        do
        {
            menu.Run();
            switch (menu.GetChosenOption())
            {
                case MenuScreen.MenuOption.Game:
                    NumPlayersScreen nps = new NumPlayersScreen(hardware);
                    nps.Run();
                    GameScreen game = new GameScreen(hardware, nps.NumPlayers,nps.NumsToken);
                    game.Run();
                    break;
                case MenuScreen.MenuOption.Load:
                    LoadScreen ls = new LoadScreen(hardware);
                    ls.Run();
                    break;
                case MenuScreen.MenuOption.Credits:
                    CreditsScreen cs = new CreditsScreen(hardware);
                    cs.Run();
                    break;
                case MenuScreen.MenuOption.Exit:
                    exit = true;
                    break;
            }
        }
        while (!exit);
    }
}
