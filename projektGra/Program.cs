using System;
using System.Collections.Generic;

namespace projektGra
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.CursorVisible = false;
            while (true)
            {
                Game.Start();
                GUI.LoadMenu(false);
                Game.GenerateMap();
                while (Game.inProgress)
                {
                    GUI.InitializeGUI();
                    GUI.UpdateInventory();
                    Game.SelectLevel();
                    if (Game.inProgress) Game.GenerateLevel();
                }
            }

        }
    }
}
