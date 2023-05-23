using System;
using System.Collections.Generic;
using System.Text;

namespace projektGra
{
    public class Room
    {
        Random Rnd = new Random();
        public int posX;
        public int posY;
        public int Height = Config.RoomHeight; //initial - 12
        public int Width = Config.RoomWidth;  //initial - 30
        public Room Up = null;
        public Room Left = null;
        public Room Down = null;
        public Room Right = null;
        public int ID;
        public int exitX;
        public int exitY;
        public bool containsFood = false;
        public bool containsItem = false;
        public List<List<string>> Board = new List<List<string>>();
        public List<Enemies> Monkeys = new List<Enemies>();
        public Room(int x, int y, int id)
        {
            posX = x;
            posY = y;
            ID = id;
            GenRoom();
        }
        public Room(bool b)
        {
            ID = -1;
        }

        private void GenRoom()
        {
            for (int y = 0; y < Height; y++)
            {
                Board.Add(new List<string>());
                for (int x = 0; x < Width; x++)
                {
                    if (y == 0 || y == Height - 1) Board[y].Add(Tiles.Wall) ;
                    else if (x == 0 || x == Width - 1) Board[y].Add(Tiles.Wall);

                    else Board[y].Add(Tiles.Empty);
                }
            }
            
        }
        public void GenContents()
        {
            //exit generate
            if (ID == Game.currLevel.ExitRoom.ID)
            {
                if (Width % 2 == 0)
                {
                    exitX = Width / 2 + Rnd.Next(0, 2) - 1;
                }
                else
                {
                    exitX = Width / 2;
                }
                if (Height % 2 == 0)
                {
                    exitY = Height / 2 + Rnd.Next(0, 2) - 1;
                }
                else
                {
                    exitY = Height / 2;
                }
                Board[exitY][exitX] = Tiles.Exit;
            }
            //rocks generating
            int rockCount = Rnd.Next(20, 81); //20, 81
            if (Game.currLevel.ExitRoom.ID == ID || Game.currLevel.StartingRoom.ID == ID) rockCount /= 4;
            if (Game.currLevel.Theme == Palettes.Space) {
                rockCount /= 4;
            }
            else if(Game.currLevel.Theme == Palettes.Ice)
            {
                rockCount /= 2;
            }
            for (int r = 0; r < rockCount; r++)
            {
                int x = Rnd.Next(1, Width - 1);
                int y = Rnd.Next(1, Height - 1);
                if (ID == 0 && (Board[y][x + 1] == Tiles.Exit ||
                        Board[y + 1][x + 1] == Tiles.Exit ||
                        Board[y + 1][x] == Tiles.Exit ||
                        Board[y + 1][x - 1] == Tiles.Exit ||
                        Board[y][x - 1] == Tiles.Exit ||
                        Board[y - 1][x - 1] == Tiles.Exit ||
                        Board[y - 1][x] == Tiles.Exit ||
                        Board[y - 1][x + 1] == Tiles.Exit))
                {
                    r--;
                }
                else if (Board[y][x + 1] == Tiles.DoorVer || Board[y][x - 1] == Tiles.DoorVer || Board[y + 1][x] == Tiles.DoorHor || Board[y - 1][x] == Tiles.DoorHor) r--;
                else if (Board[y][x] != Tiles.Empty) r--;
                else if (Board[y - 1][x] == Tiles.Exit) r--;
                else
                {
                    if (Game.currLevel.Theme == Palettes.Swamp) Board[y][x] = Tiles.Mush;
                    else Board[y][x] = Tiles.Rock;
                }
            }
            while (true)
            {
                if (!CheckPath())
                {
                    while(true)
                    {
                        int x = Rnd.Next(1, Width - 1);
                        int y = Rnd.Next(1, Height - 1);
                        if (Board[y][x] == Tiles.Rock || Board[y][x] == Tiles.Mush)
                        {
                            Board[y][x] = Tiles.Empty; 
                            //Console.WriteLine("trafil w kamien"); 
                            break;
                        }
                        //Console.WriteLine("nie trafił w kamień");
                    }
                }
                else
                {
                    break;
                }
            }
            // food generating
            if (containsFood)
            {
                while (true)
                {
                    int x = Rnd.Next(1, Width - 1);
                    int y = Rnd.Next(1, Height - 1);
                    if (Board[y][x] == Tiles.Empty)
                    {
                        Board[y][x] = Tiles.Food;
                        break;
                    }
                }
            }

            // item gen
            if (containsItem)
            {
                while (true)
                {
                    int x = Rnd.Next(1, Width - 1);
                    int y = Rnd.Next(1, Height - 1);
                    if (Board[y][x] == Tiles.Empty)
                    {
                        Board[y][x] = Tiles.Crate;
                        break;
                    }
                }
            }

            // monke generating
            
            int monkeCount = Rnd.Next(0, 3);
            if (Game.currLevel.Theme == Palettes.Factory) monkeCount = 0;
            else if (Game.currLevel.Theme == Palettes.Sunny) monkeCount += Rnd.Next(0, 3);
            if (Game.player.Inv.Contains(Items.Bananas)) monkeCount += Rnd.Next(0, 3);
            for (int i = 0; i < monkeCount; i++)
            {
                int x = Rnd.Next(1, Width - 1);
                int y = Rnd.Next(1, Height - 1);
                if (Board[y][x] == Tiles.Empty)
                {
                    Board[y][x] = Tiles.Monkey;
                    Monkeys.Add(new Enemies("monkey", x, y));
                }
                else i--;
            }

        }
        private bool CheckPath()
        {
            //Console.WriteLine("path cheking, room " + ID);
            int startPosX = Width / 2;
            int startPosY = 1;
            int pathX = startPosX;
            int pathY = startPosY;
            bool leftStart = false;
            string dir = "right";
            int moves = 0;
            List<string> doors = new List<string>();
            if (Up != null) doors.Add("top");
            if (Down != null) doors.Add("bot");
            if (Left != null) doors.Add("left");
            if (Right != null) doors.Add("right");
            while (true)
            {
                //Console.WriteLine(dir);
                switch (dir)
                {
                    case "right":
                        if (Board[pathY - 1][pathX] != Tiles.Empty)
                        {
                            if (Board[pathY][pathX + 1] != Tiles.Empty)
                            {
                                dir = "down";
                            }
                            else
                            {
                                pathX++;
                            }
                        }
                        else
                        {
                            dir = "up";
                            pathY--;
                        }
                        break;
                    case "down":
                        if (Board[pathY][pathX + 1] != Tiles.Empty)
                        {
                            if (Board[pathY + 1][pathX] != Tiles.Empty)
                            {
                                dir = "left";
                            }
                            else
                            {
                                pathY++;
                            }
                        }
                        else
                        {
                            dir = "right";
                            pathX++;
                        }
                        break;
                    case "left":
                        if (Board[pathY + 1][pathX] != Tiles.Empty)
                        {
                            if (Board[pathY][pathX - 1] != Tiles.Empty)
                            {
                                dir = "up";
                            }
                            else
                            {
                                pathX--;
                            }
                        }
                        else
                        {
                            dir = "down";
                            pathY++;
                        }
                        break;
                    case "up":
                        if (Board[pathY][pathX - 1] != Tiles.Empty)
                        {
                            if (Board[pathY - 1][pathX] != Tiles.Empty)
                            {
                                dir = "right";
                            }
                            else
                            {
                                pathY--;
                            }
                        }
                        else
                        {
                            dir = "left";
                            pathX--;
                        }
                        break;
                }
                if (pathX == Width / 2 && pathY == 1)
                {
                    //Console.WriteLine("GORNE DRZWI");
                    if (doors.Contains("top")) doors.Remove("top");
                }
                if (pathX == Width / 2 && pathY == Height - 2)
                {
                    //Console.WriteLine("DOLNE DRZWI");
                    if (doors.Contains("bot")) doors.Remove("bot");
                }
                if (pathX == 1 && pathY == Height / 2)
                {
                    //Console.WriteLine("LEWE DRZWI");
                    if (doors.Contains("left")) doors.Remove("left");
                }
                if (pathX == Width - 2 && pathY == Height / 2)
                {
                    //Console.WriteLine("PRAWE DRZWI");
                    if (doors.Contains("right")) doors.Remove("right");
                }
                if (!leftStart)
                {
                    if (pathX != startPosX || pathY != startPosY)
                    {
                        if (Width % 2 == 0)
                        {
                            if (pathX != startPosX + 1) leftStart = true;
                        }
                        else leftStart = true;
                    }
                }
                if (doors.Count == 0)
                {
                    //Console.WriteLine("Koniec bo zajebiście jest");
                    return true;
                }
                if (moves > 200)
                {
                    //Console.WriteLine("Coś się wyjebało więc wypierdalam kamień dla pewności");
                    return false;
                }
                if (moves > 5 && !leftStart)
                {
                    //Console.WriteLine("Koniec bo zapętlone na starcie");
                    return false;
                }
                if (leftStart && pathX == startPosX && pathY == startPosY)
                {
                    //Console.WriteLine("Koniec wróciliśmy na start a nie doszlismy do wszystkich drzwi");
                    return false;
                }
                moves++;
                //Console.WriteLine("moves: " + moves);
                //Console.WriteLine(doors.Count);

            }
            
        }
    }
}
