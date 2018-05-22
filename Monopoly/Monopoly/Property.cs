/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 14-May-2018: Create class
 * 0.02, 21-May-2018: Method Buy and added atribute Sold
 * 0.03, 22-May-2018:  Done functional method Buy
 */

class Property : Square
{
    public int Price { get; set; } 
    public string Colour { get; set; }
    public short NumPropietary { get; set; }
    public bool Sold { get; set; }

    public Property(short Num, short X, short Y, string Name, 
        int Price, string Colour) : base(Num,X,Y,Name)
    {
        this.Price = Price;
        this.Colour = Colour;
        //starts without owner
        NumPropietary = 0;
        Sold = false;
    }

    //Add to a player one property
    public void Buy(Player player,Property prop)
    {
        prop.NumPropietary = player.Num;
        prop.Sold = true;
        player.Money -= Price;
        player.properties.Add(prop);
    }
}

