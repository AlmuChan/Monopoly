/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 18-May-2018: Create class
 */


class Tax : Square
{
    public int Price { get; set; } 

    public Tax(short Num, short X, short Y, string Name,int Price) 
        : base(Num,X,Y,Name)
    {
        this.Price = Price;
    }
}

