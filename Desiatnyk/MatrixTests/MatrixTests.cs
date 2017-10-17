using NUnit.Framework;
using MatrixNamespace;
using CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixNamespace.Tests
{
    [TestFixture]
    public class MatrixTests
    {
        [Test]
        public void Matrix_WhenInitializingRowsLengthLowerThanZero_ShouldThrowInitializeIndexOutOfRangeException()
        {
            Matrix testMatrix = new Matrix();
            try
            {
                testMatrix = new Matrix(-1, 4);
            }
            catch (InitializeIndexOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, "Number of rows and columns must be positive");
                return;
            }
        }

        [Test]
        public void Matrix_WhenInitializingColumnsLengthLowerThanZero_ShouldThrowInitializeIndexOutOfRangeException()
        {
            Matrix testMatrix = new Matrix();
            try
            {
                testMatrix = new Matrix(4, -4);
            }
            catch (InitializeIndexOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, "Number of rows and columns must be positive");
                return;
            }
        }

        [Test]
        public void Indexer_WhenIndexLowerThanZero_ShouldThrowIndexOutOfRangeException()
        {
            Matrix testMatrix = new Matrix(4, 4);
            try
            {
                int valueFromMatrix = testMatrix[0, -1];
            }
            catch (IndexOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, "Inaccessible index of matrix");
                return;
            }
        }

        [Test]
        public void GetMatrix_TryingTakeMassiveFromMatrix_SuccessfulExecutionGetMatrixMethod()
        {
            Matrix testMatrix = new Matrix(4, 4, -100, 100);
            int[,] expectedMatrix = new int[4, 4];
            for (int i = 0; i < testMatrix.Rows; i++)
            {
                for (int j = 0; j < testMatrix.Columns; j++)
                {
                    expectedMatrix[i, j] = testMatrix[i, j];
                }
            }

            int[,] actualMatrix = testMatrix.GetMatrix();

            Assert.AreEqual(expectedMatrix, actualMatrix);
        }

        [Test]
        public void Clone_ClonningMatrix_MassivesAreEqual()
        {
            Matrix expectedMatrix = new Matrix(4, 4, -100, 100);

            Matrix actualMatrix = (Matrix)expectedMatrix.Clone();
            int[,] actualMassive = actualMatrix.GetMatrix();
            int[,] expectedMassive = expectedMatrix.GetMatrix();

            Assert.AreEqual(expectedMassive, actualMassive);
        }

        [Test]
        public void ToString_GetStringPresentationOfMatrix_StringsAreEqual()
        {
            Matrix matrix = new Matrix(4, 4, -100, 100);
            string actualString = matrix.ToString();

            string expectedString = string.Empty;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    expectedString += string.Format(matrix[i, j].ToString()).PadLeft(6);
                }
                expectedString += "\n";
            }

            Assert.AreEqual(expectedString, actualString);
        }

        [Test]
        public void Equals_MatrixesWithDifferentSizes_False()
        {
            Matrix firstMatrix = new Matrix(5, 5, -100, 100);
            Matrix secondMatrix = new Matrix(3, 3, -100, 100);

            bool result = firstMatrix.Equals(secondMatrix);

            Assert.IsFalse(result);
        }

        [Test]
        public void Equals_EqualMatrixes_True()
        {
            Matrix firstMatrix = new Matrix(5, 5, -5, 5);
            Matrix secondMatrix = new Matrix(5, 5, -5, 5);

            bool result = firstMatrix.Equals(secondMatrix);

            Assert.IsTrue(result);
        }
    }
}