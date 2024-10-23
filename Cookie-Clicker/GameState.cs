using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookie_Clicker
{
    public class GameState
    {
        public float score;
        public float previousScore;
        public double Time;
        public bool Gamestart;
        public GameState(float score, float previousScore, double time, bool gamestart)
        {
            this.score = score;
            this.previousScore = previousScore;
            Time = time;
            Gamestart = gamestart;
        }
    }
}
