/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 14-May-2018: Create class
 * 0.02, 29-May-2018: Added switch to create an image
 */


class Token
{
    public Image tokenImg;
    public short X { get; set; }
    public short Y { get; set; }

    public Token(short num)
    {
        switch(num)
        {
            case 1:
                tokenImg = new Image("Images/iron.png", 50, 50);
                break;
            case 2:
                tokenImg = new Image("Images/ship.png", 50, 52);
                break;
            case 3:
                tokenImg = new Image("Images/boot.png", 50, 52);
                break;
            case 4:
                tokenImg = new Image("Images/car.png", 50, 38);
                break;
            case 5:
                tokenImg = new Image("Images/hat.png", 67, 50);
                break;
        }
        X = Y = 530;
        tokenImg.MoveTo(X, Y);
    }

}

