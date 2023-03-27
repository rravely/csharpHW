using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player;

namespace _20230224_homeWork_poker
{
    class Program
    {
        enum POKERSUIT { SPADE = 0, DIAMOND, HEART, CLUB }

        static void Main(string[] args)
        {
            string[] handRankings = new string[12] { "로얄 스트레이트 플러쉬 ", "백스트레이트 플러쉬 ", "스트레이트 플러쉬 ", "포커 ", "풀 하우스 ", "플러쉬 ", "마운틴 ", "백스트레이트 ", "스트레이트 ", "트리플 ", "투 페어 ", "원 페어 " };


            Console.WriteLine("\n~! 경일 카지노 !~\n");

            //플레이어의 이름 받기
            Console.Write("플레이어의 이름을 입력해주세요. ");
            string playerName = Console.ReadLine();

            //플레이어 생성
            Player_info player = new Player_info(playerName, 100000);
            Player_info computer = new Player_info("컴퓨터", 100000);

            //플레이어와 컴퓨터 카드 리스트 생성
            List<int> playerOpenCards = new List<int>();
            List<int> playerHiddenCards = new List<int>();
            List<int> computerOpenCards = new List<int>();
            List<int> computerHiddenCards = new List<int>();

            //플레이어와 컴퓨터 5장의 카드 배열 생성
            int[] playerFiveCards = new int[5];
            int[] computerFiveCards = new int[5];

            //5장의 카드 결과를 받을 배열(Length of Array is 2. First: whether the quotients are equal, Second: poker pedigree number)
            int[] playerResult = new int[2];
            int[] computerResult = new int[2];


            //플레이어와 컴퓨터 결과 비교 변수
            int cardResult;

            //플레이어의 현재 소지금 출력
            PrintPlayerInfo(player);
            Console.WriteLine("\n");

            //난수 생성
            Random random = new Random();

            //52장의 카
            int[] shuffledcards;

            //턴 변수
            int turn = 0;

            //베팅 금액 변수
            int playerGambleMoney = 0, computerGambleMoney = 0, gambleMoney = 0;

            while (true)
            {
                //시작하면 오픈, 히든 카드 List 초기화
                playerOpenCards.Clear();
                computerOpenCards.Clear();
                playerHiddenCards.Clear();
                computerHiddenCards.Clear();

                //시작하면 카드 생성해서 섞
                shuffledcards = Shuffle52Cards(Create52Cards());

                //플레이어의 소지금이 0이면 게임 종료
                if (player.Money <= 0)
                {
                    Console.WriteLine("플레이어가 파산했습니다. 게임 종료");
                    break;
                }
                //컴퓨터의 소지금이 0이면 게임 종료
                if (computer.Money <= 0)
                {
                    Console.WriteLine("컴퓨터가 파산했습니다. 게임 종료");
                    break;
                }


                //컴퓨터 카드와 플레이어 카드 나눠주기
                //컴퓨터 카드 주기(하나 랜덤으로 빼고 랜덤으로 오픈 카드 선택하기)
                int selectNum = random.Next(0, 4);
                int[] computerSelectCard = SelectComputer3Cards(shuffledcards, selectNum); //Throw away one card

                selectNum = random.Next(0, 3);
                SelectOpenCard(selectNum, computerSelectCard, ref computerOpenCards, ref computerHiddenCards); //Select an open card


                //플레이어 카드 4개 보여주고 3개 받기
                int[] playerSelectedCard = SelectPlayer3Cards(shuffledcards[8], shuffledcards[9], shuffledcards[10], shuffledcards[11]);
                Console.WriteLine();
                //Console.WriteLine("당신의 카드는 다음과 같습니다. \n");
                PrintPlayerCards(3, playerSelectedCard);


                //오픈할 카드 선택
                Console.Write("\n오픈할 카드를 선택해주세요. ");
                selectNum = int.Parse(Console.ReadLine());
                SelectOpenCard(selectNum, playerSelectedCard, ref playerOpenCards, ref playerHiddenCards);


                //오픈된 카드 보여주기
                PrintOpenCards(playerOpenCards, computerOpenCards);
                //플레이어 히든 카드 보여주기
                Console.Write("\n플레이어의 ");
                PrintHiddenCards(playerHiddenCards);

                InputAndClearConsole();


                //오픈 카드2 나눠주기
                Console.WriteLine("오프 카드를 나누어 줍니다. \n");
                playerOpenCards.Add(shuffledcards[12]);
                computerOpenCards.Add(shuffledcards[4]);


                //오픈된 카드 보여주기
                PrintOpenCards(playerOpenCards, computerOpenCards);
                //플레이어 히든 카드 보여주기
                Console.Write("\n플레이어 ");
                PrintHiddenCards(playerHiddenCards);


                //오픈된 카드 4개 중 가장 높은 패 선택해서 베팅 순서 정하기
                turn = SelectHighestCard(playerOpenCards, computerOpenCards);

                playerGambleMoney = 0;
                computerGambleMoney = 0;
                //두번째 오픈 카드보고 배팅 하기1
                if ((turn % 2).Equals(0)) //짝수턴 플레이어 시작
                {
                    Console.WriteLine($"\n{player.Name}가 베팅할 차례입니다.\n");
                    Console.Write($"베팅 금액을 입력해주세요. 현재 플레이어의 소지금: {player.Money}\n");
                    playerGambleMoney = int.Parse(Console.ReadLine());
                    Console.Write($"베팅 금액: {playerGambleMoney}");
                    computerGambleMoney = playerGambleMoney;
                }
                else //컴퓨터 선택
                {
                    Console.WriteLine("\n컴퓨터가 베팅할 차례입니다.\n");
                    computerGambleMoney = random.Next(1000, 10000);
                    Console.Write($"베팅 금액: {computerGambleMoney}");
                    playerGambleMoney = computerGambleMoney;

                }
                turn++;

                int gambleResult;
                //베팅 1
                gambleResult = SelectGamble(turn, ref playerGambleMoney, ref computerGambleMoney, player, computer);
                turn++;
                if (gambleResult.Equals(3)) //다이를 선택하면 
                {
                    InputAndClearConsole();
                    Console.WriteLine("게임을 다시 시작합니다.");
                    continue;
                }
                if (computerGambleMoney > playerGambleMoney)
                {
                    playerGambleMoney = computerGambleMoney;
                }
                else
                {
                    computerGambleMoney = playerGambleMoney;
                }

                InputAndClearConsole();

                //오픈 카드3 나눠주기
                Console.WriteLine("오프 카드를 나누어 줍니다. \n");
                playerOpenCards.Add(shuffledcards[13]);
                computerOpenCards.Add(shuffledcards[5]);

                //오픈된 카드 보여주기
                PrintOpenCards(playerOpenCards, computerOpenCards);
                //플레이어 히든 카드 보여주기
                Console.Write("\n플레이어 ");
                PrintHiddenCards(playerHiddenCards);

                //베팅 2
                gambleResult = SelectGamble(turn, ref playerGambleMoney, ref computerGambleMoney, player, computer);
                if (gambleResult.Equals(3)) //다이를 선택하면 
                {
                    InputAndClearConsole();
                    Console.WriteLine("게임을 다시 시작합니다.");
                    continue;
                }
                if (computerGambleMoney > playerGambleMoney)
                {
                    playerGambleMoney = computerGambleMoney;
                }
                else
                {
                    computerGambleMoney = playerGambleMoney;
                }
                turn++;
                gambleResult = SelectGamble(turn, ref playerGambleMoney, ref computerGambleMoney, player, computer);
                if (gambleResult.Equals(3)) //다이를 선택하면 
                {
                    InputAndClearConsole();
                    Console.WriteLine("게임을 다시 시작합니다.");
                    continue;
                }
                if (computerGambleMoney > playerGambleMoney)
                {
                    playerGambleMoney = computerGambleMoney;
                }
                else
                {
                    computerGambleMoney = playerGambleMoney;
                }
                turn++;

                InputAndClearConsole();

                //오픈 카드4 나눠주기
                Console.WriteLine("오프 카드를 나누어 줍니다. \n");
                playerOpenCards.Add(shuffledcards[14]);
                computerOpenCards.Add(shuffledcards[6]);

                //오픈된 카드 보여주기
                PrintOpenCards(playerOpenCards, computerOpenCards);
                //플레이어 히든 카드 보여주기
                Console.Write("\n플레이어 ");
                PrintHiddenCards(playerHiddenCards);

                //베팅하기3
                gambleResult = SelectGamble(turn, ref playerGambleMoney, ref computerGambleMoney, player, computer);
                if (gambleResult.Equals(3)) //다이를 선택하면 
                {
                    InputAndClearConsole();
                    Console.WriteLine("게임을 다시 시작합니다.");
                    continue;
                }
                if (computerGambleMoney > playerGambleMoney)
                {
                    playerGambleMoney = computerGambleMoney;
                }
                else
                {
                    computerGambleMoney = playerGambleMoney;
                }
                turn++;
                gambleResult = SelectGamble(turn, ref playerGambleMoney, ref computerGambleMoney, player, computer);
                if (gambleResult.Equals(3)) //다이를 선택하면 
                {
                    InputAndClearConsole();
                    Console.WriteLine("게임을 다시 시작합니다.");
                    continue;
                }
                if (computerGambleMoney > playerGambleMoney)
                {
                    playerGambleMoney = computerGambleMoney;
                }
                else
                {
                    computerGambleMoney = playerGambleMoney;
                }
                turn++;

                InputAndClearConsole();


                //히든카드 나눠주기
                Console.WriteLine("마지막 히든 카드를 나누어 줍니다.\n");
                playerHiddenCards.Add(shuffledcards[15]);
                computerHiddenCards.Add(shuffledcards[7]);

                //오픈된 카드 보여주기
                PrintOpenCards(playerOpenCards, computerOpenCards);
                //플레이어 히든 카드 보여주기
                Console.Write("\n플레이어 ");
                PrintHiddenCards(playerHiddenCards);

                //베팅하기4
                gambleResult = SelectGamble(turn, ref playerGambleMoney, ref computerGambleMoney, player, computer);
                if (gambleResult.Equals(3)) //다이를 선택하면 
                {
                    InputAndClearConsole();
                    Console.WriteLine("게임을 다시 시작합니다.");
                    continue;
                }
                if (computerGambleMoney > playerGambleMoney)
                {
                    playerGambleMoney = computerGambleMoney;
                }
                else
                {
                    computerGambleMoney = playerGambleMoney;
                }
                turn++;
                gambleResult = SelectGamble(turn, ref playerGambleMoney, ref computerGambleMoney, player, computer);
                if (gambleResult.Equals(3)) //다이를 선택하면 
                {
                    InputAndClearConsole();
                    Console.WriteLine("게임을 다시 시작합니다.");
                    continue;
                }
                if (computerGambleMoney > playerGambleMoney)
                {
                    playerGambleMoney = computerGambleMoney;
                }
                else
                {
                    computerGambleMoney = playerGambleMoney;
                }
                turn++;


                InputAndClearConsole();

                //7장의 카드 공개
                Console.Write("\n컴퓨터 ");
                PrintHiddenCards(computerHiddenCards);
                PrintOpenCards(playerOpenCards, computerOpenCards);
                Console.Write("플레이어 ");
                PrintHiddenCards(playerHiddenCards);


                //플레이어 카드 5장 선택하기
                playerFiveCards = SelectOptimalCards(playerOpenCards, playerHiddenCards);


                //컴퓨터 카드 5장 선택하기
                computerFiveCards = SelectOptimalCards(computerOpenCards, computerHiddenCards);


                //컴퓨터와 플레이어 5장의 카드 공개!
                PrintFiveCards(playerFiveCards, computerFiveCards);


                //카드 계산하기
                CalculateCardResult(playerFiveCards, playerResult);
                CalculateCardResult(computerFiveCards, computerResult);


                //유저와 컴퓨터 카드 비교
                cardResult = CalculateWinner(playerResult, computerResult); // 플레이어 승(1), 컴퓨터 승(0), 둘다 13번(2), 둘다 11번(3)

                while (true)
                {
                    if (cardResult.Equals(1)) //플레이어가 이긴 경우
                    {
                        if (playerResult[1] < 13)
                        {
                            Console.WriteLine(handRankings[playerResult[1] - 1]);
                        }

                        Console.WriteLine("플레이어 승!");
                        player.Money += playerGambleMoney;
                        computer.Money -= playerGambleMoney;
                        PrintPlayerInfo(player);
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    }
                    else if (cardResult.Equals(0)) //컴퓨터가 이긴 경우 
                    {
                        if (computerResult[1] < 13)
                        {
                            Console.WriteLine(handRankings[computerResult[1] - 1]);
                        }
                        Console.WriteLine("플레이어 패!");
                        player.Money -= computerGambleMoney;
                        computer.Money += computerGambleMoney;
                        PrintPlayerInfo(player);
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    }
                    else if (cardResult.Equals(2)) // 13번인 경우 
                    {
                        cardResult = CalculateWinnerBoth13(playerResult, computerResult);
                    }
                    else
                    {
                        cardResult = CalculateWinnerBoth11(playerResult, computerResult);
                    }
                }

            }



        }

