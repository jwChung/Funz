using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.AutoFixture.Idioms;
using Jwc.AutoFixture.Xunit;
using Ploeh.AutoFixture.Idioms;
using Xunit.Extensions;

namespace Jwc.Funz
{
    public abstract class IdiomaticTestBase<T>
    {
        public static IEnumerable<object[]> Members
        {
            get
            {
                return new MemberCollection<T>(
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Select(m => new object[] { m });
            }
        }

        [Spec]
        [PropertyData("Members")]
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
        [PropertyData("Members")]
        public void SutHasCorrectConstructorsAndProperties(
            MemberInfo member,
            ConstructorInitializedMemberAssertion assertion)
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            assertion.Verify(member);
        }
    }
}