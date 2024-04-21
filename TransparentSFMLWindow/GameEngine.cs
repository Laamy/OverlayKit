#region Includes

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using View = SFML.Graphics.View;

#endregion

internal class GameEngine
{
    /// <summary>
    /// The games target framerate
    /// </summary>
    private int targetFPS = 144;

    // sdl stuff
    private RenderWindow window;

    public virtual void OnInitialize() { }

    public void Start()
    {
        // sdl renderer
        VideoMode mode = new VideoMode(800, 600);
        window = new RenderWindow(mode, "Game Engine");
        window.Closed += (s, e) => window.Close();
        window.Resized += (s, e) => Size = new Vector2u(e.Width, e.Height);

        window.SetActive();

        OnInitialize();

        long targetTicksPerFrame = TimeSpan.TicksPerSecond / targetFPS;
        long prevTicks = DateTime.Now.Ticks;

        while (window.IsOpen)
        {
            long currTicks = DateTime.Now.Ticks;
            long elapsedTicks = currTicks - prevTicks;

            if (elapsedTicks >= targetTicksPerFrame)
            {
                prevTicks = currTicks;
                OnUpdate(window); // redraw window
            }

            Thread.Sleep(1);
        }
    }

    protected virtual void OnUpdate(RenderWindow ctx) { }// draw event

    #region Easy Game Properties

    public Vector2u Size
    {
        get => window.Size;
        set
        {
            View view = new View(new FloatRect(0, 0, value.X, value.Y));
            window.SetView(view);
        }
    }

    public String Title
    { set => window.SetTitle(value); }

    #endregion
}