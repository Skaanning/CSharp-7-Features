using System;
using System.Linq;
using Xunit;

namespace Fun_With_CSharp_7
{
    public class LocalFunctions
    {
        private readonly SquaredSumNumberService _squaredSumNumberService;

        public LocalFunctions()
        {
            _squaredSumNumberService = new SquaredSumNumberService();
        }

        [Fact]
        public void TestLocalFunctions()
        {
            int[] results = {
                    _squaredSumNumberService.OldWay(5),
                    _squaredSumNumberService.AnotherOldWay(5),
                    _squaredSumNumberService.NewWay(5),
                    _squaredSumNumberService.AnotherNewWay(5),
                    _squaredSumNumberService.YetAnotherNewWay(5)
                };

            foreach (var result in results)
                Assert.Equal(55, result);
        }
        
        private class SquaredSumNumberService
        {
            public int OldWay(int num)
            {
                var sum = 0;
                foreach (int i in Enumerable.Range(1, num))
                {
                    sum += OldSquare(i);
                }
                return sum;
            }

            private int OldSquare(int x)
            {
                return x * x;
            }
            
            public int AnotherOldWay(int num)
            {
                var sum = 0;
                Func<int, int> oldSquare = x => x * x;

                foreach (int i in Enumerable.Range(1, num))
                {
                    sum += oldSquare(i);
                }
                return sum;
            }
            public int NewWay(int num)
            {
                var sum = 0;
                foreach (int i in Enumerable.Range(1, num))
                {
                    sum += NewSquare(i);
                }
                return sum;

                int NewSquare(int x)
                {
                    return x * x;
                }
            }
            
            public int AnotherNewWay(int num)
            {
                var sum = 0;
                foreach (int i in Enumerable.Range(1, num))
                {
                    sum += NewSquare();

                    int NewSquare()
                    {
                        return i * i; // notice i is from the loop scope.
                    }
                }

                return sum;
            }
            
            public int YetAnotherNewWay(int num)
            {
                return Enumerable
                    .Range(1, num)
                    .Sum(NewSquare);

                int NewSquare(int x) => x * x;
            }
        }
    }
}