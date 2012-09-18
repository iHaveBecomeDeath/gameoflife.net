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
            CellRetainer.CleanSlate();
            // Inga grannar
            for (var i = 0; i < 5; i++)
                CellRetainer.AddCell(new Cell(Guid.NewGuid(), i, i*2));
            // Två får vara grannar
            var coordinates = CellRetainer.AllCellsInExistence.First().Coordinates;
            CellRetainer.AddCell(new Cell(Guid.NewGuid(), coordinates.X, coordinates.Y - 1));
            var amountWithNoNeighbours = CellRetainer.LivingCells.Count(cell => cell.Neighbours(CellRetainer.LivingCells).Count == 0);
            var amountWithOneNeighbour = CellRetainer.LivingCells.Count(cell => cell.Neighbours(CellRetainer.LivingCells).Count == 1);

            // Act
            CellProcessor.Iterate(CellRetainer.LivingCells, CellRetainer.AllCellsInExistence);

            // Assert

            // Assert att fyra har har en granne
            // Assert att två har ingen granne
            Assert.IsTrue(amountWithNoNeighbours == 4);
            Assert.IsTrue(amountWithOneNeighbour == 2);
            // Alla ska nu vara döda
            Assert.IsTrue(CellRetainer.LivingCells.Count(c => c.CurrentState == CellState.Alive) == 0);

        }

        [TestMethod]
        public void NeighboursAreOnlyOneStepApart()
        {
            CellRetainer.CleanSlate();
            CellRetainer.AddCell(new Cell(Guid.NewGuid(), 0, 0));
            CellRetainer.AddCell(new Cell(Guid.NewGuid(), 0, 1));

            Assert.IsTrue(CellRetainer.LivingCells.First().Neighbours(CellRetainer.LivingCells).Count == 1);
            Assert.IsTrue(CellRetainer.LivingCells.Second().Neighbours(CellRetainer.LivingCells).Count == 1);
        }

        [TestMethod]
        public void ATenByTenGridGives100DeadCells()
        {
            CellRetainer.CleanSlate();
            CellProcessor.Initialize(10,10);
            Assert.AreEqual(CellRetainer.AllCellsInExistence.Count, 100);
        }

        [TestMethod]
        public void CellsWithMoreThanOneStepBetweenAreNotNeighbours()
        {
            CellRetainer.CleanSlate();
            CellRetainer.AddCell(new Cell(Guid.NewGuid(), 0, 0));
            CellRetainer.AddCell(new Cell(Guid.NewGuid(), 2, 1));

            Assert.IsTrue(CellRetainer.LivingCells.First().Neighbours(CellRetainer.LivingCells).Count == 0);
            Assert.IsTrue(CellRetainer.LivingCells.Second().Neighbours(CellRetainer.LivingCells).Count == 0);
        }

        [TestMethod]
        public void CellsWithTwoOrThreeNeighboursLiveOn()
        {
            // Arrange
            CellRetainer.CleanSlate();
            // Alla får grannar
            for (var i = 0; i < 5; i++)
                CellRetainer.AddCell(new Cell(Guid.NewGuid(), i, 0));
            // En får tre grannar
            var coordinates = CellRetainer.AllCellsInExistence.Second().Coordinates;
            CellRetainer.AddCell(new Cell(Guid.NewGuid(), coordinates.X, coordinates.Y - 1));
            var amountWithTwoNeighbours = CellRetainer.LivingCells.Count(x => x.Neighbours(CellRetainer.LivingCells).Count == 2);
            var amountWithThreeNeighbours = CellRetainer.LivingCells.Count(x => x.Neighbours(CellRetainer.LivingCells).Count == 3);

            // Act
            CellProcessor.Iterate(CellRetainer.LivingCells, CellRetainer.AllCellsInExistence);
            // Assert

            Assert.IsTrue(amountWithTwoNeighbours == 2);
            Assert.IsTrue(amountWithThreeNeighbours == 3);
            // Alla ska fortfarande leva, förutom någon ensam stackare som bara hade en granne
            Assert.IsTrue(CellRetainer.LivingCells.Count(c => c.CurrentState == CellState.Alive) == 5);
        }

        [TestMethod]
        public void CellsWithMoreThanThreeNeighboursDie()
        {
            // Arrange
            CellRetainer.CleanSlate();
            // Alla får grannar
            for (var i = 0; i < 5; i++)
                CellRetainer.AddCell(new Cell(Guid.NewGuid(), i, 0));
            // Någon får fyra grannar
            var coordinates = CellRetainer.AllCellsInExistence.Second().Coordinates;
            CellRetainer.AddCell(new Cell(Guid.NewGuid(), coordinates.X, coordinates.Y - 1));
            CellRetainer.AddCell(new Cell(Guid.NewGuid(), coordinates.X, coordinates.Y + 1));
            var cellsWithMoreThanThreeNeighbours = CellRetainer.LivingCells.Where(x => x.Neighbours(CellRetainer.LivingCells).Count > 3).ToList();

            // Act
            CellProcessor.Iterate(CellRetainer.LivingCells, CellRetainer.AllCellsInExistence);
            // Assert

            // Assert att två har många grannar
            Assert.IsTrue(cellsWithMoreThanThreeNeighbours.Count() == 2);
            // De två bör nu ha dött av
            Assert.IsFalse(CellRetainer.LivingCells.Select(x => x.Id).Contains(cellsWithMoreThanThreeNeighbours.First().Id));
            Assert.IsFalse(CellRetainer.LivingCells.Select(x => x.Id).Contains(cellsWithMoreThanThreeNeighbours.Second().Id));

        }

        [TestMethod]
        public void DeadCellsWithTwoOrThreeNeighboursComeToLife()
        {
            // Arrange
            CellRetainer.CleanSlate();
            CellProcessor.Initialize(100, 100);
            var cellIntheMiddle = CellRetainer.AllCellsInExistence.Single(cell => cell.Coordinates.X == 50 && cell.Coordinates.Y == 50);
            var cellNeighbours = cellIntheMiddle.NeighboursIncludingDead(CellRetainer.AllCellsInExistence);
            // Tre levande grannar
            CellRetainer.MakeCellLive(cellNeighbours.First().Id);
            CellRetainer.MakeCellLive(cellNeighbours.Second().Id);
            CellRetainer.MakeCellLive(cellNeighbours.Third().Id);

            // Act
            CellProcessor.Iterate(CellRetainer.LivingCells, CellRetainer.AllCellsInExistence);

            // Assert

            // Vår mittencell med tre levande grannar bör ha "kommit till liv" igen
            Assert.IsTrue(CellRetainer.LivingCells.Select(cell => cell.Id).Contains(cellIntheMiddle.Id));

        }

    }   
}
