/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 14-May-2018: Create class
 * 0.02, 21-May-2018: Mehotd Buy and added atribute Sold
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

    public void Buy(Player p)
    {
        NumPropietary = p.Num;
        Sold = true;
        p.Money -= Price;
        //p.listproperties.add ()
    }
}

