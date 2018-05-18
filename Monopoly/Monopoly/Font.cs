/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 14-May-2018: Initial version, based on Saboteur and Gauntlet
 */

using System;
using Tao.Sdl;

class Font
{
    IntPtr fontType;

    public Font(string fileName, int fontSize)
    {
        fontType = SdlTtf.TTF_OpenFont(fileName, fontSize);
        if (fontType == IntPtr.Zero)
        {
            Console.WriteLine("Font type not found");
        }
    }

    public IntPtr GetFontType()
    {
        return fontType;
    }
}
