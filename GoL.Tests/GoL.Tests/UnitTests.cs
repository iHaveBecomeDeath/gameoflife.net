using System.Collections.Generic;
using System.Linq;
using GoL.App;
using GoL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoL.Tests
{
    [TestClass]
    public class UnitTests
    {
        /*
            Any live cell with fewer than two live neighbours dies, as if caused by under-population.
            Any live cell with two or three live neighbours lives on to the next generation.
            Any live cell with more than three live neighbours dies, as if by overcrowding.
            Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
        */
        [TestMethod]
        public void CellsWithOneOrNoNeighboursDie()
        {
            // Arrange
            for (var i = 0; i < 3; i++)
                CellRetainer.Cells.Add(CellRetainer.CreateCell());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.First(), CellRetainer.Cells.Second());
            var amountWithNoNeighbours = CellRetainer.Cells.Count(x => x.Neighbours.Count == 0);
            var amountWithOneNeighbour = CellRetainer.Cells.Count(x => x.Neighbours.Count == 1);

            // Act
            CellProcessor.Iterate(CellRetainer.Cells);

            // Assert

            // Assert att två har har en granne
            // Assert att en har ingen granne
            Assert.IsTrue(amountWithNoNeighbours == 1);
            Assert.IsTrue(amountWithOneNeighbour == 2);
            // Alla ska nu vara döda
            Assert.IsTrue(CellRetainer.Cells.Count(c => c.CurrentState == CellState.Alive) == 0);

        }

        [TestMethod]
        public void CellsWithTwoOrThreeNeighboursLiveOn()
        {
            // Arrange
            for (var i = 0; i < 5; i++)
                CellRetainer.Cells.Add(CellRetainer.CreateCell());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.First(), CellRetainer.Cells.Second());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.First(), CellRetainer.Cells.Third());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.Second(), CellRetainer.Cells.Fourth());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.Third(), CellRetainer.Cells.Fourth());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.Third(), CellRetainer.Cells.Fifth());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.Fourth(), CellRetainer.Cells.Fifth());
            var amountWithTwoNeighbours = CellRetainer.Cells.Count(x => x.Neighbours.Count == 2);
            var amountWithThreeNeighbours = CellRetainer.Cells.Count(x => x.Neighbours.Count == 3);

            // Act
            CellProcessor.Iterate(CellRetainer.Cells);

            // Assert

            // Assert att två har har en granne
            // Assert att en har ingen granne
            Assert.IsTrue(amountWithTwoNeighbours == 3);
            Assert.IsTrue(amountWithThreeNeighbours == 2);
            // Alla ska fortfarande leva
            Assert.IsTrue(CellRetainer.Cells.Count(c => c.CurrentState == CellState.Alive) == 5);
        }

        [TestMethod]
        public void CellsWithMoreThanThreeNeighboursDie()
        {
            // Arrange
            for (var i = 0; i < 5; i++)
                CellRetainer.Cells.Add(CellRetainer.CreateCell());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.First(), CellRetainer.Cells.Second());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.First(), CellRetainer.Cells.Third());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.First(), CellRetainer.Cells.Fourth());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.First(), CellRetainer.Cells.Fifth());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.Second(), CellRetainer.Cells.Third());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.Second(), CellRetainer.Cells.Fourth());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.Second(), CellRetainer.Cells.Fifth());
            var amountWithMoreThanThreeNeighbours = CellRetainer.Cells.Count(x => x.Neighbours.Count > 3);

            // Act
            CellProcessor.Iterate(CellRetainer.Cells);

            // Assert

            // Assert att två har har en granne
            // Assert att en har ingen granne
            Assert.IsTrue(amountWithMoreThanThreeNeighbours == 2);
            // Tre bör vara kvar, resten har dött av
            Assert.IsTrue(CellRetainer.Cells.Count(c => c.CurrentState == CellState.Alive) == 3);
       
        }
        
        [TestMethod]
        public void DeadCellsWithTwoOrThreeNeighboursComeToLife()
        {
            // Arrange
            for (var i = 0; i < 5; i++)
                CellRetainer.Cells.Add(CellRetainer.CreateCell());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.First(), CellRetainer.Cells.Second());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.First(), CellRetainer.Cells.Third());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.First(), CellRetainer.Cells.Fourth());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.First(), CellRetainer.Cells.Fifth());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.Second(), CellRetainer.Cells.Third());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.Second(), CellRetainer.Cells.Fourth());
            CellRetainer.MakeNeighbours(CellRetainer.Cells.Second(), CellRetainer.Cells.Fifth());
            var amountWithMoreThanThreeNeighbours = CellRetainer.Cells.Count(x => x.Neighbours.Count > 3);

            // Act
            CellProcessor.Iterate(CellRetainer.Cells);

            // Assert

            // Assert att två har har en granne
            // Assert att en har ingen granne
            Assert.IsTrue(amountWithMoreThanThreeNeighbours == 2);
            // Tre bör vara kvar, resten har dött av
            Assert.IsTrue(CellRetainer.Cells.Count(c => c.CurrentState == CellState.Alive) == 3);
       
        }


    }
}
