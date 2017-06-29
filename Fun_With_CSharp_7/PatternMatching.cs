using Xunit;
using Xunit.Abstractions;

namespace Fun_With_CSharp_7
{
    public class PatternMatching
    {
        private readonly ITestOutputHelper _console;

        private static readonly PostalMail NullPostmail = null;

        private static readonly Mail[] Mails =
        {
            new PostalMail(1, "Baker Street 42"),
            new InternetMail(43, true),
            NullPostmail,
            new InternetMail(101, false),
            new HomingPigeonMail(21, "Vertica", "Falcon bird"),
            null,
            new PostalMail(13, "Sesame Street"),
            new InternetMail(404, true),
        };

        public PatternMatching(ITestOutputHelper console)
        {
            _console = console;
        }

        [Fact]
        public void UsefulSwitchStatements()
        {
            foreach (Mail mail in Mails)
            {
                _console.WriteLine("");
                switch (mail)
                {
                    case HomingPigeonMail birdMail:
                        _console.WriteLine($"the bird {birdMail.BirdName} delivered your mail");
                        break;

                    //case HomingPigeonMail birdMail when birdMail.Id > 1:
                    //    _console.WriteLine($"I would never be hit, so im a compiler error");
                    //    break;

                    default:
                        _console.WriteLine("I just grab everything, but i will always run last.");
                        break;

                    case PostalMail postMail when postMail.Id > 5:
                        _console.WriteLine($"postMail had a bigger Id than 5, {postMail.Id}");
                        break;

                    case PostalMail postmail:
                        _console.WriteLine($"I just match any postmail that isn't null - return address is {postmail.ReturnAddress}");
                        break;

                    case InternetMail internetMail when internetMail.UsedIE9 && internetMail.Id > 2:
                        _console.WriteLine($"this internet mail was made with an old browser {internetMail.Id}");
                        break;

                    case null:
                        _console.WriteLine("Special case for null");
                        break;

                    // NOTE that you cannot deconstruct the object directly in the case statement :-(
                    // Will come at a later point (hopefully with some more useful stuff like match statements etc.)  
                    // Example
                    //     case PostalMail (id, returnAddress) when id == 2:
                    //         _console.WriteLine($"I just match any postmail that isn't null - return address is {returnAddress}");
                    //         break;
                }
            }
        }

        [Fact]
        public void UsefulIsExpressions()
        {
            foreach (var mail in Mails)
            {
                if (mail is HomingPigeonMail birdMail) // creates variable birdMail if mail is a HomingPigeonMail - and not null
                {
                    _console.WriteLine($"{birdMail.BirdName} delivered a mail!");
                }
                if (mail is PostalMail postMail && postMail.ReturnAddress.Equals("Sesame Street"))
                {
                    _console.WriteLine($"this one is for kermit!");
                }
                if (mail is null)
                {
                    _console.WriteLine("The null PostMail in our mails will end here, not in the if 'mail is PostMail' above");
                }
            }
        }

        [Fact]
        public void DoesntMatchNull()
        {
            // Notice that pattern does not match null, even if its the correct type.
            PostalMail postalMail = null;

            if (postalMail is PostalMail aPostMail /* this is clearly of type PostMail, but it does not match this expression. */)
            {
                Assert.True(aPostMail != null);
            }
            else
            {
                // aPostMail is accessible here, but is not initialized

                // we end here.
                Assert.True(true);
            }

            // aPostMail is accessible here, but might not be initialized
        }
    }


    #region Models

    public abstract class Mail
    {
        public int Id { get; }
        protected Mail(int id) => Id = id;
    }

    public class PostalMail : Mail
    {
        public string ReturnAddress  { get; }

        public void Deconstruct(out int id, out string returnAddress)
        {
            id = Id;
            returnAddress = ReturnAddress;
        }

        public PostalMail(int id, string returnAddress) : base(id) => ReturnAddress = returnAddress;
    }

    public class HomingPigeonMail : PostalMail
    {
        public string BirdName { get; }

        public HomingPigeonMail(int id, string returnAddress, string birdName) : base(id, returnAddress) => BirdName = birdName;
        
    }

    public class InternetMail : Mail
    {
        public bool UsedIE9 { get; }

        public InternetMail(int id, bool usedIe9) : base(id) => UsedIE9 = usedIe9;
    }

    #endregion
}