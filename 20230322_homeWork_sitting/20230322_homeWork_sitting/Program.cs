using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student;

namespace _20230322_homeWork_sitting
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
            string[] studentInfo1DArray = new string[studentNum * 3];
            string[,] studentInfo2DArray = new string[studentNum, 3];

            //하나의 문자열 29명의 학생으로 나누기
            studentInfo1DArray = studentInfo.Split();
            for( int i = 0; i < studentNum * 3; i++)
            {
                Console.WriteLine(studentInfo1DArray[i]);
            }


            //29명의 학생 정보를 2차원 배열에 담기
            for (int i = 0; i < studentNum * 3; i++)
            {
                //string[] tmp = studentInfo1DArray[i].Split(' ');
                //studentInfo2DArray[i, 0] = studentInfo1DArray
                //studentInfo2DArray[i, 1] = studentInfo1DArray
                //studentInfo2DArray[i, 2] = studentInfo1DArray

                Console.WriteLine(studentInfo1DArray[i * 3]);
            }
            */

            int studentNum = 29;
            int[] studentRandomArray = new int[studentNum];
            string[] shuffleSittingArray = new string[studentNum];

            string inputName;

            ConsoleKeyInfo cki;


            //학생 정보 입력 ㅗ
            Student_info student1 = new Student_info("강단이", "여", "010-2676-3790");
            Student_info student2 = new Student_info("강윤석", "남", "010-9913-9662");
            Student_info student3 = new Student_info("경민아", "여", "010-2256-2104");
            Student_info student4 = new Student_info("고석환", "남", "010-9635-0224");
            Student_info student5 = new Student_info("공병현", "남", "010-9807-2898");
            Student_info student6 = new Student_info("구윤성", "남", "010-6677-5821");
            Student_info student7 = new Student_info("국동근", "남", "010-2340-2804");
            Student_info student8 = new Student_info("김대현", "남", "010-8692-2778");
            Student_info student9 = new Student_info("김민수", "남", "010-8802-6512");
            Student_info student10 = new Student_info("김윤혜", "여", "010-4434-0767");
            Student_info student11 = new Student_info("김현희", "여", "010-4043-9136");
            Student_info student12 = new Student_info("노소영", "여", "010-9037-0225");
            Student_info student13 = new Student_info("목지아", "여", "010-9042-7403");
            Student_info student14 = new Student_info("박라현", "여", "010-7653-2820");
            Student_info student15 = new Student_info("박상연", "남", "010-7528-9060");
            Student_info student16 = new Student_info("박주혜", "여", "010-4204-9704");
            Student_info student17 = new Student_info("성민경", "남", "010-3941-1247");
            Student_info student18 = new Student_info("양경훈", "남", "010-2988-6736");
            Student_info student19 = new Student_info("이도엽", "남", "010-5174-5496");
            Student_info student20 = new Student_info("이상훈", "남", "010-3505-2135");
            Student_info student21 = new Student_info("이성준", "남", "010-9519-1558");
            Student_info student22 = new Student_info("이수민", "남", "010-2959-6265");
            Student_info student23 = new Student_info("이유리", "여", "010-2094-3496");
            Student_info student24 = new Student_info("임종서", "남", "010-7657-7526");
            Student_info student25 = new Student_info("정수민", "남", "010-3456-6254");
            Student_info student26 = new Student_info("제현준", "남", "010-2230-4610");
            Student_info student27 = new Student_info("주성현", "남", "010-9524-3357");
            Student_info student28 = new Student_info("최영일", "남", "010-2123-7048");
            Student_info student29 = new Student_info("허진", "여", "010-4637-2880");

            //학생 정보를 클래스 배열에 넣고
            Student_info[] studentInfo = new Student_info[29] { student1, student2, student3, student4, student5, student6, student7, student8, student9, student10, student11, student12, student13, student14, student15, student16, student17, student18, student19, student20, student21, student22, student23, student24, student25, student26, student27, student28, student29};


            //원래 자리 배치 출력하기
            Console.WriteLine("\n ~ 원래 자리 ~\n");
            PrintStudentwith8line(studentInfo);


            //학생 정보 섞기
            Student_info[] shuffleStudentInfo = ShuffleStudent(studentInfo, studentNum);


            //자리 배치 출력하기
            Console.WriteLine("\n\n~ 섞은 자리 ~\n");
            PrintStudentwith8line(shuffleStudentInfo);

            

            //정보 출력하기
            while (true)
            {
                Console.WriteLine("종료하려면 ESC를 누르고 계속하려면 Enter를 눌러주세요. \n");


                cki = Console.ReadKey();
                if (cki.Key.Equals(ConsoleKey.Escape)) //esc 키를 누르면 종료
                {
                    break;
                }

                Console.Write("정보를 원하는 사람의 이름을 입력해주세요. ");


                inputName = Console.ReadLine();

                int inputNameIndex = studentNum;

                for (int i = 0; i < studentNum; i++)
                {
                    if (studentInfo[i].Name.Equals(inputName))
                    {
                        inputNameIndex = i;
                        break;
                    }
                }

                if (inputNameIndex.Equals(studentNum)) //만약 입력한 이름이 없으면
                {
                    Console.WriteLine("입력하신 이름의 없습니다. \n");
                }
                else //있으면 출력
                {
                    string printConsole = string.Format($"\n이름:{studentInfo[inputNameIndex].Name}  " +
                    $"성별: {studentInfo[inputNameIndex].Gender}  " +
                    $"전화번호: {studentInfo[inputNameIndex].PhoneNumber}\n");
                    Console.WriteLine(printConsole);
                }
                
            }


        }

        //학생 정보 섞고 그 배열 반환
        static public Student_info[] ShuffleStudent(Student_info[] studentInfo, int studentNum)
        {
            int dest, sour;
            Student_info temp;

            Random random = new Random();


            for (int i = 0; i < studentNum; i++)
            {
                dest = random.Next(studentNum);
                sour = random.Next(studentNum);

                temp = studentInfo[dest];
                studentInfo[dest] = studentInfo[sour];
                studentInfo[sour] = temp;
            }

            return studentInfo;
        }

        //자리에 맞게 학생 출력하기
        static void PrintStudentwith8line(Student_info[] shuffleStudentInfo)
        {
            int lineNum = shuffleStudentInfo.Length;

            Console.WriteLine("----------------------------------------------------------");            

            for (int i = 0; i < lineNum / 8; i++)
            {
                string[] tmp = new string[8];

                for (int j = 0; j < 8; j++) {
                    tmp[j] = shuffleStudentInfo[i * 8 + j].Name;
                    if (tmp[j].Length.Equals(2))
                    {
                        tmp[j] += "  ";
                    }

                }
                if (i == 0) //만약 첫번째 줄이라면
                {
                    Console.WriteLine($"{tmp[0]} {tmp[1]} {tmp[2]} {tmp[3]}    {tmp[4]} {tmp[5]} {tmp[6]}");
                }
                else
                {
                    Console.WriteLine($"{tmp[0]} {tmp[1]} {tmp[2]} {tmp[3]}    {tmp[4]} {tmp[5]} {tmp[6]} {tmp[7]}");
                }
                Console.WriteLine("----------------------------------------------------------");
            }

            //마지막 줄 처리
            Console.WriteLine($"{shuffleStudentInfo[7].Name} {shuffleStudentInfo[28].Name}");
            Console.WriteLine("----------------------------------------------------------");
        }

        
    }
}
