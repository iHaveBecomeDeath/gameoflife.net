using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoL.Entities
{
    public class Cell
    {
        public Cell():this (Guid.NewGuid())
        {
            
        }

        public Cell(Guid newGuid)
        {
            Id = newGuid;
            Neighbours = new List<Guid>();
        }

        public Guid Id { get; set; }

        public List<Guid> Neighbours { get; set; }

        public CellTransitionState TransitionState { get; set; }

        public CellState CurrentState { get; set; }
    }

    public enum CellState
    {
        Alive, Dead, Transitioning
    }

    public enum CellTransitionState
    {
        Lives, Dies, Remains
    }
}
