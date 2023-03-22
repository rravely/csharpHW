using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20230317_homeWork_baseballWithNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            const int ansCnt = 3;
            int userNumIndex = 0, userNumTemp, count = 0;
            int strikeNum = 0, ballNum = 0, outNum = 0; //
            int[] baseballNum = new int[9];

            int[] baseballThreeNum = new int[3];
            int[] userNum = new int[3];

            //맞췄는지 안맞췄는지 확인
            bool userCorrect = false;

            //셔플에 사용할 변수
            int dest, sour, temp;

            Random random = new Random();

            //배열에 숫자 차례로 저장
            for (int i=0; i<baseballNum.Length; i++)
            {
                baseballNum[i] = i + 1;
            }

            while (true)
            {
                //10판 넘어가면 게임 종료
                if (count > 9)
                {
                    Console.WriteLine("가능한 턴을 다 소진했습니다. GAME OVER");
                    break;
                }

                if (userCorrect == true || count == 0) //유저가 맞췄거나 첫판일때
                { 
                    //배열 숫자 섞기
                    for (int i = 0; i < 1000; i++)
                    {
                        dest = random.Next(9);
                        sour = random.Next(9);

                        temp = baseballNum[dest];
                        baseballNum[dest] = baseballNum[sour];
                        baseballNum[sour] = temp;
                    }

                    //컴퓨터 랜덤 3개의 수 뽑기
                    for (int i = 0; i < ansCnt; i++)
                    {
                        baseballThreeNum[i] = baseballNum[i];
                    }

                    userCorrect = false; //다시 못맞춘걸로 바꿔주기
                }

                //유저 3개의 수 입력
                while (userNumIndex < 3)
                {
                    Console.WriteLine("1 ~ 9까지의 숫자 중 하나를 선택해주세요. (중복 불가)");
                    userNumTemp = int.Parse(Console.ReadLine());

                    if (Array.Exists(userNum, x => x == userNumTemp)) //만약 입력한 값이 이미 존재한다면
                    {
                        Console.WriteLine("중복되지 않은 숫자를 입력해주세요.");
                        continue;
                    }
                    else
                    {
                        userNum[userNumIndex] = userNumTemp;
                        userNumIndex++;
                    }
                }

                //숫자 3개 확인
                Console.Write("유저가 선택한 수: ");
                Console.WriteLine(String.Join(", ", userNum));
                Console.Write("야구 숫자: ");
                Console.WriteLine(String.Join(", ", baseballThreeNum));


                //숫자 확인
                for (int i = 0; i < ansCnt; i++)
                {
                    for (int j = 0; j < ansCnt; j++)
                    {
                        if (userNum[i] == baseballNum[j])
                        {
                            if (i == j)
                            {
                                strikeNum++;
                            }
                            else
                            {
                                ballNum++;
                            }
                        }
                    }
                }

                //strike 3이면(유저가 정답을 맞추면) out 처리
                if (strikeNum == 3)
                {
                    //out 1 증가
                    outNum++;

                    //strike, ball 초기화
                    strikeNum = 0;
                    ballNum = 0;

                    //맞춤 확인
                    userCorrect = true;
                }

                //3out이면 종료
                if (outNum == 3)
                {
                    Console.WriteLine("3 out. GAME OVER");
                    break;
                }

                

                Console.WriteLine("{0} out, {1} strike, {2} ball", outNum, strikeNum, ballNum);
                Console.WriteLine("===========================================================");
                Console.WriteLine();

                //유저 선택 배열 초기화
                userNumIndex = 0;
                for (int i = 0; i < 3; i++)
                {
                    userNum[i] = 0;
                }

                //strike, ball 초기화
                strikeNum = 0;
                ballNum = 0;

                count++;
            }
        }
    }
}
