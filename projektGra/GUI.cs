using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;
using Pastel;
using System.IO;

namespace projektGra
{
    public static class GUI
    {
        static Random Rnd = new Random();
        static int WindowBorderXLeft = 0;
        static int WindowBorderXRight = 1 + 3 + Config.RoomWidth + 3 + 1 + Config.MinimapSize + 1 + 3 + 31 - 1;
        static int WindowBorderYTop = 0;
        static int WindowBorderYBot = 1 + 1 + Math.Max(Config.RoomHeight, Config.MinimapSize) + 1 + 1 + 10 - 1;
        static int MainWindowX = 4;
        static int MainWindowY = 2;
        static int MinimapX = 1 + 3 + Config.RoomWidth + 3 + 1 - 1;
        static int MinimapY = 2;
        static int LevelSelectBot = WindowBorderYBot - 9;
        static int LevelSelectLeft = WindowBorderXLeft + 1;
        static int StatsBot = WindowBorderYBot - 6;
        static int StatsLeft = WindowBorderXLeft + 1;
        static int InvLeft = WindowBorderXRight - 30;
        static int GUIEnd = WindowBorderYBot + 2;
        static int LevelInfoLeft = WindowBorderXLeft + 3;
        static int LevelInfoRight = InvLeft - 1;
        static int LevelInfoTop = WindowBorderYBot - 5;
        static int LevelInfoBot = WindowBorderYBot - 1;

