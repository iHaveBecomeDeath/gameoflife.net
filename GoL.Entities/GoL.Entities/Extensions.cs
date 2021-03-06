using System.Collections.Generic;
using System.Linq;

namespace GoL.Entities
{
    public static class Extensions
    {
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

        public static List<Cell> Neighbours(this Cell startingCell, List<Cell> livingCells)
        {
            return
                GetNeighbours(startingCell, livingCells);
        }

        public static List<Cell> NeighboursIncludingDead(this Cell startingCell, List<Cell> allCellsIncludingDead)
        {
            return
                GetNeighbours(startingCell, allCellsIncludingDead);                    
        }

        private static List<Cell> GetNeighbours(Cell startingCell, IEnumerable<Cell> listOfCells)
        {
            return listOfCells
                .Where(
                    cell =>
                    cell.Coordinates.X.IsWithinRangeOf(startingCell.Coordinates.X)
                    &&
                    cell.Coordinates.Y.IsWithinRangeOf(startingCell.Coordinates.Y)
                    && 
                    cell.Id != startingCell.Id
                )
                .ToList();
        }

        public static bool IsWithinRangeOf(this int originalPosition, int comparativePosition)
        {
            return
                originalPosition == comparativePosition
                || originalPosition - 1 == comparativePosition
                || originalPosition + 1 == comparativePosition;
        }
    }
}