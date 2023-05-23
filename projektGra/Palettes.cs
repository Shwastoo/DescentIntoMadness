using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace projektGra
{
    class Palettes
    {
        public static Color Window = Color.RosyBrown;
        public static Color Path = Color.DarkGoldenrod;

        public static Dictionary<string, object> Basic = new Dictionary<string, object>
        {
            { "name", "Totally Regular Mountain Cave" },
            { "background", Color.FromArgb(117,75,31) },
            { "objects", Color.FromArgb(245, 110, 0) },
            { "rocks", Color.FromArgb(194,87,0) },
            { "music", "\\assets\\music\\basic.wav" },
            { "details", "Nothing is changed" },
        };
        public static Dictionary<string, object> Candy = new Dictionary<string, object>
        {
            { "name", "Candy Cave" },
            { "background", Color.FromArgb(235, 67, 194) },
            { "objects", Color.FromArgb(45,235,233) },
            { "rocks", Color.FromArgb(235,211,91) },
            { "music", "\\assets\\music\\candy.wav" },
            { "details", "Can't resist the candies" }
        };
        public static Dictionary<string, object> Russia = new Dictionary<string, object>
        {
            { "name", "Mother Russia Land" },
            { "background", Color.FromArgb(0, 57, 166) },
            { "objects", Color.FromArgb(213,43,30) },
            { "rocks", Color.FromArgb(255,255,255) },
            { "music", "\\assets\\music\\russia.wav" },
            { "details", "You are drunk" }
        };
        public static Dictionary<string, object> Swamp = new Dictionary<string, object>
        {
            { "name", "Ogre Swamp" },
            { "background", Color.FromArgb(76, 110, 76) },
            { "objects", Color.FromArgb(96,240,94) },
            { "rocks", Color.FromArgb(134,194,133) },
            { "music", "\\assets\\music\\swamp.wav" },
            { "details", "Poison mushrooms replace rocks" }
        };
        public static Dictionary<string, object> Desert = new Dictionary<string, object>
        {
            { "name", "African Desert" },
            { "background", Color.FromArgb(237, 221, 164) },
            { "objects", Color.FromArgb(112,93,24) },
            { "rocks", Color.FromArgb(110,102,76) },
            { "music", "\\assets\\music\\desert.wav" },
            { "details", "No guaranteed food" }
        };
        public static Dictionary<string, object> Haunted = new Dictionary<string, object>
        {
            { "name", "Haunted Peak" },
            { "background", Color.FromArgb(50, 50, 50) },
            { "objects", Color.FromArgb(43,112,18) },
            { "rocks", Color.FromArgb(124,240,82) },
            { "music", "\\assets\\music\\haunt.wav" },
            { "details", "Double sanity loss" }
        };
        public static Dictionary<string, object> Fruit = new Dictionary<string, object>
        {
            { "name", "Fruity Garden" },
            { "background", Color.FromArgb(96, 180, 71) },
            { "objects", Color.FromArgb(240,238,24) },
            { "rocks", Color.FromArgb(240,38,29) },
            { "music", "\\assets\\music\\fruit.wav" },
            { "details", "More food" }
        };
        public static Dictionary<string, object> Sunny = new Dictionary<string, object>
        {
            { "name", "Sunny Paradise" },
            { "background", Color.FromArgb(245, 222, 1) },
            { "objects", Color.FromArgb(12,245,218) },
            { "rocks", Color.FromArgb(245,8,2) },
            { "music", "\\assets\\music\\good.wav" },
            { "details", "Monkeys like to chill here" }
        };
        public static Dictionary<string, object> Factory = new Dictionary<string, object>
        {
            { "name", "Abandoned Factory" },
            { "background", Color.FromArgb(120, 120, 120) },
            { "objects", Color.FromArgb(153,194,246) },
            { "rocks", Color.FromArgb(194,192,149) },
            { "music", "\\assets\\music\\factory.wav" },
            { "details", "Not a single soul here" }
        };
        public static Dictionary<string, object> Space = new Dictionary<string, object>
        {
            { "name", "Outer Space" },
            { "background", Color.FromArgb(20, 20, 20) },
            { "objects", Color.FromArgb(18,12,245) },
            { "rocks", Color.FromArgb(72,0,173) },
            { "music", "\\assets\\music\\space.wav" },
            { "details", "Less rocks" }
        };
        public static Dictionary<string, object> Kindergarten = new Dictionary<string, object>
        {
            { "name", "Kindergarten" },
            { "background", Color.FromArgb(44, 178, 245) },
            { "objects", Color.FromArgb(245,239,20) },
            { "rocks", Color.FromArgb(245,44,64) },
            { "music", "\\assets\\music\\kinder.wav" },
            { "details", "As peaceful as the outside" }
        };
        public static Dictionary<string, object> School = new Dictionary<string, object>
        {
            { "name", "American Highschool" },
            { "background", Color.FromArgb(238, 252, 238) },
            { "objects", Color.FromArgb(24,83,245) },
            { "rocks", Color.FromArgb(245,47,0) },
            { "music", "\\assets\\music\\school.wav" },
            { "details", "Floor plan in every room" }
        };
        public static Dictionary<string, object> Ice = new Dictionary<string, object>
        {
            { "name", "Icey Dungeon" },
            { "background", Color.FromArgb(0, 221, 245) },
            { "objects", Color.FromArgb(5,111,250) },
            { "rocks", Color.FromArgb(245,245,245) },
            { "music", "\\assets\\music\\ice.wav" },
            { "details", "Super slippery floor" }
        };
        public static Dictionary<string, object> Santa = new Dictionary<string, object>
        {
            { "name", "Santa's Workshop" },
            { "background", Color.FromArgb(168,8,0) },
            { "objects", Color.FromArgb(0, 168, 64) },
            { "rocks", Color.FromArgb(237,255,244) },
            { "music", "\\assets\\music\\santa.wav" },
            { "details", "More items (because Christmas)" }
        };
        public static Dictionary<string, object> Electric = new Dictionary<string, object>
        {
            { "name", "Power Station" },
            { "background", Color.FromArgb(245, 41, 0) },
            { "objects", Color.FromArgb(245,230,24) },
            { "rocks", Color.FromArgb(10,10,10) },
            { "music", "\\assets\\music\\electric.wav" },
            { "details", "No power (oh, the irony)" }
        };
        public static Dictionary<string, object> Hell = new Dictionary<string, object>
        {
            { "name", "Hell On Earth" },
            { "background", Color.FromArgb(255, 0, 0) },
            { "objects", Color.FromArgb(10,10,10) },
            { "rocks", Color.FromArgb(245,102,0) },
            { "music", "\\assets\\music\\hell.wav" },
            { "details", "No minimap" }
        };
        public static List<Dictionary<string, object>> Themes = new List<Dictionary<string, object>> 
        {
            Basic, Candy, Russia, Swamp, Desert, Haunted, Fruit, Sunny, Factory, Space, Kindergarten, School, Ice, Santa, Electric, Hell
        };
        public static Dictionary<string, object> Start = new Dictionary<string, object>
        {
            { "name", "Start" },
            { "background", Color.FromArgb(204, 204, 204) },
            { "objects", Color.FromArgb(0,0,0) },
            { "rocks", Color.FromArgb(0,0,0) },
            { "details", "You start here" }
        };
        public static Dictionary<string, object> End = new Dictionary<string, object>
        {
            { "name", "End" },
            { "background", Color.FromArgb(204, 204, 204) },
            { "objects", Color.FromArgb(0,0,0) },
            { "rocks", Color.FromArgb(0,0,0) },
            { "details", "The end of adventure" }
        };
        public static List<Dictionary<string, object>> GetPalleteList()
        {
            return Themes;
        }
    }

    
}
