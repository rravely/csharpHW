using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20230315_homeWork_RSP_2_
{
    class Program
    {
        static void Main(string[] args)
        {
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

                while (true)
                {
                    if (gambleMoney > userMoney)
                    {
                        Console.WriteLine("베팅 금액이 소지금보다 큽니다. 다시 입력해주세요.");
                        Console.Write($"베팅 금액을 입력해주세요. (당신의 소지금: {userMoney}) ");
                        gambleMoney = int.Parse(Console.ReadLine());
                    }
                    else
                    {
                        break;
                    }
                }

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
                    switch (userAns)
                    {
                        case 1:
                            switch (computerAns)
                            {
                                case 2:
                                    Console.WriteLine("컴퓨터의 승리!");
                                    Console.WriteLine("베팅 금액의 2배를 잃습니다.");
                                    userMoney -= gambleMoney * 2;
                                    break;
                                case 3:
                                    Console.WriteLine("당신의 승리!");
                                    Console.WriteLine("베팅 금액의 2배를 얻습니다.");
                                    userMoney += gambleMoney * 2;
                                    break;
                            }
                            break;
                        case 2:
                            switch (computerAns)
                            {
                                case 3:
                                    Console.WriteLine("컴퓨터의 승리!");
                                    Console.WriteLine("베팅 금액의 2배를 잃습니다.");
                                    userMoney -= gambleMoney * 2;
                                    break;
                                case 1:
                                    Console.WriteLine("당신의 승리!");
                                    Console.WriteLine("베팅 금액의 2배를 얻습니다.");
                                    userMoney += gambleMoney * 2;
                                    break;
                            }
                            break;
                        case 3:
                            switch (computerAns)
                            {
                                case 1:
                                    Console.WriteLine("컴퓨터의 승리!");
                                    Console.WriteLine("베팅 금액의 2배를 잃습니다.");
                                    userMoney -= gambleMoney * 2;
                                    break;
                                case 2:
                                    Console.WriteLine("당신의 승리!");
                                    Console.WriteLine("베팅 금액의 2배를 얻습니다.");
                                    userMoney += gambleMoney * 2;
                                    break;
                            }
                            break;
                        default:
                            Console.WriteLine("유효한 숫자를 입력해주세요. (1, 2, 3)");
                            turn--;
                            break;
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