        static public void PrintPlayerInfo(Player_info player)
        {
            Console.WriteLine($"{player.Name}님의 기본 소지금: {player.Money}");
        }


        //52장의 카드 순서대로 넣은 배열 생성(0부터 51번까지)
        static public int[] Create52Cards()
        {
            int[] cards = new int[52];

            for (int i = 0; i < 52; i++)
            {
                cards[i] = i;

            }
            return cards;
        }

        //52장의 카드 섞기
        static public int[] Shuffle52Cards(int[] cards)
        {
            //난수 생성
            Random random = new Random();

            //난수 변수 
            int dest, sour, temp;

            for (int i = 0; i < 1000; i++)
            {
                dest = random.Next(52);
                sour = random.Next(52);

                temp = cards[dest];
                cards[dest] = cards[sour];
                cards[sour] = temp;
            }

            return cards;
        }

        static public void returnCardResult()
        {
        }

        //컴퓨터 처음 4장 중 한장 버리기
        static public int[] SelectComputer3Cards(int[] shuffledCards, int selectNum)
        {
            int[] returnArray = new int[3];

            switch (selectNum)
            {
                case 0:
                    returnArray = new int[3] { shuffledCards[1], shuffledCards[2], shuffledCards[3] };
                    break;
                case 1:
                    returnArray = new int[3] { shuffledCards[0], shuffledCards[2], shuffledCards[3] };
                    break;
                case 2:
                    returnArray = new int[3] { shuffledCards[0], shuffledCards[1], shuffledCards[3] };
                    break;
                case 3:
                    returnArray = new int[3] { shuffledCards[0], shuffledCards[1], shuffledCards[2] };
                    break;
            }

            return returnArray;
        }


