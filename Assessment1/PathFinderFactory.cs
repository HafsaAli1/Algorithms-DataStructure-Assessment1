using Assessment1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment1
{
    // TODO: Make the enum match the algorithms you've implemented
    enum Algorithm { 
        BreadthFirst, 
        DepthFirst, 
        HillClimbing
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
                case Algorithm.DepthFirst:
                    pathFinder = new DepthFirst();
                    break;

                case Algorithm.HillClimbing:
                    pathFinder = new HillClimbing();
                    break;

                // TODO: Add more cases the more algorithms you implement
                default:
                    pathFinder = new BreadthFirst();
                    break;
            }
            return pathFinder;
        }
    }
}

