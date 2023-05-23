using System;
using System.Collections.Generic;
using System.Text;
using Pastel;
using System.Drawing;

namespace projektGra
{
    class Tiles
    {
        public static string Wall = "▓";
        public static string Empty = " ";
        public static string Player = "℗";
        public static string Room = "░";
        public static string CurRoom = "█";
        public static string DoorHor = "═";
        public static string DoorVer = "║";
        public static string Rock = "▒";
        public static string Mush = "֍";
        public static string Exit = "Ω";
        public static string Monkey = "@";
        public static string Crate = "◙";
        public static string WindowULCorner = "╔".Pastel(Palettes.Window);
        public static string WindowURCorner = "╗".Pastel(Palettes.Window);
        public static string WindowDLCorner = "╚".Pastel(Palettes.Window);
        public static string WindowDRCorner = "╝".Pastel(Palettes.Window);
        public static string WindowHor = "═".Pastel(Palettes.Window);
        public static string WindowHorUp = "╩".Pastel(Palettes.Window);
        public static string WindowHorDown = "╦".Pastel(Palettes.Window);
        public static string WindowVer = "║".Pastel(Palettes.Window);
        public static string WindowVerRight = "╠".Pastel(Palettes.Window);
        public static string WindowVerLeft = "╣".Pastel(Palettes.Window);
        public static string Path = "─────".Pastel(Palettes.Path);
        //public static string Path = "─────○─────";
        //public static string PathOK = "─────●─────";
        //public static string Level = "○";
        //public static string LevelOK = "●";
        public static string Level = "∆";
        public static string LevelOK = "▲";
        public static string PHor = "─".Pastel(Palettes.Path);
        public static string PVer = "│".Pastel(Palettes.Path);
        public static string PLD = "┐".Pastel(Palettes.Path);
        public static string PLU = "┘".Pastel(Palettes.Path);
        public static string PLVer = "┤".Pastel(Palettes.Path);
        public static string PRD = "┌".Pastel(Palettes.Path);
        public static string PRU = "└".Pastel(Palettes.Path);
        public static string PRVer = "├".Pastel(Palettes.Path);
        public static string PDHor = "┬".Pastel(Palettes.Path);
        public static string PUHor = "┴".Pastel(Palettes.Path);
        public static string PCross = "┼".Pastel(Palettes.Path);
        public static List<string> Path13 = new List<string> {PRD,PVer,PVer,PVer,PCross,PVer,PVer,PVer,PRU};
        public static List<string> Path23_12 = new List<string> {PRD,PLU,Empty,Empty,PRD,PVer,PVer,PLVer,PRU};
        public static List<string> Path23_21 = new List<string> {PRD,PLVer,PVer,PVer,PRU,Empty,Empty,PLD,PRU};
        public static List<string> Path24_31 = new List<string> {PRD,PVer,PLVer,PRVer,PVer,PVer,PVer,PRU,PLD,PVer,PRU};
        public static List<string> Path24_13 = new List<string> {PRD,PVer,PLU,PRD,PVer,PVer,PVer,PRVer,PLVer,PVer,PRU};
        public static List<string> Path24_22 = new List<string> {PRD,PVer,PLVer,PRU,Empty,Empty,Empty,PRD,PLVer,PVer,PRU};
        public static List<string> Path25_32 = new List<string> {PRD,PVer,PVer,PCross,PVer,PVer,PRU,Empty,Empty,PDHor,PVer,PVer,PRU};
        public static List<string> Path25_23 = new List<string> {PRD,PVer,PVer,PUHor,Empty,Empty,PRD,PVer,PVer,PCross,PVer,PVer,PRU};
        public static List<string> Path32_12 = new List<string> { PLD, PRU, Empty, Empty, PLD, PVer, PVer, PRVer, PLU };
        public static List<string> Path32_21 = new List<string> { PLD, PRVer, PVer, PVer, PLU, Empty, Empty, PRD, PLU };
        public static List<string> Path34_211 = new List<string> { PRD, PLVer, PVer, PRU, Empty, PLD, PVer, PRU, Empty, PLD, PRU };
        public static List<string> Path34_121 = new List<string> { PRD, PLU, Empty, PRD, PVer, PLVer, PVer, PRU, Empty, PLD, PRU };
        public static List<string> Path34_112 = new List<string> { PRD, PLU, Empty, PRD, PVer, PLU, Empty, PRD, PVer, PLVer, PRU };
        public static List<string> Path35_131 = new List<string> { PRD,PVer,PLU,PRD,PVer,PVer,PCross,PVer,PVer,PRU,PLD,PVer,PRU};
        public static List<string> Path35_221 = new List<string> { PRD,PVer,PLVer,PRU,Empty,Empty,PDHor,PVer,PVer,PRU,PLD,PVer,PRU};
        public static List<string> Path35_212 = new List<string> { PRD,PVer,PLVer,PRU,Empty,Empty,PHor,Empty,Empty,PRD,PLVer,PVer,PRU};
        public static List<string> Path35_122 = new List<string> { PRD,PVer,PLU,PRD,PVer,PVer,PUHor,Empty,Empty,PRD,PLVer,PVer,PRU};
        public static List<string> Path42_31 = new List<string> { PLD, PVer, PRVer, PLVer, PVer, PVer, PVer, PLU, PRD, PVer, PLU };
        public static List<string> Path42_13 = new List<string> { PLD, PVer, PRU, PLD, PVer, PVer, PVer, PLVer, PRVer, PVer, PLU };
        public static List<string> Path42_22 = new List<string> { PLD, PVer, PRVer, PLU, Empty, Empty, Empty, PLD, PRVer, PVer, PLU };
        public static List<string> Path43_211 = new List<string> { PLD, PRVer, PVer, PLU, Empty, PRD, PVer, PLU, Empty, PRD, PLU };
        public static List<string> Path43_121 = new List<string> { PLD, PRU, Empty, PLD, PVer, PRVer, PVer, PLU, Empty, PRD, PLU };
        public static List<string> Path43_112 = new List<string> { PLD, PRU, Empty, PLD, PVer, PRU, Empty, PLD, PVer, PRVer, PLU };
        public static List<string> Path45_2111 = new List<string> { PRD,PLVer,PVer,PRU,PLD,PVer,PRU,Empty,PLD,PRU,Empty,PLD,PRU };
        public static List<string> Path45_1211 = new List<string> { PRD,PLU,Empty,PRD,PLVer,PVer,PRU,Empty,PLD,PRU,Empty,PLD,PRU };
        public static List<string> Path45_1121 = new List<string> { PRD,PLU,Empty,PRD,PLU,Empty,PRD,PVer,PLVer,PRU,Empty,PLD,PRU };
        public static List<string> Path45_1112 = new List<string> { PRD,PLU,Empty,PRD,PLU,Empty,PRD,PVer,PLU,PRD,PVer,PLVer,PRU };
        public static List<string> Path52_32 = new List<string> { PLD, PVer, PVer, PCross, PVer, PVer, PLU, Empty, Empty, PDHor, PVer, PVer, PLU };
        public static List<string> Path52_23 = new List<string> { PLD, PVer, PVer, PUHor, Empty, Empty, PLD, PVer, PVer, PCross, PVer, PVer, PLU };
        public static List<string> Path53_131 = new List<string> { PLD, PVer, PRU, PLD, PVer, PVer, PCross, PVer, PVer, PLU, PRD, PVer, PLU };
        public static List<string> Path53_221 = new List<string> { PLD, PVer, PRVer, PLU, Empty, Empty, PDHor, PVer, PVer, PLU, PRD, PVer, PLU };
        public static List<string> Path53_212 = new List<string> { PLD, PVer, PRVer, PLU, Empty, Empty, PHor, Empty, Empty, PLD, PRVer, PVer, PLU };
        public static List<string> Path53_122 = new List<string> { PLD, PVer, PRU, PLD, PVer, PVer, PUHor, Empty, Empty, PLD, PRVer, PVer, PLU };
        public static List<string> Path54_2111 = new List<string> { PLD, PRVer, PVer, PLU, PRD, PVer, PLU, Empty, PRD, PLU, Empty, PRD, PLU };
        public static List<string> Path54_1211 = new List<string> { PLD, PRU, Empty, PLD, PRVer, PVer, PLU, Empty, PRD, PLU, Empty, PRD, PLU };
        public static List<string> Path54_1121 = new List<string> { PLD, PRU, Empty, PLD, PRU, Empty, PLD, PVer, PRVer, PLU, Empty, PRD, PLU };
        public static List<string> Path54_1112 = new List<string> { PLD, PRU, Empty, PLD, PRU, Empty, PLD, PVer, PRU, PLD, PVer, PRVer, PLU };
        public static List<string> Path21 = new List<string> { PLD,PVer,PVer,PRVer,PVer,PVer,PLU };
        public static List<string> Path31 = new List<string> { PLD,PVer,PVer,PVer,PCross,PVer,PVer,PVer,PLU };
        public static List<string> Path41 = new List<string> { PLD,PVer,PVer,PLVer,PVer,PRVer,PVer,PLVer,PVer,PVer,PLU };
        public static List<string> Path51 = new List<string> { PLD,PVer,PVer,PLVer,PVer,PVer,PCross,PVer,PVer,PLVer,PVer,PVer,PLU };
        public static string CurrLMarker = "⌂".Pastel(Color.Green);
        public static string SelMarker = "▼".Pastel(Color.Green);
        public static string HP = "♥".Pastel(Color.Red);
        public static string San = "☻".Pastel(Color.Cyan);
        public static string BarHP = "■";
        public static string BarSan = "■";
        public static string Food = "♣";




    }
}
