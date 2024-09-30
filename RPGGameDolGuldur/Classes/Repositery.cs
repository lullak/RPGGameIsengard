using System.Text.Json;

namespace RPGGameIsengard.Classes
{
    public static class Repositery
    {

        public static GameLogic LoadGame()
        {
            string gameState = File.ReadAllText("GameStateLoad.json");
            return JsonSerializer.Deserialize<GameLogic>(gameState);
        }

        public static void SaveGame(GameLogic game)
        {
            string gameState = JsonSerializer.Serialize(game);
            File.WriteAllText("GameStateLoad.json", gameState);
        }
    }
}
