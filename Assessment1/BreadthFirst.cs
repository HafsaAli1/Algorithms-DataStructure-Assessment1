using System;

namespace Assessment1
{
    internal class BreadthFirst : PathFinderInterface
    {
        public bool FindPath(int[,] map, Coord start, Coord goal, ref LinkedList<Coord> path)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            // 1. Create the Open List and Closed List
            var open = new Queue<SearchNode>();
            var closed = new Queue<SearchNode>();

            // 2. Put the start position into the Open List
            open.Enqueue(new SearchNode(start));

            SearchNode current = null;

            // 3. Repeat until Open List is empty
            while (!open.IsEmpty())
            {
                // a. Take first node as Current
                current = open.Dequeue();

                // b. If Current is the goal → success
                if (current.Position.Row == goal.Row &&
                    current.Position.Col == goal.Col)
                {
                    // Build path by following predecessors backwards
                    path = SearchUtilities.buildPathList(current);
                    return true;
                }

                // c. Add CURRENT to CLOSED
                closed.Enqueue(current);

                // d. Generate four neighbours (clockwise: N, E, S, W)
                Coord[] neighbours =
                {
                    new Coord(current.Position.Row - 1, current.Position.Col),     // North
                    new Coord(current.Position.Row,     current.Position.Col + 1), // East
                    new Coord(current.Position.Row + 1, current.Position.Col),     // South
                    new Coord(current.Position.Row,     current.Position.Col - 1)  // West
                };

                // e. Check each neighbour
                foreach (Coord nextPos in neighbours)
                {
                    // i. Bounds check
                    if (nextPos.Row < 0 || nextPos.Row >= rows ||
                        nextPos.Col < 0 || nextPos.Col >= cols)
                        continue;

                    // ii. Walls cannot be crossed
                    if (map[nextPos.Row, nextPos.Col] == 0)
                        continue;

                    // iii. Skip if this coordinate is already in CLOSED
                    if (ContainsCoord(closed, nextPos))
                        continue;

                    // iv. Skip if already in OPEN
                    if (ContainsCoord(open, nextPos))
                        continue;

                    // v. Otherwise add a new node for that neighbour
                    open.Enqueue(new SearchNode(nextPos, 0, 0, current));
                }
            }

            // If we exit the loop → no path found
            path = new LinkedList<Coord>();
            return false;
        }


        // Minimal helper (required because your Queue<T> has no Contains)
        private bool ContainsCoord(Queue<SearchNode> q, Coord c)
        {
            // Temporary queue to preserve contents
            var temp = new Queue<SearchNode>();
            bool found = false;

            while (!q.IsEmpty())
            {
                var item = q.Dequeue();

                if (item.Position.Row == c.Row &&
                    item.Position.Col == c.Col)
                {
                    found = true;
                }

                temp.Enqueue(item);
            }

            // Restore original queue
            while (!temp.IsEmpty())
            {
                q.Enqueue(temp.Dequeue());
            }

            return found;
        }
    }
}
