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

                // d. Generate neighbours (clockwise: N, E, S, W)
                Coord[] neighbours =
                {
                    new Coord(current.Position.Row - 1, current.Position.Col), // North
                    new Coord(current.Position.Row,     current.Position.Col + 1), // East
                    new Coord(current.Position.Row + 1, current.Position.Col), // South
                    new Coord(current.Position.Row,     current.Position.Col - 1)  // West
                };

                // e. Process each neighbour
                foreach (Coord nextPos in neighbours)
                {
                    // i. Bounds check
                    if (nextPos.Row < 0 || nextPos.Row >= rows ||
                        nextPos.Col < 0 || nextPos.Col >= cols)
                        continue;

                    // ii. Blocked?
                    if (map[nextPos.Row, nextPos.Col] == 0)
                        continue;

                    // iii. Already in CLOSED?
                    if (StackContains(closed, nextPos))
                        continue;

                    // iv. Already in OPEN?
                    if (StackContains(open, nextPos))
                        continue;

                    // v. Valid → push to OPEN, set predecessor
                    open.Push(new SearchNode(nextPos, 0, 0, current));
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

