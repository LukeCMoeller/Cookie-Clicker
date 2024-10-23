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
        public float score;
        public float previousScore;
        public double Time;
        public bool Gamestart;
        public string[] encoded = new string[4];
        public GameState()
        {
            score = -5;
        }
        public GameState(float score, float previousScore, double time, bool gamestart)
        {
            this.score = score;
            this.previousScore = previousScore;
            Time = time;
            Gamestart = gamestart;
            encoded[0] = Encrypt(score.ToString(), 'C');
            encoded[1] = Encrypt(previousScore.ToString(), 'C');
            encoded[2] = Encrypt(time.ToString(), 'C');
            encoded[3] = Encrypt(gamestart.ToString(), 'C');
        }
        public String[] encryptedPlus()
        {
            string[] superEncoded = new string[13];
            superEncoded[0] = Encrypt(score.ToString(), 'C');
            superEncoded[1] = "Giw2ZDUmYyEmJi1jNzEsLy8mJw==";
            superEncoded[2] = "Oiw2ZDUmYyEmJi1jNzEsLy8mJw==";
            superEncoded[3] = "GiYwb2M6LDZkNSZjMzEsISIhLzpjISYmLWM3LC8n";
            superEncoded[4] = Encrypt(previousScore.ToString(), 'C');
            superEncoded[5] = "BywtZDdjMSYzLzpjNyxjNysqMGMkNjo=";
            superEncoded[6] = "CyZkMGMpNjA3YzcxOiotJGM3LGMkJjdjImMxKjAm";
            superEncoded[7] = "DDY3YywlYzosNm9jOiYwb2MqN2QwYzcxNiY=";
            superEncoded[8] = Encrypt(Time.ToString(), 'C');
            superEncoded[9] = "Giw2YzEmMDMsLSdjIi0nYzcrIjdkMGMrKjBjIDYm";
            superEncoded[10] = "FyxjMDciMTdjNzEsNiEvJmMsLWM3KyZjJyw2IS8m";
            superEncoded[11] = "FCsqLyZjKyZjMDcxLCgmMGMrKjBjLiItLzpjMDc2ISEvJg==";
            superEncoded[12] = Encrypt(Gamestart.ToString(), 'C');
            return superEncoded;
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
    }
}
