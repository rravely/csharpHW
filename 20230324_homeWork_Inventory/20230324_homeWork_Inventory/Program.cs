using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player;

namespace _20230324_homeWork_Inventory
{
    class Program
    {
        static void Main(string[] args)
        {
            Player.Player player = new Player.Player(Player.Player.SetName());

            Console.WriteLine($"\n{player.Name}님 환영합니다. \n");
            player.PrintPlayerInventoryItmeset(player);
            Console.ReadLine();
            Console.Clear();

            player.playerMenu(player);
        }
    }
}
