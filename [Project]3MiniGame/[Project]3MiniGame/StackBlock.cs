using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StackBlock
{
    class StackBlock : Game.Game
    {
        //Arrow Key num List
        List<int> inputArrowNum = new List<int>();

        //Answer of Game
        List<int> blockList = new List<int>();

        //Player Answer
        List<int> playerBlock = new List<int>();

        Random random = new Random();

        //number of block
        int blockNum = 6;

        int arrowNum;

        //Countdown 
        int countDown = 20;
        int time = 200;

        //player info
        int playerLife = 3;

        public override void Play()
        {
            while (true)
            {
                //life under than 0
                if (playerLife.Equals(0))
                {
                    Console.Clear();
                    break;
                }

                DrawBorder();
                DrawTimer();
                //Draw Answer
                for (int i = 0; i < blockNum; i++)
                {
                    blockList.Add(random.Next(1, 5)); //1~4까지 랜덤 생성
                }

                countDown = 20;

                while (true)
                {
                    //If player input right 6 blocks
                    if (playerBlock.Count.Equals(6))
                    {
                        playerScore++;
                        break;
                    }
                    

                    //draw answer block
                    DrawBlockAnswer(blockList);

                    //draw player info
                    PrintScore(playerScore);
                    DrawPlayerLife(playerLife);
                    //darw color of arrow
                    for (int i = 1; i < 5; i++)
                    {
                        DrawArrowColor(i);
                    }
                    //draw answer block of player
                    DrawPlayerBlock(playerBlock);

                    if (Console.KeyAvailable)
                    {
                        
                        //input arrow
                        arrowNum = InputArrowKey();
                        DrawArrowColorBright(arrowNum);
                        if (blockList[playerBlock.Count].Equals(arrowNum)) //Number of input key is same as answer of block 
                        {
                            playerBlock.Add(arrowNum);
                        }
                        else
                        {
                            ErasePlayerLife(playerLife);
                            playerLife--;
                            if (playerLife.Equals(0))
                            {
                                break;
                            }
                        }
                    }

                    Thread.Sleep(time);
                    countDown--;
                    EraseTimer(countDown);

                    //If time is over
                    if (countDown.Equals(0))
                    {
                        ErasePlayerLife(playerLife);
                        playerLife--;
                        break;
                    }

                    //Speed up
                    SpeedUp(ref time, playerScore);
                    
                }

                blockList.Clear();
                playerBlock.Clear();
                ErasePlayerBlock();
            }
        }

        void DrawBorder()
        {
            Console.SetCursorPosition(0, 2);
            for (int i = 0; i < 120; i++)
            {
                Console.Write("-");
            }
            Console.SetCursorPosition(0, 30);
            for (int i = 0; i < 120; i++)
            {
                Console.Write("-");
            }
        }

        void DrawTimer()
        {
            Console.SetCursorPosition(110, 6);
            Console.Write("┌───┐");
            for (int i = 0; i < 20; i++)
            {
                Console.SetCursorPosition(110, 7 + i);
                Console.Write("│ ■│");
            }
            Console.SetCursorPosition(110, 27);
            Console.Write("└───┘");
        }

        void EraseTimer(int countDown)
        {
            Console.SetCursorPosition(110, 26 - countDown);
            Console.Write("│   │");
        }


        void DrawBlockAnswer(List<int> blockList)
        {
            int blockPosX = 30, blockPosY = 27;
            for (int i = 0; i < blockList.Count; i++)
            {
                //색 바꾸기
                switch (blockList[i])
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.DarkRed; //Left
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkBlue; //Up
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkGreen; //Down
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkYellow; //Rigth
                        break;
                }
                Console.SetCursorPosition(blockPosX, blockPosY);
                Console.WriteLine("■■■■■■■■■");
                Console.SetCursorPosition(blockPosX, blockPosY - 1);
                Console.WriteLine("■■■■■■■■■");
                Console.SetCursorPosition(blockPosX, blockPosY - 2);
                Console.WriteLine("■■■■■■■■■");
                Console.ForegroundColor = ConsoleColor.Gray;
                blockPosY -= 4;
            }
        }


        //Draw arrow with color
        void DrawArrowColor(int arrowNum)
        {
            //Color of arrow
            switch (arrowNum)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkRed; //Left
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.DarkBlue; //Up
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.DarkGreen; //Down
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.DarkYellow; //Rigth
                    break;
            }

            //draw
            switch (arrowNum)
            {
                case 1:
                    Console.SetCursorPosition(39, 33);
                    Console.Write("■");
                    Console.SetCursorPosition(37, 34);
                    Console.Write("■■");
                    Console.SetCursorPosition(35, 35);
                    Console.Write("■■■");
                    Console.SetCursorPosition(37, 36);
                    Console.Write("■■");
                    Console.SetCursorPosition(39, 37);
                    Console.Write("■");
                    break;
                case 2:
                    Console.SetCursorPosition(51, 34);
                    Console.Write("■");
                    Console.SetCursorPosition(49, 35);
                    Console.Write("■■■");
                    Console.SetCursorPosition(47, 36);
                    Console.Write("■■■■■");
                    break;
                case 3:
                    Console.SetCursorPosition(63, 34);
                    Console.Write("■■■■■");
                    Console.SetCursorPosition(65, 35);
                    Console.Write("■■■");
                    Console.SetCursorPosition(67, 36);
                    Console.Write("■");
                    break;
                case 4:
                    Console.SetCursorPosition(79, 33);
                    Console.Write("■");
                    Console.SetCursorPosition(79, 34);
                    Console.Write("■■");
                    Console.SetCursorPosition(79, 35);
                    Console.Write("■■■");
                    Console.SetCursorPosition(79, 36);
                    Console.Write("■■");
                    Console.SetCursorPosition(79, 37);
                    Console.Write("■");
                    break;
            }

            //Return to default color
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        //Draw bright color arrow pressed
        void DrawArrowColorBright(int arrowNum)
        {
            //Color of arrow
            switch (arrowNum)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red; //Left
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Blue; //Up
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Green; //Down
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Yellow; //Rigth
                    break;
            }

            //draw
            switch (arrowNum)
            {
                case 1:
                    Console.SetCursorPosition(39, 33);
                    Console.Write("■");
                    Console.SetCursorPosition(37, 34);
                    Console.Write("■■");
                    Console.SetCursorPosition(35, 35);
                    Console.Write("■■■");
                    Console.SetCursorPosition(37, 36);
                    Console.Write("■■");
                    Console.SetCursorPosition(39, 37);
                    Console.Write("■");
                    break;
                case 2:
                    Console.SetCursorPosition(51, 34);
                    Console.Write("■");
                    Console.SetCursorPosition(49, 35);
                    Console.Write("■■■");
                    Console.SetCursorPosition(47, 36);
                    Console.Write("■■■■■");
                    break;
                case 3:
                    Console.SetCursorPosition(63, 34);
                    Console.Write("■■■■■");
                    Console.SetCursorPosition(65, 35);
                    Console.Write("■■■");
                    Console.SetCursorPosition(67, 36);
                    Console.Write("■");
                    break;
                case 4:
                    Console.SetCursorPosition(79, 33);
                    Console.Write("■");
                    Console.SetCursorPosition(79, 34);
                    Console.Write("■■");
                    Console.SetCursorPosition(79, 35);
                    Console.Write("■■■");
                    Console.SetCursorPosition(79, 36);
                    Console.Write("■■");
                    Console.SetCursorPosition(79, 37);
                    Console.Write("■");
                    break;
            }
            //Return to default color
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //Return number of arrow key
        int InputArrowKey()
        {
            ConsoleKeyInfo inputKey;

            inputKey = Console.ReadKey();

            if (inputKey.Key.Equals(ConsoleKey.LeftArrow))
            {
                return 1;
            }
            else if (inputKey.Key.Equals(ConsoleKey.UpArrow))
            {
                return 2;
            }
            else if (inputKey.Key.Equals(ConsoleKey.DownArrow))
            {
                return 3;
            }
            else if (inputKey.Key.Equals(ConsoleKey.RightArrow))
            {
                return 4;
            }
            else
            {
                return 5;
            }
        }

        //Draw block of player
        void DrawPlayerBlock(List<int> playerBlock)
        {
            int blockPosX = 70, blockPosY = 27;
            for (int i = 0; i < playerBlock.Count; i++)
            {
                //색 바꾸기
                switch (playerBlock[i])
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.DarkRed; //Left
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkBlue; //Up
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkGreen; //Down
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkYellow; //Rigth
                        break;
                }
                Console.SetCursorPosition(blockPosX, blockPosY);
                Console.WriteLine("■■■■■■■■■");
                Console.SetCursorPosition(blockPosX, blockPosY - 1);
                Console.WriteLine("■■■■■■■■■");
                Console.SetCursorPosition(blockPosX, blockPosY - 2);
                Console.WriteLine("■■■■■■■■■");
                Console.ForegroundColor = ConsoleColor.Gray;
                blockPosY -= 4;
            }
        }

        //Erase block of player
        void ErasePlayerBlock()
        {
            int blockPosX = 70, blockPosY = 27;
            for (int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(blockPosX, blockPosY);
                Console.WriteLine("                  ");
                Console.SetCursorPosition(blockPosX, blockPosY - 1);
                Console.WriteLine("                  ");
                Console.SetCursorPosition(blockPosX, blockPosY - 2);
                Console.WriteLine("                  ");
                blockPosY -= 4;
            }
        }
  
        //Print player life
        void DrawPlayerLife(int playerLife)
        {
            Console.SetCursorPosition(85, 1);
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < playerLife; i++)
            {
                Console.Write("♥ ");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //Erase player life
        void ErasePlayerLife(int playerLife)
        {
            Console.SetCursorPosition(85, 1);
            for (int i = 0; i < playerLife; i++)
            {
                Console.Write("   ");
            }
        }

        //Speed Up
        void SpeedUp(ref int time, int playerScore)
        {
            if (playerScore > 10)
            {
                time = 150;
            }
        }
    }
}
