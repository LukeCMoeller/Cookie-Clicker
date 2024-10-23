using SharpDX.WIC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Cookie_Clicker
{
    public class ContentImporter
    {
        string name = "SaveData.txt";
        string assests = "Assest.txt";
        public GameState Load()
        {
            GameState GM1 = new GameState(-1, -1, -1, false);
            GameState GM2 = new GameState(-1, -1, -1, false);

            try
            {
                if (File.Exists(name))
                {
                    string json = File.ReadAllText(name);
                    GM1 = JsonSerializer.Deserialize<GameState>(json);
                }
                if (File.Exists(assests))
                {
                    string json = File.ReadAllText(assests);
                    GM2.Decoder(JsonSerializer.Deserialize<string[]>(json));
                }
                if (GM1.Score != GM2.Score || GM1.PreviousScore != GM2.PreviousScore || GM1.Time != GM2.Time || GM1.Gamestart != GM2.Gamestart)
                {
                    GM2.Score *= 3;
                    GM2.PreviousScore *= 3;
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Failed to deserialize GameState: {ex.Message}");
                return default;
            }

            return GM2;
        }
        public void Save(GameState gs)
        {
            string SearlizedString = JsonSerializer.Serialize(gs);
            string SearlizedString2 = JsonSerializer.Serialize(gs.encryptedPlus());
            File.WriteAllText(name, SearlizedString);
            File.WriteAllText(assests, SearlizedString2);
        }
    }
}
