using System;
using System.IO;

namespace Assessment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Ask the user for the map name (xxx in xxxMap.txt)
            // -------------------------------------------------------------
            Console.Write("Enter map name (without .txt): ");
            string mapName = Console.ReadLine();
            string filename = mapName + "Map.txt";

            // -------------------------------------------------------------
            // 2. Load the map from file
            // -------------------------------------------------------------
            int[,] map;
            Coord start, goal;

            try
            {
                LoadTerrainFromFile(filename, out map, out start, out goal);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading map: " + ex.Message);
                return;
            }

            // -------------------------------------------------------------
            // 3. Ask the user which algorithm to run
            // -------------------------------------------------------------
            Console.WriteLine("\nChoose algorithm:");
            Console.WriteLine("1 = Breadth First");
            Console.WriteLine("2 = Depth First");
            Console.WriteLine("3 = Hill Climbing");

            Console.Write("Enter number: ");
            int choice = int.Parse(Console.ReadLine());

            Algorithm alg = choice switch
            {
                1 => Algorithm.BreadthFirst,
                2 => Algorithm.DepthFirst,
                3 => Algorithm.HillClimbing,
                _ => Algorithm.BreadthFirst
            };

            // -------------------------------------------------------------
            // 4. Use the factory to create the algorithm instance
            // -------------------------------------------------------------
            PathFinderInterface myPathFinder = PathFinderFactory.NewPathFinder(alg);

            // Path list storage
            LinkedList<Coord> path = new LinkedList<Coord>();

            // -------------------------------------------------------------
            // 5. Call the pathfinder exactly as lecturer wrote
            // -------------------------------------------------------------
            bool success = myPathFinder.FindPath(map, start, goal, ref path);

            Console.WriteLine();
            if (!success)
            {
                Console.WriteLine("NO PATH FOUND.");
                return;
            }

            // -------------------------------------------------------------
            // 6. Display the map with path overlayed
            // -------------------------------------------------------------
            Console.WriteLine("\nMap with path:\n");

            PrintMapWithPath(map, path);

            // -------------------------------------------------------------
            // 7. Print the coordinates of the path
            // -------------------------------------------------------------
            Console.WriteLine("\nPath coordinates:");
            PrintPath(path);

            // -------------------------------------------------------------
            // 8. Save the path to a file
            // -------------------------------------------------------------
            string outFile = mapName + "_Path_" + alg.ToString() + ".txt";
            SavePathToFile(path, outFile);

            Console.WriteLine($"\nPath saved to {outFile}");
            Console.WriteLine("\nDone. Goodbye.");
        }



        // ==================================================================
        // Loads map from file: rows, cols, start coord, goal coord, terrain
        // ==================================================================
        static void LoadTerrainFromFile(string filename,
                                        out int[,] grid,
                                        out Coord start,
                                        out Coord goal)
        {
            string[] lines = File.ReadAllLines(filename);

            // Line 1: dimensions
            string[] dims = lines[0].Split(' ');
            int rows = int.Parse(dims[0]);
            int cols = int.Parse(dims[1]);

            // Line 2: start
            string[] s = lines[1].Split(' ');
            start = new Coord(int.Parse(s[0]), int.Parse(s[1]));

            // Line 3: goal
            string[] g = lines[2].Split(' ');
            goal = new Coord(int.Parse(g[0]), int.Parse(g[1]));

            // Terrain lines
            grid = new int[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                string[] parts = lines[3 + r].Split(' ');
                for (int c = 0; c < cols; c++)
                {
                    grid[r, c] = int.Parse(parts[c]);
                }
            }
        }



        // ==================================================================
        // Prints the map with the path overlayed using '*'
        // ==================================================================
        static void PrintMapWithPath(int[,] map, LinkedList<Coord> path)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            // Put path coords into a lookup table
            bool[,] onPath = new bool[rows, cols];

            var temp = new LinkedList<Coord>();
            while (!path.IsEmpty())
            {
                Coord c = path.PopFront();
                onPath[c.Row, c.Col] = true;
                temp.PushBack(c);
            }
            while (!temp.IsEmpty()) path.PushBack(temp.PopFront());

            // Display the grid
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (onPath[r, c])
                        Console.Write("* ");
                    else
                        Console.Write(map[r, c] + " ");
                }
                Console.WriteLine();
            }
        }



        // ==================================================================
        // Print just the list of coordinates
        // ==================================================================
        static void PrintPath(LinkedList<Coord> path)
        {
            var temp = new LinkedList<Coord>();
            while (!path.IsEmpty())
            {
                Coord c = path.PopFront();
                Console.WriteLine($"({c.Row}, {c.Col})");
                temp.PushBack(c);
            }
            while (!temp.IsEmpty()) path.PushBack(temp.PopFront());
        }



        // ==================================================================
        // Save the coordinates to a text file
        // ==================================================================
        static void SavePathToFile(LinkedList<Coord> path, string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                var temp = new LinkedList<Coord>();
                while (!path.IsEmpty())
                {
                    Coord c = path.PopFront();
                    writer.WriteLine($"{c.Row} {c.Col}");
                    temp.PushBack(c);
                }
                while (!temp.IsEmpty()) path.PushBack(temp.PopFront());
            }
        }
    }
}
