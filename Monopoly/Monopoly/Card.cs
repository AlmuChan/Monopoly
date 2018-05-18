/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 18-May-2018: Create class
 */


class Card : Square
{
    public string Type { get; set; }

    public Card(short Num, short X, short Y, string Name, string Type) 
        : base(Num,X,Y,Name)
    {
        this.Type = Type;
    }
}
