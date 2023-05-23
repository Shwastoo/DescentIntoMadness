using System;
using System.Collections.Generic;
using System.Text;

namespace projektGra
{
    public class Player
    {
        public Random Rnd = new Random();
        public int PosX;
        public int PosY;
        public bool placed = false;
        public int HP = 500;
        public int MaxHP = 500;
        public int Sanity = 500;
        public int MaxSanity = 500;
        public int Food = 0;
        public List<Dictionary<string, object>> Inv = new List<Dictionary<string, object>>();

        public void UpdatePos(int x, int y)
        {
            if(placed) Game.currLevel.CurrentRoom.Board[Game.player.PosY][Game.player.PosX] = Tiles.Empty;
            PosX = x;
            PosY = y;
            Game.currLevel.CurrentRoom.Board[PosY][PosX] = Tiles.Player;
            if (!placed) placed = true;

        }
        public void GetItem()
        {
            MusicPlay.CollectFood();
            int id = Rnd.Next(0, Items.ItemList.Count);
            Dictionary<string, object> rndItem = Items.ItemList[id];
            if(Inv.Contains(rndItem) && id > 2) rndItem = Items.ItemList[Rnd.Next(0, 3)];
            Inv.Add(rndItem);
            GUI.PrintInfo("You collected " + (string)rndItem["name"]);
            GUI.UpdateInventory();
            if(rndItem == Items.Food)
            {
                Food += 3;
            }
            else if(rndItem == Items.Compass)
            {
                Game.currLevel.CreateMinimap();
            }
            else if(rndItem == Items.Map)
            {
                Game.currLevel.CreateMinimap();
            }
            else if(rndItem == Items.Vitamins)
            {
                MaxHP += 100;
                HP += 100;
            }
            else if (rndItem == Items.Medication)
            {
                MaxSanity += 100;
                Sanity += 100;
            }
        }
        public void TimeFlies()
        {
            
            if ((Game.currLevel.Theme == Palettes.Haunted && !Game.player.Inv.Contains(Items.Cross)) || (Game.currLevel.Theme == Palettes.Hell && Game.player.Inv.Contains(Items.Deal)))
            {
                Sanity-=2;
            }
            else 
            {
                Sanity-=1;
            }
            if (Sanity < 0)
            {
                Sanity = 0;
                MaxSanity -= 10;
                HP -= 20;
                MaxHP -= 10;
            }
            if(!Game.DeathCheck()) GUI.UpdateStats();
        }
    }
}
