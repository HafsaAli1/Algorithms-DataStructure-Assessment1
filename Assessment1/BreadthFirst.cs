using System;
using static Assessment1.SearchNode;

namespace Assessment1
{
    internal class BreadthFirst : PathFinderInterface
    {
        public bool FindPath(int[,] map, Coord start, Coord goal, ref LinkedList<Coord> path)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            // Create Open and Closed lists (Queues)
            var open = new Queue<SearchNode>();
            var closed = new Queue<SearchNode>();

            // Put the start node on open list
            var startNode = new SearchNode(start);
            open.Enqueue(startNode);

            SearchNode current = null;

            // Loop until open list is empty
            while (!open.IsEmpty())
            {
                // Remove first item and call it Current
                current = open.Dequeue();

                // Check if current is the goal
                if (current.Position.Row == goal.Row &&
                    current.Position.Col == goal.Col)
                {
                    // Build path and return true 
                    path = SearchUtilities.buildPathList(current);
                    return true;
                }

                // Add Current to closed list
                closed.Enqueue(current);

                // Check each of the 4 directions
                foreach (Coord direction in SearchUtilities.Directions)
                {
                    Coord next = new Coord(
                        current.Position.Row + direction.Row,
                        current.Position.Col + direction.Col
                    );

                    // Check the neighbours 
                    if (next.Row < 0 || next.Row >= rows ||
                        next.Col < 0 || next.Col >= cols)
                        continue;

                    // Check if there is a wall
                    if (map[next.Row, next.Col] == 0)
                        continue;

                    // A SearchNode for the neighbour
                    SearchNode nextNode = new SearchNode(next);

                    // Check if it is already in closed list
                    if (closed.Contains(nextNode))
                        continue;

                    // Check if it is already in open list
                    if (open.Contains(nextNode))
                        continue;

                    // Add neighbour to open list
                    open.Enqueue(new SearchNode(next, 0, 0, current));
                }
            }

            // No path found
            path = new LinkedList<Coord>();
            return false;
        }
    }
}
