using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _20230320_homeWork_bingo
{
    class Program
    {
        //빙고판 형성
        //public int[,] computerBing;
        //public int[,] playerBingo;

        static void Main(string[] args)
        {
            int bingoSize, bingoCount = 0;
            int bingoNum;

            Console.WriteLine("~ 재미 없어 빙고 ~\n");

            //빙고판 크기 입력받기.
            bingoSize = InputBingoSize();

            //빙고판 형성
            int[,] computerBingo = new int[bingoSize, bingoSize];
            int[,] playerBingo = new int[bingoSize, bingoSize];

            computerBingo = CreateBingo(Shuffle_1D_Array(bingoSize), bingoSize);
            Thread.Sleep(1);
            playerBingo = CreateBingo(Shuffle_1D_Array(bingoSize), bingoSize);

            Console.WriteLine("플레이어의 빙고는 다음과 같습니다. \n");

            //선택한 빙고 숫자 확인 배열
            int[] checkBingoNum = new int[bingoSize * bingoSize];
            

            //빙고 게임 시작 !
            while (true)
            {
                //종료 조건
                if (bingoCount == 25)
                {
                    break;
                }
                if (CheckBingoLine(playerBingo) == 4 || CheckBingoLine(computerBingo) == 4)
                {
                    if (CheckBingoLine(playerBingo) == 4)
                    {
                        Console.WriteLine("플레이어 승리!");
                    }
                    else
                    {
                        Console.WriteLine("플레이어 패배!");
                    }
                    break;
                }

                if (bingoCount % 2 == 0 ) //플레이어 선택 차례면
                {
                    PrintBingo(playerBingo);
                    Console.Write("\n빙고 숫자를 골라주세요. ");

                    //빙고 숫자 받기
                    while (true)
                    {
                        bingoNum = int.Parse(Console.ReadLine());

                        //중복 확인
                        bool check = Array.Exists(checkBingoNum, x => x == bingoNum);
                        if (check)
                        {
                            Console.Write("이전에 선택한 수 입니다. 다른 수를 골라주세요. ");
                        }
                        else
                        {
                            checkBingoNum[bingoCount] = bingoNum;
                            bingoCount++;
                            break;
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("플레이어가 선택한 수는 {0}입니다.\n", bingoNum);
                }
                else //컴퓨터 선택 차례면
                {
                    Random rand = new Random();

                    while (true) {
                        bingoNum = rand.Next(bingoSize * bingoSize);
                        //중복 확인
                        bool check = Array.Exists(checkBingoNum, x => x == bingoNum);
                        if (!check)
                        {
                            checkBingoNum[bingoCount] = bingoNum;
                            bingoCount++;
                            break;
                        }
                    }

                    Thread.Sleep(2000);
                    Console.WriteLine();
                    Console.WriteLine("컴퓨터가 선택한 수는 {0}입니다.\n", bingoNum);

                }

                CheckBingoSelect(computerBingo, bingoNum);
                CheckBingoSelect(playerBingo, bingoNum);

                //PrintBingo(computerBingo);
                Console.WriteLine();
                PrintBingo(playerBingo);
                Console.ReadLine();
                Console.Clear();
                
            }

        }

        //빙고판 크기 입력받기
        static int InputBingoSize()
        {
            int bingoSize;

            while (true)
            {
                Console.Write("빙고 크기를 입력해주세요. ");
                bingoSize = int.Parse(Console.ReadLine());

                if (bingoSize >= 10 || bingoSize < 5)
                {
                    Console.WriteLine("5와 9사이의 정수를 입력해주세요.");
                }
                else
                {
                    return bingoSize;
                }
            }
        }

        //1차원 배열을 받아 빙고판 만들기
        static int[,] CreateBingo(int[] bingo1DArray, int bingoSize)
        {
            int[,] bingo2DArray = new int[bingoSize, bingoSize];

            for (int i = 0; i < bingoSize; i++)
            {
                for (int j = 0; j < bingoSize; j++)
                {
                    bingo2DArray[i, j] = bingo1DArray[i * bingoSize + j];
                }
            }
            return bingo2DArray;
        }

        //1차원 배열 섞기
        static int[] Shuffle_1D_Array(int bingoSize)
        {
            Random random = new Random();

            int dest, sour, temp;
            int[] bingo1DArray = new int[bingoSize * bingoSize];

            //셔플할 1차원 배열 만들기
            for (int i = 0; i < bingoSize * bingoSize; i++)
            {
                bingo1DArray[i] = i + 1;
            }

            //셔플
            for (int  i = 0; i < 1000; i++)
            {
                dest = random.Next(bingo1DArray.Length);
                sour = random.Next(bingo1DArray.Length);

                temp = bingo1DArray[dest];
                bingo1DArray[dest] = bingo1DArray[sour];
                bingo1DArray[sour] = temp;
            }
            return bingo1DArray;
        }

        //빙고 출력
        static void PrintBingo(int[,] bingo)
        {
            Console.WriteLine("--------------------------");
            for (int i = 0; i < bingo.GetLength(0); i++)
            {
                for (int j = 0; j < bingo.GetLength(1); j++)
                {
                    if (bingo[i, j] < 10 && bingo[i, j] > 0)
                    {
                        Console.Write("|  {0} ", bingo[i, j]);
                    }
                    else if (bingo[i, j] >= 10)
                    {
                        Console.Write("| {0} ", bingo[i, j]);
                    }
                    else
                    {
                        Console.Write("| ★ ");
                    }
                }
                Console.Write("|\n--------------------------\n");
            }
        }

        //선택한 수를 빙고판에서 0으로 만드는 메서드
        static void CheckBingoSelect(int[,] bingo, int bingoNum)
        {
            for (int i = 0; i < bingo.GetLength(0); i++)
            {
                for (int j = 0; j < bingo.GetLength(1); j++)
                {
                    if (bingo[i, j] == bingoNum)
                    {
                        bingo[i, j] = 0;
                    }
                }
            }
        }

        //빙고 라인 수 구하기
        static int CheckBingoLine(int[,] bingo)
        {
            int numBingo = 0, sum = 0;

            //가로줄 빙고 수세기
            for (int i = 0; i < bingo.GetLength(0); i++)
            {
                for (int j = 0; j < bingo.GetLength(0); j++)
                {
                    sum += bingo[i, j];
                }
                if (sum == 0)
                {
                    numBingo++;
                }

                sum = 0;
            }

            //세로줄 빙고 수세기
            for (int i = 0; i < bingo.GetLength(0); i++)
            {
                for (int j = 0; j < bingo.GetLength(0); j++)
                {
                    sum += bingo[j, i];
                }
                if (sum == 0)
                {
                    numBingo++;
                }

                sum = 0;
            }

            //왼쪽에서 오른쪽 아래로
            for (int i = 0; i < bingo.GetLength(0); i++)
            {
                sum += bingo[i, i];
            }
            if (sum == 0)
            {
                numBingo++;
            }

            //오른쪽에서 왼쪽 아래로
            for (int i = 0; i < bingo.GetLength(0); i++)
            {
                sum += bingo[bingo.GetLength(0)-i-1, i];
            }
            if (sum == 0)
            {
                numBingo++;
            }

            return numBingo;
        }

        //컴퓨터가 빙고에서 숫자 선택하기
        static void ComputerBingoSelect(int[,] bingo)
        {
            
        }
    }
}
