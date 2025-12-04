using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assessment1.SearchNode;

namespace Assessment1
{
    internal class BestFirst : PathFinderInterface
    {
        public bool FindPath(int[,] map, Coord start, Coord goal, ref LinkedList<Coord> path)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            // Create OpenList priority queue 
            var open = new PriorityQueue<SearchNode>(
                // Order by score (lowest score = highest priority)
                (a, b) => a.Score.CompareTo(b.Score)    
            );

            // Create ClosedList priority queue
            var closed = new PriorityQueue<SearchNode>(
                // Order doesn't matter here
                (a, b) => 0        
            );

            // Put the start node on OPEN
            int startScore = SearchUtilities.ManhattanDistance(start, goal);
            open.Enqueue(new SearchNode(start, 0, startScore, null));
            // Initialise current node
            SearchNode current = null;

            // Loop until open list is empty
            while (!open.IsEmpty())
            {
                // Remove node with lowest score from open list and call it current
                current = open.Dequeue();

                // Check if current is the goal
                if (current.Position.Row == goal.Row &&
                    current.Position.Col == goal.Col)
                {
                    // Build path and return true
                    path = SearchUtilities.buildPathList(current);
                    return true;
                }

                // Add current to closed list
                closed.Enqueue(current);

                // For each of the 4 directions
                foreach (Coord directions in SearchUtilities.Directions)
                {
                    Coord next = new Coord(
                        current.Position.Row + directions.Row,
                        current.Position.Col + directions.Col);

                    // Check the neighbours
                    if (next.Row < 0 || next.Row >= rows ||
                        next.Col < 0 || next.Col >= cols)
                        continue;

                    // Check for obstacles
                    if (map[next.Row, next.Col] == 0)
                        continue;

                    // Create a SearchNode for the neighbour
                    var nextNode = new SearchNode(next);

                    // If it is already in open or closed list, continue
                    if (open.Contains(nextNode) || closed.Contains(nextNode)) 
                        continue;
                   

                    // Calculate score - Manhattan distance to goal
                    nextNode.Score = SearchUtilities.ManhattanDistance(next, goal);

                    // Set the predecessor
                    nextNode.Predecessor = current;

                    // Add it to the open list
                    open.Enqueue(nextNode);
                }
            }

            // No path found
            path = new LinkedList<Coord>();
            return false;
        }
    }
}
