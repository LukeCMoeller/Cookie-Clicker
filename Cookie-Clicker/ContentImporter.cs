using SharpDX.WIC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookie_Clicker
{
    public class ContentImporter
    {
        string name = "SaveData.txt";
        string assests = "Assest.txt";
        public  string[] Load()
        {
            string[] info = new string[13];
            string[] info2 = new string[4];
            if (File.Exists(name))
            {
                using (StreamReader reader = new StreamReader(name))
                {
                    info2[0] = reader.ReadLine();
                    info2[1] = reader.ReadLine();
                    info2[2] = reader.ReadLine();
                    info2[3] = reader.ReadLine();
                    if (info2[0] == null)
                    {
                        return default;
                    }
                }
                info2[0] = info2[0].Substring(7);
                info2[1] = info2[1].Substring(15);
                info2[2] = info2[2].Substring(6);
                info2[3] = info2[3].Substring(11);
            }
            if (File.Exists(assests))
            {
                using (StreamReader reader = new StreamReader(assests))
                {
                    string line;
                    int i = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        info[i] = Decrypt(line, 'C'); 
                        i++;
                    }
                    if(info[0] == null)
                    {
                        return default;
                    }
                }
                info[0] = info[0].Substring(7);
                info[4] = info[4].Substring(15);
                info[8] = info[8].Substring(6);
                info[12] = info[12].Substring(11);
            }
            
            if (info[0] != info2[0] || info[4] != info2[1] || info[8] != info2[2] || info[12] != info2[3])
            {
                info[0] = "3" + info[0];
                info[4] = "3" + info[4];
            }
            return info;
        }
        public void Save(GameState gs)
        {
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
        /// <summary>
        /// encryption and decrytpion provided by gbt
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        static string Encrypt(string text, char key)
        {
            char[] buffer = text.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (char)(buffer[i] ^ key);
            }
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(buffer));
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
