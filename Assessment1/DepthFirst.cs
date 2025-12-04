using System;

namespace Assessment1
{
    internal class DepthFirst : PathFinderInterface
    {
        public bool FindPath(int[,] map, Coord start, Coord goal, ref LinkedList<Coord> path)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            // 1. Create the OPEN list (Stack) and CLOSED list (Stack)
            var open = new Stack<SearchNode>();
            var closed = new Stack<SearchNode>();

            // 2. Push the start node onto OPEN
            open.Push(new SearchNode(start));

            SearchNode current = null;

            // 3. Loop until OPEN empty or goal found
            while (!open.IsEmpty())
            {
                // a. Pop the top item from OPEN
                current = open.Pop();

                // b. If Current is Goal → success
                if (current.Position.Row == goal.Row &&
                    current.Position.Col == goal.Col)
                {
                    path = SearchUtilities.buildPathList(current);
                    return true;
                }

                // c. Add Current to CLOSED
                closed.Push(current);

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

                    // ii. Blocked?
                    if (map[next.Row, next.Col] == 0)
                        continue;

                    // iii. Already in CLOSED?
                    if (StackContains(closed, next))
                        continue;

                    // iv. Already in OPEN?
                    if (StackContains(open, next))
                        continue;

                    // v. Valid → push to OPEN, set predecessor
                    open.Push(new SearchNode(next, 0, 0, current));
                }
            }

            // NO path found
            path = new LinkedList<Coord>();
            return false;
        }

        // -------------------------------------------------------------------
        // Helper: check if a stack contains a Coord (same style as your BFS)
        // -------------------------------------------------------------------
        private bool StackContains(Stack<SearchNode> stack, Coord pos)
        {
            bool found = false;

            Stack<SearchNode> temp = new Stack<SearchNode>();

            // Pop all items, check each, and store in temp
            while (!stack.IsEmpty())
            {
                var item = stack.Pop();

                if (item.Position.Row == pos.Row &&
                    item.Position.Col == pos.Col)
                {
                    found = true;
                }

                temp.Push(item);
            }

            // Restore original stack
            while (!temp.IsEmpty())
            {
                stack.Push(temp.Pop());
            }

            return found;
        }
    }
}

