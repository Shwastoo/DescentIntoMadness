using System;
using System.Collections.Generic;
using System.Text;

namespace projektGra
{
    class Controls
    {
        public static Random Rnd = new Random();
        public static List<ConsoleKey> QTE = new List<ConsoleKey> { ConsoleKey.W, ConsoleKey.S, ConsoleKey.A, ConsoleKey.D };
        public static void AwaitingInput()
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            GUI.ClearInfoBox();
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (Game.currLevel.Theme == Palettes.Russia && !Game.player.Inv.Contains(Items.Pills)) Movement(Game.player.PosX, Game.player.PosY + 1);
                    else Movement(Game.player.PosX, Game.player.PosY - 1);
                    if (Game.currLevel.Theme == Palettes.Ice && !Game.player.Inv.Contains(Items.Boots))
                    {
                        Game.player.Sanity += 1;
                        Movement(Game.player.PosX, Game.player.PosY - 1);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (Game.currLevel.Theme == Palettes.Russia && !Game.player.Inv.Contains(Items.Pills)) Movement(Game.player.PosX, Game.player.PosY - 1);
                    else Movement(Game.player.PosX, Game.player.PosY + 1);
                    if (Game.currLevel.Theme == Palettes.Ice && !Game.player.Inv.Contains(Items.Boots))
                    {
                        Game.player.Sanity += 1;
                        Movement(Game.player.PosX, Game.player.PosY + 1); 
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (Game.currLevel.Theme == Palettes.Russia && !Game.player.Inv.Contains(Items.Pills)) Movement(Game.player.PosX+1, Game.player.PosY);
                    else Movement(Game.player.PosX - 1, Game.player.PosY);
                    if (Game.currLevel.Theme == Palettes.Ice && !Game.player.Inv.Contains(Items.Boots))
                    {
                        Game.player.Sanity += 1;
                        Movement(Game.player.PosX - 1, Game.player.PosY);
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (Game.currLevel.Theme == Palettes.Russia && !Game.player.Inv.Contains(Items.Pills)) Movement(Game.player.PosX-1, Game.player.PosY);
                    else Movement(Game.player.PosX + 1, Game.player.PosY);
                    if (Game.currLevel.Theme == Palettes.Ice && !Game.player.Inv.Contains(Items.Boots))
                    {
                        Game.player.Sanity += 1;
                        Movement(Game.player.PosX + 1, Game.player.PosY);
                    }
                    break;
                case ConsoleKey.C:
                    if (Game.player.Food > 0)
                    {
                        Game.ConsumeFood(false);
                        //Game.Turn();
                    }
                    break;
                case ConsoleKey.H:
                    GUI.ShowLegend(false);
                    break;
                case ConsoleKey.F1:
                    Game.levelProgress = true;
                    break;
                case ConsoleKey.F2:
                    Game.Endgame();
                    break;
            }
        }
        private static void Movement(int x, int y)
        {
            string posToMove = Game.currLevel.CurrentRoom.Board[y][x];
            if (posToMove == Tiles.Empty)
            {
                Game.player.UpdatePos(x, y);
            }
            else if(posToMove == Tiles.Food)
            {
                MusicPlay.CollectFood();
                Game.player.Food++;
                if (Game.currLevel.Theme == Palettes.Candy && !Game.player.Inv.Contains(Items.Liquorice))
                {
                    Game.ConsumeFood(false);
                }
                else
                {
                    GUI.PrintInfo("You collected food");
                }
                Game.player.UpdatePos(x, y);
            }
            else if(posToMove == Tiles.Crate)
            {
                Game.player.GetItem();
                Game.player.UpdatePos(x, y);

            }
            else if(posToMove == Tiles.DoorHor)
            {
                Game.currLevel.Minimap[Game.currLevel.CurrentRoom.posY][Game.currLevel.CurrentRoom.posX] = Tiles.Room;
                Game.currLevel.CurrentRoom.Board[Game.player.PosY][Game.player.PosX] = Tiles.Empty;
                if (y == 0)
                {
                    Game.currLevel.CurrentRoom = Game.currLevel.CurrentRoom.Up;
                    Game.player.UpdatePos(x, Game.currLevel.CurrentRoom.Height - 2);
                }
                else
                {
                    Game.currLevel.CurrentRoom = Game.currLevel.CurrentRoom.Down;
                    Game.player.UpdatePos(x, 1);
                }
            }
            else if (posToMove == Tiles.DoorVer)
            {
                Game.currLevel.Minimap[Game.currLevel.CurrentRoom.posY][Game.currLevel.CurrentRoom.posX] = Tiles.Room;
                Game.currLevel.CurrentRoom.Board[Game.player.PosY][Game.player.PosX] = Tiles.Empty;
                if (x == 0)
                {
                    Game.currLevel.CurrentRoom = Game.currLevel.CurrentRoom.Left;
                    Game.player.UpdatePos(Game.currLevel.CurrentRoom.Width-2, y);
                }
                else
                {
                    Game.currLevel.CurrentRoom = Game.currLevel.CurrentRoom.Right;
                    Game.player.UpdatePos(1, y);
                }
            }
            else if(posToMove == Tiles.Exit)
            {
                Game.levelProgress = true;
                GUI.PrintInfo("Level complete!");
            }
            else if((posToMove == Tiles.Rock || posToMove == Tiles.Wall) && !Game.player.Inv.Contains(Items.Gloves))
            {
                MusicPlay.DMG();
                Game.player.HP -= 50;
                GUI.PrintInfo("You run into an obstacle and injured yourself");
            }
            else if(posToMove == Tiles.Mush)
            {
                if (!Game.player.Inv.Contains(Items.Mask))
                {
                    MusicPlay.DMG();
                    Game.player.HP -= 100;
                    GUI.PrintInfo("You run into poisonous mushroom and got sick");
                }
                Game.player.UpdatePos(x, y);
            }
            else if (posToMove == Tiles.Monkey)
            {
                if (GUI.MonkeyFight())
                {
                    Game.player.UpdatePos(x, y);
                    foreach (Enemies m in Game.currLevel.CurrentRoom.Monkeys)
                    {
                        if (m.PosX == x && m.PosY == y)
                        {
                            Game.currLevel.CurrentRoom.Monkeys.Remove(m);
                            break;
                        }
                    }
                }
                else
                {
                    MusicPlay.DMG();
                    Game.player.HP -= 100;
                }
            }
            Game.currLevel.Minimap[Game.currLevel.CurrentRoom.posY][Game.currLevel.CurrentRoom.posX] = Tiles.CurRoom;
            if (Game.currLevel.Theme != Palettes.Hell || Game.player.Inv.Contains(Items.Deal)) GUI.UpdateMinimap();
            Game.Turn();
        }
        public static void LevelSelection()
        {
            int y = Game.lSelY;
            int x = Game.lSelX;
            List<List<Level>> l = Game.lSelection;
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (y > 0) Game.lSelY--;
                    break;
                case ConsoleKey.DownArrow:
                    if (y < l[x].Count-1) Game.lSelY++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (x > 0)
                    {
                        Game.lSelX--;
                        if (l[x-1].Count == 3 || l[x-1].Count == 5 || l[x-1].Count == 1) Game.lSelY = l[x-1].Count / 2;
                        else Game.lSelY = l[x - 1].Count / 2 - 1;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (x < l.Count-1)
                    {
                        Game.lSelX++;
                        if (l[x + 1].Count == 3 || l[x + 1].Count == 5 || l[x + 1].Count == 1) Game.lSelY = l[x+1].Count / 2;
                        else Game.lSelY = l[x + 1].Count / 2 - 1;
                    }
                    break;
                case ConsoleKey.C:
                    if (Game.player.Food > 0)
                    {
                        Game.ConsumeFood(true);
                    }
                    break;
                case ConsoleKey.H:
                    GUI.ShowLegend(true);
                    break;
                case ConsoleKey.Enter:
                    if (Game.isSelAccessible) Game.selectProgress = true;
                    break;
            }
            GUI.UpdateSelectMarker(x,y,true);
            GUI.PrintLevelInfo();

        }
        public static bool Menu()
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.Enter:
                    return true;
            }
            return false;
        }
        public static bool SkipIntro()
        {
            while(true){
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Y:
                        return true;
                    case ConsoleKey.N:
                        return false;
                }
            }
            
            
        }
    }
}