        //플레이어의 처음 4장 중 한 장 버리기
        static public int[] SelectPlayer3Cards(int a, int b, int c, int d)
        {
            int[] returnArray = new int[3];

            Console.WriteLine("당신의 카드는 다음과 같습니다. \n");
            Console.WriteLine($"1.  {NumToTrumpCard(a)}  | 2.  {NumToTrumpCard(b)}  | 3.  {NumToTrumpCard(c)}  | 4.  {NumToTrumpCard(d)}\n");
            Console.Write("버릴 카드를 선택해주세요.");
            int trashCard = int.Parse(Console.ReadLine());

            switch (trashCard)
            {
                case 1:
                    returnArray = new int[3] { b, c, d };
                    break;
                case 2:
                    returnArray = new int[3] { a, c, d };
                    break;
                case 3:
                    returnArray = new int[3] { a, b, d };
                    break;
                case 4:
                    returnArray = new int[3] { a, b, c };
                    break;
                default:
                    returnArray = new int[3] { a, b, c };
                    break;
            }

            return returnArray;
        }

        //오픈할 카드 고르기 - 플레이어 컴퓨터 둘다
        static public void SelectOpenCard(int selectNum, int[] selectedCard, ref List<int> openCards, ref List<int> hiddenCards)
        {
            switch (selectNum)
            {
                case 2:
                    openCards.Add(selectedCard[1]);
                    hiddenCards.Add(selectedCard[0]);
                    hiddenCards.Add(selectedCard[2]);
                    break;
                case 3:
                    openCards.Add(selectedCard[2]);
                    hiddenCards.Add(selectedCard[0]);
                    hiddenCards.Add(selectedCard[1]);
                    break;
                default: //잘못 입력하면 1번으로
                    openCards.Add(selectedCard[0]);
                    hiddenCards.Add(selectedCard[1]);
                    hiddenCards.Add(selectedCard[2]);
                    break;
            }
        }


