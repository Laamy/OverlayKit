using SFML.Graphics;
using System.Collections.Generic;

internal class FontRepository
{
    private static Dictionary<string, Font> _fonts = new Dictionary<string, Font>()
    {
        { "arial", new Font("Data\\Font\\Arial.ttf") }
    };

    public static Font GetFont(string name) => _fonts[name.ToLower()];
}