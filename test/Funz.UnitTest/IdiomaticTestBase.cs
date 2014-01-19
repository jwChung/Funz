using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.AutoFixture.Idioms;
using Jwc.AutoFixture.Xunit;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Xunit.Extensions;

namespace Jwc.Funz
{
    public abstract class IdiomaticTestBase<TSUT>
    {
        [Spec]
        [PropertyData("MemberData")]
        public void SutHasAppropriateGuards(
            MemberInfo member,
            GuardClauseAssertion assertion)
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            assertion.Verify(member);
        }

        [Spec]
        [PropertyData("MemberData")]
        public void SutHasCorrectInitializedMembers(
            MemberInfo member,
            IFixture fixture)
        {
            // Fixture setup
            var assertion = new ConstructorInitializedMemberAssertion(
                fixture,
                EqualityComparer<object>.Default,
                new ParameterPropertyMatcher());

            // Exercise system
            // Verify outcome
            assertion.Verify(member);
        }

        public static IEnumerable<object[]> MemberData
        {
            get
            {
                return new MemberCollection<TSUT>(
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Select(m => new object[] { m });
            }
        }
    }
}