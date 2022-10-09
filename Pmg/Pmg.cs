
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Pmg;

public class Game : GameWindow
{
    public Game(int width, int height, string title, VSyncMode isVSyncMode) : base(GameWindowSettings.Default, 
        new NativeWindowSettings() { Size = (width, height), Title = title })
    {
        VSync = isVSyncMode;
    }

    public static VSyncMode GetVSyncMode(int vsync)
    {
        return vsync switch
        {
            0 => VSyncMode.Off,
            1 => VSyncMode.On,
            _ => VSyncMode.Adaptive
        };
    }
    
    protected override void OnLoad() 
    { 
        base.OnLoad(); 
        GL.ClearColor(Color4.Red);
    } 
    protected override void OnRenderFrame(FrameEventArgs args) 
    {
            
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    { 
        if (KeyboardState.IsKeyDown(Keys.Escape))
        { 
            Close();
        }
        base.OnUpdateFrame(args);
    }
}