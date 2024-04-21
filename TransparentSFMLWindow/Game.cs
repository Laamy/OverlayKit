#region Includes

using OverlayKit.Window;

using SFML.Graphics;

#endregion

internal class Game : GameEngine
{
    public Game()
    {
        // we've finished so start the app
        Start();
    }

    public override void OnInitialize()
    {
        WinHandler.AddTransparencyLayer(WinHandler.GetCurrentProcessWindowPtr());
    }

    protected override void OnUpdate(RenderWindow ctx)
    {
        ctx.Clear(Color.Black); // clear buffer ready for next frame
        ctx.DispatchEvents(); // handle window events

        // draw stuff here

        ctx.Display(); // swap buffers
    }
}