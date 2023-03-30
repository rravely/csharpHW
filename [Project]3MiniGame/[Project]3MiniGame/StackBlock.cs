using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        //player info
        public int playerScore = 0;
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
                //Draw Answer
                for (int i = 0; i < blockNum; i++)
                {
                    blockList.Add(random.Next(1, 5)); //1~4까지 랜덤 생성
                }


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

                    //input arrow
                    arrowNum = InputArrowKey();
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

                    //draw answer block of player
                    DrawPlayerBlock(playerBlock);

                    ErasePlayerLife(playerLife);
                }

                blockList.Clear();
                playerBlock.Clear();
                ErasePlayerBlock();
            }
        }

        static void DrawBorder()
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


        static void DrawBlockAnswer(List<int> blockList)
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



        static void DrawArrowColor(int arrowNum)
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

        //Return number of arrow key
        static int InputArrowKey()
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
        static void DrawPlayerBlock(List<int> playerBlock)
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
        static void ErasePlayerBlock()
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
        static void DrawPlayerLife(int playerLife)
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
        static void ErasePlayerLife(int playerLife)
        {
            Console.SetCursorPosition(85, 1);
            for (int i = 0; i < playerLife; i++)
            {
                Console.Write("   ");
            }
        }
    }
}
