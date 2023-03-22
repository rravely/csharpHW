using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20230316_homeWork_star
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputNum;

            Console.Write("출력할 별 수를 입력해주세요. ");
            inputNum = int.Parse(Console.ReadLine());

            //1. 별 만들기

            Console.WriteLine("1번 별..");
            
            for (int i = 1; i < inputNum + 1; i++)
            {
                for (int j = 1; j < i + 1;j++)
                {
                    Console.Write('*');
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("2번 별..");

            for (int i = 1; i < inputNum + 1; i++)
            {
                for (int j = 1; j < inputNum - i + 1; j++)
                {
                    Console.Write(' ');
                }
                for (int k = 1; k < i + 1; k++)
                {
                    Console.Write('*');
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("3번 별..");
            for (int i = inputNum; i > 0; i--)
            {
                for (int k = 1; k < i + 1; k++)
                {
                    Console.Write('*');
                }
                for (int j = 1; j < inputNum - i + 1; j++)
                {
                    Console.Write(' ');
                }
                Console.WriteLine();
            }

            Console.WriteLine("4번 별..");
            for (int i = inputNum; i > 0; i--)
            {
                for (int j = 1; j < inputNum - i + 1; j++)
                {
                    Console.Write(' ');
                }
                for (int k = 1; k < i + 1; k++)
                {
                    Console.Write('*');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
