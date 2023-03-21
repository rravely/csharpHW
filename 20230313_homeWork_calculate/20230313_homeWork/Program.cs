using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20230313_homeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. 두 수를 입력 받고 결과 출력");
            int inputNum1, inputNum2, result;
            Console.Write("첫번째 수를 입력해주세요. ");
            inputNum1 = int.Parse(Console.ReadLine());
            Console.Write("두번째 수를 입력해주세요. ");
            inputNum2 = int.Parse(Console.ReadLine());
            result = inputNum1 + inputNum2;
            Console.WriteLine($"{inputNum1} + {inputNum2} = {result}");
            result = inputNum1 - inputNum2;
            Console.WriteLine($"{inputNum1} - {inputNum2} = {result}");
            result = inputNum1 * inputNum2;
            Console.WriteLine($"{inputNum1} * {inputNum2} = {result}");
            result = inputNum1 / inputNum2;
            Console.WriteLine($"{inputNum1} / {inputNum2} = {result}");
            result = inputNum1 % inputNum2;
            Console.WriteLine($"{inputNum1} % {inputNum2} = {result}");
            Console.ReadLine();


            //2. 플레이어와 몬스터
            //플레이어는 다음과 같은 스탯이 존재
            //2-1. HP, Input(atk)
            //몬스터도 위와 같은 스탯이 존재
            //플레이어가 공격하면 몬스터의 HP 감소
            /*
             * 플레이어가 전장을 누비던 도중 000을 만났다
             * 플레이어 공격!!!! : 10
             * 고블린이 플레이어 공격에 데미지 먹음
             * 몬스터의 남은 체력: ???
             */
            Console.WriteLine("2. 플레이어와 몬스터");

            string playerName;
            string[] monName = { "스몰고블린", "빅고블린" };
            int playerHP = 100, playerMP = 12, monHP = 100, playerAtkNum, turn = 0;
            int[,] playerAtk = new int[,] { { 10, 0 }, { 20, 2 }, { 30, 5 } }; //플레이어 HP와 MP 2d배열
            int[] monAtk = { 10, 20 }; // 고블린 종류에 따른 공격력

            Console.Write("플레이어 이름: ");
            playerName = Console.ReadLine();

            //랜덤으로 고블린 만나기
            Random rand = new Random();
            int i = rand.Next(1, 3);

            Console.WriteLine($"{playerName}가 전장을 누비던 도중 {monName[i]}을 만났다.");
            Console.ReadLine();

            for (; ; )
            {
                if (turn % 2 == 0) //짝수턴이면 플레이어 공격
                {
                    Console.WriteLine("플레이어 공격: 1. 10  2. 20  3. 30");
                    Console.Write("공격을 선택하세요. ");
                    playerAtkNum = int.Parse(Console.ReadLine());

                    Console.WriteLine($"{monName[i]}이 플레이어 공격에 대미지를 입었다.");
                    monHP -= playerAtk[playerAtkNum - 1, 0];
                    playerMP -= playerAtk[playerAtkNum - 1, 1];
                    Console.WriteLine($"{monName[i]}의 남은 HP: {monHP}");
                    Console.WriteLine("플레이어의 남은 MP: {0}", playerMP);
                }
                else //홀수턴이면 몬스터 공격
                {
                    Console.WriteLine($"화가난 {monName[i]}이 플레이어를 공격했다.");
                    playerHP -= monAtk[i];
                    Console.WriteLine("플레이어의 남은 HP: {0}", playerHP);
                }

                turn += 1;
                Console.WriteLine();

                if (playerHP <= 0 || playerMP < 0) //플레이어는 HP가 없거나 MP가 없으면 패배
                {
                    Console.WriteLine("패배");
                    break;
                }
                else if (monHP <= 0)
                {
                    Console.WriteLine("승리");
                    break;
                }
                Console.WriteLine("============================");
            }
            
            
        }
    }
}
