/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 14-May-2018: Create class
 */

class Property : Square
{
    public string Price { get; set; } // To change
    public string Colour { get; set; }

    public Property(short Num, short X, short Y, string Name, 
        string Price, string Colour) : base(Num,X,Y,Name)
    {
        this.Price = Price;
        this.Colour = Colour;
    }
}

