namespace Jwc.Funz
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Experiment;
    using Experiment.Idioms;
    using Experiment.Xunit;

    public abstract class IdiomaticTest<TSUT>
    {
        [Test]
        public IEnumerable<ITestCase> SutHasAppropriateGuards()
        {
            var members = typeof(TSUT).GetIdiomaticMembers()
                .Except(this.ExceptToVerifyGuardClause());

            return TestCases.WithArgs(members)
                .WithAuto<GuardClauseAssertion>()
                .Create((m, a) => a.Verify(m));
        }

        [Test]
        public IEnumerable<ITestCase> SutCorrectlyInitializesMembers()
        {
            var members = typeof(TSUT).GetIdiomaticMembers()
                .Except(this.ExceptToVerifyInitialization());

            return TestCases.WithArgs(members)
                .WithAuto<MemberInitializationAssertion>()
                .Create((m, a) => a.Verify(m));
        }

        protected virtual IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield break;
        }

        protected virtual IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield break;
        }
    }
}