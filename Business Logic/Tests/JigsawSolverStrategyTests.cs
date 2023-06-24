using Xunit;
using Business_Model.Models;
using Business_Logic.GameLogic.Solver;
using Business_Model.Abstractions;

namespace Business_Logic.Tests.GameLogic.Solver
{
    public class RegularSolverStrategyTests
    { 

        [Fact]
        public void BacktrackSolve_ShouldReturnTrueForSolvedSudoku()
        {
            // Arrange
            Sudoku regularSudoku = new RegularSudoku();
            RegularSolverStrategy solver = new RegularSolverStrategy();

            // Act
            bool solved = solver.BacktrackSolve(regularSudoku);

            // Assert
            Assert.True(solved);
        }


        [Fact]
        public void FindUnassignedCell_ShouldReturnNullForSolvedSudoku()
        {
            // Arrange
            Sudoku regularSudoku = new RegularSudoku();
            RegularSolverStrategy solver = new RegularSolverStrategy();
            solver.BacktrackSolve(regularSudoku); // Solve the sudoku

            // Act
            var unassignedCell = solver.FindUnassignedCell(regularSudoku);

            // Assert
            Assert.Null(unassignedCell);
        }


        [Fact]
        public void GetCellValue_ShouldReturnZeroForInvalidCoordinates()
        {
            // Arrange
            Sudoku regularSudoku = new RegularSudoku();
            RegularSolverStrategy solver = new RegularSolverStrategy();

            // Act
            int cellValue = solver.GetCellValue(10, 10, regularSudoku);

            // Assert
            Assert.Equal(0, cellValue);
        }

        [Fact]
        public void GetGroup_ShouldReturnNullForCellNotInAnyGroup()
        {
            // Arrange
            Sudoku regularSudoku = new RegularSudoku();
            RegularSolverStrategy solver = new RegularSolverStrategy();
            Leaf cell = new Leaf { currentValue = 0, position = new Position(0, 0) };

            // Act
            var group = solver.GetGroup(cell, regularSudoku);

            // Assert
            Assert.Null(group);
        }
    }
}

