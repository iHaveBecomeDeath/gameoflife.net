using System;
using System.Collections.Generic;
using System.Linq;
using GoL.Entities;

namespace GoL.App
{
    public class CellRetainer
    {
        private static List<Cell> _allCells;

        public static List<Cell> LivingCells
        {
            get
            {
                return 
                    AllCellsInExistence
                        .Where(
                            cl => 
                                cl.CurrentState == CellState.Alive
                            )
                        .ToList();
            }
        }

        public static List<Cell> AllCellsInExistence
        {
            get { return _allCells ?? (_allCells = new List<Cell>()); }
        }
        public static void CleanSlate()
        {
            _allCells = new List<Cell>();
        }
        public static Cell AddCell(Cell cellToAdd)
        {
            var existingCell =
                AllCellsInExistence.SingleOrDefault(
                    cell =>
                    (cell.Coordinates.X == cellToAdd.Coordinates.X) 
                    && (cell.Coordinates.Y == cellToAdd.Coordinates.Y));
            if (existingCell == null)
                AllCellsInExistence.Add(cellToAdd);
            else
                AllCellsInExistence.Remove(existingCell);
                AllCellsInExistence.Add(cellToAdd);
            return cellToAdd;
        }

        public static void MakeCellLive(Guid id)
        {
            AllCellsInExistence.Single(cell => cell.Id == id).CurrentState = CellState.Alive;
        }

        public static void KillCell(Guid id)
        {
            AllCellsInExistence.Single(cell => cell.Id == id).CurrentState = CellState.Dead;
        }
    }
}