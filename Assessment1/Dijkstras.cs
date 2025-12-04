using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment1
{
    internal class Dijkstras : PathFinderInterface
    {
        public bool FindPath(int[,] map, Coord start, Coord goal, ref LinkedList<Coord> path)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            // Create Open list 
            var open = new PriorityQueue<SearchNode>(
                // Order by cost - lowest cost = highest priority
                (a, b) => a.Cost.CompareTo(b.Cost));

            // Create visited array
            bool[,] visited = new bool[rows, cols];

            // Push start state onto OpenList
            open.Enqueue(new SearchNode(start, cost: 0, score: 0, pred: null));
            // Initialise current node
            SearchNode current = null;


            // Loop until OpenList is empty
            while (!open.IsEmpty())
            {
                // Pop the first element from OpenList
                current = open.Dequeue();

                // Check if current is the goal
                if (current.Position.Row == goal.Row &&
                    current.Position.Col == goal.Col)
                {
                    // Build path
                    path = SearchUtilities.buildPathList(current);
                    return true;
                }

                // Mark current as visited
                visited[current.Position.Row, current.Position.Col] = true;


                // Check each of the 4 directions
                foreach (Coord directions in SearchUtilities.Directions)
                {
                    Coord next = new Coord(
                        current.Position.Row + directions.Row,
                        current.Position.Col + directions.Col
                    );

                    // Check the neighbours
                    if (next.Row < 0 || next.Row >= rows ||
                        next.Col < 0 || next.Col >= cols)
                        continue;

                    // Check for walls
                    if (map[next.Row, next.Col] == 0)
                        continue;

                    // Skip if already visited
                    if (visited[next.Row, next.Col])
                        continue;


                    // The cost to move to neighbour
                    int moveCost = map[next.Row, next.Col];
                    // New cost to reach neighbour
                    int newCost = current.Cost + moveCost;

                    // Create a SearchNode for the neighbour
                    var n = new SearchNode(next);

                    // Check if neighbour is already on open List
                    if (open.Contains(n))
                    {
                        // Get existing node from OpenList
                        SearchNode existing = open.GetExisting(n);
                        // If new cost is not lower then skip to next neighbour
                        if (newCost >= existing.Cost)
                            continue;

                        // Use existing node
                        n = existing;
                    }

                    // n's new predecessor and cost
                    n.Predecessor = current;
                    n.Cost = newCost;

                    // If n was not on open list add it
                    if (!open.Contains(n))
                        open.Enqueue(n);

                }
            }

            // No path found
            path = new LinkedList<Coord>();
            return false;
        }
    }
}
