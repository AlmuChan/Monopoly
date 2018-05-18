/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 18-May-2018: Create class
 */


class Tax : Square
{
    public string Price { get; set; } //To change

    public Tax(short Num, short X, short Y, string Name,string Price) 
        : base(Num,X,Y,Name)
    {
        this.Price = Price;
    }
}

