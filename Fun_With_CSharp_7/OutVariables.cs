using Xunit;

namespace Fun_With_CSharp_7
{
    public class OutVariables
    {
        [Theory]
        [InlineData("4", 4)]
        [InlineData("5", 5)]
        [InlineData("6", 6)]
        public void UglyOutVariables(string input, int value) // the C# 6.0 way :(
        {
            int result;
            Assert.True(int.TryParse(input, out result));

            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData("4", 4)]
        [InlineData("5", 5)]
        [InlineData("6", 6)]
        public void PrettyOutVariables(string input, int value)
        {
            Assert.True(int.TryParse(input, out int result));
            // Notice result is accessible here.
            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData("4", 4)]
        [InlineData("5", 5)]
        [InlineData("6", 6)]
        public void PrettyImplicitOutVariables(string input, int value)
        {
            Assert.True(int.TryParse(input, out var result)); // can use implicitly typed out variables.

            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData("4", 4)]
        [InlineData("not a number", 0)]
        public void OutVariableScope(string input, int value)
        {
            if (int.TryParse(input, out var result))
            {
                Assert.Equal(value, result);
            }
            else
            {
                // result is still available here - just the default value.
                Assert.Equal(0, result);
            }

            // result is still available here :) - could be set, or could be the default value.
            Assert.Equal(value, result);
        }
    }
}