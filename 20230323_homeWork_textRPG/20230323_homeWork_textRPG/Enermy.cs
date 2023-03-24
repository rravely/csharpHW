using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enermy
{
    public interface IWalk //걸어다니기
    {
        void Walk();
    }

    public interface IDie //죽기
    {
        void Die();
    }

    public abstract class Enermy
    {
        static public  void defaultAtk(Player player, Monster mon, int turn) //기본 공격
        {
            if (turn % 2 == 0)
            {
                Console.WriteLine($"{player.Name}이 {mon.Name}를 공격했습니다!");
                mon.HP -= 10;
            }
            else
            {
                Console.WriteLine($"{mon.Name}이 {player.Name}를 공격했습니다.");
                player.HP -= 10;
            }
            
        }

        public abstract void SkillAtk(); //스킬 공격
    }

    public class Player : Enermy, IWalk, IDie
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int HpPotionNum { get; set; }
        public int MpPotionNum { get; set; }
        public int Money { get; set; }
        public bool isAlive { get; set; }

        public Player(string Name)
        {
            this.Name = Name;
            HP = 100;
            MP = 100;
            HpPotionNum = 5;
            MpPotionNum = 5;
            Money = 0;
            //isAlive = true;
        }

        public override void SkillAtk()
        {
            throw new NotImplementedException();
        }

        public void Walk()
        {
            Console.WriteLine($"{Name}님이 지나갑니다. ");
        }

        public void Die()
        {
            Console.WriteLine("플레이어가 죽었습니다...");
        }

        static public void DrinkPotion(Player player, string potion)
        {
            char inputAns;
            ConsoleKeyInfo cki;

            //남은 포션 표시
            if (potion.Equals("HP"))
            {
                Console.WriteLine($"남은 포션: {player.HpPotionNum}개\n");
            }
            else
            {
                Console.WriteLine($"남은 포션: {player.MpPotionNum}개\n");
            }

            
            Console.WriteLine($"포션을 드시겠습니까? (Yes: ↑, No: ↓)");
            cki = Console.ReadKey();

            //올바른 입력을 받을 때까지
            while (true)
            {
                if (cki.Key.Equals(ConsoleKey.UpArrow)) //포션을 먹겠다고 하면
                {
                    Console.WriteLine();
                    if (potion.Equals("HP")) //HP 포션이면
                    {
                        Console.WriteLine("HP 포션을 마셔 HP가 50 회복되었습니다.");
                        player.HP += 50;
                        player.HpPotionNum -= 1;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("MP 포션을 마셔 MP가 50 회복되었습니다.");
                        player.MP += 50;
                        player.MpPotionNum -= 1;
                        break;
                    }
                }
                else if(cki.Key.Equals(ConsoleKey.DownArrow)) {
                    Console.WriteLine();
                    Console.WriteLine("전투를 계속합니다. ");
                    break;
                }
                else
                {
                    Console.WriteLine("잘못 입력했습니다. 다시 입력해주세요.");
                    cki = Console.ReadKey();
                }
            }

        }
    }

    public class Monster : Enermy, IWalk, IDie
    {
        public string Name;
        public int HP;
        public int MP;
        public int Money;
        //public bool isAlive;

        
        public Monster(string Name, int HP, int MP, int Money)
        {
            this.Name = Name;
            this.HP = HP;
            this.MP = MP;
            this.Money = Money;
            
        }

        public void Die()
        {
            Console.WriteLine("몬스터가 죽었습니다. ");
        }

        public override void SkillAtk()
        {
            throw new NotImplementedException();
        }

        public void Walk()
        {
            Console.WriteLine($"{Name}이 지나갑니다. ");
        }
    }

}
