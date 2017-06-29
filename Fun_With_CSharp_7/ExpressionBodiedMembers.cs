using System;
using System.Diagnostics;
using Xunit;

namespace Fun_With_CSharp_7
{
    public class ExpressionBodiedMembers
    {
        [Fact]
        public void TestExpressionBodiedMembers()
        {
            var trump = new Person("Mr Trump.");
            Assert.Throws<ArgumentNullException>(() => trump.Name = null);

            var noName = new Person(null);
            Assert.Equal("No name", noName.Name);
        }


        // This all works as expected
        public class Person
        {
            private string _name;
            public string Name
            {
                // Expression bodied get accessor 
                get => _name;
                // Expression bodied set accessor along with throw expression 
                set => _name = value ?? throw new ArgumentNullException(nameof(value));
            }

            // Expression bodied constructor 
            public Person(string name) => Name = name ?? "No name";


            // Expression bodied finalizer 
            ~Person() => Console.WriteLine("Person has been finalized");
        }

    }

}