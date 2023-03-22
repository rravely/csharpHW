using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20230316_homeWork_huntMonster
{
    class Program
    {
        enum PLAYERSTATE { FIGHT, RUN};
        enum PLAYERSKILLATK { A = 10, B = 15, C = 20}; //플레이어 스킬 공격력
        enum PLAYERSKILLMP { A = 20, B = 30, C = 40}; //플레이어 스킬 사용 시 소모 MP
        enum MONSKILLATK { A = 5, B = 35, C = 10}; //몬스터 스킬 공격력
        enum MONSKILLMP { A = 10, B = 70, C = 20}; //몬스터 스킬 사용 시 소모 MP
        
        static void Main(string[] args)
        {
            int playerHp = 100, playerMp = 100, playerAtk = 5, playerDef = 30, playerHpPotionNum = 5, playerMpPotionNum = 5, playerMoney = 0;
            int playerInputAtkNum, monAtkNum, criProb, monAvoidProb, playerAvoidProb, criAtk, playerState = 0, turn = 0;
            int monHp = 100, monMp = 100, monAtk = 5, monDef = 30, monMoney = 1000;
            char playerPotionAns, playerRunAns;

            Random random = new Random();

            //START
            Console.WriteLine("** MONSTER HUNT **");
            Console.WriteLine("You meet a monster!");
            Console.WriteLine();

            while (true)
            {
                // 종료 조건
                if (playerHp <= 0) //플레이어 패배
                {
                    Console.WriteLine("Fail to hunt Monsters. GAME OVER");
                    break;
                }
                if (monHp <= 0) //몬스터 피 0
                {
                    Console.WriteLine("몬스터 사냥에 성공하였습니다.");
                    playerMoney += monMoney;
                    Console.WriteLine("플레이어의 소지금: {0}", playerMoney);
                    break;
                }
                if (playerState == (int)PLAYERSTATE.RUN)
                {
                    Console.WriteLine("플레이어는 도망갔습니다. 게임 종료.");
                    break;
                }


                //도망갈지 물어보기
                if (playerHp <= 10)
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
                if (playerHp <= 20 && playerHpPotionNum > 0)
                {
                    Console.WriteLine("플레이어의 HP가 20 이하로 떨어졌습니다. 플레이어의 남은 HP: {0}", playerHp);
                    Console.WriteLine("포션을 드시겠습니까? (Y/N)");
                    playerPotionAns = Convert.ToChar(Console.ReadLine());

                    switch (playerPotionAns)
                    {
                        case 'Y':
                            playerHpPotionNum--; //포션 개수 감소
                            playerHp += 200; //포션 하나당 Hp 200 증가
                            Console.WriteLine("포션을 먹었습니다.");
                            Console.WriteLine("플레이어의 남은 HP: {0}, 남은 HP 포션: {1}", playerHp, playerHpPotionNum);
                            break;
                        default:
                            Console.WriteLine("포션을 먹지 않습니다.");
                            break;
                    }
                    Console.WriteLine();
                }

                //Mp 포션 먹을 조건
                if (playerMp <= 20 && playerMpPotionNum > 0)
                {
                    Console.WriteLine("플레이어의 MP가 20 이하로 떨어졌습니다. 현재 MP: {0}", playerMp);
                    Console.WriteLine("포션을 드시겠습니까? (Y/N)");
                    playerPotionAns = Convert.ToChar(Console.ReadLine());

                    switch (playerPotionAns)
                    {
                        case 'Y':
                            playerMpPotionNum--; //포션 개수 감소
                            playerMp += 50; //포션 하나당 Mp 50 증가
                            Console.WriteLine("포션을 먹었습니다.");
                            Console.WriteLine("플레이어의 남은 MP: {0}, 남은 MP 포션: {1}", playerMp, playerMpPotionNum);
                            break;
                        default:
                            Console.WriteLine("포션을 먹지 않습니다.");
                            break;
                    }
                    Console.WriteLine();
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
                                Console.WriteLine("몬스터에게 크리티컬 피해를 입혔습니다. 몬스터에게 입힌 피해량: {0}", playerAtk + criAtk);

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
                                        Console.WriteLine($"몬스터의 방어력이 {monDef}만큼 깎이고 {(playerAtk + criAtk) - monDef}만큼의 피해를 입습니다.");
                                        monDef = 0;
                                    }

                                }

                            }
                            else //크리 안터지면
                            {
                                Console.WriteLine("몬스터에게 기본 피해를 입혔습니다. 몬스터에게 입힌 피해량: {0}", playerAtk);

                                if (monDef >= playerAtk)
                                {
                                    monDef -= playerAtk;
                                    Console.WriteLine($"몬스터가 플레이어로부터 피해를 입어 방어력이 {playerAtk}만큼 깎였습니다.");

                                }
                                else
                                {
                                    if (monDef == 0)
                                    {
                                        monHp -= playerAtk;
                                        Console.WriteLine($"몬스터가 플레이어로부터 피해를 입어 {playerAtk}만큼의 피해를 입습니다.");
                                    }
                                    else
                                    {
                                        monHp -= (playerAtk - monDef);
                                        Console.WriteLine($"몬스터가 플레이어로부터 피해를 입어 방어력이 {monDef}만큼 깎이고 {playerAtk - monDef}만큼의 피해를 입습니다.");
                                        monDef = 0;
                                    }

                                }
                            }
                            break;
                        case 1: //첫 스킬 ~
                            if (playerMp < (int)PLAYERSKILLMP.A)
                            {
                                Console.WriteLine("MP가 부족하여 스킬을 사용할 수 없습니다. ");
                                turn--;

                                //여기서 마나 포션 먹을 수 있게 구현해야함,,
                            }
                            else
                            {
                                Console.WriteLine("플레이어가 '휘이잉' 스킬을 사용했습니다. ");
                                playerMp -= (int)PLAYERSKILLMP.A;

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
                                    Console.WriteLine("몬스터에게 입힌 피해량: {0}", (int)PLAYERSKILLATK.A);

                                    if (monDef >= (int)PLAYERSKILLATK.A)
                                    {
                                        monDef -= (int)PLAYERSKILLATK.A;
                                        Console.WriteLine($"몬스터가 플레이어로부터 피해를 입어 방어력이 {(int)PLAYERSKILLATK.A}만큼 깎였습니다.");

                                    }
                                    else
                                    {
                                        if (monDef == 0)
                                        {
                                            monHp -= (int)PLAYERSKILLATK.A;
                                            Console.WriteLine($"몬스터가 플레이어로부터 피해를 입어 {(int)PLAYERSKILLATK.A - monDef}만큼의 피해를 입습니다.");
                                        }
                                        else
                                        {
                                            monHp -= ((int)PLAYERSKILLATK.A - monDef);
                                            Console.WriteLine($"몬스터가 플레이어로부터 피해를 입어 방어력이 {monDef}만큼 깎이고 {(int)PLAYERSKILLATK.A - monDef}만큼의 피해를 입습니다.");
                                            monDef = 0;
                                        }

                                    }
                                }
                                
                            }
                            break;
                        case 2:
                            if (playerMp < (int)PLAYERSKILLMP.B)
                            {
                                Console.WriteLine("MP가 부족하여 스킬을 사용할 수 없습니다. ");
                                turn--;
                                

                            }
                            else
                            {
                                Console.WriteLine("플레이어가 '뾰로롱' 스킬을 사용했습니다. ");
                                playerMp -= (int)PLAYERSKILLMP.B;

                                if (monAvoidProb < 70)
                                {
                                    Console.WriteLine("몬스터가 공격을 회피했습니다.");
                                }
                                else if (monAvoidProb < 90)
                                {
                                    Console.WriteLine("몬스터가 데미지를 무효화 했습니다.");
                                }
                                else
                                {
                                    Console.WriteLine("몬스터에게 입힌 피해량: {0}", (int)PLAYERSKILLATK.B);

                                    if (monDef >= (int)PLAYERSKILLATK.B)
                                    {
                                        monDef -= (int)PLAYERSKILLATK.B;
                                        Console.WriteLine($"몬스터가 플레이어로부터 피해를 입어 방어력이 {(int)PLAYERSKILLATK.B}만큼 깎였습니다.");

                                    }
                                    else
                                    {
                                        if (monDef == 0)
                                        {
                                            monHp -= (int)PLAYERSKILLATK.B;
                                            Console.WriteLine($"몬스터가 {(int)PLAYERSKILLATK.B - monDef}만큼의 피해를 입습니다.");
                                        }
                                        else
                                        {
                                            monHp -= ((int)PLAYERSKILLATK.B - monDef);
                                            Console.WriteLine($"몬스터의 방어력이 {monDef}만큼 깎이고 {(int)PLAYERSKILLATK.B - monDef}만큼의 피해를 입습니다.");
                                            monDef = 0;
                                        }

                                    }
                                }
                            }
                            break;
                        case 3:
                            if (playerMp < (int)PLAYERSKILLMP.C)
                            {
                                Console.WriteLine("MP가 부족하여 스킬을 사용할 수 없습니다. ");
                                turn--;
                            }
                            else
                            {
                                Console.WriteLine("플레이어가 '붕붕' 스킬을 사용했습니다. ");
                                playerMp -= (int)PLAYERSKILLMP.C;

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
                                    Console.WriteLine("몬스터에게 입힌 피해량: {0}", (int)PLAYERSKILLATK.C);

                                    if (monDef >= (int)PLAYERSKILLATK.C)
                                    {
                                        monDef -= (int)PLAYERSKILLATK.C;
                                        Console.WriteLine($"몬스터가 플레이어로부터 피해를 입어 방어력이 {(int)PLAYERSKILLATK.C}만큼 깎였습니다.");

                                    }
                                    else
                                    {
                                        if (monDef == 0)
                                        {
                                            monHp -= (int)PLAYERSKILLATK.C;
                                            Console.WriteLine($"몬스터가 플레이어로부터 피해를 입어 {(int)PLAYERSKILLATK.C - monDef}만큼의 피해를 입습니다.");
                                        }
                                        else
                                        {
                                            monHp -= ((int)PLAYERSKILLATK.C - monDef);
                                            Console.WriteLine($"몬스터가 플레이어로부터 피해를 입어 방어력이 {monDef}만큼 깎이고 {(int)PLAYERSKILLATK.C - monDef}만큼의 피해를 입습니다.");
                                            monDef = 0;
                                        }

                                    }
                                }
                            }
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
                            if (playerDef >= monAtk)
                            {
                                playerDef -= monAtk;
                                Console.WriteLine($"플레이어의 방어력이 {monAtk}만큼 깎였습니다.");

                            }
                            else
                            {
                                playerDef = 0;
                                playerHp -= (monAtk - playerDef);
                                Console.WriteLine($"플레이어의 방어력이 {playerDef}만큼 깎이고 {monAtk - playerDef}만큼의 피해를 입습니다.");
                            }
                            break;
                        case 1:
                            if (monMp < (int)MONSKILLMP.A)
                            {
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("몬스터가 '휘두르기' 스킬을 사용했습니다. ");
                                monMp -= (int)MONSKILLMP.A;
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
                                    Console.WriteLine("플레이어에게 입힌 피해량: {0}", (int)MONSKILLATK.A);

                                    if (playerDef >= (int)MONSKILLATK.A)
                                    {
                                        playerDef -= (int)MONSKILLATK.A;
                                        Console.WriteLine($"플레이어의 방어력이 {(int)MONSKILLATK.A}만큼 깎였습니다.");

                                    }
                                    else
                                    {
                                        if (playerDef == 0)
                                        {
                                            playerHp -= (int)MONSKILLATK.A;
                                            Console.WriteLine($"플레이어가 {(int)MONSKILLATK.A - playerDef}만큼의 피해를 입습니다.");
                                        }
                                        else
                                        {
                                            playerHp -= ((int)MONSKILLATK.A - playerDef);
                                            Console.WriteLine($"플레이어가 방어력이 {playerDef}만큼 깎이고 {(int)MONSKILLATK.A - playerDef}만큼의 피해를 입습니다.");
                                            playerDef = 0;
                                        }

                                    }
                                }

                            }
                            break;
                        case 2:
                            if (monMp < (int)MONSKILLMP.B)
                            {
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("몬스터가 '던지기' 스킬을 사용했습니다. ");
                                monMp -= (int)MONSKILLMP.B;
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
                                    Console.WriteLine("플레이어에게 입힌 피해량: {0}", (int)MONSKILLATK.B);

                                    if (playerDef >= (int)MONSKILLATK.B)
                                    {
                                        playerDef -= (int)MONSKILLATK.B;
                                        Console.WriteLine($"플레이어의 방어력이 {(int)MONSKILLATK.B}만큼 깎였습니다.");

                                    }
                                    else
                                    {
                                        if (playerDef == 0)
                                        {
                                            playerHp -= (int)MONSKILLATK.B;
                                            Console.WriteLine($"플레이어가 {(int)MONSKILLATK.B - playerDef}만큼의 피해를 입습니다.");
                                        }
                                        else
                                        {
                                            playerHp -= ((int)MONSKILLATK.B - playerDef);
                                            Console.WriteLine($"플레이어의 방어력이 {playerDef}만큼 깎이고 {(int)MONSKILLATK.B - playerDef}만큼의 피해를 입습니다.");
                                            playerDef = 0;
                                        }

                                    }
                                }

                            }
                            break;
                        case 3:
                            if (monMp < (int)MONSKILLMP.C)
                            {
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("몬스터가 '띄우기' 스킬을 사용했습니다. ");
                                monMp -= (int)MONSKILLMP.C;
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
                                    Console.WriteLine("플레이어에게 입힌 피해량: {0}", (int)MONSKILLATK.C);

                                    if (playerDef >= (int)MONSKILLATK.C)
                                    {
                                        playerDef -= (int)MONSKILLATK.C;
                                        Console.WriteLine($"플레이어의 방어력이 {(int)MONSKILLATK.C}만큼 깎였습니다.");

                                    }
                                    else
                                    {
                                        if (playerDef == 0)
                                        {
                                            playerHp -= (int)MONSKILLATK.C;
                                            Console.WriteLine($"플레이어가 {(int)MONSKILLATK.C - playerDef}만큼의 피해를 입습니다.");
                                        }
                                        else
                                        {
                                            playerHp -= ((int)MONSKILLATK.C - playerDef);
                                            Console.WriteLine($"플레이어의 방어력이 {playerDef}만큼 깎이고 {(int)MONSKILLATK.C - playerDef}만큼의 피해를 입습니다.");
                                            playerDef = 0;
                                        }

                                    }
                                }

                            }
                            break;
                    }
                }

                //플레이어와 몬스터 스탯 출력
                PrintPlayerInfo(playerHp, playerMp, playerAtk, playerDef, monHp, monMp, monAtk, monDef);
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

        static void PlayerUseSkill(int playerMp, int monHp, int turn, int skillMp)
        {
            if (playerMp < skillMp)
            {
                Console.WriteLine("MP가 부족하여 스킬을 사용할 수 없습니다. ");
                turn--;
            }
            else
            {
                Console.WriteLine("'봉봉' 스킬을 사용했습니다. ");
                monHp -= skillMp;
                playerMp -= skillMp;
            }
        }
    }
}
