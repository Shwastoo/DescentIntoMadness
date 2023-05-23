using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace projektGra
{
    public class Map
    {
        Random Rnd = new Random();
        public List<List<int>> ConnectionMap = new List<List<int>>();
        public List<Level> Levels = new List<Level>();
        public List<int> RLC = new List<int> { 2, 4, 5 };
        public List<int> template = new List<int> { 2, 3, 4, 5 };
        public List<int> depth1 = new List<int>();
        public List<int> depth2 = new List<int>();
        public int DepthCount = 16;

        public void GenerateLevels()
        {
            for(int i = 0; i <= 3; i++)
            {
                if (i == 0) Levels.Add(new Level(i, 0));
                else {
                    Levels.Add(new Level(i, 1));
                    depth1.Add(i);
                }
                ConnectionMap.Add(new List<int>());
                for (int y = 0; y < Levels.Count-1; y++)
                {
                    ConnectionMap[y].Add(0);
                }
                for (int x = 0; x < Levels.Count; x++) ConnectionMap[Levels.Count - 1].Add(0);
            }
            ConnectionMap[0][1] = 1;
            ConnectionMap[0][2] = 1;
            ConnectionMap[0][3] = 1;
            int lastLevelCount = 3;
            int levelCount;
            int depth = 2;
            while (depth<DepthCount)
            {
                //foreach (int val in RLC) Console.Write(val);
                //Console.Write("\n");
                do
                {
                    levelCount = RLC[Rnd.Next(0, RLC.Count)];
                } while (lastLevelCount == levelCount);
                RLC.AddRange(template);
                RLC.Remove(levelCount);
                for(int count = 0; count < levelCount; count++)
                {
                    int id = Levels.Count;
                    Levels.Add(new Level(id, depth));
                    depth2.Add(id);
                    ConnectionMap.Add(new List<int>());
                    for (int y = 0; y < Levels.Count - 1; y++)
                    {
                        ConnectionMap[y].Add(0);
                    }
                    for (int x = 0; x < Levels.Count; x++) ConnectionMap[Levels.Count - 1].Add(0);
                }

                //connections

                AddConnections();
                depth1 = depth2;
                depth2 = new List<int>();
                lastLevelCount = levelCount;
                depth++;
                
            }

            Levels.Add(new Level(Levels.Count, depth));
            ConnectionMap.Add(new List<int>());
            for (int y = 0; y < Levels.Count - 1; y++)
            {
                ConnectionMap[y].Add(0);
            }
            for (int x = 0; x < Levels.Count; x++) ConnectionMap[Levels.Count - 1].Add(0);
            foreach (int id in depth1) ConnectionMap[id][Levels.Count - 1] = 1;

            for (int lvl = 0; lvl < Levels.Count; lvl++){
                int connCount = ConnectionMap[lvl].Count(x => x == 1);
                Levels[lvl].Paths = connCount;
                int connTo = 0;
                foreach(List<int> row in ConnectionMap)
                {
                    if (row[lvl] == 1) connTo++;
                }
                Levels[lvl].PathsTo = connTo;
            }

            /*
            foreach (List<int> row in ConnectionMap)
            {
                foreach (int val in row) Console.Write(val + " ");
                Console.Write("\n");
            }
            */

            
        }
        private void AddConnections()
        {
            //Z OBAWY O PANA WZROK I ZDROWIE PSYCHICZNE ODRADZAM ZAGLĄDAĆ DO KODU PONIŻEJ
            //Z OBAWY O PANA WZROK I ZDROWIE PSYCHICZNE ODRADZAM ZAGLĄDAĆ DO KODU PONIŻEJ
            //Z OBAWY O PANA WZROK I ZDROWIE PSYCHICZNE ODRADZAM ZAGLĄDAĆ DO KODU PONIŻEJ
            //Z OBAWY O PANA WZROK I ZDROWIE PSYCHICZNE ODRADZAM ZAGLĄDAĆ DO KODU PONIŻEJ
            //Z OBAWY O PANA WZROK I ZDROWIE PSYCHICZNE ODRADZAM ZAGLĄDAĆ DO KODU PONIŻEJ
            //Z OBAWY O PANA WZROK I ZDROWIE PSYCHICZNE ODRADZAM ZAGLĄDAĆ DO KODU PONIŻEJ
            //Z OBAWY O PANA WZROK I ZDROWIE PSYCHICZNE ODRADZAM ZAGLĄDAĆ DO KODU PONIŻEJ
            //Z OBAWY O PANA WZROK I ZDROWIE PSYCHICZNE ODRADZAM ZAGLĄDAĆ DO KODU PONIŻEJ
            //Z OBAWY O PANA WZROK I ZDROWIE PSYCHICZNE ODRADZAM ZAGLĄDAĆ DO KODU PONIŻEJ
            //Z OBAWY O PANA WZROK I ZDROWIE PSYCHICZNE ODRADZAM ZAGLĄDAĆ DO KODU PONIŻEJ
            //Z OBAWY O PANA WZROK I ZDROWIE PSYCHICZNE ODRADZAM ZAGLĄDAĆ DO KODU PONIŻEJ
            //foreach (int x in depth1) Console.Write(x + " ");
            //Console.Write("\n");
            //foreach (int x in depth2) Console.Write(x + " ");
            //Console.Write("\n");
            switch (depth1.Count)
            {
                case 2:
                    switch (depth2.Count)
                    {
                        case 3:
                            switch (Rnd.Next(0, 2))
                            {
                                case 0: //1-2
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[1]][depth2[2]] = 1;
                                    break;
                                case 1: //2-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[0]][depth2[1]] = 1;
                                    ConnectionMap[depth1[1]][depth2[2]] = 1;
                                    break;
                            }
                            break;
                        case 4:
                            switch (Rnd.Next(0, 3))
                            {
                                case 0: //1-3
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[1]][depth2[2]] = 1;
                                    ConnectionMap[depth1[1]][depth2[3]] = 1;
                                    break;
                                case 1: //2-2
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[0]][depth2[1]] = 1;
                                    ConnectionMap[depth1[1]][depth2[2]] = 1;
                                    ConnectionMap[depth1[1]][depth2[3]] = 1;
                                    break;
                                case 2: //3-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[0]][depth2[1]] = 1;
                                    ConnectionMap[depth1[0]][depth2[2]] = 1;
                                    ConnectionMap[depth1[1]][depth2[3]] = 1;
                                    break;
                            }
                            break;
                        case 5:
                            switch (Rnd.Next(0, 2))
                            {
                                case 0: //2-3
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[0]][depth2[1]] = 1;
                                    ConnectionMap[depth1[1]][depth2[2]] = 1;
                                    ConnectionMap[depth1[1]][depth2[3]] = 1;
                                    ConnectionMap[depth1[1]][depth2[4]] = 1;
                                    break;
                                case 1: //3-2
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[0]][depth2[1]] = 1;
                                    ConnectionMap[depth1[0]][depth2[2]] = 1;
                                    ConnectionMap[depth1[1]][depth2[3]] = 1;
                                    ConnectionMap[depth1[1]][depth2[4]] = 1;
                                    break;
                            }
                            break;
                    }
                    break;
                case 3:
                    switch (depth2.Count)
                    {
                        case 2:
                            switch (Rnd.Next(0, 2))
                            {
                                case 0: //1-2
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[2]][depth2[1]] = 1;
                                    break;
                                case 1: //2-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[0]] = 1;
                                    ConnectionMap[depth1[2]][depth2[1]] = 1;
                                    break;
                            }
                            break;
                        case 4:
                            switch (Rnd.Next(0, 3))
                            {
                                case 0: //1-1-2
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[2]][depth2[2]] = 1;
                                    ConnectionMap[depth1[2]][depth2[3]] = 1;
                                    break;
                                case 1: //1-2-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[1]][depth2[2]] = 1;
                                    ConnectionMap[depth1[2]][depth2[3]] = 1;
                                    break;
                                case 2: //2-1-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[0]][depth2[1]] = 1;
                                    ConnectionMap[depth1[1]][depth2[2]] = 1;
                                    ConnectionMap[depth1[2]][depth2[3]] = 1;
                                    break;
                            }
                            break;
                        case 5:
                            switch (Rnd.Next(0, 4))
                            {
                                /*
                                case 0: //1-1-3
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[2]][depth2[2]] = 1;
                                    ConnectionMap[depth1[2]][depth2[3]] = 1;
                                    ConnectionMap[depth1[2]][depth2[4]] = 1;
                                    break;
                                */
                                case 0: //1-3-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[1]][depth2[2]] = 1;
                                    ConnectionMap[depth1[1]][depth2[3]] = 1;
                                    ConnectionMap[depth1[2]][depth2[4]] = 1;
                                    break;
                                /*
                                case 2: //3-1-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[0]][depth2[1]] = 1;
                                    ConnectionMap[depth1[0]][depth2[2]] = 1;
                                    ConnectionMap[depth1[1]][depth2[3]] = 1;
                                    ConnectionMap[depth1[2]][depth2[4]] = 1;
                                    break;
                                */
                                case 1: //1-2-2
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[1]][depth2[2]] = 1;
                                    ConnectionMap[depth1[2]][depth2[3]] = 1;
                                    ConnectionMap[depth1[2]][depth2[4]] = 1;
                                    break;
                                case 2: //2-1-2
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[0]][depth2[1]] = 1;
                                    ConnectionMap[depth1[1]][depth2[2]] = 1;
                                    ConnectionMap[depth1[2]][depth2[3]] = 1;
                                    ConnectionMap[depth1[2]][depth2[4]] = 1;
                                    break;
                                case 3: //2-2-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[0]][depth2[1]] = 1;
                                    ConnectionMap[depth1[1]][depth2[2]] = 1;
                                    ConnectionMap[depth1[1]][depth2[3]] = 1;
                                    ConnectionMap[depth1[2]][depth2[4]] = 1;
                                    break;
                            }
                            break;
                    }
                    break;
                case 4:
                    switch (depth2.Count)
                    {
                        case 2:
                            switch (Rnd.Next(0, 3))
                            {
                                case 0: //1-3
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[2]][depth2[1]] = 1;
                                    ConnectionMap[depth1[3]][depth2[1]] = 1;
                                    break;
                                case 1: //2-2
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[0]] = 1;
                                    ConnectionMap[depth1[2]][depth2[1]] = 1;
                                    ConnectionMap[depth1[3]][depth2[1]] = 1;
                                    break;
                                case 2: //3-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[0]] = 1;
                                    ConnectionMap[depth1[2]][depth2[0]] = 1;
                                    ConnectionMap[depth1[3]][depth2[1]] = 1;
                                    break;
                            }
                            break;
                        case 3:
                            switch (Rnd.Next(0, 3))
                            {
                                case 0: //1-1-2
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[2]][depth2[2]] = 1;
                                    ConnectionMap[depth1[3]][depth2[2]] = 1;
                                    break;
                                case 1: //1-2-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[2]][depth2[1]] = 1;
                                    ConnectionMap[depth1[3]][depth2[2]] = 1;
                                    break;
                                case 2: //2-1-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[0]] = 1;
                                    ConnectionMap[depth1[2]][depth2[1]] = 1;
                                    ConnectionMap[depth1[3]][depth2[2]] = 1;
                                    break;
                            }
                            break;
                        case 5:
                            switch (Rnd.Next(0, 4))
                            {
                                case 0: //1-1-1-2
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[2]][depth2[2]] = 1;
                                    ConnectionMap[depth1[3]][depth2[3]] = 1;
                                    ConnectionMap[depth1[3]][depth2[4]] = 1;
                                    break;
                                case 1: //1-1-2-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[2]][depth2[2]] = 1;
                                    ConnectionMap[depth1[2]][depth2[3]] = 1;
                                    ConnectionMap[depth1[3]][depth2[4]] = 1;
                                    break;
                                case 2: //1-2-1-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[1]][depth2[2]] = 1;
                                    ConnectionMap[depth1[2]][depth2[3]] = 1;
                                    ConnectionMap[depth1[3]][depth2[4]] = 1;
                                    break;
                                case 3: //2-1-1-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[0]][depth2[1]] = 1;
                                    ConnectionMap[depth1[1]][depth2[2]] = 1;
                                    ConnectionMap[depth1[2]][depth2[3]] = 1;
                                    ConnectionMap[depth1[3]][depth2[4]] = 1;
                                    break;
                            }
                            break;
                    }
                    break;
                case 5:
                    switch (depth2.Count)
                    {
                        case 2:
                            switch (Rnd.Next(0, 2))
                            {
                                case 0: //2-3
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[0]] = 1;
                                    ConnectionMap[depth1[2]][depth2[1]] = 1;
                                    ConnectionMap[depth1[3]][depth2[1]] = 1;
                                    ConnectionMap[depth1[4]][depth2[1]] = 1;
                                    break;
                                case 1: //3-2
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[0]] = 1;
                                    ConnectionMap[depth1[2]][depth2[0]] = 1;
                                    ConnectionMap[depth1[3]][depth2[1]] = 1;
                                    ConnectionMap[depth1[4]][depth2[1]] = 1;
                                    break;
                            }
                            break;
                        case 3:
                            switch (Rnd.Next(0, 4))
                            {
                                /*
                                case 0: //1-1-3
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[2]][depth2[2]] = 1;
                                    ConnectionMap[depth1[3]][depth2[2]] = 1;
                                    ConnectionMap[depth1[4]][depth2[2]] = 1;
                                    break;
                                */
                                case 0: //1-3-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[2]][depth2[1]] = 1;
                                    ConnectionMap[depth1[3]][depth2[1]] = 1;
                                    ConnectionMap[depth1[4]][depth2[2]] = 1;
                                    break;
                                /*
                                case 2: //3-1-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[0]] = 1;
                                    ConnectionMap[depth1[2]][depth2[0]] = 1;
                                    ConnectionMap[depth1[3]][depth2[1]] = 1;
                                    ConnectionMap[depth1[4]][depth2[2]] = 1;
                                    break;
                                */
                                case 1: //1-2-2
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[2]][depth2[1]] = 1;
                                    ConnectionMap[depth1[3]][depth2[2]] = 1;
                                    ConnectionMap[depth1[4]][depth2[2]] = 1;
                                    break;
                                case 2: //2-1-2
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[0]] = 1;
                                    ConnectionMap[depth1[2]][depth2[1]] = 1;
                                    ConnectionMap[depth1[3]][depth2[2]] = 1;
                                    ConnectionMap[depth1[4]][depth2[2]] = 1;
                                    break;
                                case 3: //2-2-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[0]] = 1;
                                    ConnectionMap[depth1[2]][depth2[1]] = 1;
                                    ConnectionMap[depth1[3]][depth2[1]] = 1;
                                    ConnectionMap[depth1[4]][depth2[2]] = 1;
                                    break;
                            }
                            break;
                        case 4:
                            switch (Rnd.Next(0, 4))
                            {
                                case 0: //1-1-1-2
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[2]][depth2[2]] = 1;
                                    ConnectionMap[depth1[3]][depth2[3]] = 1;
                                    ConnectionMap[depth1[4]][depth2[3]] = 1;
                                    break;
                                case 1: //1-1-2-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[2]][depth2[2]] = 1;
                                    ConnectionMap[depth1[3]][depth2[2]] = 1;
                                    ConnectionMap[depth1[4]][depth2[3]] = 1;
                                    break;
                                case 2: //1-2-1-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[1]] = 1;
                                    ConnectionMap[depth1[2]][depth2[1]] = 1;
                                    ConnectionMap[depth1[3]][depth2[2]] = 1;
                                    ConnectionMap[depth1[4]][depth2[3]] = 1;
                                    break;
                                case 3: //2-1-1-1
                                    ConnectionMap[depth1[0]][depth2[0]] = 1;
                                    ConnectionMap[depth1[1]][depth2[0]] = 1;
                                    ConnectionMap[depth1[2]][depth2[1]] = 1;
                                    ConnectionMap[depth1[3]][depth2[2]] = 1;
                                    ConnectionMap[depth1[4]][depth2[3]] = 1;
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }
    }
}