        //플레이어 카드 출력
        static public void PrintPlayerCards(int num, int[] cardsArray)
        {
            Console.WriteLine();
            for (int i = 0; i < num; i++)
            {
                Console.Write($"{i + 1}.  {NumToTrumpCard(cardsArray[i])}   ");
            }
            Console.WriteLine();
        }

        //처음 2개의 오픈 카드 중에 가장 높은 패를 가진 사람 구하기 (플레이어 승: 짝수 반환, 컴퓨터 승: 홀수 반환)
        static public int SelectHighestCard(List<int> playerOpenCards, List<int> computerOpenCards)
        {
            int[] playerCardsReminder = new int[2];
            int[] computerCardsReminder = new int[2];

            int playerMaxCards = 52, computerMaxCards = 52;

            //각 포커의 나머지를 구한다.
            for (int i = 0; i < 2; i++)
            {
                playerCardsReminder[i] = playerOpenCards[i] % 13;
                computerCardsReminder[i] = computerOpenCards[i] % 13;
            }

            //나머지 배열을 정렬
            Array.Sort(playerCardsReminder);
            Array.Sort(computerCardsReminder);


            //포카 나머지 배열의 최대값 비교

            if (playerCardsReminder[0].Equals(0) || computerCardsReminder.Equals(0)) //A를 가진 경우 
            {
                if (playerCardsReminder[0].Equals(0) && computerCardsReminder.Equals(0)) //둘다 A인 경우 -> 몫 비교 해야됨
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if ((playerOpenCards[i] % 13).Equals(playerCardsReminder[0]))
                        {
                            playerMaxCards = playerOpenCards[i];
                        }
                        if ((computerOpenCards[i] % 13).Equals(computerCardsReminder[0]))
                        {
                            computerMaxCards = computerOpenCards[i];
                        }
                    }
                }
                else if (playerCardsReminder[0].Equals(0)) //플레이어만 A를 가진 경우
                {
                    return 0; // 플레이어 승
                }
                else // 컴퓨터만 A를 가진 경우
                {
                    return 1; // 컴퓨터 승
                }

            }
            else if (playerCardsReminder[1].Equals(computerCardsReminder[1])) //A를 가지지 않은 것 중에 최댓값이 같으면
            {
                for (int i = 0; i < 2; i++)
                {
                    if ((playerOpenCards[i] % 13).Equals(playerCardsReminder[1]))
                    {
                        playerMaxCards = playerOpenCards[i];
                    }
                    if ((computerOpenCards[i] % 13).Equals(computerCardsReminder[1]))
                    {
                        computerMaxCards = computerOpenCards[i];
                    }
                }
            }
            else //최댓값이 다르면
            {
                if (playerCardsReminder[1] > computerCardsReminder[1]) //플레이어 숫자가 큰 경우
                {
                    return 0;

                }
                else
                {
                    return 1;
                }
            }

