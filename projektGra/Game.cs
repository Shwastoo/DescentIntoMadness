using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;

namespace projektGra
{
    public class Game
    {
        public static bool inProgress = true;
        public static bool levelProgress = false;
        public static bool selectProgress = false;
        public static bool levelSelected = false;
        public static Map map;
        public static Level currLevel;
        public static Player player;
        public static List<List<Level>> lSelection;
        public static int currentDepth = 0;
        public static int lSelX;
        public static int lSelY;
        public static bool isSelAccessible = false;
        public static List<Dictionary<string, object>> Themes = Palettes.GetPalleteList();

        public static void Start()
        {
            inProgress = true;
            levelProgress = false;
            selectProgress = false;
            levelSelected = false;
            currentDepth = 0;
        }
        public static void GenerateMap()
        {
            player = new Player();
            map = new Map();
            map.GenerateLevels();
            currLevel = map.Levels[0];
        }
        public static void SelectLevel()
        {
            MusicPlay.LoadMusic("\\assets\\music\\mountain.wav");
            selectProgress = false;
            MusicPlay.PlayMusic();
            MusicPlay.LoadMusic("\\assets\\music\\select.wav");
            lSelection = GUI.LevelSelect();
            lSelX = 0;
            lSelY = lSelection[lSelX].IndexOf(currLevel);
            GUI.UpdateLevelMarker();
            GUI.PrintLevelInfo();
            while (!selectProgress)
            {
                Controls.LevelSelection();
            }
            MusicPlay.Stop();
            MusicPlay.Transition();
            ConsumeFood(true);
            int levelID = lSelection[lSelX][lSelY].ID;
            if(levelID == map.Levels.Count - 1)
            {
                Endgame();
            }
            currLevel = map.Levels[levelID];
            currentDepth++;
            GUI.TheEnd("level");


        }
        public static void GenerateLevel()
        {
            MusicPlay.LoadMusic((string)currLevel.Theme["music"]);
            levelProgress = false;
            GUI.InitializeGUI();
            currLevel.LevelLayout();
            currLevel.CreateMinimap();
            player.placed = false;
            currLevel.PlacePlayer();
            MusicPlay.PlayMusic();
            if(currLevel.Theme!=Palettes.Hell || player.Inv.Contains(Items.Deal)) GUI.UpdateMinimap();
            GUI.RoomLoad();
            while (!levelProgress)
            {
                Controls.AwaitingInput();
            }
            MusicPlay.Stop();
            if (inProgress)
            {
                MusicPlay.LoadMusic("\\assets\\music\\levelEnd.wav");
                MusicPlay.Transition();
                GUI.TheEnd("level");
            }
            //MusicPlay.Stop();
        }
        public static void ConsumeFood(bool outside)
        {
            if (player.Food > 0)
            {
                MusicPlay.Eat();
                player.Food--;
                if (outside && !selectProgress)
                {
                    player.Sanity += 100;
                    player.HP += 100;
                    if (player.Sanity > player.MaxSanity) player.Sanity = player.MaxSanity;
                    if (player.HP > player.MaxHP) player.HP = player.MaxHP;
                }
                else if(!outside)
                {
                    if (currLevel.Theme == Palettes.Kindergarten)
                    {
                        player.Sanity += 100;
                        player.HP += 100;
                        GUI.PrintInfo("You ate food. +100 HP, +100 Sanity");
                    }
                    else
                    {
                        player.Sanity += 50;
                        player.HP += 50;
                        GUI.PrintInfo("You ate food. +50 HP, +50 Sanity");
                    }
                    if (player.Sanity > player.MaxSanity) player.Sanity = player.MaxSanity;
                    if (player.HP > player.MaxHP) player.HP = player.MaxHP;
                }
            }
            else player.HP -= 200;
            if (!DeathCheck())
            {
                GUI.UpdateStats();
            }

        }
        public static void Turn()
        {
            player.TimeFlies();
            if (inProgress)
            {
                foreach (Enemies m in currLevel.CurrentRoom.Monkeys)
                {
                    m.UpdatePos();
                }
                GUI.RoomLoad();
            }
        }
        public static bool DeathCheck()
        {
            if (player.HP <= 0)
            {
                player.HP = 0;
                GUI.UpdateStats();
                Death();
                return true;
            }
            else return false;
        }
        private static void Death()
        {
            MusicPlay.Stop();
            inProgress = false;
            GUI.TheEnd("death");
            MusicPlay.Stop();
            levelProgress = true;
        }
        public static void Endgame()
        {
            MusicPlay.Stop();
            inProgress = false;
            GUI.TheEnd("win");
            MusicPlay.Stop();
            levelProgress = true;
        }
    }
}
