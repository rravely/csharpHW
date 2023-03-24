using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enermy;

namespace _20230323_homeWork_textRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            //시작
            Console.WriteLine("~! 몬스터 잡기 !~");


            //플레이어 이름 받기
            string playerName = InputPlayerName();


            //플레이어 생성
            Enermy.Player player = new Player(playerName);


            //몬스터 10마리 생성
            int monsterNum = 6;

            Enermy.Monster[] monsters = new Monster[monsterNum];

            monsters[0] = new Monster("몬스터1", 50, 100, 500);
            monsters[1] = new Monster("몬스터2", 70, 100, 700);
            monsters[2] = new Monster("몬스터2", 100, 100, 1000);
            monsters[3] = new Monster("몬스터2", 120, 100, 1200);
            monsters[4] = new Monster("몬스터2", 150, 100, 1500);
            monsters[5] = new Monster("몬스터2", 200, 100, 2000);


            //플레이어 스탯 출력
            Console.WriteLine($"\n{player.Name}님의 상태\n");
            PrintPlayerState(player);
            Console.WriteLine("\n 사냥을 시작합니다 !");
            ConsoleReadAndClear();


            //몬스터 생성할 난수 생성
            Random random = new Random();


            //콘솔 키 입력받기
            ConsoleKeyInfo cki;

            //전투
            while (true)
            {
                //플레이어 스탯 출력
                PrintPlayerState(player);
                ConsoleReadAndClear();

                //몬스터 랜덤 생성
                int monSelectNum = random.Next(monsterNum);
                Enermy.Monster mon = new Monster(monsters[monSelectNum].Name, monsters[monSelectNum].HP, monsters[monSelectNum].MP, monsters[monSelectNum].Money);

                //몬스터 지나가기
                mon.Walk();

                //사냥 여부 묻기
                Console.WriteLine($"\n{mon.Name}을 사냥하시겠습니까? (Yes: 아무거나, No: ↓)");
                cki = Console.ReadKey();
                
                if (cki.Key.Equals(ConsoleKey.DownArrow))
                {
                    Console.WriteLine();
                    player.Walk();
                    ConsoleReadAndClear();
                    continue;
                }

                //공격 턴 설정
                int turn = 0;

                //사냥 전 상태 보여주기
                PrintPlayerMonsterState(player, mon);
                Console.Write("전투를 시작하려면 엔터를 눌러주세요! ");
                ConsoleReadAndClear();


                //싸우기
                while (true)
                {
                    if ((turn % 2).Equals(0)) //플레이어 공격턴이면
                    {
                        if (player.HP <= 0) //플레이어 피가 0이면 
                        {
                            player.Die();
                            break;
                        }
                        else if (player.HP <= 20 && player.HpPotionNum > 0) //플레이어의 HP가 20이하고 HP 포션이 있으면
                        {
                            Enermy.Player.DrinkPotion(player, "HP");
                            ConsoleReadAndClear();
                        }
                        else if (player.MP <= 20 && player.MpPotionNum > 0) //플레이어의 MP가 20이하고 MP 포션이 있으면
                        {
                            Enermy.Player.DrinkPotion(player, "MP");
                            ConsoleReadAndClear();
                        }

                        Enermy.Enermy.defaultAtk(player, mon, turn);
                        PrintPlayerMonsterState(player, mon);

                    }
                    else //몬스터 공격 턴이면
                    {
                        if (mon.HP <= 0) // 몬스터 피가 0이면
                        {
                            mon.Die();
                            Console.WriteLine("\n사냥 성공 !\n");
                            player.Money += mon.Money;

                            PrintPlayerState(player);
                            Console.Write("사냥을 계속하려면 엔터를 눌러주세요. ");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                        Enermy.Enermy.defaultAtk(player, mon, turn);
                        PrintPlayerMonsterState(player, mon);
                        
                    }
                    turn++;
                    ConsoleReadAndClear();
                }


                //플레이어의 피가 0이되면 종료
                if (player.HP.Equals(0))
                {
                    Console.WriteLine("플레이어의 HP가 0이 되어 게임을 종료합니다.");
                    break;
                }

                
            }


        }

        //플레이어의 이름 받기
        static public string InputPlayerName()
        {
            string playerName;

            Console.Write("플레이어의 이름을 입력해주세요. ");
            playerName = Console.ReadLine();
            Console.WriteLine();

            return playerName;
        }

        static public void PrintPlayerMonsterState(Player player, Monster monster)
        {
            Console.WriteLine("\n==========================");
            Console.WriteLine($"{player.Name}의 상태");
            Console.WriteLine($"HP: {player.HP}");
            Console.WriteLine($"MP: {player.MP}");
            Console.WriteLine($"HP 포션 개수: {player.HpPotionNum}");
            Console.WriteLine($"MP 포션 개수: {player.MpPotionNum}");
            Console.WriteLine($"소지금: {player.Money}");
            Console.WriteLine("============================");
            Console.WriteLine($"{monster.Name}의 상태");
            if (monster.HP < 0)
            {
                Console.WriteLine($"HP: 0");
            }
            else {
                Console.WriteLine($"HP: {monster.HP}");
            }
            Console.WriteLine($"MP: {monster.MP}");
            Console.WriteLine($"돈: {monster.Money}");
            Console.WriteLine("============================\n");

        }

        static public void PrintPlayerState(Player player)
        {
            Console.WriteLine("\n===========================");
            Console.WriteLine($"{player.Name}의 상태");
            Console.WriteLine($"HP: {player.HP}");
            Console.WriteLine($"MP: {player.MP}");
            Console.WriteLine($"HP 포션 개수: {player.HpPotionNum}");
            Console.WriteLine($"MP 포션 개수: {player.MpPotionNum}");
            Console.WriteLine($"소지금: {player.Money}");
            Console.WriteLine("===========================\n");
        }

        static public void ConsoleReadAndClear()
        {
            Console.ReadLine();
            Console.Clear();
        }
    }
}