        public static void LoadMenu(bool clear)
        {
            MusicPlay.LoadMusic("\\assets\\music\\coin.wav");
            for (int y = WindowBorderYTop; y <= WindowBorderYBot; y++)
            {
                if (y == WindowBorderYTop || y == WindowBorderYBot)
                {
                    for (int x = WindowBorderXLeft; x <= WindowBorderXRight; x++)
                    {
                        Console.SetCursorPosition(x, y);
                        if (x == WindowBorderXLeft && y == WindowBorderYTop) Console.Write(Tiles.WindowULCorner);
                        else if (x == WindowBorderXRight && y == WindowBorderYTop) Console.Write(Tiles.WindowURCorner);
                        else if (x == WindowBorderXRight && y == WindowBorderYBot) Console.Write(Tiles.WindowDRCorner);
                        else if (x == WindowBorderXLeft && y == WindowBorderYBot) Console.Write(Tiles.WindowDLCorner);
                        else Console.Write(Tiles.WindowHor);
                    }
                }
                else
                {
                    for (int x = WindowBorderXLeft; x <= WindowBorderXRight; x++)
                    {
                        Console.SetCursorPosition(x, y);
                        if (x == WindowBorderXLeft || x == WindowBorderXRight) Console.Write(Tiles.WindowVer);
                        else Console.Write(Tiles.Empty);
                    }
                }
            }
            if (clear) return;
            StreamReader file = new StreamReader("./assets/misc/logo3.txt", Encoding.ASCII);

            for (int i = 0; i < 12; i++)
            {
                Console.SetCursorPosition(13, 3 + i);
                Console.WriteLine(file.ReadLine().Pastel(Palettes.Window));
            }
            Console.SetCursorPosition(WindowBorderXRight / 2 - 10, 20);
            Console.WriteLine("Press ENTER to start".Pastel(Palettes.Window));
            bool enterPressed;
            do
            {
                enterPressed = Controls.Menu();
            } while (!enterPressed);
            MusicPlay.Transition();
            bool alt = true;
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(WindowBorderXRight / 2 - 10, 20);
                if (alt) Console.WriteLine("                    ".Pastel(Palettes.Window));
                else Console.WriteLine("Press ENTER to start".Pastel(Palettes.Window));
                System.Threading.Thread.Sleep(250);
                alt = !alt;
            }
            LoadMenu(true);
            if (!SkipIntro())
            {
                LoadMenu(true);
                Story(true);
            }
            

        }
        private static bool SkipIntro()
        {
            Console.SetCursorPosition(WindowBorderXRight / 2 - 8, WindowBorderYBot / 2);
            Console.WriteLine("Skip intro? [Y/N]".Pastel(Palettes.Window));
            return Controls.SkipIntro();
            
        }
        private static void Story(bool intro)
        {
            StreamReader file = null;
            if (intro)
            {
                MusicPlay.LoadMusic("\\assets\\music\\mountain.wav");
                file = new StreamReader("./assets/misc/story.txt", Encoding.ASCII);
            }
            else
            {
                MusicPlay.LoadMusic("\\assets\\music\\win.wav");
                file = new StreamReader("./assets/misc/ending.txt", Encoding.ASCII);
            }
            MusicPlay.PlayMusic();
            string line;
            for (int i = 0; i < 11; i++)
            {
                line = file.ReadLine();
                Console.SetCursorPosition(WindowBorderXRight / 2 - line.Length/2, 2+i*2);
                foreach(char c in line)
                {
                    Console.Write(Convert.ToString(c).Pastel(Palettes.Window));
                    System.Threading.Thread.Sleep(50);
                }
                System.Threading.Thread.Sleep(1200);
            }
            if (intro)
            {
                line = file.ReadLine();

            }
            else
            {
                line = "THANKS FOR PLAYING!!!";
            }
            LoadMenu(true);
            Console.SetCursorPosition(WindowBorderXRight / 2 - line.Length / 2, WindowBorderYBot / 2);
            foreach (char c in line)
            {
                Console.Write(Convert.ToString(c).Pastel(Palettes.Window));
                System.Threading.Thread.Sleep(200);
            }
            System.Threading.Thread.Sleep(3500);
            if(intro)LoadMenu(true);
        }
        public static void ShowLegend(bool outside)
        {
            for(int x = 1; x<=8; x++)
            {
                Console.SetCursorPosition(0, 0);
                StreamReader file = new StreamReader("./assets/misc/helpMap" + x + ".txt", Encoding.ASCII);
                for (int i = 0; i < 26; i++)
                {
                    Console.WriteLine(file.ReadLine());

                }
                Console.ReadKey(true);
            }
            LoadMenu(true);
            UpdateInventory();
            InitializeGUI();
            if (outside)
            {
                LevelSelect();
            }
            else
            {
                RoomLoad();
                UpdateMinimap();
            }
            
        }
        public static void InitializeGUI()
        {
            for (int y = WindowBorderYTop; y <= WindowBorderYBot; y++)
            {
                if(y == WindowBorderYTop || y == WindowBorderYBot)
                {
                    for (int x = WindowBorderXLeft; x <= WindowBorderXRight; x++)
                    {
                        Console.SetCursorPosition(x, y);
                        if (x == WindowBorderXLeft && y == WindowBorderYTop) Console.Write(Tiles.WindowULCorner);
                        else if (x == WindowBorderXRight && y == WindowBorderYTop) Console.Write(Tiles.WindowURCorner);
                        else if (x == WindowBorderXRight && y == WindowBorderYBot) Console.Write(Tiles.WindowDRCorner);
                        else if (x == WindowBorderXLeft && y == WindowBorderYBot) Console.Write(Tiles.WindowDLCorner);
                        else if (x == InvLeft && y == WindowBorderYTop) Console.Write(Tiles.WindowHorDown);
                        else if (x == InvLeft && y == WindowBorderYBot) Console.Write(Tiles.WindowHorUp);
                        else Console.Write(Tiles.WindowHor);
                    }
                }
                else if(y == LevelSelectBot || y == StatsBot)
                {
                    for (int x = WindowBorderXLeft; x <= InvLeft; x++)
                    {
                        Console.SetCursorPosition(x, y);
                        if (x == WindowBorderXLeft) Console.Write(Tiles.WindowVerRight);
                        else if (x == InvLeft) Console.Write(Tiles.WindowVerLeft);
                        else Console.Write(Tiles.WindowHor);
                    }
                    Console.SetCursorPosition(WindowBorderXRight, y);
                    Console.Write(Tiles.WindowVer);
                }
                else
                {
                    for(int x = WindowBorderXLeft; x <= WindowBorderXRight; x++)
                    {
                        Console.SetCursorPosition(x, y);
                        if (x == WindowBorderXLeft || x == WindowBorderXRight)
                        {
                            Console.Write(Tiles.WindowVer);
                        }
                        else if (x == InvLeft)
                        {
                            Console.Write(Tiles.WindowVer);
                        }
                        else if (x > InvLeft)
                        {
                            continue;
                        }
                        else
                        {
                            Console.Write(Tiles.Empty);
                        }
                    }
                }
            }
            Console.SetCursorPosition(LevelInfoRight - 10, LevelInfoTop - 5);
            Console.Write("H for help".Pastel(Palettes.Window));
            UpdateStats();
            Console.SetCursorPosition(0, GUIEnd);
        }
        public static void UpdateInventory()
        {
            Console.SetCursorPosition(InvLeft + 11, 1);
            Console.Write("Inventory".Pastel(Palettes.Window));
            for(int i=0;i<Game.player.Inv.Count;i++)
            {
                Console.SetCursorPosition(InvLeft + 2, 3+i);
                Console.Write("● ".Pastel(Palettes.Window) + ((string)Game.player.Inv[i]["name"]).Pastel(Palettes.Window));
            }
        }
        public static void UpdateStats()
        {
            Console.SetCursorPosition(WindowBorderXLeft + 3, LevelSelectBot / 2 + 9);
            Console.Write("                                               ");
            Console.SetCursorPosition(WindowBorderXLeft + 3, LevelSelectBot / 2 + 10);
            Console.Write("                                               ");

            Console.SetCursorPosition(WindowBorderXLeft + 3, LevelSelectBot / 2 + 9);
            Console.Write(Tiles.HP + " ");
            for (int hp = 0; hp < 20; hp++)
            {
                if (hp < Game.player.HP / (Game.player.MaxHP/20)) Console.Write(Tiles.BarHP.Pastel(Color.Red));
                else Console.Write(Tiles.BarHP.Pastel(Color.DarkRed));
            }
            Console.SetCursorPosition(WindowBorderXLeft + 26, LevelSelectBot / 2 + 9);
            for (int i = 4; i > Game.player.HP.ToString().Length; i--) Console.Write(" ");
            Console.Write(Convert.ToString(Game.player.HP).Pastel(Palettes.Window) + "/".Pastel(Palettes.Window) + Convert.ToString(Game.player.MaxHP).Pastel(Palettes.Window) +
                "   Food: ".Pastel(Palettes.Window) + Convert.ToString(Game.player.Food).Pastel(Palettes.Window));

            Console.SetCursorPosition(WindowBorderXLeft + 3, LevelSelectBot / 2 + 10);
            Console.Write(Tiles.San + " ");
            for (int sn = 0; sn < 20; sn++)
            {
                if (sn < Game.player.Sanity / (Game.player.MaxSanity / 20)) Console.Write(Tiles.BarSan.Pastel(Color.Cyan));
                else Console.Write(Tiles.BarSan.Pastel(Color.DarkCyan));
            }
            Console.SetCursorPosition(WindowBorderXLeft + 26, LevelSelectBot / 2 + 10);
            for (int i = 4; i > Game.player.Sanity.ToString().Length; i--) Console.Write(" ");
            Console.Write(Convert.ToString(Game.player.Sanity).Pastel(Palettes.Window) + "/".Pastel(Palettes.Window) + Convert.ToString(Game.player.MaxSanity).Pastel(Palettes.Window));
            if (Game.player.Food > 0) Console.Write("   C to consume".Pastel(Palettes.Window));

            Console.SetCursorPosition(0, GUIEnd);
        }
        public static void RoomLoad()
        {
            for (int y = 0; y < Game.currLevel.CurrentRoom.Height; y++)
            {
                for (int x = 0; x < Game.currLevel.CurrentRoom.Width; x++)
                {
                    Console.SetCursorPosition(MainWindowX + x, MainWindowY + y);
                    double dist = Math.Sqrt(Math.Pow(Game.player.PosX - x, 2) + Math.Pow(Game.player.PosY - y, 2));
                    if ((Game.currLevel.Theme == Palettes.Electric && dist <= 5.5 && !Game.player.Inv.Contains(Items.Flashlight)) || Game.currLevel.Theme!=Palettes.Electric || Game.player.Inv.Contains(Items.Flashlight))
                    {
                        if (Game.currLevel.CurrentRoom.Board[y][x] == Tiles.Rock || Game.currLevel.CurrentRoom.Board[y][x] == Tiles.Mush || Game.currLevel.CurrentRoom.Board[y][x] == Tiles.DoorHor || Game.currLevel.CurrentRoom.Board[y][x] == Tiles.DoorVer)
                        {
                            Console.Write(Game.currLevel.CurrentRoom.Board[y][x].Pastel((Color)Game.currLevel.Theme["rocks"]).PastelBg((Color)Game.currLevel.Theme["background"]));
                        }
                        else
                        {
                            Console.Write(Game.currLevel.CurrentRoom.Board[y][x].Pastel((Color)Game.currLevel.Theme["objects"]).PastelBg((Color)Game.currLevel.Theme["background"]));
                        }
                    }
                    else
                    {
                        Console.Write(Tiles.Empty);
                    }
                    
                }
            }
            Console.SetCursorPosition(0, GUIEnd);
        }
        public static void UpdateMinimap()
        {
            for (int y = 0; y < Config.MinimapSize + 2; y++)
            {
                Console.SetCursorPosition(MinimapX, MinimapY + y);
                for (int x = 0; x < Config.MinimapSize + 2; x++)
                {
                    if (Game.currLevel.Minimap[y][x] == Tiles.Wall) Console.Write(Game.currLevel.Minimap[y][x].Pastel(Palettes.Window));
                    else if (y == Game.currLevel.StartingRoom.posY && x == Game.currLevel.StartingRoom.posX) Console.Write(Game.currLevel.Minimap[y][x].Pastel(Color.Yellow));
                    else if (Game.player.Inv.Contains(Items.Compass) && y == Game.currLevel.ExitRoom.posY && x == Game.currLevel.ExitRoom.posX) Console.Write(Game.currLevel.Minimap[y][x].Pastel(Color.Red));
                    else Console.Write(Game.currLevel.Minimap[y][x]);
                }
            }
            Console.SetCursorPosition(0, GUIEnd);
        }
        public static List<List<Level>> LevelSelect()
        {
            List<int> d0 = new List<int>();
            List<int> d1 = new List<int>();
            List<int> d2 = new List<int>();
            List<int> d3 = new List<int>();
            List<List<Level>> lSelect = new List<List<Level>>
            {
                new List<Level>(),
                new List<Level>(),
                new List<Level>(),
                new List<Level>(),
            };
            for (int l = 0; l < Game.map.Levels.Count; l++)
            {
                if (Game.map.Levels[l].Depth == Game.currentDepth) lSelect[0].Add(Game.map.Levels[l]);
                else if (Game.map.Levels[l].Depth == Game.currentDepth + 1) lSelect[1].Add(Game.map.Levels[l]);
                else if (Game.map.Levels[l].Depth == Game.currentDepth + 2) lSelect[2].Add(Game.map.Levels[l]);
                else if (Game.map.Levels[l].Depth == Game.currentDepth + 3) lSelect[3].Add(Game.map.Levels[l]);
                else if (Game.map.Levels[l].Depth > Game.currentDepth + 3) break;
            }
            d0 = lSelect[0].Select(x => x.ID).ToList();
            d1 = lSelect[1].Select(x => x.ID).ToList();
            d2 = lSelect[2].Select(x => x.ID).ToList();
            d3 = lSelect[3].Select(x => x.ID).ToList();
            /*
            foreach (int id in d0) Console.Write(id);
            Console.Write("\n");
            foreach (int id in d1) Console.Write(id);
            Console.Write("\n");
            foreach (int id in d2) Console.Write(id);
            Console.Write("\n");
            foreach (int id in d3) Console.Write(id);
            Console.Write("\n");
            */
            DrawRow(d0, 2);
            DrawRow(d1, 14);
            DrawRow(d2, 26);
            DrawRow(d3, 38);
            DrawPaths(d0, d1, 13);
            DrawPaths(d1, d2, 25);
            DrawPaths(d2, d3, 37);

            for (int i = 3; i >= 0; i--)
            {
                if (lSelect[i].Count == 0) lSelect.RemoveAt(i);
            }
            return lSelect;
        }
        private static void DrawRow(List<int> l, int rowOffset)
        {
            if (l.Count == 1)
            {
                Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2);
                if (CheckConn(Game.currLevel.ID, l[0])) Console.Write(Tiles.Path + Tiles.LevelOK.Pastel((Color)Game.map.Levels[l[0]].Theme["background"]) + Tiles.Path);
                else Console.Write(Tiles.Path + Tiles.Level.Pastel((Color)Game.map.Levels[l[0]].Theme["background"]) + Tiles.Path);
                if(Game.currentDepth == 0)
                {
                    Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2);
                    Console.Write("     ");
                }
                else
                {
                    Console.SetCursorPosition(LevelSelectLeft + rowOffset + 6, LevelSelectBot / 2);
                    Console.Write("     ");
                }
            }
            else if (l.Count == 2)
            {
                int j = 0;
                for (int i = -3;i <= 3; i += 6)
                {
                    Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 + i);
                    if (CheckConn(Game.currLevel.ID, l[j])) Console.Write(Tiles.Path + Tiles.LevelOK.Pastel((Color)Game.map.Levels[l[j]].Theme["background"]) + Tiles.Path);
                    else Console.Write(Tiles.Path + Tiles.Level.Pastel((Color)Game.map.Levels[l[j]].Theme["background"]) + Tiles.Path);
                    j++;
                }
            }
            else if (l.Count == 3)
            {
                int j = 0;
                for (int i = -4; i <= 4; i += 4)
                {
                    Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 + i);
                    if (CheckConn(Game.currLevel.ID, l[j])) Console.Write(Tiles.Path + Tiles.LevelOK.Pastel((Color)Game.map.Levels[l[j]].Theme["background"]) + Tiles.Path);
                    else Console.Write(Tiles.Path + Tiles.Level.Pastel((Color)Game.map.Levels[l[j]].Theme["background"]) + Tiles.Path);
                    j++;
                }
            }
            else if (l.Count == 4)
            {
                int j = 0;
                for (int i = -5; i <= 5; i += 3)
                {
                    Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 + i);
                    if (CheckConn(Game.currLevel.ID, l[j])) Console.Write(Tiles.Path + Tiles.LevelOK.Pastel((Color)Game.map.Levels[l[j]].Theme["background"]) + Tiles.Path);
                    else Console.Write(Tiles.Path + Tiles.Level.Pastel((Color)Game.map.Levels[l[j]].Theme["background"]) + Tiles.Path);
                    if (i == -2) i++;
                    j++;
                }
            }
            else if (l.Count == 5)
            {
                int j = 0;
                for (int i = -6; i <= 6; i += 3)
                {
                    Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 + i);
                    if (CheckConn(Game.currLevel.ID, l[j])) Console.Write(Tiles.Path + Tiles.LevelOK.Pastel((Color)Game.map.Levels[l[j]].Theme["background"]) + Tiles.Path);
                    else Console.Write(Tiles.Path + Tiles.Level.Pastel((Color)Game.map.Levels[l[j]].Theme["background"]) + Tiles.Path);
                    j++;
                }
            }
        }
        private static void DrawPaths(List<int> d1, List<int> d2, int rowOffset)
        {
            if (d1.Count == 1)
            {
                for (int y = 0; y < Tiles.Path13.Count; y++)
                {
                    if(Game.currentDepth == 0)
                    {
                        Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 4 + y);
                        Console.Write(Tiles.Path13[y]);
                    }
                }
            }
            else if (d1.Count == 2)
            {
                if (d2.Count == 3)
                {
                    if (Game.map.Levels[d1[0]].Paths == 2)
                    {
                        for (int y = 0; y < Tiles.Path23_21.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 4 + y);
                            Console.Write(Tiles.Path23_21[y]);
                        }
                    }
                    else if (Game.map.Levels[d1[1]].Paths == 2)
                    {
                        for (int y = 0; y < Tiles.Path23_12.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 4 + y);
                            Console.Write(Tiles.Path23_12[y]);
                        }
                    }
                }
                else if (d2.Count == 4)
                {
                    if (Game.map.Levels[d1[0]].Paths == 3)
                    {
                        for (int y = 0; y < Tiles.Path24_31.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 5 + y);
                            Console.Write(Tiles.Path24_31[y]);
                        }
                    }
                    else if (Game.map.Levels[d1[1]].Paths == 3)
                    {
                        for (int y = 0; y < Tiles.Path24_13.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 5 + y);
                            Console.Write(Tiles.Path24_13[y]);
                        }
                    }
                    else if (Game.map.Levels[d1[0]].Paths == 2 && Game.map.Levels[d1[1]].Paths == 2)
                    {
                        for (int y = 0; y < Tiles.Path24_22.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 5 + y);
                            Console.Write(Tiles.Path24_22[y]);
                        }
                    }
                }
                else if (d2.Count == 5)
                {
                    if (Game.map.Levels[d1[0]].Paths == 3)
                    {
                        for (int y = 0; y < Tiles.Path25_32.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path25_32[y]);
                        }
                    }
                    else if (Game.map.Levels[d1[1]].Paths == 3)
                    {
                        for (int y = 0; y < Tiles.Path25_23.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path25_23[y]);
                        }
                    }
                }
                else if (d2.Count == 1)
                {
                    for (int y = 0; y < Tiles.Path21.Count; y++)
                    {
                        Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 3 + y);
                        Console.Write(Tiles.Path21[y]);
                    }
                }
            }
            else if (d1.Count == 3)
            {
                if(d2.Count == 2)
                {
                    if(Game.map.Levels[d2[0]].PathsTo == 2)
                    {
                        for (int y = 0; y < Tiles.Path32_21.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 4 + y);
                            Console.Write(Tiles.Path32_21[y]);
                        }
                    }
                    else if (Game.map.Levels[d2[1]].PathsTo == 2)
                    {
                        for (int y = 0; y < Tiles.Path32_12.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 4 + y);
                            Console.Write(Tiles.Path32_12[y]);
                        }
                    }
                }
                else if(d2.Count == 4)
                {
                    if(Game.map.Levels[d1[0]].Paths == 2)
                    {
                        for (int y = 0; y < Tiles.Path34_211.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 5 + y);
                            Console.Write(Tiles.Path34_211[y]);
                        }
                    }
                    else if (Game.map.Levels[d1[1]].Paths == 2)
                    {
                        for (int y = 0; y < Tiles.Path34_121.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 5 + y);
                            Console.Write(Tiles.Path34_121[y]);
                        }
                    }
                    else if (Game.map.Levels[d1[2]].Paths == 2)
                    {
                        for (int y = 0; y < Tiles.Path34_112.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 5 + y);
                            Console.Write(Tiles.Path34_112[y]);
                        }
                    }
                }
                else if(d2.Count == 5)
                {
                    if (Game.map.Levels[d1[1]].Paths == 3)
                    {
                        for (int y = 0; y < Tiles.Path35_131.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path35_131[y]);
                        }
                    }
                    else if(Game.map.Levels[d1[0]].Paths == 2 && Game.map.Levels[d1[1]].Paths == 2)
                    {
                        for (int y = 0; y < Tiles.Path35_221.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path35_221[y]);
                        }
                    }
                    else if (Game.map.Levels[d1[0]].Paths == 2 && Game.map.Levels[d1[2]].Paths == 2)
                    {
                        for (int y = 0; y < Tiles.Path35_212.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path35_212[y]);
                        }
                    }
                    else if (Game.map.Levels[d1[1]].Paths == 2 && Game.map.Levels[d1[2]].Paths == 2)
                    {
                        for (int y = 0; y < Tiles.Path35_122.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path35_122[y]);
                        }
                    }
                }
                else if (d2.Count == 1)
                {
                    for (int y = 0; y < Tiles.Path31.Count; y++)
                    {
                        Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 4 + y);
                        Console.Write(Tiles.Path31[y]);
                    }
                }
            }
            else if(d1.Count == 4)
            {
                if(d2.Count == 2)
                {
                    if (Game.map.Levels[d2[0]].PathsTo == 3)
                    {
                        for (int y = 0; y < Tiles.Path42_31.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 5 + y);
                            Console.Write(Tiles.Path42_31[y]);
                        }
                    }
                    else if (Game.map.Levels[d2[1]].PathsTo == 3)
                    {
                        for (int y = 0; y < Tiles.Path42_13.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 5 + y);
                            Console.Write(Tiles.Path42_13[y]);
                        }
                    }
                    else if(Game.map.Levels[d2[0]].PathsTo == 2 && Game.map.Levels[d2[1]].PathsTo == 2)
                    {
                        for (int y = 0; y < Tiles.Path42_22.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 5 + y);
                            Console.Write(Tiles.Path42_22[y]);
                        }
                    }
                }
                else if (d2.Count == 3)
                {
                    if (Game.map.Levels[d2[0]].PathsTo == 2)
                    {
                        for (int y = 0; y < Tiles.Path43_211.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 5 + y);
                            Console.Write(Tiles.Path43_211[y]);
                        }
                    }
                    else if (Game.map.Levels[d2[1]].PathsTo == 2)
                    {
                        for (int y = 0; y < Tiles.Path43_121.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 5 + y);
                            Console.Write(Tiles.Path43_121[y]);
                        }
                    }
                    else if (Game.map.Levels[d2[2]].PathsTo == 2)
                    {
                        for (int y = 0; y < Tiles.Path43_112.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 5 + y);
                            Console.Write(Tiles.Path43_112[y]);
                        }
                    }
                }
                else if (d2.Count == 5)
                {
                    if (Game.map.Levels[d1[0]].Paths == 2)
                    {
                        for (int y = 0; y < Tiles.Path45_2111.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path45_2111[y]);
                        }
                    }
                    else if (Game.map.Levels[d1[1]].Paths == 2)
                    {
                        for (int y = 0; y < Tiles.Path45_1211.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path45_1211[y]);
                        }
                    }
                    else if (Game.map.Levels[d1[2]].Paths == 2)
                    {
                        for (int y = 0; y < Tiles.Path45_1121.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path45_1121[y]);
                        }
                    }
                    else if (Game.map.Levels[d1[3]].Paths == 2)
                    {
                        for (int y = 0; y < Tiles.Path45_1112.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path45_1112[y]);
                        }
                    }
                }
                else if (d2.Count == 1)
                {
                    for (int y = 0; y < Tiles.Path41.Count; y++)
                    {
                        Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 5 + y);
                        Console.Write(Tiles.Path41[y]);
                    }
                }
            }
            else if (d1.Count == 5)
            {
                if(d2.Count == 2)
                {
                    if (Game.map.Levels[d2[0]].PathsTo == 3)
                    {
                        for (int y = 0; y < Tiles.Path52_32.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path52_32[y]);
                        }
                    }
                    else if (Game.map.Levels[d2[1]].PathsTo == 3)
                    {
                        for (int y = 0; y < Tiles.Path52_23.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path52_23[y]);
                        }
                    }
                }
                else if(d2.Count == 3)
                {
                    if (Game.map.Levels[d2[1]].PathsTo == 3)
                    {
                        for (int y = 0; y < Tiles.Path53_131.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path53_131[y]);
                        }
                    }
                    else if (Game.map.Levels[d2[0]].PathsTo == 2 && Game.map.Levels[d2[1]].PathsTo == 2)
                    {
                        for (int y = 0; y < Tiles.Path53_221.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path53_221[y]);
                        }
                    }
                    else if (Game.map.Levels[d2[0]].PathsTo == 2 && Game.map.Levels[d2[2]].PathsTo == 2)
                    {
                        for (int y = 0; y < Tiles.Path53_212.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path53_212[y]);
                        }
                    }
                    else if (Game.map.Levels[d2[1]].PathsTo == 2 && Game.map.Levels[d2[2]].PathsTo == 2)
                    {
                        for (int y = 0; y < Tiles.Path53_122.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path53_122[y]);
                        }
                    }
                }
                else if (d2.Count == 4)
                {
                    if (Game.map.Levels[d2[0]].PathsTo == 2)
                    {
                        for (int y = 0; y < Tiles.Path54_2111.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path54_2111[y]);
                        }
                    }
                    else if (Game.map.Levels[d2[1]].PathsTo == 2)
                    {
                        for (int y = 0; y < Tiles.Path54_1211.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path54_1211[y]);
                        }
                    }
                    else if (Game.map.Levels[d2[2]].PathsTo == 2)
                    {
                        for (int y = 0; y < Tiles.Path54_1121.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path54_1121[y]);
                        }
                    }
                    else if (Game.map.Levels[d2[3]].PathsTo == 2)
                    {
                        for (int y = 0; y < Tiles.Path54_1112.Count; y++)
                        {
                            Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                            Console.Write(Tiles.Path54_1112[y]);
                        }
                    }
                }
                else if (d2.Count == 1)
                {
                    for (int y = 0; y < Tiles.Path51.Count; y++)
                    {
                        Console.SetCursorPosition(LevelSelectLeft + rowOffset, LevelSelectBot / 2 - 6 + y);
                        Console.Write(Tiles.Path51[y]);
                    }
                }
            }
        }
        public static void UpdateLevelMarker()
        {
            int pos = Game.lSelY;
            if (Game.lSelection[0].Count == 1)
            {
                Console.SetCursorPosition(LevelSelectLeft + 2 + 5, LevelSelectBot / 2 + 1);
                Console.Write(Tiles.CurrLMarker);
                Console.SetCursorPosition(LevelSelectLeft + 2 + 5, LevelSelectBot / 2 - 1);
                Console.Write(Tiles.SelMarker);

            }
            else if (Game.lSelection[0].Count == 2)
            {
                Console.SetCursorPosition(LevelSelectLeft + 2 + 5, LevelSelectBot / 2 - 2 + pos * 6);
                Console.Write(Tiles.CurrLMarker);
                Console.SetCursorPosition(LevelSelectLeft + 2 + 5, LevelSelectBot / 2 - 4 + pos * 6);
                Console.Write(Tiles.SelMarker);
            }
            else if(Game.lSelection[0].Count == 3)
            {
                Console.SetCursorPosition(LevelSelectLeft + 2 + 5, LevelSelectBot / 2 - 3 + pos * 4);
                Console.Write(Tiles.CurrLMarker);
                Console.SetCursorPosition(LevelSelectLeft + 2 + 5, LevelSelectBot / 2 - 5 + pos * 4);
                Console.Write(Tiles.SelMarker);
            }
            else if(Game.lSelection[0].Count == 4)
            {
                if(pos < 2) Console.SetCursorPosition(LevelSelectLeft + 2 + 5, LevelSelectBot / 2 - 4 + pos * 3);
                else Console.SetCursorPosition(LevelSelectLeft + 2 + 5, LevelSelectBot / 2 - 4 + pos * 3 + 1);
                Console.Write(Tiles.CurrLMarker);
                if(pos < 2) Console.SetCursorPosition(LevelSelectLeft + 2 + 5, LevelSelectBot / 2 - 6 + pos * 3);
                else Console.SetCursorPosition(LevelSelectLeft + 2 + 5, LevelSelectBot / 2 - 6 + pos * 3 + 1);
                Console.Write(Tiles.SelMarker);
            }
            else if(Game.lSelection[0].Count == 5)
            {
                Console.SetCursorPosition(LevelSelectLeft + 2 + 5, LevelSelectBot / 2 - 5 + pos * 3);
                Console.Write(Tiles.CurrLMarker);
                Console.SetCursorPosition(LevelSelectLeft + 2 + 5, LevelSelectBot / 2 - 7 + pos * 3);
                Console.Write(Tiles.SelMarker);
            }
        }
        public static void UpdateSelectMarker(int x, int y, bool del)
        {
            int posX;
            int posY;
            string replace;
            if (del)
            {
                posX = x;
                posY = y;
                replace = Tiles.Empty;
            }
            else
            {
                posX = Game.lSelX;
                posY = Game.lSelY;
                replace = Tiles.SelMarker;
            }
            if (Game.lSelection[posX].Count == 1)
            {
                Console.SetCursorPosition(LevelSelectLeft + 2 + 5 + posX * 12, LevelSelectBot / 2 - 1);
                Console.Write(replace);
            }
            else if (Game.lSelection[posX].Count == 2)
            {
                Console.SetCursorPosition(LevelSelectLeft + 2 + 5 + posX * 12, LevelSelectBot / 2 - 4 + posY * 6);
                Console.Write(replace);
            }
            else if (Game.lSelection[posX].Count == 3)
            {
                Console.SetCursorPosition(LevelSelectLeft + 2 + 5 + posX * 12, LevelSelectBot / 2 - 5 + posY * 4);
                Console.Write(replace);
            }
            else if (Game.lSelection[posX].Count == 4)
            {
                if (posY < 2) Console.SetCursorPosition(LevelSelectLeft + 2 + 5 + posX * 12, LevelSelectBot / 2 - 6 + posY * 3);
                else Console.SetCursorPosition(LevelSelectLeft + 2 + 5 + posX * 12, LevelSelectBot / 2 - 6 + posY * 3 + 1);
                Console.Write(replace);
            }
            else if (Game.lSelection[posX].Count == 5)
            {
                Console.SetCursorPosition(LevelSelectLeft + 2 + 5 + posX * 12, LevelSelectBot / 2 - 7 + posY * 3);
                Console.Write(replace);
            }
            if (del) UpdateSelectMarker(x,y,false);
        }
        public static void PrintLevelInfo()
        {
            for (int y = LevelInfoTop; y <= LevelInfoBot; y++)
            {
                for(int x = LevelInfoLeft; x <= LevelInfoRight; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }

            Console.SetCursorPosition(LevelInfoLeft, LevelInfoTop);
            //name
            Console.Write(Convert.ToString(Game.lSelection[Game.lSelX][Game.lSelY].Theme["name"]).Pastel(Palettes.Window));
            //Console.Write(" " + Game.lSelX + " " + Game.lSelY);
            int id = Game.lSelection[Game.lSelX][Game.lSelY].ID;
            int depth = Game.lSelection[Game.lSelX][Game.lSelY].Depth;
            int height = Game.lSelection[Game.lSelX][Game.lSelY].Height;
            //Console.WriteLine("ID: ".Pastel(Palettes.Window) + Convert.ToString(id).Pastel(Palettes.Window));
            //Console.SetCursorPosition(LevelInfoLeft, LevelInfoTop + 2);
            //Console.WriteLine("Depth: ".Pastel(Palettes.Window) + Convert.ToString(depth).Pastel(Palettes.Window));
            //height
            Console.SetCursorPosition(LevelInfoLeft, LevelInfoTop + 1);
            Console.Write("Height: ".Pastel(Palettes.Window) + Convert.ToString(height).Pastel(Palettes.Window) + "m".Pastel(Palettes.Window));
            //details
            Console.SetCursorPosition(LevelInfoLeft + 17, LevelInfoTop + 1);
            Console.Write("Details: ".Pastel(Palettes.Window));
            Console.SetCursorPosition(LevelInfoLeft + 17, LevelInfoTop + 3);
            Console.Write(Convert.ToString(Game.lSelection[Game.lSelX][Game.lSelY].Theme["details"]).Pastel(Palettes.Window));
            //enter
            Console.SetCursorPosition(LevelInfoLeft, LevelInfoTop + 3);
            if (CheckConn(Game.currLevel.ID, id)) {
                Console.Write("ENTER to select".Pastel(Palettes.Window));
                Game.isSelAccessible = true;
            }
            else
            {
                Game.isSelAccessible = false;
            }
        }
        public static void ClearInfoBox()
        {
            for (int y = LevelInfoTop; y <= LevelInfoBot; y++)
            {
                for (int x = LevelInfoLeft; x <= LevelInfoRight; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }
            Console.SetCursorPosition(0, GUIEnd);
        }
        public static void PrintInfo(string mess)
        {
            Console.SetCursorPosition(LevelInfoLeft, LevelInfoTop);
            Console.Write(mess.Pastel(Palettes.Window));
            Console.SetCursorPosition(0, GUIEnd);
        }
        public static bool MonkeyFight()
        {
            MusicPlay.DMG();
            Console.SetCursorPosition(LevelInfoLeft, LevelInfoTop);
            Console.Write("You are fighting a monkey!".Pastel(Palettes.Window));
            Console.SetCursorPosition(LevelInfoLeft, LevelInfoTop+1);
            Console.Write("Press the correct button quickly".Pastel(Palettes.Window));
            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(500);
                Console.Write(".".Pastel(Palettes.Window));
            }
            System.Threading.Thread.Sleep(500);
            ConsoleKey QTE = Controls.QTE[Rnd.Next(0, Controls.QTE.Count-1)];
            Console.Write(" " + Convert.ToString(QTE).Pastel(Color.Red));
            var t1 = (DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds;
            ConsoleKey input = Console.ReadKey(true).Key;
            var t2 = (DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds;
            ClearInfoBox();
            int time = 500;
            if (Game.player.Inv.Contains(Items.Coffee)) time = 1000;
            if (input == QTE && t2 - t1 < time)
            {
                MusicPlay.CollectFood();
                int chance;
                if (Game.currLevel.Theme == Palettes.Desert) chance = 6;
                else if (Game.currLevel.Theme == Palettes.Fruit) chance = 2;
                else chance = 4;
                if (Rnd.Next(0, chance) == 0)
                {
                    PrintInfo("You defeated monkey, and picked her food");
                    Game.player.Food++;
                }
                else
                {
                    PrintInfo("You defeated monkey, but it was empty-handed");
                }
                return true;
            }
            else if (input != QTE)
            {
                MusicPlay.DMG();
                if (Rnd.Next(0, 4) == 0 && Game.player.Food>0)
                {
                    PrintInfo("You missed. It stole your food! -100 HP, -1 Food");
                    Game.player.Food--;
                }
                else
                {
                    PrintInfo("You missed. Monkey beat you. -100 HP");
                }
                return false;
            }
            else
            {
                MusicPlay.DMG();
                if (Rnd.Next(0, 4) == 0 && Game.player.Food > 0)
                {
                    PrintInfo("Too slow. It stole your food! -100 HP, -1 Food");
                    Game.player.Food--;
                }
                else
                {
                    PrintInfo("Too slow. Monkey beat you. -100 HP");
                }
                return false;
            }
        }
        private static bool CheckConn(int idSrc, int idDest)
        {
            for(int x = 0; x < Game.map.ConnectionMap.Count; x++)
            {
                if (Game.map.ConnectionMap[idSrc][x] == 1)
                {
                    if (x == idDest) return true;
                    //else CheckConn(x, idDest);
                }
            }
            return false;
        }
        public static void TheEnd(string opt)
        {
            
            int left = WindowBorderXLeft + 1;
            int top = WindowBorderYTop + 1;
            int right;
            int bot;
            int time;
            if (opt == "level")
            {
                right = InvLeft - 1;
                bot = LevelSelectBot - 1;
                time = 12;
            }
            else
            {
                right = WindowBorderXRight - 1;
                bot = WindowBorderYBot - 1;

                Console.SetCursorPosition(InvLeft, WindowBorderYTop);
                Console.Write(Tiles.WindowHor);
                Console.SetCursorPosition(InvLeft, WindowBorderYBot);
                Console.Write(Tiles.WindowHor);
                Console.SetCursorPosition(0, StatsBot);
                Console.Write(Tiles.WindowVer);
                Console.SetCursorPosition(0, LevelSelectBot);
                Console.Write(Tiles.WindowVer);
                time = 5;
            }

            int yMax = Math.Abs(top - 1 - bot - 1) / 2;
            int xMax = Math.Abs(left - 1 - right - 1) / 2;
            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    System.Threading.Thread.Sleep(time);
                    Console.SetCursorPosition(left + x, top + y);
                    Console.Write(" ");
                    Console.SetCursorPosition(right - x, top + y);
                    Console.Write(" ");
                    Console.SetCursorPosition(left + x, bot - y);
                    Console.Write(" ");
                    Console.SetCursorPosition(right - x, bot - y);
                    Console.Write(" ");
                }
            }
            bool enterPressed;
            if (opt == "death")
            {
                MusicPlay.LoadMusic("\\assets\\music\\death.wav");
                MusicPlay.Transition();
                System.Threading.Thread.Sleep(500);
                Console.SetCursorPosition(xMax - 4, yMax);
                Console.Write("GAME OVER".Pastel(Palettes.Window));
                Console.SetCursorPosition(0, GUIEnd);
                do
                {
                    enterPressed = Controls.Menu();
                } while (!enterPressed);
            }
            else if (opt == "win")
            {
                MusicPlay.LoadMusic("\\assets\\music\\win.wav");
                MusicPlay.Transition();
                Story(false);
                //System.Threading.Thread.Sleep(500);
                //Console.SetCursorPosition(xMax - 3, yMax);
                //Console.Write("YOU WIN".Pastel(Palettes.Window));
                //Console.SetCursorPosition(0, GUIEnd);
                //do
                //{
                //    enterPressed = Controls.Menu();
                //} while (!enterPressed);
            }
            else if (opt == "level"){
                Console.SetCursorPosition(0, GUIEnd);
            }
        }
    }
}
