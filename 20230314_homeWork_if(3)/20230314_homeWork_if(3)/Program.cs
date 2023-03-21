using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20230314_homeWork_if_3_
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * 3-3. 캐릭터 창을 띄우기
            ==============
            || 1. 용사 2. 법사 3. 도적 4. 호구
            =============================

            1번을 누르면 용사가 선택되고 스탯을 입력 받는다.

            ============================
            00의 스탯
            소지금: 10000
            ==========================
            몬스터를 만나면 베팅금액을 걸고 잡몹을 조지면 3배가 된다.
            */

            int playerCharNum, playerHp, playerAtk, playerMoney, gambleMoney;
            string playerChar;
            string[] monName = { "스몰고블린", "빅고블린" };
            int[] monHp = { 10, 20 }; // 고블린 종류에 따른 공격력

            Console.WriteLine("원하는 캐릭터를 선택하세요.");
            Console.WriteLine("유효한 숫자를 입력하지 않을 경우 당신은 자동 ~호구~.");
            Console.WriteLine("======================================");
            Console.WriteLine("| 1. 용사  2. 법사  3. 도적  4. 호구 |");
            Console.WriteLine("======================================");

            playerCharNum = int.Parse(Console.ReadLine());

            if (playerCharNum == 1)
            {
                playerChar = "용사";
            }
            else if (playerCharNum == 2)
            {
                playerChar = "법사";
            }
            else if (playerCharNum == 3)
            {
                playerChar = "도적";
            }
            else
            {
                playerChar = "호구";
            }
            //else
            //{
                //Console.WriteLine("잘못 입력했습니다. 다시 입력해주세요.");
            //}

            Console.WriteLine($"당신은 {playerChar}입니다.");

            Console.WriteLine($"{playerChar}의 스탯을 입력해주세요.");
            Console.Write($"{playerChar}의 Hp: ");
            playerHp = int.Parse(Console.ReadLine());
            Console.Write($"{playerChar}의 Atk: ");
            playerAtk = int.Parse(Console.ReadLine()); 
            Console.Write($"{playerChar}의 Money: ");
            playerMoney = int.Parse(Console.ReadLine());

            //플레이어 스탯창 출력
            printPlayerInfo(playerChar, playerHp, playerAtk, playerMoney);

            //랜덤으로 고블린 만나기
            Random rand = new Random();
            int monNum = rand.Next(0, 2);

            Console.WriteLine($"{playerChar}가 전장을 누비던 도중 {monName[monNum]}을 만났다.");
            Console.ReadLine();

            while (true)
            {
                //배팅
                Console.WriteLine("당신의 소지금 중 일부(혹은 전체)를 배팅할 수 있습니다.");
                Console.Write("배팅 금액: ");
                gambleMoney = int.Parse(Console.ReadLine());

                if (gambleMoney > playerMoney) //배팅 금액이 소지금보다 큰 경우
                {
                    Console.Clear();
                    Console.WriteLine("소지금을 초과하여 배팅할 수 없습니다.");
                    //플레이어 스탯창 출력
                    printPlayerInfo(playerChar, playerHp, playerAtk, playerMoney);

                }
                else if (gambleMoney < 0) //배팅 금액이 음수인 경우
                {
                    Console.Clear();
                    Console.WriteLine("음수는 입력할 수 없습니다.");
                    //플레이어 스탯창 출력
                    printPlayerInfo(playerChar, playerHp, playerAtk, playerMoney);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("{0}을 배팅하셨습니다.", gambleMoney);
                    //플레이어 스탯창 출력
                    printPlayerInfo(playerChar, playerHp, playerAtk, playerMoney);


                    //전투
                    Console.WriteLine($"{playerChar}은 몬스터에게 {playerAtk}만큼의 피해를 입혔습니다.");
                    Console.WriteLine();

                    if (playerAtk >= monHp[monNum])
                    {
                        Console.WriteLine("몬스터 처치 성공!");
                        Console.WriteLine("배팅 금액의 3배 획득!");
                        playerMoney += gambleMoney * 2;
                    }
                    else
                    {
                        Console.WriteLine("몬스터 처치 실패");
                        Console.WriteLine("배팅 금액을 돌려받을 수 없습니다.");
                        playerMoney -= gambleMoney;
                    }
                    Console.WriteLine($"{playerChar}의 소지금이 {playerMoney}가 되었습니다.");
                    Console.WriteLine();
                    //플레이어 스탯창 출력
                    printPlayerInfo(playerChar, playerHp, playerAtk, playerMoney);

                    break;
                }
                
            }  
        }

        static void printPlayerInfo(string playerChar, int playerHp, int playerAtk, int playerMoney)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("========================");
            Console.WriteLine($"{playerChar}의 스탯");
            Console.WriteLine("Hp: {0}", playerHp);
            Console.WriteLine("공격: {0}", playerAtk);
            Console.WriteLine("소지금: {0}", playerMoney);
            Console.WriteLine("========================");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
