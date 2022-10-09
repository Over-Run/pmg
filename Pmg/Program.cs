namespace Pmg;
using OpenTK.Windowing.Common;




public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            

            using (var game = new Game(800, 600, "panda music game", Game.GetVSyncMode(1)))
            {
                Console.WriteLine(game.RenderTime);
                Console.WriteLine(game.UpdateTime);
                
                game.Run();
            }
        }
    }