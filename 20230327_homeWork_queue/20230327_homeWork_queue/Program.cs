using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20230327_homeWork_queue
{
    class Program
    {
        static void Main(string[] args)
        {
            //플레이어의 대기열 생성
            Queue<string> playerQueue = new Queue<string>();

            //대기열에 추가할 플레이어 이름
            string playerName;

            while (true)
            {
                playerName = InputPlayerName();
                PlayerQueue(playerName, playerQueue); //플레이어 대기열에 넣기
            }
        }

        static void PlayerQueue(string playerName, Queue<string> playerQueue)
        {
            if (playerQueue.Count.Equals(3)) //플레이어 큐에 4명이 되면
            {
                Console.WriteLine($"{playerName}님을 포함한 4명의 플레이어가 사냥을 떠났습니다. \n");

                /*
                //대기열에서 삭제
                for (int i = 0; i < playerQueue.Count; i++)
                {
                    playerQueue.Dequeue();
                }
                */
                playerQueue.Clear(); //큐 비우기
                Console.WriteLine($"현재 대기열: {playerQueue.Count}명");
            }
            else
            {
                playerQueue.Enqueue(playerName);
                Console.WriteLine($"현재 대기열: {playerQueue.Count}명");
                
                //대기열에 있는 사람 출력
                foreach (object name in playerQueue) 
                {
                    Console.Write($"{name} ");
                }
                Console.WriteLine();
            }
        }

        static string InputPlayerName()
        {
            string playerName;
            Console.Write("대기열에 입장할 플레이어 이름: ");
            
            //이름이 들어올 때까지 입력하게 만들기
            while (true)
            {
                playerName = Console.ReadLine();
                if (playerName.Equals(""))
                {
                    continue;
                }
                else
                {
                    Console.WriteLine();
                    return playerName;
                }
            }
        }
    }
}
