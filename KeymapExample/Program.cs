using OverlayKit.Keymap;
using OverlayKit.Window;
using System;

class Program
{
    static void Main(string[] args)
    {
        Keymap keymap = new Keymap();

        keymap.OnKeyPress += OnKeyPress;

        Console.ReadKey();
    }

    private static void OnKeyPress(KeyEvent @event)
    {
        Console.WriteLine($"{@event.Key} {@event.VKey}");
    }
}