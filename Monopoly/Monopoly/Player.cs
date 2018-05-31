/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 15-May-2018: Created class, constructor and method ShowMenu
 * 0.02, 18-May-2018: Added money and display it
 * 0.03, 18-May-2018: Added tokenChoose
 */

using System.Collections.Generic;

class Player
{
    public short Num { get; set; }
    public short Pos { get; set; }
    public int Money { get; set; }
    public bool InJail { get; set; }
    public bool AI { get; set; }
    public short countJail { get; set; }
    public List<Property> properties { get; set; }
    public Token tokenChoosen { get; set; }

    public Player(short Num,Token tokenChoosen,bool AI)
    {
        this.tokenChoosen = tokenChoosen;
        this.Num = Num;
        Pos = 0;
        Money = 2000;
        InJail = false;
        countJail = 0;
        this.AI = AI;
        properties = new List<Property>();
    }

    public void DecreaseMoney(int amount)
    {
        Money -= amount;
    }

    public void IncreaseMoney(int amount)
    {
        Money += amount;
    }

    public void ShowProperties()
    {
        Font font18 = new Font("Fonts/riffic-bold.ttf", 18);
        Font font15 = new Font("Fonts/riffic-bold.ttf", 15);

        Hardware.WriteHiddenText("My Properties:", 650, 200,
                0xFF, 0xFA, 0x00, font18);

        string text;
        short y = 250;
        foreach (Property p in properties)
        {
            text = p.Num + "- " + p.Name + ", "+ p.Colour;
            Hardware.WriteHiddenText(text, 650, y,
                0xFF, 0xFA, 0x00, font15);
            y += 50;
        }
    }

}
