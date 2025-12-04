using Assessment1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment1
{
    // Enumeration of available pathfinding algorithms
    enum Algorithm { 
        BreadthFirst, 
        DepthFirst, 
        HillClimbing,
        BestFirst,
        Dijkstras
    }

    internal class PathFinderFactory
    {
        // Static factory method - can be called when no object is instantiated
        // Implements Polymorphism:
        // returns reference of base class type, but actual object is of derived type
        public static PathFinderInterface NewPathFinder(Algorithm algorithm)
        {
            PathFinderInterface pathFinder; // variable type references the INTERFACE (abstract base)
            switch (algorithm)
            {
                // Depth First Search
                case Algorithm.DepthFirst:
                    pathFinder = new DepthFirst();
                    break;

                // Hill Climbing Search
                case Algorithm.HillClimbing:
                    pathFinder = new HillClimbing();
                    break;

                // Best First Search
                case Algorithm.BestFirst:
                    pathFinder = new BestFirst();
                    break;

                // Dijkstras
                case Algorithm.Dijkstras:
                    pathFinder = new Dijkstras();
                    break;

                // Default to Breadth First Search
                default:
                    pathFinder = new BreadthFirst();
                    break;
            }
            return pathFinder;
        }
    }
}

