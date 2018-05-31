/*
 * Almudena López Sánchez 
 * 
 * 0.01, 14-May-2018: Create class
 */

 
class Monopoly
{
    static bool lenguage;

    static void Main(string[] args)
    {
        lenguage = true;
        GameController gameC = new GameController();
        gameC.Run();
    }
    
    public static void SetLenguage(bool leng)
    {
        lenguage = leng;
    }

    public static bool GetLenguage()
    {
        return lenguage;
    }
}

