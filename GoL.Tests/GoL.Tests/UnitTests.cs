using System;
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
                CellRetainer.LivingCells.Add(CellRetainer.CreateCell());
            CellRetainer.MakeNeighbours(CellRetainer.LivingCells.First(), CellRetainer.LivingCells.Second());
            var amountWithNoNeighbours = CellRetainer.LivingCells.Count(x => x.Neighbours(CellRetainer.LivingCells).Count == 0);
            var amountWithOneNeighbour = CellRetainer.LivingCells.Count(x => x.Neighbours(CellRetainer.LivingCells).Count == 1);

            // Act
            CellProcessor.Iterate(CellRetainer.LivingCells, CellRetainer.AllCellsInExistence);
            // Assert

            // Assert att två har har en granne
            // Assert att en har ingen grannet
            Assert.IsTrue(amountWithNoNeighbours == 1);
            Assert.IsTrue(amountWithOneNeighbour == 2);
            // Alla ska nu vara döda
            Assert.IsTrue(CellRetainer.LivingCells.Count(c => c.CurrentState == CellState.Alive) == 0);

        }

        [TestMethod]
        public void CellsWithTwoOrThreeNeighboursLiveOn()
        {
            // Arrange
            for (var i = 0; i < 5; i++)
                CellRetainer.LivingCells.Add(CellRetainer.CreateCell());
            CellRetainer.MakeNeighbours(CellRetainer.LivingCells.First(), CellRetainer.LivingCells.Second());
            CellRetainer.MakeNeighbours(CellRetainer.LivingCells.First(), CellRetainer.LivingCells.Third());
            CellRetainer.MakeNeighbours(CellRetainer.LivingCells.Second(), CellRetainer.LivingCells.Fourth());
            CellRetainer.MakeNeighbours(CellRetainer.LivingCells.Third(), CellRetainer.LivingCells.Fourth());
            CellRetainer.MakeNeighbours(CellRetainer.LivingCells.Third(), CellRetainer.LivingCells.Fifth());
            CellRetainer.MakeNeighbours(CellRetainer.LivingCells.Fourth(), CellRetainer.LivingCells.Fifth());
            var amountWithTwoNeighbours = CellRetainer.LivingCells.Count(x => x.Neighbours.Count == 2);
            var amountWithThreeNeighbours = CellRetainer.LivingCells.Count(x => x.Neighbours.Count == 3);

            // Act
            CellProcessor.Iterate(CellRetainer.LivingCells, CellRetainer.AllCellsInExistence);
            // Assert

            // Assert att två har har en granne
            // Assert att en har ingen granne
            Assert.IsTrue(amountWithTwoNeighbours == 3);
            Assert.IsTrue(amountWithThreeNeighbours == 2);
            // Alla ska fortfarande leva
            Assert.IsTrue(CellRetainer.LivingCells.Count(c => c.CurrentState == CellState.Alive) == 5);
        }

        [TestMethod]
        public void CellsWithMoreThanThreeNeighboursDie()
        {
            // Arrange
            for (var i = 0; i < 5; i++)
                CellRetainer.LivingCells.Add(CellRetainer.CreateCell());
            CellRetainer.MakeNeighbours(CellRetainer.LivingCells.First(), CellRetainer.LivingCells.Second());
            CellRetainer.MakeNeighbours(CellRetainer.LivingCells.First(), CellRetainer.LivingCells.Third());
            CellRetainer.MakeNeighbours(CellRetainer.LivingCells.First(), CellRetainer.LivingCells.Fourth());
            CellRetainer.MakeNeighbours(CellRetainer.LivingCells.First(), CellRetainer.LivingCells.Fifth());
            CellRetainer.MakeNeighbours(CellRetainer.LivingCells.Second(), CellRetainer.LivingCells.Third());
            CellRetainer.MakeNeighbours(CellRetainer.LivingCells.Second(), CellRetainer.LivingCells.Fourth());
            CellRetainer.MakeNeighbours(CellRetainer.LivingCells.Second(), CellRetainer.LivingCells.Fifth());
            var amountWithMoreThanThreeNeighbours = CellRetainer.LivingCells.Count(x => x.Neighbours.Count > 3);

            // Act
            CellProcessor.Iterate(CellRetainer.LivingCells, CellRetainer.AllCellsInExistence);
            // Assert

            // Assert att två har har en granne
            // Assert att en har ingen granne
            Assert.IsTrue(amountWithMoreThanThreeNeighbours == 2);
            // Tre bör vara kvar, resten har dött av
            Assert.IsTrue(CellRetainer.LivingCells.Count(c => c.CurrentState == CellState.Alive) == 3);

        }

        [TestMethod]
        public void DeadCellsWithTwoOrThreeNeighboursComeToLife()
        {
            // Arrange
            CellRetainer.AllCellsInExistence.Add(new Cell(Guid.NewGuid(), 0, 0));
            CellRetainer.AllCellsInExistence.Add(new Cell(Guid.NewGuid(), 0, 1));
            CellRetainer.AllCellsInExistence.Add(new Cell(Guid.NewGuid(), 1, 0));

            // Act
            CellProcessor.Iterate(CellRetainer.LivingCells, CellRetainer.AllCellsInExistence);

            // Assert


            // Tre bör vara kvar, resten har dött av
            Assert.IsTrue(CellRetainer.LivingCells.Count(c => c.CurrentState == CellState.Alive) == 3);

        }

    }   
}
