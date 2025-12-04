using System;
using System.IO;

namespace Assessment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Input map name (xxx in xxxMap.txt)
            Console.Write("Enter map name: ");

            // Get the map filename
            string mapName = Console.ReadLine();
            string filename = mapName + "Map.txt";


            // 2. Load the map from file
            int[,] map;
            Coord start, goal;

            try
            {
                LoadTerrainFromFile(filename, out map, out start, out goal);
            }
            // 
            catch (Exception ex)
            {
                Console.WriteLine("Error loading map: " + ex.Message);
                return;
            }


            // Choose which algorithm to run
            Console.WriteLine("\nChoose algorithm:");
            Console.WriteLine("1 = Breadth First");
            Console.WriteLine("2 = Depth First");
            Console.WriteLine("3 = Hill Climbing");
            Console.WriteLine("4 = Best First");
            Console.WriteLine("5 = Dijkstras");

            Console.Write("Enter number: ");
            int choice = int.Parse(Console.ReadLine());

            // Use number to select algorithm
            Algorithm alg = choice switch
            {
                1 => Algorithm.BreadthFirst,
                2 => Algorithm.DepthFirst,
                3 => Algorithm.HillClimbing,
                4 => Algorithm.BestFirst,
                5 => Algorithm.Dijkstras,
                _ => Algorithm.BreadthFirst
            };


            // Instantiate the chosen pathfinder
            PathFinderInterface myPathFinder = PathFinderFactory.NewPathFinder(alg);

            // A place to store the path
            LinkedList<Coord> path = new LinkedList<Coord>();


            // Call the pathfinder 
            bool success = myPathFinder.FindPath(map, start, goal, ref path);

            Console.WriteLine();
            if (!success)
            {
                Console.WriteLine("NO PATH FOUND.");
                return;
            }


            // Display the map with path overlayed
            Console.WriteLine("\nMap with path:\n");
            MapPath(map, path);


            // 7. Print the coordinates of the path
            Console.WriteLine("\nPath coordinates:");
            PrintPath(path);

  
            // 8. Save the path to a file
            string outFile = mapName + "_Path_" + alg.ToString() + ".txt";
            SavePathToFile(path, outFile);
            Console.WriteLine($"\nPath saved to {outFile}");
            Console.WriteLine("\nGoodbye.");
        }



        // Loads map from file:
        static void LoadTerrainFromFile(string filename,
                                        out int[,] grid,
                                        out Coord start,
                                        out Coord goal)
        {
            string[] lines = File.ReadAllLines(filename);

            // Line 1 is dimensions
            string[] dims = lines[0].Split(' ');
            int rows = int.Parse(dims[0]);
            int cols = int.Parse(dims[1]);

            // Line 2 is start coordinates
            string[] s = lines[1].Split(' ');
            start = new Coord(int.Parse(s[0]), int.Parse(s[1]));

            // Line 3 is goal coordinates
            string[] g = lines[2].Split(' ');
            goal = new Coord(int.Parse(g[0]), int.Parse(g[1]));

            // Terrain lines
            grid = new int[rows, cols];

            // Read each row
            for (int r = 0; r < rows; r++)
            {
                // Split the line into parts
                string[] parts = lines[3 + r].Split(' ');
                // Read each column
                for (int c = 0; c < cols; c++)
                {
                    // Parse the integer and store in grid
                    grid[r, c] = int.Parse(parts[c]);
                }
            }
        }



        // Prints the map with the path overlayed using #
        static void MapPath(int[,] map, LinkedList<Coord> path)
        {
            // Get map dimensions
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            // Create a 2D array to mark path cells
            bool[,] onPath = new bool[rows, cols];

            var temp = new LinkedList<Coord>();
            while (!path.IsEmpty())
            {
                Coord c = path.PopFront();
                onPath[c.Row, c.Col] = true;
                temp.PushBack(c);
            }
            // Restore the path list
            while (!temp.IsEmpty()) path.PushBack(temp.PopFront());

            // Display the grid
            for (int r = 0; r < rows; r++)
            {
                // For each column
                for (int c = 0; c < cols; c++)
                {
                    // Check if this cell is on the path
                    if (onPath[r, c])
                        // Print P for path
                        Console.Write("# ");
                    // Not on path so print original value
                    else
                        Console.Write(map[r, c] + " ");
                }
                Console.WriteLine();
            }
        }



        // Print just the list of coordinates
        static void PrintPath(LinkedList<Coord> path)
        {
            var temp = new LinkedList<Coord>();
            while (!path.IsEmpty())
            {
                // Remove from front
                Coord c = path.PopFront();
                // Print coordinate
                Console.WriteLine($"({c.Row}, {c.Col})");
                // Store in temp to restore later
                temp.PushBack(c);
            }
            while (!temp.IsEmpty()) path.PushBack(temp.PopFront());
        }



        // Save the coordinates to a text file
        static void SavePathToFile(LinkedList<Coord> path, string filename)
        {
            // Open the file for writing
            using (StreamWriter writer = new StreamWriter(filename))
            {
                // 
                var temp = new LinkedList<Coord>();
                // 
                while (!path.IsEmpty())
                {
                    // Remove from front
                    Coord c = path.PopFront();
                    // Write coordinate to file
                    writer.WriteLine($"{c.Row} {c.Col}");
                    temp.PushBack(c);
                }
                while (!temp.IsEmpty()) path.PushBack(temp.PopFront());
            }
        }
    }
}
