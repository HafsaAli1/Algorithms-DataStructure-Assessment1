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
        public SearchNode? Predecessor { get; set; }

        // contructor
        public SearchNode(Coord pos, int cost = 0, int score = 0, SearchNode? pred = null)
        {
            Position = pos;
            Cost = cost;
            Score = score;
            Predecessor = pred;
        }


        // Override Equals and GetHashCode so SearchNode comparisons
        public override bool Equals(object? obj)
        {
            // If the other object is null or not a SearchNode then return false
            if (obj is not SearchNode other)
                return false;

            // Two nodes are equal if their coordinates match
            return this.Position.Row == other.Position.Row &&
                   this.Position.Col == other.Position.Col;
        }

        // Override GetHashCode so hash matches Equals behaviour
        public override int GetHashCode()
        {
            // Combine row and col so hash matches Equals behaviour
            return HashCode.Combine(Position.Row, Position.Col);
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

            // Calculate the Manhattan Distance between two coordinates
            public static int ManhattanDistance(Coord current, Coord goal)
            {
                // Calculate the differences in rows and columns
                int dRow = Math.Abs(current.Row - goal.Row);
                int dCol = Math.Abs(current.Col - goal.Col);
                // Return the sum of the differences
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



