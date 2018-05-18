/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 14-May-2018: Initial version, based on Saboteur and Gauntlet
 */

using System;
using Tao.Sdl;

class Image
{
    public short X { get; set; }
    public short Y { get; set; }
    public short width { get; set; }
    public short height { get; set; }
    private IntPtr internalPointer;

    public Image(string fileName, short width, short height)
    {
        internalPointer = SdlImage.IMG_Load(fileName);
        if (internalPointer == IntPtr.Zero)
            Console.WriteLine("Image not found");

        this.width = width;
        this.height = height;
    }

    public void MoveTo(short x, short y)
    {
        X = x;
        Y = y;
    }

    public IntPtr GetPointer()
    {
        return internalPointer;
    }
}

