using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Cookie_Clicker
{
    public class GameState
    {
        public float Score { get; set; }
        public float PreviousScore { get; set; }
        public double Time { get; set; }
        public bool Gamestart { get; set; }
        public GameState(float score, float previousScore, double time, bool gamestart)
        {
            this.Score = score;
            this.PreviousScore = previousScore;
            Time = time;
            Gamestart = gamestart;
        }
        public String[] encryptedPlus()
        {
            string[] superEncoded = new string[13];
            superEncoded[0] = Encrypt(Score.ToString());
            superEncoded[1] = "Giw2ZDUmYyEmJi1jNzEsLy8mJw==";
            superEncoded[2] = "Oiw2ZDUmYyEmJi1jNzEsLy8mJw==";
            superEncoded[3] = "GiYwb2M6LDZkNSZjMzEsISIhLzpjISYmLWM3LC8n";
            superEncoded[4] = Encrypt(PreviousScore.ToString());
            superEncoded[5] = "BywtZDdjMSYzLzpjNyxjNysqMGMkNjo=";
            superEncoded[6] = "CyZkMGMpNjA3YzcxOiotJGM3LGMkJjdjImMxKjAm";
            superEncoded[7] = "DDY3YywlYzosNm9jOiYwb2MqN2QwYzcxNiY=";
            superEncoded[8] = Encrypt(Time.ToString());
            superEncoded[9] = "Giw2YzEmMDMsLSdjIi0nYzcrIjdkMGMrKjBjIDYm";
            superEncoded[10] = "FyxjMDciMTdjNzEsNiEvJmMsLWM3KyZjJyw2IS8m";
            superEncoded[11] = "FCsqLyZjKyZjMDcxLCgmMGMrKjBjLiItLzpjMDc2ISEvJg==";
            superEncoded[12] = Encrypt(Gamestart.ToString());
            return superEncoded;
        }
        public void Decoder(string[] what)
        {

            string[] decodedArray = new string[what.Length];

            for (int i = 0; i < what.Length; i++)
            {

                decodedArray[i] = Decrypt(what[i]);
            }
            Score = float.Parse(decodedArray[0]);
            PreviousScore = float.Parse(decodedArray[4]);
            Time = double.Parse(decodedArray[8]);
            Gamestart = bool.Parse(decodedArray[12]);
        }
        static string Encrypt(string text)
        {
            string result = "";
            int shift = 2; // Shift by 2 for letter "C"

            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    char d = char.IsUpper(c) ? 'A' : 'a';
                    result += (char)((((c + shift) - d) % 26) + d);
                }
                else
                {
                    result += c; // Leave non-letter characters as they are
                }
            }

            return result;
        }

        static string Decrypt(string text)
        {
            string result = "";
            int shift = 2; // Shift by 2 for letter "C"

            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    char d = char.IsUpper(c) ? 'A' : 'a';
                    result += (char)((((c - shift) - d + 26) % 26) + d);
                }
                else
                {
                    result += c; // Leave non-letter characters as they are
                }
            }
            return result;
        }
    }
}
