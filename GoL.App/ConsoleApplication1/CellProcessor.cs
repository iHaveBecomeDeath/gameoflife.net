using System.Collections.Generic;
using System.Linq;
using GoL.Entities;

namespace GoL.App
{
    public class CellProcessor
    {
        public static void Iterate(List<Cell> listOfLivingCells)
        {
            CalculateTransitions(listOfLivingCells);
            Transition(listOfLivingCells);
        }

        private static void CalculateTransitions(IEnumerable<Cell> listOfLivingCells)
        {
            foreach (var cell in listOfLivingCells)
            {
                switch (cell.Neighbours.Count)
                {
                    case 2:
                        cell.TransitionState = CellTransitionState.Remains;
                        break;
                    case 3:
                        cell.TransitionState = CellTransitionState.Remains;
                        break;
                    default: cell.TransitionState = CellTransitionState.Dies;
                        break;
                    //TODO : Brought to life!
                }
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