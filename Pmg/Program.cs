namespace Pmg;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;




public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
                Title = "Panda music game",
                Flags = ContextFlags.ForwardCompatible
            };

            using (var game = new Pgm.Game(800, 600, "panda music game"))
            {
                Console.WriteLine(game.RenderTime);
                Console.WriteLine(game.UpdateTime);
                game.VSync = VSyncMode.On;
                game.Run();
            }
        }
    }