using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20230315_homeWork_UpDown
{
    class Program
    {
        static void Main(string[] args)
        {
            /*3. 컴퓨터 숫자 맞추기
             컴퓨터는 0~999까지 랜덤하게 하나의 수
             플레이어는 10번의 기회가 있다.
             플레이어가 수를 입력하면 컴퓨터는 그 수가 큰지 작은지 정답인지 알려준다.
             정답을 맞추면 해당게임 종료
            */

            int targetNum, userNum, turn = 1;

            Random randomNum = new Random();
            targetNum = randomNum.Next(1000);

            Console.WriteLine("* 숫자 맞추기 * ");
            Console.WriteLine("숫자를 입력해주세요. (0과 999사이의 정수)");

            while (true)
            {
                userNum = int.Parse(Console.ReadLine());
                Console.WriteLine(targetNum);
                if (targetNum == userNum)
                {
                    Console.WriteLine("정답입니다.");
                    break;
                }
                else if (userNum < 0 || userNum >= 1000)
                {
                    Console.WriteLine("0과 999사이의 정수를 입력해주세요. ");
                    turn--;
                }

                else if (targetNum < userNum) {
                    Console.WriteLine("정답보다 큽니다. 다시 입력해주세요.");
                }
                else 
                {
                    Console.WriteLine("정답보다 작습니다. 다시 입력해주세요.");
                }

                Console.WriteLine($"남은 턴: {10 - turn}");
                Console.WriteLine();
                turn++;

                if (turn == 11)
                {
                    Console.WriteLine("턴을 모두 소진했습니다. 게임 종료");
                    break;
                }
            }

            
        }
    }
}
