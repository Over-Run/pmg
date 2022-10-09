
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Pmg;

public class Pgm
{
    public class Game : GameWindow
    {
        public Game(int width, int height, string title) : base(GameWindowSettings.Default,
            new NativeWindowSettings() { Size = (width, height), Title = title })
        {
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

            if (KeyboardState.IsKeyReleased(Keys.B))
            {
                GL.ClearColor(Color4.Blue);
            }

            if (KeyboardState.IsKeyReleased(Keys.R))
            {
                GL.ClearColor(Color4.Red);
            }
            if (KeyboardState.IsKeyReleased(Keys.G))
            {
                GL.ClearColor(Color4.Green);
            }
            base.OnUpdateFrame(args);
        }
    }
}