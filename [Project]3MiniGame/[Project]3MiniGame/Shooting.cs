using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shooting
{
    class Shooting : Game.Game
    {
        //Position of box
        int[] boxPosX = new int[3];
        int[] boxPosY = new int[3];
        int boxNum = 2;

        //Default gun position
        int gunPosX = 60, gunPosY = 37;

        //Position of bullets
        List<int> bulletsPosX = new List<int>();
        List<int> bulletsPosY = new List<int>();

        Random random = new Random();

        ConsoleKeyInfo inputKey;

        //player info
        public int playerScore = 0;
        int life = 3;

        //game speed
        int speed = 200;

        public override void Play()
        {
            while (true)
            {
                //Initial box position
                boxPosY[0] = 0;
                boxPosY[1] = -15;

                //Position box created
                boxPosX[0] = random.Next(40, 75);
                boxPosX[1] = random.Next(40, 75);

                //If life is under than 0, then die
                if (life < 1)
                {
                    break;
                }

                while (true)
                {
                    
                    //If 2 boxes out of range, break while loop
                    if (boxPosY[0].Equals(39) && boxPosY[1].Equals(39))
                    {
                        break;
                    }
                   

                    //Input key
                    if (Console.KeyAvailable) //If there is key in buffer
                    {
                        inputKey = Console.ReadKey(true);
                        if (inputKey.Key.Equals(ConsoleKey.LeftArrow))
                        {
                            //Smaller than border
                            if (gunPosX > 42)
                            {
                                gunPosX -= 2;
                            }
                            else
                            {
                                gunPosX = 42;
                            }

                        }
                        else if (inputKey.Key.Equals(ConsoleKey.RightArrow))
                        {
                            if (gunPosX < 77)
                            {
                                gunPosX += 2;
                            }
                            else
                            {
                                gunPosX = 77;
                            }

                        }
                        else if (inputKey.Key.Equals(ConsoleKey.Escape))
                        {
                            //Console.Clear();
                            break;
                        }
                    }

                    //If bullets hits the top
                    for (int i = 0; i < bulletsPosX.Count; i++)
                    {
                        if (bulletsPosY[i] < 3) // consider buffer size
                        {
                            bulletsPosX.RemoveAt(i);
                            bulletsPosY.RemoveAt(i);
                        }
                    }
                    PrintScore(playerScore);
                    PrintLife(life);

                    DrawBorder();

                    //draw box, gun, bullets
                    DrawBox(boxPosX[0], boxPosY[0]);
                    DrawGun(gunPosX, gunPosY);
                    DrawBox(boxPosX[1], boxPosY[1]);

                    bulletsPosX.Add(gunPosX + 1);
                    bulletsPosY.Add(gunPosY - 2);
                    for (int i = 0; i < bulletsPosX.Count; i++)
                    {
                        DrawBullets(bulletsPosX[i], bulletsPosY[i]);
                    }

                    Thread.Sleep(speed);

                    //erase box, gun, bullets
                    EraseBox(boxPosX[0], boxPosY[0]);
                    EraseGun(gunPosX, gunPosY);
                    EraseBox(boxPosX[1], boxPosY[1]);
                    for (int i = 0; i < bulletsPosX.Count; i++)
                    {
                        EraseBullets(bulletsPosX[i], bulletsPosY[i]);
                    }
                    ErasePlayerLife(life);

                    //Box hits with bottom
                    for (int i = 0; i < boxNum; i++)
                    {
                        if (boxPosY[i] >= 35 && boxPosY[i] < 38) // If Box hits with gun
                        {
                            life -= 1;
                            //If life is under than 0, then die
                            if (life < 0)
                            {
                                break;
                            }
                            RemoveBox(ref boxPosX[i], ref boxPosY[i]);
                        }
                    }

                    //check bullet hits box
                    for (int i = 0; i < bulletsPosX.Count; i++) //bullets
                    {
                        for (int j = 0; j < boxNum; j++) //boxs
                        {
                            if (bulletsPosX[i] >= boxPosX[j] && bulletsPosX[i] <= boxPosX[j] + 6) //Position of bullets between box
                            {
                                if (bulletsPosY[i] <= boxPosY[j] + 3 && bulletsPosY[i] >= boxPosY[j])
                                {
                                    RemoveBox(ref boxPosX[j], ref boxPosY[j]);
                                    bulletsPosX.RemoveAt(i); //erase about index i
                                    bulletsPosY.RemoveAt(i);
                                    playerScore++;
                                    break;
                                }
                            }
                        }

                    }

                    //Change box, gun, bullets position
                    for (int j = 0; j < bulletsPosX.Count; j++)
                    {
                        bulletsPosY[j] -= 4;
                    }
                    for (int j = 0; j < boxPosY.Length; j++)
                    {
                        if (!boxPosY[j].Equals(39))
                        {
                            boxPosY[j] += 2;
                        }
                    }

                    //If player score is more than 10, speed up
                    SpeedUp(ref speed, playerScore);
                }
            }


            // Draw box
            void DrawBox(int coordX, int coordY)
            {
                if (coordY < 33 && coordY > 0)
                {
                    Console.SetCursorPosition(coordX, coordY);
                    Console.Write("■■■");
                    Console.SetCursorPosition(coordX, coordY + 1);
                    Console.Write("■■■");
                    Console.SetCursorPosition(coordX, coordY + 2);
                    Console.Write("■■■");
                }
            }

            //Erase box
            void EraseBox(int coordX, int coordY)
            {
                if (coordY < 33 && coordY > 0)
                {
                    Console.SetCursorPosition(coordX, coordY);
                    Console.Write("      ");
                    Console.SetCursorPosition(coordX, coordY + 1);
                    Console.Write("      ");
                    Console.SetCursorPosition(coordX, coordY + 2);
                    Console.Write("      ");
                }
            }

            //Remove Box
            void RemoveBox(ref int boxPosX, ref int boxPosY)
            {
                boxPosX = 0;
                boxPosY = 39;
            }


            //Draw gun
            void DrawGun(int coordX, int coordY)
            {
                Console.SetCursorPosition(coordX, coordY);
                Console.WriteLine("■");
                Console.SetCursorPosition(coordX + 2, coordY);
                Console.WriteLine("■");
                Console.SetCursorPosition(coordX - 2, coordY);
                Console.WriteLine("■");
                Console.SetCursorPosition(coordX, coordY - 1);
                Console.WriteLine("■");
                Console.SetCursorPosition(coordX, coordY + 1);
                Console.WriteLine("■");

            }

            //Erase gun
            void EraseGun(int coordX, int coordY)
            {
                Console.SetCursorPosition(coordX, coordY);
                Console.WriteLine("   ");
                Console.SetCursorPosition(coordX + 2, coordY);
                Console.WriteLine("   ");
                Console.SetCursorPosition(coordX - 2, coordY);
                Console.WriteLine("   ");
                Console.SetCursorPosition(coordX, coordY - 1);
                Console.WriteLine("   ");
                Console.SetCursorPosition(coordX, coordY + 1);
                Console.WriteLine("   ");

            }


            //Draw bullets
            void DrawBullets(int coordX, int coordY)
            {
                Console.SetCursorPosition(coordX, coordY);
                Console.WriteLine("o");
                Console.SetCursorPosition(coordX, coordY - 1);
                Console.WriteLine(" ");
            }
            void EraseBullets(int coordX, int coordY)
            {
                Console.SetCursorPosition(coordX, coordY);
                Console.WriteLine(" ");
                Console.SetCursorPosition(coordX, coordY - 1);
                Console.WriteLine(" ");
            }

         
            //Print current life
            void PrintLife(int life)
            {
                Console.SetCursorPosition(85, 1);
                Console.ForegroundColor = ConsoleColor.Red;
                for (int i = 0; i < life; i++)
                {
                    Console.Write("♥ ");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            //Erase current life
            void ErasePlayerLife(int playerLife)
            {
                Console.SetCursorPosition(85, 1);
                for (int i = 0; i < playerLife; i++)
                {
                    Console.Write("   ");
                }
            }

            //Draw border
            void DrawBorder()
            {
                for (int i = 0; i < 40; i++)
                {
                    Console.SetCursorPosition(39, i);
                    Console.Write("|");
                }
                for (int i = 0; i < 40; i++)
                {
                    Console.SetCursorPosition(80, i);
                    Console.Write("|");
                }
            }

            //Speed up
            void SpeedUp(ref int speed, int playerScore)
            {
                if (playerScore >= 10)
                {
                    speed = 150;
                }
                else if (playerScore >= 25)
                {
                    speed = 100;
                }
                else if (playerScore >= 45)
                {
                    speed = 50;
                }
                else if (playerScore >= 55)
                {
                    speed = 30;
                }
                else if (playerScore >= 70)
                {
                    speed = 15;
                }
            }
        }
    }
}
