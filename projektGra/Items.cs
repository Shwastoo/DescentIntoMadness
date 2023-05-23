using System;
using System.Collections.Generic;
using System.Text;

namespace projektGra
{
    public class Items
    {
        public static Dictionary<string, object> Food = new Dictionary<string, object>
        {
            { "name", "Food Pack" }
        };
        public static Dictionary<string, object> Compass = new Dictionary<string, object>
        {
            { "name", "Compass" }
        };
        public static Dictionary<string, object> Map = new Dictionary<string, object>
        {
            { "name", "Cave Map" }
        };
        public static Dictionary<string, object> Coffee = new Dictionary<string, object>
        {
            { "name", "Coffee" }
        }; 
        public static Dictionary<string, object> Vitamins = new Dictionary<string, object>
        {
            { "name", "Vitamins" }
        };
        public static Dictionary<string, object> Medication = new Dictionary<string, object>
        {
            { "name", "Medication" }
        }; 
        public static Dictionary<string, object> Gloves = new Dictionary<string, object>
        {
            { "name", "Spongy Gloves" }
        };
        public static Dictionary<string, object> Mask = new Dictionary<string, object>
        {
            { "name", "Gas Mask" }
        };
        public static Dictionary<string, object> Cross = new Dictionary<string, object>
        {
            { "name", "Holy Cross" }
        };
        public static Dictionary<string, object> Boots = new Dictionary<string, object>
        {
            { "name", "Spike Boots" }
        }; 
        public static Dictionary<string, object> Flashlight = new Dictionary<string, object>
        {
            { "name", "Flashlight" }
        };
        public static Dictionary<string, object> Liquorice = new Dictionary<string, object>
        {
            { "name", "Liquorice" }
        };
        public static Dictionary<string, object> Pills = new Dictionary<string, object>
        {
            { "name", "Vomit Pills" }
        };
        public static Dictionary<string, object> Bananas = new Dictionary<string, object>
        {
            { "name", "Rotten Bananas" }
        }; 
        public static Dictionary<string, object> Deal = new Dictionary<string, object>
        {
            { "name", "Devil Deal" }
        };
        public static Dictionary<string, object> Shovel = new Dictionary<string, object>
        {
            { "name", "Shovel" }
        };

        public static List<Dictionary<string, object>> ItemList = new List<Dictionary<string, object>>
        {
            Food,
            Vitamins,
            Medication,
            Compass, 
            Map, 
            Coffee,
            Gloves,
            Mask,
            Cross,
            Boots,
            Flashlight,
            Liquorice,
            Pills,
            Bananas,
            Deal,
            Shovel
        };
    }
}
