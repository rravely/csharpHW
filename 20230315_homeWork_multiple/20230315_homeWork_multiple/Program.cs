using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20230315_homeWork_multiple
{
    class Program
    {
        static void Main(string[] args)
        {
            /*과제1. 원하는 구구단 출력하기
             ex. 2를 입력하면 
                2 * 1 = 2
                2 * 2 = 4
                ...
             */

            int inputNum = 0, count = 1, result;
            string SinputNum;

            while (true)
            {
                Console.WriteLine("구구단 출력하고 싶은 수를 입력하세요. (범위 1 ~ 9)");
                SinputNum = Console.ReadLine();

                try
                {
                    inputNum = int.Parse(SinputNum);

                    if (inputNum > 0 && inputNum < 10) //*실수 처리 안함
                    {
                        while (count < 10)
                        {
                            result = inputNum * count;
                            Console.WriteLine($"{inputNum} * {count} = {result}");
                            count++;
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("1과 9사이의 정수를 입력해주세요.");
                        Console.WriteLine();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("{0}은 정수가 아닙니다.", SinputNum);
                    Console.WriteLine();
                }

                
            }

        }
    }
}
