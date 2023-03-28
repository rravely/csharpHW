using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _20230328_test_cursor
{
    
    class Program
    {

        static void Main(string[] args)
        {
            int WindowWidth = 60;
            int WindowHeight = 40;
            
            //콘솔 창 크기 설정
            Console.SetWindowSize(WindowWidth, WindowHeight); //가로, 세로
            Console.SetBufferSize(WindowWidth, WindowHeight);

            //박스 위치를 담는 배열
            int[] boxPosX = new int[3];
            int[] boxPosY = new int[3];
            int boxNum = 2;

            //총 위치
            int gunPosX, gunPosY;

            //총알 위치를 담는 리스트
            List<int> bulletsPosX = new List<int>();
            List<int> bulletsPosY = new List<int>();

            Random random = new Random();

            Console.CursorVisible = false;

            ConsoleKeyInfo inputKey;

            int playerScore = 0;
            int life = 3;

            //총 처음 위치
            gunPosX = 15;
            gunPosY = 38;

            while (true)
            {
                //박스 처음 위치
                boxPosY[0] = 0;
                boxPosY[1] = 10;

                //박스 생성할 위치 
                boxPosX[0] = random.Next(30);
                boxPosX[1] = random.Next(30);

                while (true)
                {
                    //키 입력받기
                    if (Console.KeyAvailable) //버퍼에 입력받은 키가 있는지
                    {
                        inputKey = Console.ReadKey(true);
                        if (inputKey.Key.Equals(ConsoleKey.LeftArrow))
                        {
                            //이거 안하면 버퍼를 벗어날 수 있다. 
                            if (gunPosX > 3)
                            {
                                gunPosX -= 2;
                            }
                            else
                            {
                                gunPosX = 2;
                            }

                        }
                        else if (inputKey.Key.Equals(ConsoleKey.RightArrow))
                        {
                            if (gunPosX < 38)
                            {
                                gunPosX += 2;
                            }
                            else
                            {
                                gunPosX = 38;
                            }

                        }
                        else if (inputKey.Key.Equals(ConsoleKey.Escape))
                        {
                            //Console.Clear();
                            break;
                        }
                    }

                    //총알이 맨 위와 부딪힘
                    for (int i = 0; i < bulletsPosX.Count; i++)
                    {
                        if (bulletsPosY[i] < 3) // 2로 하면 버퍼 벗어날 수 있다.
                        {
                            bulletsPosX.RemoveAt(i);
                            bulletsPosY.RemoveAt(i);
                        }
                    }
                    PrintScore(playerScore);

                    DrawBorder();

                    //박스, 총, 총알 그리기
                    DrawBox(boxPosX[0], boxPosY[0]);
                    DrawGun(gunPosX, gunPosY);
                    DrawBox(boxPosX[1], boxPosY[1]);

                    bulletsPosX.Add(gunPosX + 1);
                    bulletsPosY.Add(gunPosY - 2);
                    for (int i = 0; i < bulletsPosX.Count; i++)
                    {
                        DrawBullets(bulletsPosX[i], bulletsPosY[i]);
                    }

                    Thread.Sleep(300);

                    //박스, 총, 총알 지우기
                    EraseBox(boxPosX[0], boxPosY[0]);
                    EraseGun(gunPosX, gunPosY);
                    EraseBox(boxPosX[1], boxPosY[1]);
                    for (int i = 0; i < bulletsPosX.Count; i++)
                    {
                        EraseBullets(bulletsPosX[i], bulletsPosY[i]);
                    }

                    //박스와 총알 부딪힘 확인
                    for (int i = 0; i < bulletsPosX.Count; i++) //총알
                    {
                        for (int j = 0; j < boxNum; j++) //박스 
                        {
                            if (bulletsPosX[i] >= boxPosX[j] && bulletsPosX[i] <= boxPosX[j] + 6) //박스 가로 길이보다 안에 위치하고
                            {
                                if (bulletsPosY[i] <= boxPosY[j] + 3 && bulletsPosY[i] >= boxPosY[j])
                                {
                                    RemoveBox(ref boxPosX[j], ref boxPosY[j]);
                                    bulletsPosX.RemoveAt(i); //i 인덱스에 대한 것 지우기
                                    bulletsPosY.RemoveAt(i); //i 인덱스에 대한 것 지우기
                                    playerScore++;
                                    break;
                                }
                            }
                        }

                    }

                    //박스 바닥과 부딪힘
                    for (int i = 0; i < boxNum; i++)
                    {
                        if (boxPosY[i] >= 32) // 총이 있는 곳과 부딪히면 
                        {
                            life -= 1;
                            RemoveBox(ref boxPosX[i], ref boxPosY[i]);
                        }
                    }

                    if (boxPosY[0].Equals(35) && boxPosY[1].Equals(35))
                    {
                        break;
                    }


                    //박스, 총, 총알 위치 변경
                    for (int j = 0; j < bulletsPosX.Count; j++)
                    {
                        bulletsPosY[j] -= 2;
                    }
                    boxPosY[0] += 2;
                    boxPosY[1] += 2;

                }

                
            }
        }

        // 박스 그리기
        static void DrawBox(int coordX, int coordY)
        {
            if (coordY < 33)
            {
                Console.SetCursorPosition(coordX, coordY);
                Console.Write("■■■");
                Console.SetCursorPosition(coordX, coordY + 1);
                Console.Write("■■■");
                Console.SetCursorPosition(coordX, coordY + 2);
                Console.Write("■■■");
            }
        }

        // 박스 지우기
        static void EraseBox(int coordX, int coordY)
        {
            Console.SetCursorPosition(coordX, coordY);
            Console.Write("      ");
            Console.SetCursorPosition(coordX, coordY + 1);
            Console.Write("      ");
            Console.SetCursorPosition(coordX, coordY + 2);
            Console.Write("      ");
        }

        // 박스 삭제
        static void RemoveBox(ref int boxPosX, ref int boxPosY)
        {
            boxPosX = 0;
            boxPosY = 35;
        }


        //총 그리기
        static void DrawGun(int coordX, int coordY)
        {
            Console.SetCursorPosition(coordX, coordY);
            Console.WriteLine("■");
            Console.SetCursorPosition(coordX + 2, coordY);
            Console.WriteLine("■");
            Console.SetCursorPosition(coordX - 2, coordY);
            Console.WriteLine("■");
            Console.SetCursorPosition(coordX, coordY - 1);
            Console.WriteLine("■");

        }

        //총 지우기
        static void EraseGun(int coordX, int coordY)
        {
            Console.SetCursorPosition(coordX, coordY);
            Console.WriteLine("   ");
            Console.SetCursorPosition(coordX + 2, coordY);
            Console.WriteLine("   ");
            Console.SetCursorPosition(coordX - 2, coordY);
            Console.WriteLine("   ");
            Console.SetCursorPosition(coordX, coordY - 1);
            Console.WriteLine("   ");

        }


        //총알 그리기
        static void DrawBullets(int coordX, int coordY)
        {
            Console.SetCursorPosition(coordX, coordY);
            Console.WriteLine("o");
            Console.SetCursorPosition(coordX, coordY - 1);
            Console.WriteLine(" ");
        }
        static void EraseBullets(int coordX, int coordY)
        {
            Console.SetCursorPosition(coordX, coordY);
            Console.WriteLine(" ");
            Console.SetCursorPosition(coordX, coordY - 1);
            Console.WriteLine(" ");
        }

        // 현재 점수 출력
        static void PrintScore(int playerScore)
        {
            Console.SetCursorPosition(45, 5);
            Console.Write($"Score: {playerScore}");
        }

        static void DrawBorder()
        {
            for (int i = 0; i < 40; i++)
            {
                Console.SetCursorPosition(42, i);
                Console.Write("|");
            }
        }
    }
}
