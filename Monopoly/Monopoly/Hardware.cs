﻿/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 14-May-2018: Initial version, based on Saboteur and Gauntlet
 * 0.02, 22-May-2018: Added a new method 
 * 0.03, 30-May-2018: Added method NextRandom
 */

using System;
using Tao.Sdl;

class Hardware
{
    static short width, height;
    static IntPtr hiddenScreen;

    public const int KEY_SPACE = Sdl.SDLK_SPACE;
    public const int KEY_ESC = Sdl.SDLK_ESCAPE;
    public const int KEY_RIGHT = Sdl.SDLK_RIGHT;
    public const int KEY_LEFT = Sdl.SDLK_LEFT;
    public const int KEY_1 = Sdl.SDLK_1;
    public const int KEY_2 = Sdl.SDLK_2;
    public const int KEY_3 = Sdl.SDLK_3;
    public const int KEY_4 = Sdl.SDLK_4;
    public const int KEY_5 = Sdl.SDLK_5;
    public const int KEY_6 = Sdl.SDLK_6;
    public const int KEY_7 = Sdl.SDLK_7;
    public const int KEY_8 = Sdl.SDLK_8;
    public const int KEY_9 = Sdl.SDLK_9;
    public const int KEY_0 = Sdl.SDLK_0;
    static Random rnd = new Random();

    public static int NextRandom(int n1, int n2)
    {
        return rnd.Next(n1, n2); ;
    }

    public Hardware(short Width, short Height, short depth, bool fullScreen)
    {
        width = Width;
        height = Height;

        int flags = Sdl.SDL_HWSURFACE | Sdl.SDL_DOUBLEBUF | Sdl.SDL_ANYFORMAT;
        if (fullScreen)
            flags = flags | Sdl.SDL_FULLSCREEN;

        Sdl.SDL_Init(Sdl.SDL_INIT_EVERYTHING);
        hiddenScreen = Sdl.SDL_SetVideoMode(width, height, depth, flags);
        Sdl.SDL_Rect rect = new Sdl.SDL_Rect(0, 0, width, height);
        Sdl.SDL_SetClipRect(hiddenScreen, ref rect);

        SdlTtf.TTF_Init();
    }

    // Draws an image in its current coordinates
    public void DrawImage(Image img)
    {
        Sdl.SDL_Rect source = new Sdl.SDL_Rect(0, 0, img.width,
            img.height);
        Sdl.SDL_Rect target = new Sdl.SDL_Rect(img.X, img.Y,
            img.width, img.height);
        Sdl.SDL_BlitSurface(
            img.GetPointer(), ref source, hiddenScreen, ref target);
    }

    public void DrawSprite(
        Image image, short xScreen, short yScreen,
        short x, short y, short width, short height)
    {
        Sdl.SDL_Rect src = new Sdl.SDL_Rect(x, y, width, height);
        Sdl.SDL_Rect dest = new Sdl.SDL_Rect(xScreen, yScreen, width, height);
        Sdl.SDL_BlitSurface(image.GetPointer(), ref src, hiddenScreen, ref dest);
    }

    public void ShowHiddenScreen()
    {
        Sdl.SDL_Flip(hiddenScreen);
    }

    // Detects if the user presses a determinate key
    public bool KeyPressed(int c)
    {
        bool pressed = false;
        Sdl.SDL_PumpEvents();
        Sdl.SDL_Event myEvent;
        Sdl.SDL_PollEvent(out myEvent);
        int numkeys;
        byte[] keys = Tao.Sdl.Sdl.SDL_GetKeyState(out numkeys);
        if (keys[c] == 1)
            pressed = true;
        return pressed;
    }

    //Detects if the user presses a key
    public int KeyPressed()
    {
        int pressed = -1;

        Sdl.SDL_PumpEvents();
        Sdl.SDL_Event keyEvent;
        if (Sdl.SDL_PollEvent(out keyEvent) == 1)
        {
            if (keyEvent.type == Sdl.SDL_KEYDOWN)
            {
                pressed = keyEvent.key.keysym.sym;
            }
        }

        return pressed;
    }

    public static void WriteHiddenText(string txt,
        short x, short y, byte r, byte g, byte b, Font f)
    {
        Sdl.SDL_Color color = new Sdl.SDL_Color(r, g, b);
        IntPtr textoComoImagen = SdlTtf.TTF_RenderText_Solid(
            f.GetFontType(), txt, color);
        if (textoComoImagen == IntPtr.Zero)
            Environment.Exit(5);

        Sdl.SDL_Rect origen = new Sdl.SDL_Rect(0, 0, width, height);
        Sdl.SDL_Rect dest = new Sdl.SDL_Rect(x, y, width, height);

        Sdl.SDL_BlitSurface(textoComoImagen, ref origen,
            hiddenScreen, ref dest);

        Sdl.SDL_FreeSurface(textoComoImagen);
    }

    public void ClearScreen()
    {
        Sdl.SDL_Rect source = new Sdl.SDL_Rect(0, 0, width, height);
        Sdl.SDL_FillRect(hiddenScreen, ref source, 0);
    }

    // Clears the menu of player ( right part)
    public void ClearRightPart()
    {
        // x, y, width, height
        Sdl.SDL_Rect source = new Sdl.SDL_Rect(
            600, 200, 400, (short)(height));
        Sdl.SDL_FillRect(hiddenScreen, ref source, 0);
    }
}

