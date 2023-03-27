using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20230327_homeWork_stackWithArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int stackSize = 0;
            string[] stack = new string[20];
            string inputData;

            while (true)
            {
                Console.WriteLine("스택에 넣기(↓), 빼기(↑)");
                ConsoleKeyInfo inputCkey;
                
                while (true) //키 받아서 처리
                {
                    inputCkey = Console.ReadKey();

                    if (inputCkey.Key.Equals(ConsoleKey.DownArrow))
                    {
                        //스택에 쌓을 데이터 받기
                        Console.WriteLine("스택에 넣을 데이터를 입력해주세요. ");
                        inputData = Console.ReadLine();
                        push(ref stackSize, stack, inputData);
                        break;
                    }
                    else if (inputCkey.Key.Equals(ConsoleKey.UpArrow))
                    {
                        pop(ref stackSize, stack);
                        break;
                    }
                }

                PrintCurrentStack(stackSize, stack);
            }
        }
        

        static void push(ref int stackSize, string[] stack, string inputData)
        {
            if (stackSize == 20)
            {
                Console.WriteLine("더 이상 스택에 넣을 수 없습니다.");
            }
            else
            {
                stack[stackSize] = inputData; //스택 배열에 값을 넣고
                stackSize += 1; //인덱스 1증가
            }
        }

        static void pop(ref int stackSize, string[] stack)
        {
            if (stackSize.Equals(0)) //스택에 값이 없으면
            {
                Console.WriteLine("현재 스택에 들어있는 것이 없습니다.");
            }
            else
            {
                stackSize -= 1; //스택 크기 하나 감소 
            }
        }

        static void PrintCurrentStack(int stackSize, string[] stack)
        {
            Console.WriteLine();
            Console.WriteLine(" <현재 스택> ");
            Console.Write("|");
            for (int i = 0; i < stackSize; i++)
            {
                Console.Write($"  {stack[i]}  |");
            }
            Console.WriteLine();
        }
    }
}
