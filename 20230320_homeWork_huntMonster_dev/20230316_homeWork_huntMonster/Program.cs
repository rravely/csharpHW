using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20230316_homeWork_huntMonster
{
    class Program
    {
        enum PLAYERSTATE { FIGHT, RUN };
        //enum PLAYERSKILLATK { A = 10, B = 15, C = 20 }; //플레이어 스킬 공격력
        //enum PLAYERSKILLMP { A = 20, B = 30, C = 40 }; //플레이어 스킬 사용 시 소모 MP
        //enum MONSKILLATK { A = 5, B = 35, C = 10 }; //몬스터 스킬 공격력
        //enum MONSKILLMP { A = 10, B = 70, C = 20 }; //몬스터 스킬 사용 시 소모 MP

        public struct Player {
            public int hp;
            public int mp;
            public int atk;
            public int def;
            public int hpPotionNum;
            public int mpPotionNum;
            public int money;
        }

        public struct Monster
        {
            public int hp;
            public int mp;
            public int atk;
            public int def;
            public int money;
        }

        public struct Skill
        {
            public string Name;
            public int Atk;
            public int Mp;

            public Skill(string name, int atk, int mp)
            {
                Name = name;
                Atk = atk;
                Mp = mp;
            }

        }

        static void Main(string[] args)
        {
            //int playerHp = 100, playerMp = 100, playerAtk = 5, playerDef = 30, playerHpPotionNum = 5, playerMpPotionNum = 5, playerMoney = 0;
            //int monHp = 100, monMp = 100, monAtk = 5, monDef = 30, monMoney = 1000;
            int playerInputAtkNum, monAtkNum, criProb, monAvoidProb, playerAvoidProb, criAtk, playerState = 0, turn = 0;
            char playerRunAns, continueHuntAns;

            //player 객체 생성
            Player player = new Player();
            player.hp = 100;
            player.mp = 100;
            player.atk = 5;
            player.def = 30;
            player.hpPotionNum = 5;
            player.mpPotionNum = 5;
            player.money = 0;

            //monster 객체 생성
            Monster mon = new Monster();
            mon.hp = 100;
            mon.mp = 100;
            mon.atk = 5;
            mon.def = 30; 
            mon.money = 1000;

            //player skill
            Skill playerSkillA = new Skill("휘이잉", 10, 20);
            Skill playerSkillB = new Skill("뾰로롱", 15, 30);
            Skill playerSkillC = new Skill("붕붕", 20, 40);

            //monster skill
            Skill monSkillA = new Skill("던지기", 5, 10);
            Skill monSkillB = new Skill("휘두르기", 35, 70);
            Skill monSkillC = new Skill("때리기", 5, 10);


            Random random = new Random();

            //START
            Console.WriteLine("** MONSTER HUNT **");
            Console.WriteLine("You meet a monster!");
            Console.WriteLine();

            while (true)
            {
                // 종료 조건
                if (player.hp <= 0) //플레이어 패배
                {
                    Console.WriteLine("Fail to hunt Monsters. GAME OVER");
                    break;
                }
                if (mon.hp <= 0) //몬스터 피 0
                {
                    Console.WriteLine("몬스터 사냥에 성공하였습니다.");
                    player.money += mon.money;
                    Console.WriteLine("플레이어의 소지금: {0}", player.money);

                    Console.WriteLine("\n사냥을 계속 하시겠습니까? ");
                    continueHuntAns = Convert.ToChar(Console.ReadLine());

                    if (continueHuntAns == 'Y')
                    {
                        Console.WriteLine("!! 새로운 몬스터가 나타났다!!\n");

                        mon.hp = 100;
                        mon.mp = 100;
                        mon.atk = 5;
                        mon.def = 30;
                        mon.money = 1000;
                        turn = 0;
                    }
                    else
                    {
                        Console.WriteLine("사냥을 종료합니다.");
                        break;
                    }
                    
                }
                if (playerState == (int)PLAYERSTATE.RUN)
                {
                    Console.WriteLine("플레이어는 도망갔습니다. 게임 종료.");
                    break;
                }


                //도망갈지 물어보기
                if (player.hp <= 10)
                {
                    Console.WriteLine("플레이어의 HP가 100이하로 떨어졌습니다. ");
                    Console.WriteLine("도망가시겠습니까? (Y/N)");
                    playerRunAns = Convert.ToChar(Console.ReadLine());

                    if (playerRunAns == 'Y')
                    {
                        playerState = 1;
                        continue;
                    }
                }

                //Hp 포션 먹을 조건
                if (player.hp <= 20 && player.hpPotionNum > 0)
                {
                    PlayerDrinkPotion("HP", ref player.hp, ref player.hpPotionNum);
                }

                //Mp 포션 먹을 조건
                if (player.mp <= 20 && player.mpPotionNum > 0)
                {
                    PlayerDrinkPotion("MP", ref player.mp, ref player.mpPotionNum);
                }

                //공격 동작
                if (turn % 2 == 0) //짝수 턴 플레이어 공격
                {
                    Console.WriteLine("~ 플레이어 공격턴 ~");
                    Console.WriteLine("어떤 공격을 하시겠습니까?");
                    Console.WriteLine("0. 기본공격  1. 휘이잉  2. 뾰로롱  3. 붕붕");
                    playerInputAtkNum = int.Parse(Console.ReadLine()); //공격 입력 받기

                    //공격 크리티컬이 터질 확률
                    criProb = random.Next(100);
                    //몬스터 회피, 대미지 무효화가 터질 확률
                    monAvoidProb = random.Next(100);

                    switch (playerInputAtkNum)
                    {
                        case 0: //기본공격인 경우
                            if (criProb >= 40)//크리가 터졌을 때
                            {
                                criAtk = random.Next(10, 1000);
                                Console.WriteLine("몬스터에게 크리티컬 피해를 입혔습니다. 몬스터에게 입힌 피해량: {0}", player.atk + criAtk);

                                PlayerAtkMon(ref mon.def, ref player.atk, criAtk, ref mon.hp);

                            }
                            else //크리 안터지면
                            {
                                Console.WriteLine("몬스터에게 기본 피해를 입혔습니다. 몬스터에게 입힌 피해량: {0}", player.atk);

                                PlayerAtkMon(ref mon.def, ref player.atk, 0, ref mon.hp); //크리 안터지면 criAtk 변수 0
                            }
                            break;
                        case 1: //첫 스킬 ~
                            PlayerUseSkill(ref player.mp, ref mon.def, ref mon.hp, monAvoidProb, ref turn, playerSkillA);
                            break;
                        case 2:
                            PlayerUseSkill(ref player.mp, ref mon.def, ref mon.hp, monAvoidProb, ref turn, playerSkillB);
                            break;
                        case 3:
                            PlayerUseSkill(ref player.mp, ref mon.def, ref mon.hp, monAvoidProb, ref turn, playerSkillC);
                            break;
                    }
                    
                }
                else //홀수 턴 몬스터 공격
                {
                    Console.WriteLine("~ 몬스터의 공격턴 ~");

                    //몬스터 공격 선택
                    monAtkNum = random.Next(4);

                    //플레이어가 회피하거나 데미지를 무효화할 확률
                    playerAvoidProb = random.Next(100);

                    switch (monAtkNum) 
                    {
                        case 0: // 기본 공격
                            if (player.def >= mon.atk)
                            {
                                player.def -= mon.atk;
                                Console.WriteLine($"플레이어의 방어력이 {mon.atk}만큼 깎였습니다.");

                            }
                            else
                            {
                                player.def = 0;
                                player.hp -= (mon.atk - player.def);
                                Console.WriteLine($"플레이어의 방어력이 {player.def}만큼 깎이고 {mon.atk - player.def}만큼의 피해를 입습니다.");
                            }
                            break;
                        case 1:
                            MonsterUseSkill(ref mon.mp, ref player.def, ref player.hp, playerAvoidProb, ref turn, monSkillA);
                            break;
                        case 2:
                            MonsterUseSkill(ref mon.mp, ref player.def, ref player.hp, playerAvoidProb, ref turn, monSkillB);
                            break;
                        case 3:
                            MonsterUseSkill(ref mon.mp, ref player.def, ref player.hp, playerAvoidProb, ref turn, monSkillC);
                            break;
                    }
                }

                //플레이어와 몬스터 스탯 출력
                PrintPlayerInfo(player.hp, player.mp, player.atk, player.def, mon.hp, mon.mp, mon.atk, mon.def);
                Console.ReadLine();
                Console.Clear();

                //턴 증가
                turn++;
                Console.WriteLine();
 
            }
        }
        static void PrintPlayerInfo(int playerHp, int playerMp, int playerAtk, int playerDef, int monHp, int monMp, int monAtk, int monDef)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("==============================");
            Console.WriteLine("     PLAYER         MONSTER");
            Console.WriteLine("HP : {0}          HP : {1}", playerHp, monHp);
            Console.WriteLine("MP : {0}          MP : {1}", playerMp, monMp);
            Console.WriteLine("ATK: {0}           ATK: {1}", playerAtk, monAtk);
            Console.WriteLine("DEF: {0}          DEF: {1}", playerDef, monDef);
            Console.WriteLine("==============================");
            Console.WriteLine();//▮▮▯▯
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void PlayerDrinkPotion(string potionName, ref int playerP, ref int playerPotionNum )
        {
            char playerPotionAns;

            Console.WriteLine("플레이어의 {0}가 20 이하로 떨어졌습니다. 현재 {0}: {1}", potionName, playerP);
            Console.WriteLine("포션을 드시겠습니까? (Y/N)");
            playerPotionAns = Convert.ToChar(Console.ReadLine());

            switch (playerPotionAns)
            {
                case 'Y':
                    playerPotionNum--; //포션 개수 감소
                    playerP += 50; //포션 하나당 Mp 50 증가
                    Console.WriteLine("포션을 먹었습니다.");
                    Console.WriteLine("플레이어의 남은 {2}: {0}, 남은 {2} 포션: {1}", playerP, playerPotionNum, potionName);
                    break;
                default:
                    Console.WriteLine("포션을 먹지 않습니다.");
                    break;
            }
            Console.WriteLine();
        }

        static void PlayerUseSkill(ref int playerMp, ref int monDef, ref int monHp, int monAvoidProb, ref int turn, Skill playerSkill)
        {
            if (playerMp < playerSkill.Mp)
            {
                Console.WriteLine("MP가 부족하여 스킬을 사용할 수 없습니다. ");
                turn--;

                //여기서 마나 포션 먹을 수 있게 구현해야함,,
            }
            else
            {
                Console.WriteLine("플레이어가 '{0}' 스킬을 사용했습니다. ", playerSkill.Name);
                playerMp -= playerSkill.Mp;

                if (monAvoidProb < 20)
                {
                    Console.WriteLine("몬스터가 공격을 회피했습니다.");
                }
                else if (monAvoidProb < 40)
                {
                    Console.WriteLine("몬스터가 데미지를 무효화 했습니다.");
                }
                else
                {
                    Console.WriteLine("몬스터에게 입힌 피해량: {0}", playerSkill.Atk);

                    if (monDef >= playerSkill.Atk)
                    {
                        monDef -= playerSkill.Atk;
                        Console.WriteLine($"몬스터가 플레이어로부터 피해를 입어 방어력이 {playerSkill.Atk}만큼 깎였습니다.");

                    }
                    else
                    {
                        if (monDef == 0)
                        {
                            monHp -= playerSkill.Atk;
                            Console.WriteLine($"몬스터가 플레이어로부터 피해를 입어 {playerSkill.Atk - monDef}만큼의 피해를 입습니다.");
                        }
                        else
                        {
                            monHp -= (playerSkill.Atk - monDef);
                            Console.WriteLine($"몬스터가 플레이어로부터 피해를 입어 방어력이 {monDef}만큼 깎이고 {playerSkill.Atk - monDef}만큼의 피해를 입습니다.");
                            monDef = 0;
                        }

                    }
                }

            }
        }

        static void MonsterUseSkill(ref int monMp, ref int playerDef, ref int playerHp, int playerAvoidProb, ref int turn, Skill monSkill)
        {
            if (monMp >= monSkill.Mp)
            {
                Console.WriteLine("몬스터가 '휘두르기' 스킬을 사용했습니다. ");
                monMp -= monSkill.Mp;
                if (playerAvoidProb < 20)
                {
                    Console.WriteLine("플레이어가 공격을 회피했습니다.");
                }
                else if (playerAvoidProb < 40)
                {
                    Console.WriteLine("플레이어가 데미지를 무효화 했습니다.");
                }
                else
                {
                    Console.WriteLine("플레이어에게 입힌 피해량: {0}", monSkill.Atk);

                    if (playerDef >= monSkill.Atk)
                    {
                        playerDef -= monSkill.Atk;
                        Console.WriteLine($"플레이어의 방어력이 {monSkill.Atk}만큼 깎였습니다.");

                    }
                    else
                    {
                        if (playerDef == 0)
                        {
                            playerHp -= monSkill.Atk;
                            Console.WriteLine($"플레이어가 {monSkill.Atk - playerDef}만큼의 피해를 입습니다.");
                        }
                        else
                        {
                            playerHp -= (monSkill.Atk - playerDef);
                            Console.WriteLine($"플레이어가 방어력이 {playerDef}만큼 깎이고 {monSkill.Atk - playerDef}만큼의 피해를 입습니다.");
                            playerDef = 0;
                        }

                    }
                }
            }
        }

        static void PlayerAtkMon(ref int monDef, ref int playerAtk, int criAtk, ref int monHp )
        {
            if (monDef >= (playerAtk + criAtk))
            {
                monDef -= (playerAtk + criAtk);
                Console.WriteLine($"몬스터의 방어력이 {(playerAtk + criAtk)}만큼 깎였습니다.");

            }
            else
            {
                if (monDef == 0)
                {
                    monHp -= (playerAtk + criAtk);
                    Console.WriteLine($"몬스터가 {(playerAtk + criAtk)}만큼의 피해를 입습니다.");
                }
                else
                {
                    monHp -= ((playerAtk + criAtk) - monDef);
                    Console.WriteLine($"몬스터가 방어력이 {monDef}만큼 깎이고 {(playerAtk + criAtk) - monDef}만큼의 피해를 입습니다.");
                    monDef = 0;
                }

            }


        }
    }
}
