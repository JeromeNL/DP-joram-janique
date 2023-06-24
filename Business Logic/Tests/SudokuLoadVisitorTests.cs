using Business_Logic.GameLogic.Reader;
using Business_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Business_Model.Tests
{
    public class SudokuLoadVisitorTests
    {

        [Fact]
        public void ParseRegularSudokuData_ShouldReturnCorrectArray()
        {
            // Arrange
            SudokuLoadVisitor visitor = new SudokuLoadVisitor();
            string data = "530070000600195000098000060800060003400803001700020006060000280000419005000080079";
            int size = 9;
            int[,] expectedValues = new int[,]
            {
                { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
                { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
                { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
                { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
                { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
                { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
                { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
                { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
                { 0, 0, 0, 0, 8, 0, 0, 7, 9 }
            };

            // Act
            int[,] result = visitor.ParseRegularSudokuData(data, size);

            // Assert
            Assert.Equal(expectedValues, result);
        }

        // Add more tests for other methods...

        // You can also create tests for other methods in the SudokuLoadVisitor class.



    }
}