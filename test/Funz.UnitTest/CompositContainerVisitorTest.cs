using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.AutoFixture.Idioms;
using Jwc.AutoFixture.Xunit;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Funz
{
    public class CompositContainerVisitorTest
    {
        [Spec]
        [GuardMemberData]
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
        [InitializedMemberData]
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
        
        [Spec]
        public void SutIsContainerVisitorOfEnumerable(
            CompositContainerVisitor<string> sut)
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.IsAssignableFrom<IContainerVisitor<IEnumerable<string>>>(sut);
        }

        private class GuardMemberDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                return new MemberCollection<CompositContainerVisitor<object>>(
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Select(m => new object[] { m });
            }
        }

        private class InitializedMemberDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                return new MemberCollection<CompositContainerVisitor<object>>(
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Exclude(x => x.Result)
                .Select(m => new object[] { m });
            }
        }
    }
}