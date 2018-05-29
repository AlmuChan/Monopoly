/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 14-May-2018: Create class
 */


class Token
{
    public Image tokenImg;
    public short X { get; set; }
    public short Y { get; set; }

    public Token()
    {
        tokenImg = new Image("Images/iron.png", 103, 78);
        X = Y = 530;
        tokenImg.MoveTo(X, Y);
    }

}

