using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20230314_homeWork_if
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            3-1.입력을 두개를 받는다
            사칙연산 프로그램 만들기
            1 덧셈
            2 뺄셈
            3 곱하기
            4 나누기
            이후 연산 결과 보여주기
            두 수의 덧셈 결과: 5
             */

            //1. 사칙연산 프로그램
            float numA, numB, res;
            int ans;

            Console.WriteLine("사칙연산 프로그램");
            Console.ReadLine();
            Console.WriteLine("두 수를 차례로 입력해주세요.");

            numA = float.Parse(Console.ReadLine());
            numB = float.Parse(Console.ReadLine());

            Console.WriteLine("===========================");
            Console.WriteLine("원하는 사칙연산을 선택하세요.");
            Console.WriteLine("1. 더하기\n2. 빼기\n3. 곱하기\n4. 나누기");
            Console.WriteLine("===========================");
            Console.Write("원하는 사칙연산: ");

            ans = int.Parse(Console.ReadLine());

            if (ans == 1)
            {
                res = numA + numB;
                Console.WriteLine("두 수의 덧셈 결과: {0}", res);
            }
            else if (ans == 2)
            {
                res = numA - numB;
                Console.WriteLine("두 수의 뺄셈 결과: {0}", res);
            }
            else if (ans == 3)
            {
                res = numA * numB;
                Console.WriteLine("두 수의 곱셈 결과: {0}", res);
            }
            else
            {
                res = numA / numB;
                Console.WriteLine("두 수의 나눗셈 결과: {0}", res);
            }

            

        }
    }
}
