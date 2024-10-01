using System.Text.Json;

namespace RPGGameIsengard.Classes
{
    public static class Filehandler
    {
        private static readonly string _filePath = "GameStateLoad.json";

        public static GameLogic LoadGame()
        {
            return JsonSerializer.Deserialize<GameLogic>(File.ReadAllText(_filePath));
        }

        public static void SaveGame(GameLogic game)
        {
            File.WriteAllText(_filePath, JsonSerializer.Serialize(game));
        }
    }
}
