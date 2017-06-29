using System;
using Xunit;

namespace Fun_With_CSharp_7
{
    public class RefReturns
    {
        
        // I want to find the value 8 in my matrix and change it to 42;

        [Fact] 
        public void OldWay()
        {
            int[,] matrix =
            {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 10, 11, 12 },
                { 13, 14, 15, 16 },
            };

            Tuple<int, int> indices = matrix.Find(val => val == 8);

            Assert.Equal(1, indices.Item1); // x position = 1
            Assert.Equal(3, indices.Item2); // y position = 3

            matrix[indices.Item1, indices.Item2] = 42;

            Assert.Equal(42, matrix[1, 3]);
        }


        [Fact]
        public void NewWayWithoutLocalRef()
        {
            int[,] matrix =
            {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 10, 11, 12 },
                { 13, 14, 15, 16 },
            };

            // lack of ref keyword on variable - means it ignores the ref return
            // value is just a normal int.
            int value = matrix.RefFind(val => val == 8); 

            Assert.Equal(8, value);

            value = 42; // value is now 42, but matrix[1, 3] is still 8

            // the value in our matrix was never changed
            Assert.Equal(8, matrix[1, 3]);
        }

        [Fact]
        public void NewWay()
        {
            int[,] matrix =
            {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 10, 11, 12 },
                { 13, 14, 15, 16 },
            };

            // value is no longer a normal int but a reference to an int
            ref int value = ref matrix.RefFind(val => val == 8);

            Assert.Equal(8, value);

            // we now change the value in the matrix directly
            value = 42;

            Assert.Equal(value, matrix[1, 3]);
        }
    }

    public static class MatrixHelper
    {
        public static Tuple<int, int> Find(this int[,] matrix, Func<int, bool> predicate)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                { 
                    if (predicate(matrix[i, j]))
                        return new Tuple<int, int>(i, j);
                }
            }

            return new Tuple<int, int>(-1, -1); // Not found
        }

        // New way
        public static ref int RefFind(this int[,] matrix, Func<int, bool> predicate)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (predicate(matrix[i, j]))
                        return ref matrix[i, j]; // Notice return ref keyword!

                }
            }

            throw new InvalidOperationException("Not found");
        }
    }
}