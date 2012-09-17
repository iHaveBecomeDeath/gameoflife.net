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

        public static Cell CreateCell()
        {
            return new Cell(Guid.NewGuid());
        }

    }
}