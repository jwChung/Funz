using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment;
using Jwc.Experiment.Idioms;
using Jwc.Experiment.Xunit;

namespace Jwc.Funz
{
    public abstract class IdiomaticTest<TSUT>
    {
        [FirstClassTest]
        public IEnumerable<ITestCase> SutHasAppropriateGuards()
        {
            return typeof(TSUT).GetIdiomaticMembers()
                .Except(this.ExceptToVerifyGuardClause())
                .Select(m => new TestCase(new Action<NullGuardClauseAssertion>(
                    a => a.Verify(m))));
        }

        [FirstClassTest]
        public IEnumerable<ITestCase> SutCorrectlyInitializesMembers()
        {
            return typeof(TSUT).GetIdiomaticMembers()
                .Except(this.ExceptToVerifyInitialization())
                .Select(m => new TestCase(new Action<MemberInitializationAssertion>(
                    a => a.Verify(m))));
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