using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment1
{
    internal class HillClimbing : PathFinderInterface
    {
        public bool FindPath(int[,] map, Coord start, Coord goal, ref LinkedList<Coord> path)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            // Create open list
            var open = new Queue<SearchNode>();

            // Create closed list
            var closed = new Queue<SearchNode>();

            // create temporary list for neighbours
            var tmp = new Queue<SearchNode>();

            // Create visited array
            bool[,] visited = new bool[rows, cols];

            // Put the start node on open list and mark as visited
            open.Enqueue(new SearchNode(start));
            visited[start.Row, start.Col] = true;

            // Initialise current node
            SearchNode? current = null;

            // Loop until open list is empty
            while (!open.IsEmpty())
            {
                // Remove the first item from open list and call it current
                current = open.Dequeue();

                // Check if current is the goal
                if (current.Position.Row == goal.Row &&
                    current.Position.Col == goal.Col)
                {
                    // Build path 
                    path = SearchUtilities.buildPathList(current);
                    return true;
                }

                // Add current to closed list
                closed.Enqueue(current);

                // Check each of the 4 directions
                foreach (Coord direction in SearchUtilities.Directions)
                {
                    Coord next = new Coord(
                        current.Position.Row + direction.Row,
                        current.Position.Col + direction.Col
                    );
                

                // Check inside map bounds
                if (next.Row < 0 || next.Row >= rows ||
                        next.Col < 0 || next.Col >= cols)
                        continue;

                    // Check walls 
                    if (map[next.Row, next.Col] == 0)
                        continue;

                    // Check if already visited
                    if (visited[next.Row, next.Col])
                        continue;

                    // Calculate heuristic value
                    int h = SearchUtilities.ManhattanDistance(next, goal);

                    // Create SearchNode for neighbour
                    var node = new SearchNode(next, 0, h, current);

                    // Mark as visited and add to Temporary list
                    visited[next.Row, next.Col] = true;
                    tmp.Enqueue(node);
                }

                // If Temporary list is empty continue
                if (tmp.IsEmpty())
                    continue;

                var list = new List<SearchNode>();
                // Move TmpList to a List for sorting
                while (!tmp.IsEmpty())
                    list.Add(tmp.Dequeue());
                // Sort by heuristic value
                list.Sort((a, b) => a.Score.CompareTo(b.Score));

                // Add TmpList to the front of OpenList
                var old = new Queue<SearchNode>();
                // Move OpenList to old list
                while (!open.IsEmpty())
                    old.Enqueue(open.Dequeue());

                // Move sorted neighbours to OpenList
                foreach (var node in list)
                    open.Enqueue(node);

                // Move old OpenList items to the back of OpenList
                while (!old.IsEmpty())
                    open.Enqueue(old.Dequeue());
            }

            // No path found
            path = new LinkedList<Coord>();
            return false;
        }
    }
}
