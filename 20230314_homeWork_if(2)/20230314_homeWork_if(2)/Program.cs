using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20230314_homeWork_if_2_
{
    class Program
    {
        static void Main(string[] args)
        {
            //3 - 2.두개의 정수를 입력받아 두 정수의 차를 출력한다
            //단 입력된 숫자가 순서의 상관없이 무조건 큰 수에서 작은수를 뺀 값을 출력

            int numA, numB, res;

            Console.WriteLine("사칙연산 프로그램");
            Console.ReadLine();
            Console.WriteLine("두 수를 차례로 입력해주세요.");

            numA = int.Parse(Console.ReadLine());
            numB = int.Parse(Console.ReadLine());

            if (numA >= numB)
            {
                res = numA - numB;
                Console.WriteLine("두 수의 차는: {0}", res);
            }
            else
            {
                res = numB - numA;
                Console.WriteLine("두 수의 차는: {0}", res);
            }
        }
    }
}
