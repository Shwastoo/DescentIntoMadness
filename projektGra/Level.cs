using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Pastel;

namespace projektGra
{
    public class Level
    {
        Random Rnd = new Random();
        public int ID;
        public int Depth;
        public int Height;
        public int Paths;
        public int PathsTo;
        public List<List<Room>> Layout = new List<List<Room>>();
        public List<List<string>> Minimap = new List<List<string>>();
        public List<Room> Rooms = new List<Room>();
        public Room StartingRoom;
        public Room ExitRoom;
        public Room CurrentRoom;
        public int RoomCount;
        public int FoodCount;
        public Dictionary<string, object> Theme;

        public Level(int id, int depth)
        {
            ID = id;
            Depth = depth;
            if (Depth == Game.map.DepthCount) Height = 0;
            else Height = (Game.map.DepthCount - depth) * 100 + Rnd.Next(-30, 30);
            RoomCount = 4 + Depth*2;

            if (Depth != 0 && Depth != Game.map.DepthCount) Theme = Game.Themes[Rnd.Next(0, Game.Themes.Count)];
            else if (Depth == 0) Theme = Palettes.Start;
            else Theme = Palettes.End;

            if (Theme == Palettes.Desert && !Game.player.Inv.Contains(Items.Shovel)) FoodCount = Rnd.Next(0, RoomCount / 3 + 2);
            else if (Theme == Palettes.Fruit) FoodCount = Rnd.Next(3, RoomCount / 3 + 2);
            else FoodCount = Rnd.Next(2, RoomCount / 3 + 2);
        }
        public void LevelLayout()
        {

            for (int y = 0; y < Config.MinimapSize + 2; y++)
            {
                Layout.Add(new List<Room>());
                Minimap.Add(new List<string>());
                for (int x = 0; x < Config.MinimapSize + 2; x++)
                {
                    if (y == 0 || y == Config.MinimapSize + 1) Layout[y].Add(new Room(false));
                    else if (x == 0 || x == Config.MinimapSize + 1) Layout[y].Add(new Room(false));

                    else Layout[y].Add(null);
                    Minimap[y].Add(" ");
                }
            }
            //int posX = Rnd.Next(1,Config.MinimapSize+1);
            //int posY = Rnd.Next(1,Config.MinimapSize+1);
            //StartingRoom = new Room(posX, posY, 0);
            int posX;
            int posY;
            if (Config.MinimapSize%2 == 0)
            {
                posX = Config.MinimapSize / 2 + Rnd.Next(0, 2);
                posY = Config.MinimapSize / 2 + Rnd.Next(0, 2);
            }
            else
            {
                posX = Config.MinimapSize / 2 + 1;
                posY = Config.MinimapSize / 2 + 1;
            }
            StartingRoom = new Room(posX, posY, 0);
            CurrentRoom = StartingRoom;
            Rooms.Add(StartingRoom);
            Layout[posY][posX] = StartingRoom;
            for (int i = 1; i < RoomCount; i++)
            {
                Room neighbor = Rooms[Rnd.Next(0, Rooms.Count)];
                string side = SelectSide(neighbor);
                
                if (!GenerateNeighbor(neighbor, side)) 
                {
                    i--;
                }
            }
            ChooseExitRoom();
            ChooseFoodRooms();
            ChooseItemRooms();
            AddDoors();
            GenerateRoomContents();
        }
        private void ChooseExitRoom()
        {
            double distance = 0;
            Room endRoom = null;
            foreach(Room r in Rooms)
            {
                double dist = Math.Sqrt(Math.Pow(StartingRoom.posX - r.posX,2) + Math.Pow(StartingRoom.posY - r.posY, 2));
                if (dist >= distance)
                {
                    endRoom = r;
                    distance = dist;
                }
            }
            ExitRoom = endRoom;
        }
        private void ChooseFoodRooms()
        {
            for (int f = 0; f<FoodCount; f++)
            {
                int randID = Rnd.Next(0, Rooms.Count);
                if (!Rooms[randID].containsFood) Rooms[randID].containsFood = true;
                else f--;
            }
        }
        private void ChooseItemRooms()
        {
            int max = 1;
            if (Theme == Palettes.Santa) max = 2;
            for (int f = 0; f < max; f++)
            {
                int randID = Rnd.Next(0, Rooms.Count);
                if (!Rooms[randID].containsItem) Rooms[randID].containsItem = true;
                else f--;
            }
        }
        private void AddDoors()
        {
            for (int y=1;y<Config.MinimapSize+1;y++)
            {
                for (int x=1;x<Config.MinimapSize+1;x++)
                {
                    Room Curr = Layout[y][x];
                    Room Up = Layout[y - 1][x];
                    Room Left = Layout[y][x - 1];
                    Room Right = Layout[y][x + 1];
                    Room Down = Layout[y + 1][x];
                    if (Curr != null)
                    {
                        if (Up != null)
                        {
                            if (Up.ID != -1)
                            {
                                Curr.Up = Up;
                                Up.Down = Curr;
                                if (Curr.Width % 2 == 0) Curr.Board[0][Curr.Width / 2 - 1] = Tiles.DoorHor;
                                Curr.Board[0][Curr.Width / 2] = Tiles.DoorHor;
                                if (Curr.Width % 2 == 0) Up.Board[Up.Height - 1][Up.Width / 2 - 1] = Tiles.DoorHor;
                                Up.Board[Up.Height - 1][Up.Width / 2] = Tiles.DoorHor;
                            }
                        }
                        if (Down != null)
                        {
                            if (Down.ID != -1)
                            {
                                Curr.Down = Down;
                                Down.Up = Curr;
                                if (Curr.Width % 2 == 0) Curr.Board[Curr.Height - 1][Curr.Width / 2 - 1] = Tiles.DoorHor;
                                Curr.Board[Curr.Height - 1][Curr.Width / 2] = Tiles.DoorHor;
                                if (Curr.Width % 2 == 0) Down.Board[0][Down.Width / 2 - 1] = Tiles.DoorHor;
                                Down.Board[0][Down.Width / 2] = Tiles.DoorHor;
                            }
                        }
                        if (Left != null)
                        {
                            if (Left.ID != -1)
                            {
                                Curr.Left = Left;
                                Left.Right = Curr;
                                if (Curr.Height % 2 == 0) Curr.Board[Curr.Height / 2 - 1][0] = Tiles.DoorVer;
                                Curr.Board[Curr.Height / 2][0] = Tiles.DoorVer;
                                if (Curr.Height % 2 == 0) Left.Board[Curr.Height / 2 - 1][Curr.Width - 1] = Tiles.DoorVer;
                                Left.Board[Curr.Height / 2][Curr.Width - 1] = Tiles.DoorVer;
                            }
                        }
                        if (Right != null)
                        {
                            if (Right.ID != -1)
                            {
                                Curr.Right = Right;
                                Right.Left = Curr;
                                if (Curr.Height % 2 == 0) Curr.Board[Curr.Height / 2 - 1][Curr.Width - 1] = Tiles.DoorVer;
                                Curr.Board[Curr.Height / 2][Curr.Width - 1] = Tiles.DoorVer;
                                if (Curr.Height % 2 == 0) Right.Board[Curr.Height / 2 - 1][0] = Tiles.DoorVer;
                                Right.Board[Curr.Height / 2][0] = Tiles.DoorVer;
                            }
                        }
                    }
                }
            }
        }
        private void GenerateRoomContents()
        {
            foreach (Room r in Rooms) r.GenContents();
        }
        public void CreateMinimap()
        {
            for (int y = 0; y < Config.MinimapSize + 2; y++)
            {
                for (int x = 0; x < Config.MinimapSize + 2; x++)
                {
                    if (Layout[y][x] != null)
                    {
                        if (Layout[y][x].ID == -1) Minimap[y][x] = Tiles.Wall;
                        else if (y == CurrentRoom.posY && x == CurrentRoom.posX) Minimap[y][x] = Tiles.CurRoom;
                        else if (Theme == Palettes.School || Game.player.Inv.Contains(Items.Map)) Minimap[y][x] = Tiles.Room;
                        else if (Game.player.Inv.Contains(Items.Compass) && ExitRoom.posX == x && ExitRoom.posY == y) Minimap[y][x] = Tiles.Room;
                    }
                }
            }
            
        }
        public void PlacePlayer()
        {
            Game.player.UpdatePos(StartingRoom.Width / 2 + Rnd.Next(0, 2) - 1, StartingRoom.Height / 2 + Rnd.Next(0, 2) - 1);
            CurrentRoom.Board[Game.player.PosY][Game.player.PosX] = Tiles.Player;
            for(int x = -1; x <= 1; x++)
            {
                for(int y = -1; y <= 1; y++)
                {
                    if(CurrentRoom.Board[Game.player.PosY+y][Game.player.PosX+x] == Tiles.Rock) CurrentRoom.Board[Game.player.PosY+y][Game.player.PosX+x] = Tiles.Empty;
                }
            }
        }
        private string SelectSide(Room n)
        {
            List<string> possibleSides = new List<string>();
            Room Up = Layout[n.posY - 1][n.posX];
            Room Left = Layout[n.posY][n.posX - 1];
            Room Right = Layout[n.posY][n.posX + 1];
            Room Down = Layout[n.posY + 1][n.posX];
            if (Up == null) possibleSides.Add("up");
            if (Down == null) possibleSides.Add("down");
            if (Left == null) possibleSides.Add("left");
            if (Right == null) possibleSides.Add("right");

            if (possibleSides.Count == 0) return "none";
            else return possibleSides[Rnd.Next(0, possibleSides.Count)];
        }
        private bool CheckWalls(Room n)
        {
            Room UpLeft = Layout[n.posY - 1][n.posX - 1];
            Room Up = Layout[n.posY - 1][n.posX];
            Room UpRight = Layout[n.posY - 1][n.posX + 1];
            Room Left = Layout[n.posY][n.posX - 1];
            Room Right = Layout[n.posY][n.posX + 1];
            Room DownLeft = Layout[n.posY + 1][n.posX - 1];
            Room Down = Layout[n.posY + 1][n.posX];
            Room DownRight = Layout[n.posY + 1][n.posX + 1];
            if (Up != null && Right != null) if (Up.ID != -1 && Right.ID != -1) if (UpRight != null) return false;
            if (Right != null && Down != null) if (Right.ID != -1 && Down.ID != -1) if (DownRight != null) return false;
            if (Down != null && Left != null) if (Down.ID != -1 && Left.ID != -1) if (DownLeft != null) return false;
            if (Left != null && Up != null) if (Left.ID != -1 && Up.ID != -1) if (UpLeft != null) return false;
            return true;
        }
        private bool GenerateNeighbor(Room n, string s)
        {
            Room newRoom = null;
            switch (s)
            {
                case "up":
                    newRoom = new Room(n.posX, n.posY - 1, Rooms.Count);
                    break;
                case "down":
                    newRoom = new Room(n.posX, n.posY + 1, Rooms.Count);
                    break;
                case "left":
                    newRoom = new Room(n.posX - 1, n.posY, Rooms.Count);
                    break;
                case "right":
                    newRoom = new Room(n.posX + 1, n.posY, Rooms.Count);
                    break;
                case "none":
                    return false;
            }
            if(CheckWalls(newRoom))
            {
                Rooms.Add(newRoom);
                Layout[newRoom.posY][newRoom.posX] = newRoom;
                return true;
            }
            else return false;
        }
    }
}
