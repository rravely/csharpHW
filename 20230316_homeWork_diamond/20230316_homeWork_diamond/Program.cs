using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20230316_homeWork_diamond
{
    class Program
    {
        static void Main(string[] args)
        {
            //2. 다이아몬드
            //출력할 다이아몬드 형태를 사용자로부터 입력을 받는다
            //만약 짝수라면 홀수를 다시 입력하고 사용자에게 무한으로 요구 반복
            //홀수면 다이아몬드의 중간 부분의 별 숫자와 사용자의 입력과 같은 다이아몬드 출력

            int inputNum;

            while (true)
            {
                Console.Write("홀수를 입력해주세요. ");
                inputNum = int.Parse(Console.ReadLine());

                if (inputNum % 2 == 1)
                {
                    break;
                }
            }

            for (int i = -(inputNum / 2); i < (inputNum / 2) + 1; i++)
            {
                for (int j = 1; j <= Math.Abs(i); j++)
                {
                    Console.Write(' ');
                }
                for (int j = 1; j <= inputNum - Math.Abs(i) * 2; j++)
                {
                    Console.Write('*');
                }
                
                Console.WriteLine();
            }
        }
    }
}
