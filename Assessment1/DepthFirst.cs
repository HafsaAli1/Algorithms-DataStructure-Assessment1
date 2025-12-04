using System;
using static Assessment1.SearchNode;

namespace Assessment1
{
    internal class DepthFirst : PathFinderInterface
    {
        public bool FindPath(int[,] map, Coord start, Coord goal, ref LinkedList<Coord> path)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            // Create open list 
            var open = new Stack<SearchNode>();

            // Create closed list
            var closed = new LinkedList<Coord>();

            // Put the start node on open list
            open.Push(new SearchNode(start));
            // Initialise current node
            SearchNode current = null;

            // Loop until open list is empty
            while (!open.IsEmpty())
            {
                // Pop the top item from open list and call it Current
                current = open.Pop();

                // Check if current is the goal
                if (current.Position.Row == goal.Row &&
                    current.Position.Col == goal.Col)
                {
                    // Build path and return true
                    path = SearchUtilities.buildPathList(current);
                    return true;
                }

                // Add Current to closed list
                closed.PushFront(current.Position);

                // Check each of the 4 directions
                foreach (Coord dir in SearchUtilities.Directions)
                {
                    Coord next = new Coord(
                        current.Position.Row + dir.Row,
                        current.Position.Col + dir.Col
                    );

                    // Check the neighbours
                    if (next.Row < 0 || next.Row >= rows ||
                        next.Col < 0 || next.Col >= cols)
                        continue;

                    // Check if there is a wall
                    if (map[next.Row, next.Col] == 0)
                        continue;

                    // Check if already in closed list
                    if (closed.Contains(next))
                        continue;

                    // Check if already in open list
                    if (open.Contains(new SearchNode(next)))
                        continue;

                    // Add neighbour to open list
                    open.Push(new SearchNode(next, 0, 0, current));
                }
            }

            // No path found
            path = new LinkedList<Coord>();
            return false;
        }
    }
}

