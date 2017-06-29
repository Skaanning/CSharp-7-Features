using Xunit;

namespace Fun_With_CSharp_7
{
    public class DigitalSeparators
    {
        [Fact]
        public void Separate()
        {
            int billion = 1000000000;

            int separatedBillion = 1_000_000_000;

            Assert.Equal(billion, separatedBillion);
        }

        [Fact]
        public void ArbitrarySeparation()
        {
            int billion = 1000000000;

            int separatedBillion = 1_0_0_0_000___00______0;

            Assert.Equal(billion, separatedBillion);
        }
    }
}