using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player
{
    class Player
    {
        public string Name { get; set; }
        public List<string> Inventory; //인벤토리
        public List<string> ItemSet; //착용한 아이템

       public Player(string Name)
        {
            this.Name = Name;
            Inventory = new List<string>();
            ItemSet = new List<string>();
        }

        //플레이어의 이름 받기
        static public string SetName()
        {
            Console.Write("플레이어의 이름을 입력해주세요. ");
            string playerName = Console.ReadLine();
            return playerName;
        }

        //플레이어 인벤토리와 착용셋 출력
        public void PrintPlayerInventoryItmeset(Player player)
        {
            Console.Write("<착용>    ");
            Console.Write("| " + string.Join(" | ", player.ItemSet) + " |\n\n");
            Console.Write("<인벤토리>");
            Console.WriteLine("| " + string.Join(" | ", player.Inventory) + " |\n\n");
        }


        //플레이어 메뉴 선택
        public void playerMenu(Player player)
        {
            ConsoleKeyInfo inputKey;
            
            while (true)
            {
                Console.WriteLine("원하는 것을 선택해주세요. \n");
                Console.Write("아이템 생성(←)  제거(↑)  착용(↓)  해제(→)  /  게임 종료(esc) \n");
                inputKey = Console.ReadKey();

                switch (inputKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        CreateItem(player);
                        break;
                    case ConsoleKey.UpArrow:
                        DeleteItem(player);
                        break;
                    case ConsoleKey.DownArrow:
                        FitItem(player);
                        break;
                    case ConsoleKey.RightArrow:
                        ClearItem(player);
                        break;
                }
                if (inputKey.Key.Equals(ConsoleKey.Escape))
                {
                    break;
                }

                PrintPlayerInventoryItmeset(player);
                Console.ReadKey();
                Console.Clear();
            }
            
        }

        //플레이어 아이템 생성
        public void CreateItem(Player player)
        {
            ConsoleKeyInfo inputKey;
            Console.WriteLine("\n생성할 아이템을 선택해주세요.\n");
            Console.WriteLine("도끼(←)  검(↑)  활(↓)  망치(→)\n");

            while (true)
            {
                inputKey = Console.ReadKey(true);
                
                if (inputKey.Key.Equals(ConsoleKey.LeftArrow))
                {
                    player.Inventory.Add("도끼");
                    break;
                }
                else if (inputKey.Key.Equals(ConsoleKey.UpArrow))
                {
                    player.Inventory.Add("검  ");
                    break;
                }
                else if (inputKey.Key.Equals(ConsoleKey.DownArrow))
                {
                    player.Inventory.Add("활  ");
                    break;
                }
                else if (inputKey.Key.Equals(ConsoleKey.RightArrow))
                {
                    player.Inventory.Add("망치");
                    break;
                }
            }


            
        }

        //플레이어 인벤토리의 아이템 제거
        public void DeleteItem(Player player)
        {
            ConsoleKeyInfo inputKey;
            bool checkContains;
            Console.WriteLine("제거할 아이템을 선택해주세요.\n");
            PrintPlayerInventoryItmeset(player);
            Console.WriteLine("도끼(←)  검(↑)  활(↓)  망치(→)\n");

            while (true)
            {
                inputKey = Console.ReadKey(true);
                //입력받은 키에 해당하는 아이템이 인벤토리에 있는지 확인
                checkContains = player.Inventory.Contains(InputKeyConverToString(inputKey));

                if (inputKey.Key.Equals(ConsoleKey.LeftArrow))
                {
                    CheckContainAndDelete(checkContains, player, "도끼");
                    break;
                }
                else if (inputKey.Key.Equals(ConsoleKey.UpArrow))
                {
                    CheckContainAndDelete(checkContains, player, "검  ");
                    break;
                }
                else if (inputKey.Key.Equals(ConsoleKey.DownArrow))
                {
                    CheckContainAndDelete(checkContains, player, "활  ");
                    break;
                }
                else if (inputKey.Key.Equals(ConsoleKey.RightArrow))
                {
                    CheckContainAndDelete(checkContains, player, "망치");
                    break;
                }
            }
        }

        //플레이어 아이템 착용
        public void FitItem(Player player)
        {
            ConsoleKeyInfo inputKey;
            bool checkContains;
            Console.WriteLine("착용할 아이템을 선택해주세요. \n");
            PrintPlayerInventoryItmeset(player);
            Console.WriteLine("도끼(←)  검(↑)  활(↓)  망치(→)\n");

            while (true)
            {
                inputKey = Console.ReadKey(true);
                //입력받은 키에 해당하는 아이템이 인벤토리에 있는지 확인
                checkContains = player.Inventory.Contains(InputKeyConverToString(inputKey));

                if (inputKey.Key.Equals(ConsoleKey.LeftArrow))
                {
                    CheckContainAndRemoveFit(checkContains, player, "도끼");
                    break;
                }
                else if (inputKey.Key.Equals(ConsoleKey.UpArrow))
                {
                    CheckContainAndRemoveFit(checkContains, player, "검  ");
                    break;
                }
                else if (inputKey.Key.Equals(ConsoleKey.DownArrow))
                {
                    CheckContainAndRemoveFit(checkContains, player, "활  ");
                    break;
                }
                else if (inputKey.Key.Equals(ConsoleKey.RightArrow))
                {
                    CheckContainAndRemoveFit(checkContains, player, "망치");
                    break;
                }
            }

        }

        //플레이어 아이템 해제
        public void ClearItem(Player player)
        {
            ConsoleKeyInfo inputKeyItem, inputKeyDelete;
            bool checkContains, checkDelete;
            Console.WriteLine("해제할 아이템을 선택해주세요. \n");
            PrintPlayerInventoryItmeset(player);
            Console.WriteLine("도끼(←)  검(↑)  활(↓)  망치(→)\n");

            inputKeyItem = Console.ReadKey(true);

            //입력받은 키에 해당하는 아이템이 인벤토리에 있는지 확인
            checkContains = player.ItemSet.Contains(InputKeyConverToString(inputKeyItem));
            if (checkContains)
            {
                //아이템 버릴지 인벤토리에 넣을지 고르기
                Console.WriteLine("인벤토리에 넣기(↑)  버리기(↓)");
                while (true)
                {
                    inputKeyDelete = Console.ReadKey(true);
                    if (inputKeyDelete.Key.Equals(ConsoleKey.UpArrow)) //인벤토리에 넣기를 선택하면
                    {
                        checkDelete = false; //제거 false
                        break;
                    }
                    else if (inputKeyDelete.Key.Equals(ConsoleKey.DownArrow)) //버리기를 선택하면
                    {
                        checkDelete = true; //제거 true
                        break;
                    }
                }

                while (true)
                {
                    if (inputKeyItem.Key.Equals(ConsoleKey.LeftArrow))
                    {
                        CheckContainAndRemoveOrDelete(checkDelete, player, "도끼");
                        break;
                    }
                    else if (inputKeyItem.Key.Equals(ConsoleKey.UpArrow))
                    {
                        CheckContainAndRemoveOrDelete(checkDelete, player, "검  ");
                        break;
                    }
                    else if (inputKeyItem.Key.Equals(ConsoleKey.DownArrow))
                    {
                        CheckContainAndRemoveOrDelete(checkDelete, player, "활  ");
                        break;
                    }
                    else if (inputKeyItem.Key.Equals(ConsoleKey.RightArrow))
                    {
                        CheckContainAndRemoveOrDelete(checkDelete, player, "망치");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("\n해당 아이템을 착용하지 않았습니다.");
            }
        }

        //입력받은 키에 해당하는 아이템 이름 반환
        public string InputKeyConverToString(ConsoleKeyInfo cki)
        {
            switch (cki.Key)
            {
                case ConsoleKey.LeftArrow:
                    return "도끼";
                case ConsoleKey.UpArrow:
                    return "검  ";
                case ConsoleKey.DownArrow:
                    return "활  ";
                default:
                    return "망치";
            }
        }

        //존재 여부에 따라 존재하면 제거하고 아니면 없다고 출력
        public void CheckContainAndDelete(bool checkContains, Player player, string itemName)
        {
            if (checkContains) //존재하면
            {
                player.Inventory.Remove(itemName); //착용에 추가
            }
            else
            {
                Console.WriteLine("\n인벤토리에 해당 아이템이 없습니다.");
            }
        }

        //존재 여부에 따라 존재하면 제거하고 착용하기
        public void CheckContainAndRemoveFit(bool checkContains, Player player, string itemName)
        {
            if (checkContains) //존재하면
            {
                player.Inventory.Remove(itemName); //인벤토리에서 제거하고
                player.ItemSet.Add(itemName); //착용셋에 추가
            }
            else
            {
                Console.WriteLine("\n인벤토리에 해당 아이템이 없습니다.");
            }
        }

        public void CheckContainAndRemoveOrDelete(bool checkDelete, Player player, string itemName)
        {
            if (checkDelete) //착용 해제를 선택하면
            {
                player.ItemSet.Remove(itemName); //착용셋에서 해제하고 버림
            }
            else //버리기를 선택하면
            {
                player.ItemSet.Remove(itemName); //착용셋에서 해제하고
                player.Inventory.Add(itemName); //인벤토리에 추가
            }
        }
    }
}
