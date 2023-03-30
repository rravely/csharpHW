using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RunDino
{
    class RunDino : Game.Game
    {
        ConsoleKeyInfo inputcki;

        //Dino walk motion (0, 1)
        int walk = 0;

        //Position of 5 hurdles
        int[] hurdlePosX = new int[5];

        //player score
        public int playerScore = 0;
        
        int countDinoFoot = 0;

        //Dino moving 
        int[] dinoMoving = new int[30] { 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 4, 4, 3, 3, 2, 2, 1 };
        int dinoMovingIndex = 0;
        int jumpHeight = 0;

        //check game over
        bool isGameOver = false;

        Random random = new Random();

        //speed
        int speed = 100;

        public override void Play()
        {
            while (true)
            {
                if (isGameOver)
                {
                    break;
                }
                hurdlePosX[0] = random.Next(119, 123);
                hurdlePosX[1] = random.Next(160, 185);
                hurdlePosX[2] = random.Next(225, 250);
                hurdlePosX[3] = random.Next(285, 310);
                hurdlePosX[4] = random.Next(340, 360);

                while (true)
                {
                    if (isGameOver)
                    {
                        break;
                    }
                    //If all hurdle are end
                    if (hurdlePosX[4] < 1)
                    {
                        break;
                    }

                    //Input space bar
                    if (Console.KeyAvailable)
                    {
                        inputcki = Console.ReadKey();
                        if (inputcki.Key.Equals(ConsoleKey.Spacebar))
                        {
                            if (dinoMovingIndex.Equals(0)) //Dino can jump when it is walking
                            {
                                walk = 2;
                                dinoMovingIndex++;
                            }
                        }
                    }

                    if (dinoMovingIndex > 28) //If jumping is end, walk again
                    {
                        dinoMovingIndex = 0;
                        walk = 0;
                    }
                    else if (dinoMovingIndex >= 1) //Jumping
                    {
                        dinoMovingIndex++;
                        jumpHeight = dinoMoving[dinoMovingIndex];
                    }

                    //Draw ground, hurdle, dino
                    DrawGround();

                    DrawDino(dinoMoving[dinoMovingIndex], ref walk);
                    for (int j = 0; j < hurdlePosX.Length; j++)
                    {
                        DrawHurdle(hurdlePosX[j]);
                    }
                    PrintScore(playerScore);

                    Thread.Sleep(speed);

                    //Erase dino
                    EraseDino(dinoMoving[dinoMovingIndex]);
                    //Erase hurdle
                    for (int j = 0; j < hurdlePosX.Length; j++)
                    {
                        EraseHurdle(hurdlePosX[j]);
                    }

                    //Position of hurdle change
                    for (int j = 0; j < hurdlePosX.Length; j++)
                    {
                        hurdlePosX[j]--;
                    }

                    //Check crush
                    for (int i = 0; i < hurdlePosX.Length; i++)
                    {

                        if (checkCrush(hurdlePosX[i], jumpHeight))
                        {
                            isGameOver = true; //crush
                        }

                    }

                    //speed up
                    SpeedUp(ref speed, playerScore);

                    countDinoFoot++;
                    playerScore = countDinoFoot / 30;
                }
            }
        }

        void DrawGround()
        {
            for (int i = 0; i < 120; i++)
            {
                Console.SetCursorPosition(i, 30);
                Console.Write("=");
            }

        }


        void DrawDino(int dinoMoving, ref int walk)
        {
            Console.SetCursorPosition(0, 20 - dinoMoving);

            string dino = @"
      ■■■
      ■()■
      ■■■▶
■      ■
■  ■■■■
  ■■■■■
    ■■■■
"; //9줄
            Console.Write(dino);

            if (walk.Equals(0))
            {
                Console.SetCursorPosition(0, 28 - dinoMoving);
                Console.Write("     ■  ■");
                Console.SetCursorPosition(0, 29 - dinoMoving);
                Console.Write("     ■   ■");
                walk = 1;
            }
            else if (walk.Equals(1))
            {
                Console.SetCursorPosition(0, 28 - dinoMoving);
                Console.Write("      ■  ■");
                Console.SetCursorPosition(0, 29 - dinoMoving);
                Console.Write("       ■  ■");
                walk = 0;
            }
            else
            {
                Console.SetCursorPosition(0, 28 - dinoMoving);
                Console.Write("     ■  ■");
                Console.SetCursorPosition(0, 29 - dinoMoving);
                Console.Write("     ■   ■");
            }
        }

        void EraseDino(int dinoMoving)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(0, 20 - dinoMoving + i);
                Console.Write("                ");
            }
        }

        void DrawHurdle(int hurdlecoordX)
        {
            if (hurdlecoordX < 117 && hurdlecoordX > 1) //when X coordinate of hurdle is in window
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.SetCursorPosition(hurdlecoordX, 25 + i);
                    Console.Write("□□");
                }

            }
        }

        void EraseHurdle(int hurdlecoordX)
        {
            if (hurdlecoordX < 117 && hurdlecoordX > 1) //when X coordinate of hurdle is in window
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.SetCursorPosition(hurdlecoordX, 25 + i);
                    Console.Write("    ");
                }

            }
        }

        //Check dino crush with hurdle
        bool checkCrush(int hurdlecoordX, int jumpHeight)
        {
            switch (jumpHeight)
            {
                case 0:
                    if (hurdlecoordX < 15 && hurdlecoordX > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 1:
                    if (hurdlecoordX < 14 && hurdlecoordX > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    if (hurdlecoordX < 14 && hurdlecoordX > 3)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 3:
                    if (hurdlecoordX < 14 && hurdlecoordX > 3)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 4:
                    if (hurdlecoordX < 13 && hurdlecoordX > 3)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
       
        //Speed up
        void SpeedUp(ref int speed, int playerScore)
        {
            if (playerScore >= 10)
            {
                speed = 70;
            }
            else if (playerScore >= 15)
            {
                speed = 50;
            }
            else if (playerScore >= 20)
            {
                speed = 30;
            }
            else if (playerScore >= 25)
            {
                speed = 10;
            }
            else if (playerScore >= 30)
            {
                speed = 5;
            }
        }
    }
}