            //최대 숫자 카드의 모양으로 판단
            if (playerMaxCards < computerMaxCards)
            {
                return 0; // 플레이어 승
            }
            else
            {
                return 1; //컴퓨터 1
            }

        }



        //베팅하기
        static public int SelectGamble(int turn, ref int playerGambleMoney, ref int computerGambleMoney, Player_info player, Player_info computer)
        {
            int computerAns, playerAns;

            Random random = new Random();

            if ((turn % 2).Equals(0))
            {
                Console.WriteLine($"\n다음 중 선택해주세요. 플레이어 베팅 금액: {playerGambleMoney}, 컴퓨터 베팅 금액: {computerGambleMoney}");
                Console.WriteLine("1. 하프  2. 콜  3. 다이");
                playerAns = int.Parse(Console.ReadLine());

                switch (playerAns)
                {
                    case 1: //하프
                        playerGambleMoney *= 2;
                        Console.WriteLine($"배팅 금액: {playerGambleMoney}");
                        return 1;
                    case 2: //콜
                        playerGambleMoney = computerGambleMoney;
                        Console.WriteLine($"배팅 금액: {playerGambleMoney}");
                        return 2;
                    case 3: //다이 
                        player.Money -= playerGambleMoney;
                        computer.Money += computerGambleMoney;
                        Console.WriteLine($"\n컴퓨터의 소지금: {computer.Money}");
                        Console.WriteLine($"플레이어의 소지금: {player.Money}");
                        return 3;
                }
            }
            else
            {
                computerAns = random.Next(1, 4);

                switch (computerAns)
                {
                    case 1: //하프 
                        Console.WriteLine("\n컴퓨터: 하프!\n");
                        computerGambleMoney *= 2;
                        Console.WriteLine($"배팅 금액: {computerGambleMoney}");
                        return 1;
                    case 2: //콜
                        computerGambleMoney = playerGambleMoney;
                        Console.WriteLine("\n컴퓨터: 콜!\n");
                        Console.WriteLine($"배팅 금액: {computerGambleMoney}");
                        return 2;
                    case 3: //다이 
                        Console.WriteLine("\n컴퓨터: 다이!");
                        computer.Money -= computerGambleMoney;
                        player.Money += playerGambleMoney;
                        Console.WriteLine($"\n컴퓨터의 소지금: {computer.Money}");
                        Console.WriteLine($"플레이어의 소지금: {player.Money}");
                        return 3;
                }
            }
            //Console.WriteLine($"배팅 금액: {gambleMoney}");
            return 2; //플레이어가 잘못 입력하면 기본적으로 콜

        }

        //플레이어와 컴퓨터의 족보 비교 (플레이어 승: 1, 컴퓨터 승: 0, 둘다 13이면 2)
        static public int CalculateWinner(int[] playerCards, int[] computerCards)
        {
            //플레이어 승이면 1 반환, 컴퓨터 승이면 0 반환, 둘다 13이면 2반환

            if (playerCards[1].Equals(13) && computerCards.Equals(13)) //둘다 13일 경우
            {
                return 2;
            }
            else if (playerCards[1].Equals(12) && computerCards.Equals(12)) // 둘다 원페어(12)인 경우 -> 모양 비교해야
            {
                return 12;
            }
            else if (playerCards[1].Equals(11) && computerCards.Equals(11)) // 둘다 투페어(11)인 경우 -> 모양, 숫자 비교해야
            {
                return 11;
            }
            else if (playerCards[1].Equals(10) && computerCards.Equals(10)) // 둘다 트리플(10)인 경우 -> 숫자 비교해야
            {
                return 10;
            }
            else if (playerCards[1].Equals(9) && computerCards.Equals(9)) // 둘다 스트레이트(9)인 경우 -> 탑 비교해야
            {
                return 9;
            }
            else if (playerCards[1].Equals(6) && computerCards.Equals(6)) // 둘다 플래쉬(6)인 경우 -> 탑 비교해야
            {
                return 6;
            }
            else if (playerCards[1].Equals(5) && computerCards.Equals(5)) // 둘다 풀하우스(5)인 경우 -> 트리플 탑 비교해야
            {
                return 5;
            }
            else if (playerCards[1].Equals(4) && computerCards.Equals(4)) // 둘다 포카(4)인 경우 -> 숫자 비교해야
            {
                return 4;
            }
            else if (playerCards[1].Equals(3) && computerCards.Equals(3)) // 둘다 스티플(3)인 경우 -> 탑 비교해야
            {
                return 3;
            }
            else if (playerCards[1].Equals(computerCards[1])) //만약 포커 조건이 같으면
            {
                if (playerCards[0] < computerCards[0])
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            else if (playerCards[1] < computerCards[1])
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        //정렬된 카드 받아서 포커 유무 판단
        static public void CalculateCardResult(int[] cards, int[] resultArray)
        {

            //13으로 나눈 몫이 같은지
            bool checkQuotient = Equals(cards[0] / 13, cards[1] / 13) && Equals(cards[2] / 13, cards[3] / 13) && Equals(cards[4] / 13, cards[0] / 13) && Equals(cards[4] / 13, cards[2] / 13);
            //13으로 나눈 나머지가 같은지
            bool checkContinuous = Equals(cards[0] + 1, cards[1] % 13) && Equals(cards[1] + 1, cards[2] % 13) && Equals(cards[2] + 1, cards[3] % 13) && Equals(cards[3] + 1, cards[4] % 13);
            //13으로 나눈 나머지가 0 9 10 11 12인지 
            bool checkRSP = Equals(cards[0] % 13, 0) && Equals(cards[1] % 13, 9) && Equals(cards[2] % 13, 10) && Equals(cards[3] % 13, 11) && Equals(cards[4] % 13, 12);

            if (checkQuotient) //만약 카드의 몫이 같으면 !
            {
                if (checkContinuous) //연속인지 판단
                {
                    if ((cards[0] % 13).Equals(0)) //만약 첫 카드의 나머지가 0(에이스)로 시작하면
                    {
                        resultArray[0] = cards[0] / 13; // 13으로 나눈 몫(모양)
                        resultArray[1] = 2; //포커 족보 중 2번
                    }
                    else
                    {
                        resultArray[0] = 5; // 모양이 다르다는 뜻의 5
                        resultArray[1] = 2; //포커 족보 중 3번
                    }
                }
                else //연속이 아니면 
                {
                    if (checkRSP) //나머지가 0 9 10 11 12
                    {
                        resultArray[0] = cards[0] / 13; // 13으로 나눈 몫(모양)
                        resultArray[1] = 1; //포커 족보 중 1번
                    }
                    else
                    {
                        resultArray[0] = cards[0] / 13; // 13으로 나눈 몫(모양)
                        resultArray[1] = 6; //포커 족보 중 6번
                    }
                }
            }
            else //카드의 몫이 다르면
            {
                if (checkContinuous) //나머지가 연속이면
                {
                    if ((cards[0] % 13).Equals(0)) //만약 첫 카드의 나머지가 0(에이스)로 시작하면
                    {
                        resultArray[0] = 5; // 모양이 다르다는 뜻의 5
                        resultArray[1] = 8; //포커 족보 중 8번
                    }
                    else //몫은 다른데 숫자 연속적
                    {
                        resultArray[0] = 5; // 모양이 다르다는 뜻의 5
                        resultArray[1] = 9; //포커 족보 중 9번
                    }
                }
                else //나머지 연속이 아니면
                {
                    if (checkRSP) //나머지가 0 9 10 11 12
                    {
                        resultArray[0] = 5; // 모양이 다르다는 뜻의 5
                        resultArray[1] = 7; //포커 족보 중 7번
                    }
                    else
                    {
                        int[] reminderArray = new int[5];
                        //나머지를 다 저장하고
                        for (int i = 0; i < 5; i++)
                        {
                            reminderArray[i] = cards[i] % 13;
                        }
                        Array.Sort(reminderArray); //정렬

                        //중복값 제거
                        int[] distinctArray = reminderArray.Distinct().ToArray();

                        if (distinctArray.Length.Equals(2)) //3,2 또는 4, 1
                        {
                            int count = 0;
                            for (int i = 0; i < 5; i++)
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    if (!i.Equals(j))
                                    {
                                        if (reminderArray[i].Equals(reminderArray[j]))
                                        {
                                            count++;
                                        }
                                    }
                                }
                            }

                            if (count.Equals(8)) //풀 하우스
                            {
                                resultArray[0] = 5; //모양이 다르다는 뜻의 5
                                resultArray[1] = 5; //포커 족보 중 5번
                            }
                            else //포커
                            {
                                resultArray[0] = 5; //모양이 다르다는 뜻의 5
                                resultArray[1] = 4; //포커 족보 중 4번
                            }

                        }
                        else if (distinctArray.Length.Equals(3)) // 트리플이나 같은거 투페어 
                        {
                            int count = 0;
                            for (int i = 0; i < 5; i++)
                            {
                                for (int j = 0; j < 5; j++)
                                {
                                    if (!i.Equals(j))
                                    {
                                        if (reminderArray[i].Equals(reminderArray[j]))
                                        {
                                            count++;
                                        }
                                    }
                                }
                            }

                            if (count.Equals(6)) //트리플
                            {
                                resultArray[0] = 5; // 모양이 다르다는 뜻의 5
                                resultArray[1] = 10; //포커 족보 중 10번
                            }
                            else //투페어
                            {
                                resultArray[0] = 5; // 모양이 다르다는 뜻의 5
                                resultArray[1] = 11; //포커 족보 중 11번
                            }

                        }
                        else if (distinctArray.Length.Equals(4)) // 원 페어 
                        {
                            resultArray[0] = 5; // 모양이 다르다는 뜻의 5
                            resultArray[1] = 12; //포커 족보 중 12번
                        }
                        else
                        {
                            resultArray[0] = 5; // 모양이 다르다는 뜻의 5
                            resultArray[1] = 13; //포커 족보 중 7번
                        }
                    }
                }
            }
        }

        //둘다 족보가 13 경우 계산 (플레이어 승: 1, 컴퓨터 승: 0 )
        static public int CalculateWinnerBoth13(int[] playerCards, int[] computerCards)
        {
            int[] playerCardsReminder = new int[5];
            int[] computerCardsReminder = new int[5];

            int playerMaxCards = 52, computerMaxCards = 52;

            //각 포커의 나머지를 구한다.
            for (int i = 0; i < 5; i++)
            {
                playerCardsReminder[i] = playerCards[i] % 13;
                computerCardsReminder[i] = computerCards[i] % 13;
            }

            //나머지 배열을 정렬
            Array.Sort(playerCardsReminder);
            Array.Sort(computerCardsReminder);


            //포카 나머지 배열의 최대값 비교
            if (playerCardsReminder[4].Equals(computerCardsReminder[4])) // 최댓값이 같으면
            {
                for (int i = 0; i < 5; i++)
                {
                    if ((playerCards[i] % 13).Equals(playerCardsReminder[4]))
                    {
                        playerMaxCards = playerCards[i];
                    }
                    if ((computerCards[i] % 13).Equals(computerCardsReminder[4]))
                    {
                        computerMaxCards = computerCards[i];
                    }
                }
            }

            //최대 숫자 카드의 모양으로 판단
            if (playerMaxCards < computerMaxCards)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        //둘다 원페어인 경우 계산 
        static public int CalculateWinnerBoth12(int[] playerResult, int[] computerResult)
        {
            return 1;
        }

        //둘다 투페어인 경우 계산 
        static public int CalculateWinnerBoth11(int[] playerResult, int[] computerResult)
        {
            return 1;
        }


        //숫자로 카드 모양 반환하기
        static public string NumToTrumpCard(int num)
        {
            string resultCardString = "";

            switch (num / 13)
            {
                case (int)POKERSUIT.SPADE:
                    resultCardString += "♠";
                    break;
                case (int)POKERSUIT.DIAMOND:
                    resultCardString += "◆";
                    break;
                case (int)POKERSUIT.HEART:
                    resultCardString += "♥";
                    break;
                case (int)POKERSUIT.CLUB:
                    resultCardString += "♣";
                    break;
            }

            int trumpNum = num % 13 + 1;
            switch (trumpNum)
            {
                case 1:
                    resultCardString += "A";
                    break;
                case 11:
                    resultCardString += "J";
                    break;
                case 12:
                    resultCardString += "Q";
                    break;
                case 13:
                    resultCardString += "K";
                    break;
                default:
                    resultCardString += Convert.ToString(num % 13 + 1);
                    break;

            }
            return resultCardString;
        }


        //오픈 카드 보여주기
        static public void PrintOpenCards(List<int> playerOpenCards, List<int> computerOpenCards)
        {

            //컴퓨터 카드
            Console.Write("컴퓨터 오픈 카드: ");
            for (int i = 0; i < computerOpenCards.Count; i++)
            {
                Console.Write($" { NumToTrumpCard(computerOpenCards[i])} ");
            }

            //플레이어 카드
            Console.Write("\n\n플레이어 오픈 카드: ");
            for (int i = 0; i < playerOpenCards.Count; i++)
            {
                Console.Write($" { NumToTrumpCard(playerOpenCards[i])} ");
            }
            Console.WriteLine();
        }

        //플레이어 히든 카드 보여주기
        static public void PrintHiddenCards(List<int> playerHiddenCards)
        {
            //오픈된 카드 보여주기
            //Console.WriteLine("\n플레이어의 히든 카드는 다음과 같습니다.\n");

            //플레이어 카드
            Console.Write("히든 카드: ");
            for (int i = 0; i < playerHiddenCards.Count; i++)
            {
                Console.Write($" { NumToTrumpCard(playerHiddenCards[i])} ");
            }
            Console.WriteLine();
        }

        //7장 중에 5장 랜덤으로 고르기
        static public int[] SelectRandom5Cards(List<int> openCards, List<int> hiddenCards)
        {
            int[] fiveCardsArray = new int[5];
            int[] fiveIndex = new int[5];
            List<int> fiveCardsList = new List<int>();
            List<int> sevenCardsList = new List<int>();

            //오픈 카드와 히든 카드를 하나의 리스트에 추가
            for (int i = 0; i < openCards.Count; i++)
            {
                sevenCardsList.Add(openCards[i]);
            }
            for (int i = 0; i < hiddenCards.Count; i++)
            {
                sevenCardsList.Add(hiddenCards[i]);
            }

            //0~7 중에서 랜덤으로 인덱스를 뽑는데 뽑은 숫자가 있으면 다시 뽑기
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                int tmp = random.Next(7);
                if (Array.Exists(fiveIndex, x => x == tmp))
                {
                    i--;
                }
                else
                {
                    fiveIndex[i] = tmp;
                    fiveCardsList.Add(sevenCardsList[tmp]);
                }
            }
            // Console.WriteLine(string.Join(" ", fiveCardsList));
            fiveCardsArray = fiveCardsList.ToArray();
            return fiveCardsArray;

        }

        //7장 중에 최적의 5장의 카드 고르기
        static public int[] SelectOptimalCards(List<int> openCards, List<int> hiddenCards)
        {
            List<int> sevenCards = new List<int>();
            int[] optimalFiveCards = new int[5]; // 반환할 값 

            //오픈 카드와 히든 카드 다 리스트에 넣기
            for (int i = 0; i < openCards.Count; i++)
            {
                sevenCards.Add(openCards[i]);
            }
            for (int i = 0; i < hiddenCards.Count; i++)
            {
                sevenCards.Add(hiddenCards[i]);
            }


            List<int> tmpCardNumList = new List<int>(); //5장의 카드 저장할 리스트
            int[] tmpCardNumArray = new int[5];
            int[] tmpCardResult = new int[2];
            int maxPoker = 14; // 높은 족보를 저장할 변수

            //7장의 카드 중 5장을 골라서 가장 높은 족보일 경우 저장
            for (int i = 0; i < sevenCards.Count; i++)
            {
                for (int j = 0; j < sevenCards.Count; j++)
                {
                    if (!i.Equals(j))
                    {
                        for (int k = 0; k < sevenCards.Count; k++)
                        {
                            if (!k.Equals(i) && !k.Equals(j)) //만약 k가 i와 j와 다르다면
                            {
                                tmpCardNumList.Add(sevenCards[k]); //k인덱스에 해당하는 카드 값 저장하고
                            }
                        } // 7 Combination 5

                        tmpCardNumArray = tmpCardNumList.ToArray();

                        CalculateCardResult(tmpCardNumArray, tmpCardResult);
                        if (tmpCardResult[1] < maxPoker) // 그동안 저장한 족보중 가장 높은 값보다 작으면 
                        {
                            maxPoker = tmpCardResult[1]; //그 족보값 저장하고 
                            optimalFiveCards = tmpCardNumList.ToArray();
                        }
                        tmpCardNumList.Clear();
                    }

                }
            }

            if (maxPoker.Equals(13)) //만약에 적합한 족보를 찾지 못하면 (13 경우면)
            {
                tmpCardNumList.Clear();

                int maxNum = sevenCards[0];
                int maxNumIndex = 0;
                //13으로 나누었을 때 최대값을 저장
                for (int i = 1; i < sevenCards.Count; i++)
                {
                    if ((maxNum % 13) < (sevenCards[i] % 13)) //나머지가 더 크면 
                    {
                        maxNum = sevenCards[i];
                        maxNumIndex = i;
                    }
                    else if ((maxNum % 13).Equals(sevenCards[i] % 13)) //나머지가 같으면 몫까지 비교..
                    {
                        if (maxNum / 13 < sevenCards[i] / 13)
                        {
                            maxNum = sevenCards[i];
                            maxNumIndex = i;
                        }
                    }
                }
                tmpCardNumList.Add(maxNum);

                for (int i = 0; i < sevenCards.Count; i++)
                {
                    if (!i.Equals(maxNumIndex) && (tmpCardNumList.Count < 5))
                    {
                        tmpCardNumList.Add(sevenCards[i]);
                    }
                }

                optimalFiveCards = tmpCardNumList.ToArray();
            }

            return optimalFiveCards;

        }


        //컴퓨터와 플레이어 5장의 카드 출력하기
        static public void PrintFiveCards(int[] playerCards, int[] computerCards)
        {
            //컴퓨터 카드
            Console.Write("\n컴퓨터 카드 공개: ");
            for (int i = 0; i < computerCards.Length; i++)
            {
                Console.Write($" { NumToTrumpCard(computerCards[i])} ");
            }
            Console.WriteLine();
            //플레이어 카드
            Console.Write("\n플레이어 카드 공개: ");
            for (int i = 0; i < playerCards.Length; i++)
            {
                Console.Write($" { NumToTrumpCard(playerCards[i])} ");
            }
            Console.WriteLine();
        }


        //엔터 입력받고 콘솔 클리어 하기
        static public void InputAndClearConsole()
        {
            Console.ReadLine();
            Console.Clear();
        }
    }

}