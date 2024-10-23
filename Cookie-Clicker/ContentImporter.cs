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
            GameState GM1 = new GameState();
            GameState GM2 = new GameState();
            if (File.Exists(name))
            {
                using (StreamReader reader = new StreamReader(name))
                {
                    GM1 = JsonSerializer.Deserialize<GameState>(reader.ReadLine());
                    if(GM1 == null)
                    {
                        return default;
                    }
                }
            }
            if (File.Exists(assests))
            {
                using (StreamReader reader = new StreamReader(assests))
                {
                    GM2 = JsonSerializer.Deserialize<GameState>(reader.ReadLine());
                    if(GM2 == null)
                    {
                        return default;
                    }
                }
            }
            if (GM1.score != GM2.score || GM1.previousScore != GM2.previousScore || GM1.Time != GM2.Time || GM1.Gamestart != GM2.Gamestart)
            {
                GM2.score += 3;
                GM2.previousScore += 3;
            }
            return GM2;
        }
        public void Save(GameState gs)
        {
            string SearlizedString = JsonSerializer.Serialize(gs.encoded);
            string SearlizedString2 = JsonSerializer.Serialize(gs.encryptedPlus());
            using (StreamWriter writer = new StreamWriter(name))
            {
                writer.WriteLine(SearlizedString);
            }
            using (StreamWriter writer = new StreamWriter(assests))
            {
                writer.WriteLine(SearlizedString2);
            }
            //try to serialize gamestate. 
            /*
            using (StreamWriter writer = new StreamWriter(name))
            {
                writer.WriteLine("Score: " + gs.score.ToString());
                writer.WriteLine("PreviousScore: " + gs.previousScore.ToString());
                writer.WriteLine("Time: " + gs.Time.ToString("F2"));
                writer.WriteLine("GameStart: " + gs.Gamestart.ToString());
            }
            using (StreamWriter writer = new StreamWriter(assests))
            {
                writer.WriteLine(Encrypt("Score: " + (gs.score).ToString(), 'C'));
                writer.WriteLine("Giw2ZDUmYyEmJi1jNzEsLy8mJw==");
                writer.WriteLine("Oiw2ZDUmYyEmJi1jNzEsLy8mJw==");
                writer.WriteLine("GiYwb2M6LDZkNSZjMzEsISIhLzpjISYmLWM3LC8n");
                writer.WriteLine(Encrypt("PreviousScore: " + gs.previousScore.ToString(),'C'));
                writer.WriteLine("BywtZDdjMSYzLzpjNyxjNysqMGMkNjo=");
                writer.WriteLine("CyZkMGMpNjA3YzcxOiotJGM3LGMkJjdjImMxKjAm");
                writer.WriteLine("DDY3YywlYzosNm9jOiYwb2MqN2QwYzcxNiY=");
                writer.WriteLine(Encrypt("Time: " + gs.Time.ToString("F2"), 'C'));
                writer.WriteLine("Giw2YzEmMDMsLSdjIi0nYzcrIjdkMGMrKjBjIDYm");
                writer.WriteLine("FyxjMDciMTdjNzEsNiEvJmMsLWM3KyZjJyw2IS8m");
                writer.WriteLine("FCsqLyZjKyZjMDcxLCgmMGMrKjBjLiItLzpjMDc2ISEvJg==");
                writer.WriteLine(Encrypt("GameStart: " + gs.Gamestart.ToString(), 'C'));
            }*/

        }
        
        static string Decrypt(string encryptedText, char key)
        {
            byte[] buffer = Convert.FromBase64String(encryptedText);
            char[] decryptedBuffer = new char[buffer.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                decryptedBuffer[i] = (char)(buffer[i] ^ key);
            }
            return new string(decryptedBuffer);
        }
    }
}
