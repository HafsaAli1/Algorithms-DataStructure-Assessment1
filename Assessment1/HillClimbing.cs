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

            // 1) Create OpenList, ClosedList and TmpList
            var open = new Queue<SearchNode>();
            var closed = new Queue<SearchNode>();
            var tmp = new Queue<SearchNode>();

            // Visited grid to avoid revisiting states
            bool[,] visited = new bool[rows, cols];

            // 2) Push the initial state (start) on to OpenList
            open.Enqueue(new SearchNode(start));
            visited[start.Row, start.Col] = true;

            SearchNode? current = null;

            // 3) Until a goal state is found or OpenList is empty
            while (!open.IsEmpty())
            {
                // (a) Remove (pop) the first element from OpenList and call it 'current'.
                current = open.Dequeue();

                // (c) If 'current' is the goal state, return success
                if (current.Position.Row == goal.Row &&
                    current.Position.Col == goal.Col)
                {
                    path = SearchUtilities.buildPathList(current);
                    return true;
                }

                // (1)(e) Add 'current' to ClosedList (mainly for debugging / completeness)
                closed.Enqueue(current);

                // (d) Generate neighbours (N, E, S, W — clockwise)
                Coord[] neighbours =
                {
                    new Coord(current.Position.Row - 1, current.Position.Col),     // North
                    new Coord(current.Position.Row,     current.Position.Col + 1), // East
                    new Coord(current.Position.Row + 1, current.Position.Col),     // South
                    new Coord(current.Position.Row,     current.Position.Col - 1)  // West
                };

                // (d)(i–ii) For each rule that can match 'current'
                foreach (Coord nextPos in neighbours)
                {
                    // Check inside map bounds
                    if (nextPos.Row < 0 || nextPos.Row >= rows ||
                        nextPos.Col < 0 || nextPos.Col >= cols)
                        continue;

                    // Check wall (0 means blocked)
                    if (map[nextPos.Row, nextPos.Col] == 0)
                        continue;

                    // Already visited?
                    if (visited[nextPos.Row, nextPos.Col])
                        continue;

                    // (d)(i) Calculate heuristic value (Manhattan distance)
                    int h = SearchUtilities.ManhattanDistance(nextPos, goal);

                    // Create SearchNode with predecessor
                    var node = new SearchNode(nextPos, 0, h, current);

                    // Mark visited and add to TmpList
                    visited[nextPos.Row, nextPos.Col] = true;
                    tmp.Enqueue(node);
                }

                // If no neighbours were added, move on to the next in OpenList (backtracking)
                if (tmp.IsEmpty())
                    continue;

                // (e) Sort TmpList according to the heuristic values
                var list = new List<SearchNode>();
                while (!tmp.IsEmpty())
                    list.Add(tmp.Dequeue());

                list.Sort((a, b) => a.Score.CompareTo(b.Score));

                // (f) Add TmpList to the front of OpenList
                // To "prepend" them, we temporarily move OpenList aside,
                // then put sorted neighbours in first, then the old contents after.
                var old = new Queue<SearchNode>();
                while (!open.IsEmpty())
                    old.Enqueue(open.Dequeue());

                foreach (var node in list)
                    open.Enqueue(node);

                while (!old.IsEmpty())
                    open.Enqueue(old.Dequeue());
            }

            // If we get here → OpenList empty and no goal found
            path = new LinkedList<Coord>();
            return false;
        }
    }
}
