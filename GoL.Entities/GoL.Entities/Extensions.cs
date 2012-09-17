using System.Collections.Generic;
using System.Linq;

namespace GoL.Entities
{
    public static class Extensions
    {
        public static bool IsNeighbourWith(this Cell cellInQuestion, Cell potentialNeighbour)
        {
            return cellInQuestion.Neighbours.Contains(potentialNeighbour.Id);
        }
        
        public static Cell Second(this IEnumerable<Cell> listOfCells)
        {
            return listOfCells.Skip(1).First();
        }
        public static Cell Third(this IEnumerable<Cell> listOfCells)
        {
            return listOfCells.Skip(2).First();
        }
        public static Cell Fourth(this IEnumerable<Cell> listOfCells)
        {
            return listOfCells.Skip(3).First();
        }
        public static Cell Fifth(this IEnumerable<Cell> listOfCells)
        {
            return listOfCells.Skip(4).First();
        }

    }
}