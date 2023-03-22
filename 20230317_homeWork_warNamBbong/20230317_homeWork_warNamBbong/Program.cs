using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _20230317_homeWork_warNamBbong
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] card = new string[52];
            int count = 0; //카드 뽑기 카운트 변수
            int userMoney = 10000, gambleMoney = 100;

            string comCardA, comCardB, userCard;
            int comCardNumA, comCardNumB, userCardNum;

            // 카드 섞기 위한 변수
            int dest, sour;
            string temp;

            //승리 패배 여부 변수
            bool result;

            Random random = new Random();

            //52장의 카드 만들기
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 14; j++)
                {
                    string tmp = "";

                    switch (i)
                    {
                        case 0:
                            tmp += "◆";
                            break;
                        case 1:
                            tmp += "♠";
                            break;
                        case 2:
                            tmp += "♣";
                            break;
                        default:
                            tmp += "♥";
                            break;
                    }

                    tmp += Convert.ToString(j);
                    card[13 * i + j - 1] = tmp;
                }

            }

            //카드 섞기
            for (int i = 0; i < 1000; i++)
            {
                dest = random.Next(52);
                sour = random.Next(52);

                temp = card[dest];
                card[dest] = card[sour];
                card[sour] = temp;
            }

            //시작 창
            string[] startLabel = new string[9] {
                "   ######   ##########      ##      ########    ##########",
                "####            ##         ####     ##     ##       ##    ",
                "#               ##         #  #     ##      ##      ##    ",
                "###             ##        ##  ##    ##     ##       ##    ",
                "   ######       ##       ########   ########        ##    ",
                "        ##      ##       #      #   ## ###          ##    ",
                "        ##      ##       #      #   ## ###          ##    ",
                "       ###      ##      ##      ##  ##   ####       ##    ",
                "########        ##      #        #  ##      ##      ##    ",
                 };

            Console.WriteLine();
            Console.WriteLine();
            printCenterSort("경일 카지노에 오신걸 환영합니다.");
            Console.WriteLine();
            printCenterSort(startLabel);
            Console.WriteLine();
            printCenterSort("엔터 눌러서 시작하기");
            Console.ReadLine();

            while (true)
            {
                Console.Clear();

                //종료 조건1: 51장의 카드를 모두 소진
                if (count > 16)
                {
                    Console.WriteLine("게임 종료.\n");
                    Console.WriteLine("당신의 소지금: {0}", userMoney);
                    break;
                }
                //종료 조건2: 소지금이 최소 베팅 금액보다 작을 때
                if (userMoney < 100)
                {
                    Console.WriteLine("더 이상 베팅할 소지금이 없습니다.\n ");
                    Console.WriteLine("게임 종료.");
                    break;
                }

                //판 수에 맞춰 카드 정하기
                comCardA = card[count * 3];
                comCardB = card[count * 3 + 1];
                userCard = card[count * 3 + 2];

                //컴퓨터 카드 출력
                Console.WriteLine();
                Console.WriteLine("딜러는 다음 카드를 보여줍니다.\n\n");
                Thread.Sleep(2000);
                Console.WriteLine("{0} {1}\n\n", PrintCard(comCardA), PrintCard(comCardB));
                Thread.Sleep(1000);

                //유저 소지금 출력
                Console.WriteLine("현재 당신의 소지금: {0}\n", userMoney);

                //베팅 금액 입력
                while (true)
                {
                    Console.Write("베팅 금액을 입력해주세요. (최소 금액: 100) ");
                    gambleMoney = int.Parse(Console.ReadLine());

                    if (gambleMoney < 100)
                    {
                        Console.WriteLine("최소 베팅 금액은 100원입니다.");
                    }
                    else if (gambleMoney > userMoney)
                    {
                        Console.WriteLine("소지금보다 많은 금액을 베팅할 수 없습니다.");
                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine();
                //플레이어 카드 공개
                Console.WriteLine("플레이어의 카드를 오픈합니다.\n");
                Thread.Sleep(2000);
                Console.WriteLine("당신의 카드: {0}\n", PrintCard(userCard));

                //카드 계산

                //카드 숫자 뽑아내기
                comCardNumA = int.Parse(comCardA.Substring(1));
                comCardNumB = int.Parse(comCardB.Substring(1));
                userCardNum = int.Parse(userCard.Substring(1));

                if (comCardNumA < comCardNumB) //A카드가 B카드보다 작을 때
                {
                    if (comCardNumA < userCardNum && userCardNum < comCardNumB) //유저카드가 A카드와 B카드 사이에 있을 때
                    {
                        Console.WriteLine("플레이어 승리!");
                        result = true;
                    }
                    else
                    {
                        Console.WriteLine("플레이어 패배!");
                        result = false;
                    }
                }
                else
                {
                    if (comCardNumA > userCardNum && userCardNum > comCardNumB) //유저카드가 A카드와 B카드 사이에 있을 때
                    {
                        Console.WriteLine("플레이어 승리!");
                        result = true;
                    }
                    else
                    {
                        Console.WriteLine("플레이어 패배!");
                        result = false;
                    }
                }

                if (result == true)
                {
                    userMoney += gambleMoney * 2;
                }
                else
                {
                    userMoney -= gambleMoney * 2;
                }
                count++;

                Console.WriteLine("\n=======================================\n\n");
                Console.WriteLine("당신의 소지금: {0}", userMoney);
                Console.WriteLine();
                Console.ReadLine();
            }



            //앞에 문자 자르기
            //Console.WriteLine(card[31].Substring(1));
        }

        static string PrintCard(string card)
        {
            int cardNum;
            cardNum = int.Parse(card.Substring(1));

            switch (cardNum)
            {
                case 11:
                    return card.Substring(0, 1) + "J";
                case 12:
                    return card.Substring(0, 1) + "Q";
                case 13:
                    return card.Substring(0, 1) + "K";
                case 1:
                    return card.Substring(0, 1) + "A";
                default:
                    return card;
            }

        }

        static void printCenterSort(string str)
        {
            int bytelen = Encoding.Default.GetBytes(str).Length;
            int padlen = 60 - (bytelen / 2);

            if (bytelen % 2 != 0)
            {
                Console.WriteLine("{0}", "".PadLeft(padlen) + str + "".PadRight(padlen - 1));
            }
            else
            {
                Console.WriteLine("{0}", "".PadLeft(padlen) + str + "".PadRight(padlen));
            }

        }

        static void printCenterSort(string[] strArray)
        {
            for (int i = 0; i < strArray.Length; i++)
            {
                int bytelen = Encoding.Default.GetBytes(strArray[i]).Length;
                int padlen = 60 - (bytelen / 2);

                if (bytelen % 2 != 0)
                {
                    Console.WriteLine("{0}", "".PadLeft(padlen) + strArray[i] + "".PadRight(padlen - 1));
                }
                else
                {
                    Console.WriteLine("{0}", "".PadLeft(padlen) + strArray[i] + "".PadRight(padlen));
                }
            }
        }
    }
}
