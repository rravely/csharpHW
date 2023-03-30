using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{ 
    abstract class Game
    {
        public abstract void Play();

        //Print current score
        protected void PrintScore(int playerScore)
        {
            Console.SetCursorPosition(100, 1);
            Console.Write($"Score: {playerScore}");
        }
    }
}
