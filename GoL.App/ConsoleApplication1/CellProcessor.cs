using System;
using System.Collections.Generic;
using System.Linq;
using GoL.Entities;

namespace GoL.App
{
    public class CellProcessor
    {

        public static void Initialize(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new Exception("width and height must be greater than 0");
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                    CellRetainer.AddCell(
                        new Cell
                        {
                            Coordinates = new CellCoordinates
                            {
                                X = x,
                                Y = y,
                            },
                            CurrentState = CellState.Dead,
                            Id = Guid.NewGuid(),
                        }
                        );
            }
        }


        public static void Iterate(List<Cell> listOfLivingCells, List<Cell> allCellsThatExist)
        {
            CalculateTransitions(listOfLivingCells, allCellsThatExist);
            Transition(allCellsThatExist);
        }

        private static void CalculateTransitions(List<Cell> listOfLivingCells, IEnumerable<Cell> allCellsThatExist)
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
                // och deras ev levande grannar
                var neighbourCount = potentiallyLivingCell.Neighbours(listOfLivingCells).Count;
                if(neighbourCount == 2 || neighbourCount == 3)
                    potentiallyLivingCell.TransitionState = CellTransitionState.Lives;
            }
        }

        private static void Transition(IEnumerable<Cell> listOfCells)
        {
            foreach (var cell in listOfCells.Where(c => c.TransitionState != CellTransitionState.Remains))
            {
                switch (cell.TransitionState)
                {
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