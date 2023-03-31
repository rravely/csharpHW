using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _Project_3MiniGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console Init
            int consoleWidth = 120, consoleHeight = 40;
            Console.SetWindowSize(consoleWidth, consoleHeight);
            Console.SetBufferSize(consoleWidth, consoleHeight);
            Console.CursorVisible = false;

            //Continue or finish
            int selectFinish = 1;

            while (selectFinish.Equals(1))
            {
                //Start Screen
                PrintStartLabel();

                //Input player Name
                string playerName = InputPlayerName();


                int playerTotalScore = 0;
                
                //Shooting game start
                Shooting.Shooting shooting = new Shooting.Shooting();
                shooting.Play();
                playerTotalScore += shooting.playerScore;
                Console.Clear();

                //Print current score and next game
                InputEnterToNextGame(playerTotalScore);
                
                
                //Stack block game start
                StackBlock.StackBlock stackBlock = new StackBlock.StackBlock();
                stackBlock.Play();
                playerTotalScore += stackBlock.playerScore;
                Console.Clear();

                //Print current score and next game
                InputEnterToNextGame(playerTotalScore);


                //Run Dino game start
                RunDino.RunDino runDino = new RunDino.RunDino();
                runDino.Play();
                playerTotalScore += runDino.playerScore;
                Console.Clear();
                

                //Display total score of player and Best score ever
                PrintPlayerScore(playerTotalScore);
                InputEnter();
                //Read Best score in txt file
                string[] bestScoreInfo = ReadBestScore();
                if (int.Parse(bestScoreInfo[1]) <= playerTotalScore)
                {
                    //Save score in txt file
                    SavePlayerScoreInText(playerName, playerTotalScore);
                    bestScoreInfo[0] = playerName;
                    bestScoreInfo[1] = Convert.ToString(playerTotalScore);
                }

                //Display Best score and ask player to continue or finish
                selectFinish = ExitWindow(bestScoreInfo[0], int.Parse(bestScoreInfo[1]));
                Console.Clear();
            }
            

        }

        //Print Start label
        static void PrintStartLabel()
        {
            ConsoleKeyInfo inputcki;
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    inputcki = Console.ReadKey();
                    break;
                }
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("\n\n\n\n");
                string[] startLabel = new string[9] {
                "   ######   ##########      ##      ########    ##########",
                "####            ##         ####     ##     ##       ##    ",
                "#               ##         #  #     ##      ##      ##    ",
                "###             ##        ##  ##    ##     ##       ##    ",
                "   ######       ##       ########   ########        ##    ",
                "        ##      ##       #      #   ## ###          ##    ",
                "        ##      ##       #      #   ## ###          ##    ",
                "       ###      ##      ##      ##  ##   ####       ##    ",
                "########        ##      #        #  ##      ##      ##    ",
                 };
                PrintCenterSort(startLabel);
                Console.SetCursorPosition(48, 30);
                Console.Write("Press any key to start");
                Thread.Sleep(500);
                Console.SetCursorPosition(48, 30);
                Console.Write("                                    ");
                Thread.Sleep(500);
            }
            Console.Clear();

        }

        //Print string array Center
        static void PrintCenterSort(string[] strArray)
        {
            for (int i = 0; i < strArray.Length; i++)
            {
                int bytelen = Encoding.Default.GetBytes(strArray[i]).Length;
                int padlen = 60 - (bytelen / 2);

                if (bytelen % 2 != 0)
                {
                    Console.WriteLine("{0}", "".PadLeft(padlen) + strArray[i] + "".PadRight(padlen - 1));
                }
                else
                {
                    Console.WriteLine("{0}", "".PadLeft(padlen) + strArray[i] + "".PadRight(padlen));
                }
            }
        }

        //Print string Center
        static void PrintCenterSort(string str)
        {
            int bytelen = Encoding.Default.GetBytes(str).Length;
            int padlen = 60 - (bytelen / 2);

            if (bytelen % 2 != 0)
            {
                Console.WriteLine("{0}", "".PadLeft(padlen) + str + "".PadRight(padlen - 1));
            }
            else
            {
                Console.WriteLine("{0}", "".PadLeft(padlen) + str + "".PadRight(padlen));
            }

        }

        //Input player's name
        static string InputPlayerName()
        {
            Console.SetCursorPosition(47, 18);
            Console.CursorVisible = true;
            Console.Write("Player Name: ");
            Console.SetCursorPosition(61, 18);
            string playerName = Console.ReadLine();
            Console.SetCursorPosition(0, 21);
            PrintCenterSort($"Hello, {playerName}.");
            Console.SetCursorPosition(0, 23);
            PrintCenterSort("If you want to start, please press Enter Key.");

            Console.CursorVisible = false;

            ConsoleKeyInfo cki;
            do {
                cki = Console.ReadKey();
            } while (!cki.Key.Equals(ConsoleKey.Enter));

            Console.Clear();
            return playerName;
        }

        //Input Enter if player wants to go next game
        static void InputEnterToNextGame(int playerScore)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 19);
            PrintCenterSort("If you ready to next game, press Enter Key.");

            Console.SetCursorPosition(0, 21);
            PrintCenterSort($"Your total score: {playerScore}");

            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey();
            } while (!cki.Key.Equals(ConsoleKey.Enter));

            Console.Clear();
        }

        //Input Enter
        static void InputEnter()
        {
            PrintCenterSort("Press Enter Key.");
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey();
            } while (!cki.Key.Equals(ConsoleKey.Enter));

            Console.Clear();
        }

        //Display total score of player
        static void PrintPlayerScore(int playerScore)
        {
            Console.SetCursorPosition(0, 19);
            PrintCenterSort($"Your total score: {playerScore}");
        }

        //Save text file
        static void SavePlayerScoreInText(string playerName, int playerScore)
        {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory)));
            string savePath = Path.Combine(path, "playerScore.txt");
            string saveText = string.Format($"{playerName} {playerScore}");
            System.IO.File.WriteAllText(savePath, saveText, Encoding.Default);
        }

        //Read text file
        static string[] ReadBestScore()
        {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory)));
            string filePath = Path.Combine(path, "playerScore.txt");
            //string filePath = @"C:\Users\User\source\repos\[Project]3MiniGame\playerScore.txt";
            string textValue = System.IO.File.ReadAllText(filePath);
            string[] textValueArray = new string[2];
            textValueArray = textValue.Split(' ');
            return textValueArray;
        }

        //Print Best Score
        static int ExitWindow(string playerName, int playerScore)
        {
            Console.SetCursorPosition(0, 18);
            PrintCenterSort("Best Score");
            Console.SetCursorPosition(0, 19);
            PrintCenterSort($"{playerName}: {playerScore}");

            PrintContinueFinish(0);

            ConsoleKeyInfo cki;
            cki = Console.ReadKey();
            int index = 0;
            while (!cki.Key.Equals(ConsoleKey.Enter))
            {
                if (cki.Key.Equals(ConsoleKey.LeftArrow))
                {
                    index = 1;
                }
                else if (cki.Key.Equals(ConsoleKey.RightArrow))
                {
                    index = 0;
                }
                PrintContinueFinish(index);
                cki = Console.ReadKey();
            }
            return index;
        }

        //Print continue or finish
        static void PrintContinueFinish(int index)
        {
            Console.SetCursorPosition(51, 22);
            Console.Write("Continue");
            Console.SetCursorPosition(65, 22);
            Console.Write("Finish");

            switch (index)
            {
                case 0: //exit
                    Console.SetCursorPosition(48, 22);
                    Console.Write("  ");
                    Console.SetCursorPosition(62, 22);
                    Console.Write("▶");
                    break;
                case 1:
                    Console.SetCursorPosition(48, 22);
                    Console.Write("▶");
                    Console.SetCursorPosition(62, 22);
                    Console.Write("  ");
                    break;
            }

        }
    }
}
