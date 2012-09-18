using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoL.Entities
{
    public class Cell
    {
        public Cell() { Coordinates = new CellCoordinates(); }

        public Cell(Guid newGuid)
        {
            Id = newGuid;
            Coordinates = new CellCoordinates();
        }

        public Cell(Guid newGuid, int coordX, int coordY):this(newGuid)
        {
            Coordinates = new CellCoordinates {X = coordX, Y = coordY};
        }

        public Guid Id { get; set; }

        public CellTransitionState TransitionState { get; set; }

        public CellState CurrentState { get; set; }

        public CellCoordinates Coordinates { get; set; }
    }

    public class CellCoordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

    }

    public enum CellState
    {
        Alive, Dead, Transitioning
    }

    public enum CellTransitionState
    {
        Remains, Lives, Dies
    }
}
