using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment1
{
    public readonly struct Coord
    {
        public int Row { get; }
        public int Col { get; }

        public Coord(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
    public class SearchNode
    {
        public Coord Position { get; }
        public int Cost { get; set; } // cumulative terrain cost (for Dijkstra and A*)
        public int Score { get; set; } // heuristic function (for Hillclimbing onwards)
        public int Estimate => Cost + Score; // Estimated path cost (for A*)

        public SearchNode? Predecessor { get; set; }

        // contructor
        public SearchNode(Coord pos, int cost = 0, int score = 0, SearchNode? pred = null)
        {
            Position = pos;
            Cost = cost;
            Score = score;
            Predecessor = pred;
        }
    }

    public class SearchUtilities
    {
        // Builds a path list by walking the predecessor references between nodes
        // and extracting the coodinates from the visited nodes.
        public static LinkedList<Coord> buildPathList(SearchNode? goal)
        {
            LinkedList<Coord> path = new LinkedList<Coord>();

            // start at the goal and walk backwards
            SearchNode node = goal;
            while (node != null)
            {
                path.PushFront(node.Position);
                node = node.Predecessor;
            }

            return path;
        }

        public static int ManhattanDistance(Coord current, Coord goal)
        {
            int dRow = Math.Abs(current.Row - goal.Row);
            int dCol = Math.Abs(current.Col - goal.Col);

            return dRow + dCol; ;
        }


        // North, East, South, West for generating new nodes
        public static readonly Coord[] Directions = new Coord[]
        {
            new Coord(-1, 0), // North
            new Coord(0, 1),  // East
            new Coord(1, 0),  // South
            new Coord(0, -1)  // West
        };
    }

}

