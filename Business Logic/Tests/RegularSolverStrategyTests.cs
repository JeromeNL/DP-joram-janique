using Xunit;
using System.IO;
using System;
using Business_Model.Models;
using Business_Logic.GameLogic.Printer;
using Business_Logic.GameLogic.Solver;
using Business_Model.Abstractions;

namespace Business_Logic.Tests.GameLogic.Solver
{
    public class JigsawSolverStrategyTests
    {
        [Fact]
        public void BacktrackSolve_ShouldReturnTrueForSolvedSudoku()
        {
            // Arrange
            Sudoku JigsawSudoku = new JigsawSudoku();
            JigsawSolverStrategy solver = new JigsawSolverStrategy();

            // Act
            bool solved = solver.BacktrackSolve(JigsawSudoku);

            // Assert
            Assert.True(solved);
        }



        [Fact]
        public void FindUnassignedCell_ShouldReturnNullForSolvedSudoku()
        {
            // Arrange
            Sudoku JigsawSudoku = new JigsawSudoku();
            JigsawSolverStrategy solver = new JigsawSolverStrategy();
            solver.BacktrackSolve(JigsawSudoku); // Solve the sudoku

            // Act
            var unassignedCell = solver.FindUnassignedCell(JigsawSudoku);

            // Assert
            Assert.Null(unassignedCell);
        }


    }
}

