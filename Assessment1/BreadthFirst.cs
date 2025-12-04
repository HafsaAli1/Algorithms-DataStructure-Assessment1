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

                foreach (Coord direction in SearchUtilities.Directions)
                {
                    Coord next = new Coord(
                        current.Position.Row + direction.Row,
                        current.Position.Col + direction.Col
                    );
                

                // i. Bounds check
                if (next.Row < 0 || next.Row >= rows ||
                        next.Col < 0 || next.Col >= cols)
                        continue;

                    // ii. Walls cannot be crossed
                    if (map[next.Row, next.Col] == 0)
                        continue;

                    // iii. Skip if this coordinate is already in CLOSED
                    if (ContainsCoord(closed, next))
                        continue;

                    // iv. Skip if already in OPEN
                    if (ContainsCoord(open, next))
                        continue;

                    // v. Otherwise add a new node for that neighbour
                    open.Enqueue(new SearchNode(next, 0, 0, current));
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
