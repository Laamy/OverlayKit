#region Includes

using OverlayKit.Keymap;
using OverlayKit.Window;

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#endregion

internal class Game : GameEngine
{
    Keymap keymap = new Keymap();

    public Game()
    {
        keymap.OnKeyPress += OnKeyPress;

        // we've finished so start the app
        Start();
    }

    ConcurrentBag<string> Events = new ConcurrentBag<string>() { "Start of key history" };

    public void AppendEvent(string @event)
    {
        if (Events.Count > 20)
        {
            var tmp = Events.ToList();
            tmp.RemoveAt(19);

            Events = new ConcurrentBag<string>(tmp);
        }

        Events.Add(@event);
    }

    public void OnKeyPress(OverlayKit.Keymap.KeyEvent @event)
    {
        if (@event.VKey == VKeyCodes.KeyUp)
            AppendEvent($"{(Keys)@event.Key} is down");

        if (@event.VKey == VKeyCodes.KeyDown)
            AppendEvent($"{(Keys)@event.Key} is up");
    }

    public override void OnInitialize()
    {
        WinHandler.AddTransparencyLayer(WinHandler.GetCurrentProcessWindowPtr());
    }

    protected override void OnUpdate(RenderWindow ctx)
    {
        ctx.Clear(Color.Black); // clear buffer ready for next frame
        ctx.DispatchEvents(); // handle window events

        int index = 0;

        // draw stuff here
        foreach (var @event in Events)
        {
            Text text = new Text(@event, FontRepository.GetFont("Arial"), 24);
            text.Position = new Vector2f(10, 10 + (24 * index));
            text.FillColor = Color.Red;

            ctx.Draw(text);
            index++;
        }

        ctx.Display(); // swap buffers
    }
}