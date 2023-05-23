using System;
using System.Collections.Generic;
using System.Text;

namespace projektGra
{
    public class Enemies
    {
        public Random Rnd = new Random();
        public int PosX;
        public int PosY;
        public Enemies(string type, int x, int y)
        {
            PosX = x;
            PosY = y;
        }
        public void UpdatePos()
        {
            switch (Rnd.Next(0,4))
            {
                case 0:
                    Movement(PosX, PosY - 1);
                    if (Game.currLevel.Theme == Palettes.Ice && Game.player.Inv.Contains(Items.Boots))
                    {
                        Movement(PosX, PosY - 1);
                    }
                    break;
                case 1:
                    Movement(PosX, PosY + 1);
                    if (Game.currLevel.Theme == Palettes.Ice && Game.player.Inv.Contains(Items.Boots))
                    {
                        Movement(PosX, PosY + 1);
                    }
                    break;
                case 2:
                    Movement(PosX - 1, PosY);
                    if (Game.currLevel.Theme == Palettes.Ice && Game.player.Inv.Contains(Items.Boots))
                    {
                        Movement(PosX - 1, PosY);
                    }
                    break;
                case 3:
                    Movement(PosX + 1, PosY);
                    if (Game.currLevel.Theme == Palettes.Ice && Game.player.Inv.Contains(Items.Boots))
                    {
                        Movement(PosX + 1, PosY);
                    }
                    break;
            }
        }
        private void Movement(int x, int y)
        {
            string posToMove = Game.currLevel.CurrentRoom.Board[y][x];
            if (posToMove == Tiles.Empty)
            {
                Game.currLevel.CurrentRoom.Board[PosY][PosX] = Tiles.Empty;
                PosX = x;
                PosY = y;
                Game.currLevel.CurrentRoom.Board[PosY][PosX] = Tiles.Monkey;
            }
        }
    }
}
