using System;
using Xunit;

namespace Fun_With_CSharp_7
{
    // Prevoiusly you could only throw exceptions with a statement, 
    // now you can throw exceptions with an expression as well!
    public class ThrowExpressions
    {
        [Fact]
        public void NullCheckForServices()
        {
            Assert.Throws(typeof(ArgumentNullException), 
                () => new ThrowExpressionsService(null, null, null));
        }

        [Fact]
        public void WorksAsNormal()
        {
            var service = new ThrowExpressionsService("someService", "someString");

            Assert.Equal("someService", service._someService);
        }

        [Fact]
        public void ConditionalExpression()
        {
            var throwExpressionService = new ThrowExpressionsService("someService", "String is not hello");

            Assert.Throws(typeof(Exception), () => throwExpressionService.SafeDouble(int.MaxValue));

            Assert.Throws(typeof(Exception), () => throwExpressionService.BetterSafeDouble(int.MaxValue));

            Assert.Equal(false, throwExpressionService.SomeStringEqualsHello);
        }


        /// <summary>
        /// Mock service
        /// </summary>
        private class ThrowExpressionsService
        {
            public readonly object _someService;
            private readonly string _someString;
            private readonly string _anotherString;

            // The old ways :(
            public ThrowExpressionsService(object someService, string someString, string anotherString)
            {
                if (someService == null) throw new ArgumentNullException(nameof(someService));
                if (someString == null) throw new ArgumentNullException(nameof(someString));
                if (anotherString == null) throw new ArgumentNullException(nameof(anotherString));

                _someService = someService;
                _someString = someString;
                _anotherString = anotherString;
            }

            // Better with throw expressions :-)
            public ThrowExpressionsService(object someService, string someString)
            {
                _someService = someService ?? throw new ArgumentNullException(nameof(someService));
                _someString = someString ?? throw new ArgumentNullException(nameof(someString));
            }

            public int SafeDouble(int someNumber)
            {
                if (someNumber < (int.MaxValue / 2))
                {
                    return someNumber * 2;
                }

                throw new Exception("Number is too big to safely double");
            }

            public int BetterSafeDouble(int someNumber)
            {
                return someNumber < (int.MaxValue / 2)
                    ? someNumber * 2
                    : throw new Exception("Number is too big to safely double");
                
            }

            // works on expression bodied members as well 
            public bool SomeStringEqualsHello => _someString?.Equals("Hello") ?? throw new ArgumentNullException("");
        }
    }
}