/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 15-May-2018: Create class and constructor
 */

class Square
{
    public short Num { get; set; }
    public short X { get; set; }
    public short Y { get; set; }
    public string Name { get; set; }

    public Square(short Num, short X, short Y, string Name)
    {
        this.Num = Num;
        this.Y = Y;
        this.X = X;
        this.Name = Name;
    }
}
