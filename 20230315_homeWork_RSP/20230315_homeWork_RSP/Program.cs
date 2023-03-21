using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20230315_homeWork_RSP
{
    class Program
    {
        static void Main(string[] args)
        {
            /*2. 가위바위보 게임(if하나 swtich 하나)
             유저는 입력을 받는다. (가위, 바위, 보 중 하나)
             컴퓨터는 가위바위보 중에 랜덤하게 하나 나옴
             유저가 가위를 내고 컴퓨터가 바위를 냈다면 패배.
             유저는 배팅 가능(단, 내가 베팅한 경우에 승리하면 베팅금의 2배 먹고, 지면 베팅 금액의 2배 잃음, 비기면 베팅 금액만 잃음)
             종료 조건: 소지금이 없는 경우나 10판 초과되면 게임 종료.
             */

            int userAns, computerAns, userMoney = 10000, gambleMoney, turn = 0;

            Console.WriteLine("* 가위바위보 게임 *");
            Console.WriteLine($"당신의 소지금은 현재: {userMoney}");
            Console.WriteLine();


            while (turn < 10)
            {
                if (userMoney <= 0)
                {
                    Console.WriteLine("가지고 있는 돈을 모두 잃었습니다.");
                    Console.WriteLine("게임 종료.");
                    break;
                }

                Console.Write($"베팅 금액을 입력해주세요. (당신의 소지금: {userMoney}) ");
                gambleMoney = int.Parse(Console.ReadLine());

                Console.WriteLine("원하는 번호를 선택해주세요.");
                Console.WriteLine("1. 가위  2. 바위  3. 보");

                userAns = int.Parse(Console.ReadLine());

                Random randomRSP = new Random();
                computerAns = randomRSP.Next(1, 4);

                if (userAns == computerAns)
                {
                    Console.WriteLine("비겼습니다. 베팅 금액을 잃습니다.");
                    userMoney -= gambleMoney;
                }
                else
                {
                    if (userAns == 1)
                    {
                        if (computerAns == 2)
                        {
                            Console.WriteLine("컴퓨터의 승리!");
                            Console.WriteLine("베팅 금액의 2배를 잃습니다.");
                            userMoney -= gambleMoney * 2;
                        }
                        else if (computerAns == 3)
                        {
                            Console.WriteLine("당신의 승리!");
                            Console.WriteLine("베팅 금액의 2배를 얻습니다.");
                            userMoney += gambleMoney * 2;

                        }
                    }
                    else if (userAns == 2)
                    {
                        if (computerAns == 3)
                        {
                            Console.WriteLine("컴퓨터의 승리!");
                            Console.WriteLine("베팅 금액의 2배를 잃습니다.");
                            userMoney -= gambleMoney * 2;
                        }
                        else if (computerAns == 1)
                        {
                            Console.WriteLine("당신의 승리!");
                            Console.WriteLine("베팅 금액의 2배를 얻습니다.");
                            userMoney += gambleMoney * 2;

                        }
                    }
                    else
                    {
                        if (computerAns == 1)
                        {
                            Console.WriteLine("컴퓨터의 승리!");
                            Console.WriteLine("베팅 금액의 2배를 잃습니다.");
                            userMoney -= gambleMoney * 2;
                        }
                        else if (computerAns == 2)
                        {
                            Console.WriteLine("당신의 승리!");
                            Console.WriteLine("베팅 금액의 2배를 얻습니다.");
                            userMoney += gambleMoney * 2;

                        }
                    }

                }

                turn++;
                Console.WriteLine($"남은 판 수: {10 - turn}");
                Console.WriteLine();
            }

            if (turn == 9)
            {
                Console.WriteLine("10판을 모두 진행했습니다.");
                Console.WriteLine($"당신의 소지금: {userMoney}");
            }

        }
    }
}
