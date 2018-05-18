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
        menu.Run();
    }
}
