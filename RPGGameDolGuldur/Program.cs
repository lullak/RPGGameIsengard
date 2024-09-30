using RPGGameIsengard.Classes;

namespace RPGGameIsengard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameLogic gameLogic = new GameLogic();
            gameLogic.StartGameLoop();


            Console.ReadKey();
        }
    }
}
