using System;
using System.Linq;
using Xunit;

namespace Fun_With_CSharp_7
{
    // !!! NOTICE !!! 
    // To use the new tuples you need the System.ValueTuple NuGet package.
    public class NewTuples
    {
        public (int min, int max) GetMinAndMax(int[] numbers)
        {
            var min = numbers.Min();
            var max = numbers.Max();

            return (min, max);

            // You can name the members. This would be ignored in this instance though, 
            // as we specify what the members should be called in our method signature.

            // return (minimum: min, maximum: max);
        }

        public double Distance((int x, int y) startPosition, (int x, int y) endPosition) // can be used as a parameters as well. 
        {
            return Math.Sqrt(Math.Pow(endPosition.x - startPosition.x, 2) + Math.Pow(endPosition.y - startPosition.y, 2)) ;
        }

        [Fact]
        public void TestMinMax()
        {
            var numbers = new[] {1, 3, 5, 7, 6, 2, 10, 5};

            var minMaxTuple = GetMinAndMax(numbers);                    // implicitly  typed
//          (int min, int max) minMaxTuple = GetMinAndMax(numbers);     // explicitly typed
            Assert.Equal(1, minMaxTuple.min);
            Assert.Equal(10, minMaxTuple.max);

            // other ways to get and 'deconstruct' tuple.
            (int min, int max) = GetMinAndMax(numbers);
            (var min1, var max2) = GetMinAndMax(numbers);

            Assert.Equal(1, min);
            Assert.Equal(10, max);

            var (nMin, nMax) = GetMinAndMax(numbers);
            Assert.Equal(1, nMin);
            Assert.Equal(10, nMax);
        }

        [Fact]
        public void TupleAsParameter()
        {
            var startPosition = (0, 0);
            // access with  startPosition.Item1
            //              startPosition.Item2

            (int x, int y) endPosition = (3, 4);
            // access with  endPosition.x
            //              endPosition.y

            var anotherEndPosition = (xAxis: 5, yAxis: 10);
            // access with  anotherEndPosition.xAxis
            //              anotherEndPosition.yAxis

            var distance = Distance(startPosition, endPosition);
            Assert.Equal(5d, distance);
        }

        [Fact]
        public void TestDeconstruction()
        {
            var person = new Person("Bo", "Ipsen", 60);

            // Deconstruct person into variables
            // this calls the matching deconstruct method on person
            (string first, string last, int age) = person;
            Assert.Equal(person.FirstName, first);
            Assert.Equal(person.LastName, last);
            Assert.Equal(person.Age, age);

            // other ways.
            var (first2, last2) = person;
            Assert.Equal(first, first2);
            Assert.Equal(last, last2);


            // The order of params matters.
            var (lastName, firstName) = person;
            Assert.Equal(person.FirstName, lastName);
            Assert.Equal(person.LastName, firstName);
        }
    }


    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Person(string firstName, string lastName, int age) 
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        // Deconstruct is a special 'method name' now. Will be used to 'deconstruct' an object
        public void Deconstruct(out string firstName, out string lastName, out int age)
        {
            firstName = FirstName;
            lastName = LastName;
            age = Age;
        }

        public void Deconstruct(out string firstName, out string lastName)
        {
            firstName = FirstName;
            lastName = LastName;
        }


        // The compiler is unable to differentiate between this one and the other Deconstruct with 3 out params. 
//        public void Deconstruct(out int one, out int two, out int three)
//        {
//            one = 1;
//            two = 2;
//            three = 3;
//        }
    }
}