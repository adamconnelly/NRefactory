using NUnit.Framework;
using ICSharpCode.NRefactory.CSharp.CodeIssues;
using ICSharpCode.NRefactory.CSharp.Refactoring;

namespace ICSharpCode.NRefactory.CSharp.CodeIssues
{
    [TestFixture]
	public class NoDefaultConstructorIssueTests : InspectionActionTestBase
	{
		[Test]
        public void ShouldReturnIssueIfBaseConstructorNotInvoked()
        {
			var testInput =
@"class BaseClass
{
	public BaseClass(string input) {}
}

class ChildClass : BaseClass
{
}";

			Test<NoDefaultConstructorIssue>(testInput, 1);
        }

		[Test]
		public void ShouldNotReturnIssueIfBaseClassHasDefaultConstructor()
		{
			var testInput =
@"class BaseClass
{
}

class ChildClass : BaseClass
{
}";

			Test<NoDefaultConstructorIssue>(testInput, 0);
		}

		[Test]
		public void ShouldNotReturnIssueIfBaseConstructorIsInvoked()
		{
			var testInput =
@"class BaseClass
{
	public BaseClass(string input) {}
}

class ChildClass : BaseClass
{
	public ChildClass() : base(""test"") {}
}";

			Test<NoDefaultConstructorIssue>(testInput, 0);
		}

		[Test]
		public void ShouldReturnIssueIfInvalidArgumentsArePassedToBaseConstructor()
		{
			var testInput =
@"class BaseClass
{
	public BaseClass(string input) {}
}

class ChildClass : BaseClass
{
	public ChildClass() : base(123) {}
}";

			Test<NoDefaultConstructorIssue>(testInput, 1);
		}

		[Test]
		public void ShouldIgnoreInterfaces()
		{
			var testInput =
@"class TestClass : System.Collections.IList
{
}";

			Test<NoDefaultConstructorIssue>(testInput, 0);
		}
	}
}

