using Xunit;

namespace Fun_With_CSharp_7
{
    public class BinaryLiterals
    {
        [Fact]
        public void NewBinaryLiterals()
        {
            int i = 15;

            int j = 0b1111;

            Assert.Equal(i, j);
        }

        [Fact]
        public void BinarySeparation()
        {
            int i = 255;

            int j = 0b1111_1111;

            Assert.Equal(i, j);
        }
    }
}