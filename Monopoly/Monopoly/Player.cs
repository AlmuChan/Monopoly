/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 15-May-2018: Create class, constructor and method ShowMenu
 * 0.02, 18-May-2018: Add money and display it
 */

class Player
{
    string[] menu = { "1. Roll Dices", "2. Show propierties",
        "3. Finish turn" };
    public short Num { get; set; }
    public short Pos { get; set; }
    public int Money { get; set; }
    public bool InJail { get; set; }

    public Player(short Num)
    {
        this.Num = Num;
        Pos = 0;
        Money = 2000;
        InJail = false;
    }

    public void DecreaseMoney(int amount)
    {
        Money -= amount;
    }

    public void IncreaseMoney(int amount)
    {
        Money += amount;
    }

    public void ShowMenu()
    {
        Font font30 = new Font("Fonts/riffic-bold.ttf", 30);
        Font font18 = new Font("Fonts/riffic-bold.ttf", 18);
        Hardware.WriteHiddenText("Player "+ Num, 650, 100,
                0xFF, 0x00, 0x00, font30);

        Hardware.WriteHiddenText("Money: " + Money, 700, 140,
                0xFF, 0x00, 0x00, font18);
        
        for (short i = 0,y = 200; i < menu.Length; i++)
        {
            Hardware.WriteHiddenText(menu[i], 650, y,
                0xFF, 0xFA, 0x00, font18);
            y += 50;
        }   
    }

    public void ShowProperties()
    {
        //TO DO
    }

}
