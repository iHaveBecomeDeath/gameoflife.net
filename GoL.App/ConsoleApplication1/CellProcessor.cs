using System.Collections.Generic;
using System.Linq;
using GoL.Entities;

namespace GoL.App
{
    public class CellProcessor
    {
        public static void Iterate(List<Cell> listOfLivingCells, List<Cell> allCellsThatExist)
        {
            CalculateTransitions(listOfLivingCells, allCellsThatExist);
            Transition(listOfLivingCells);
        }

        private static void CalculateTransitions(List<Cell> listOfLivingCells, List<Cell> allCellsThatExist)
        {
            foreach (var cell in listOfLivingCells)
            {
                switch (cell.Neighbours(listOfLivingCells).Count)
                {
                    case 2:
                        cell.TransitionState = CellTransitionState.Remains;
                        break;
                    case 3:
                        cell.TransitionState = CellTransitionState.Remains;
                        break;
                    default: cell.TransitionState = CellTransitionState.Dies;
                        break;
                }
            }
            // alla som inte är levande just nu
            foreach (var potentiallyLivingCell in allCellsThatExist.Where(x => x.CurrentState == CellState.Dead))
            {
                var neighbourCount = potentiallyLivingCell.Neighbours(listOfLivingCells).Count;
                if(neighbourCount == 2 || neighbourCount == 3)
                    potentiallyLivingCell.TransitionState = CellTransitionState.Lives;
            }
        }

        private static void Transition(IEnumerable<Cell> listOfLivingCells)
        {
            foreach (var cell in listOfLivingCells.Where(c => c.TransitionState != CellTransitionState.Remains))
            {
                switch (cell.TransitionState)
                {
                    case CellTransitionState.Dies:
                        cell.CurrentState = CellState.Dead;
                        break;
                    case CellTransitionState.Lives:
                        cell.CurrentState = CellState.Alive;
                        break;
                    default:
                        cell.CurrentState = CellState.Dead;
                        break;
                }
            }
        }
    }

}