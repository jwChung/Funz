using System;
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
    public abstract class IdiomaticTest<TSUT>
    {
        private const BindingFlags _bindingFlags =
            BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly;

        public virtual MemberCollection<TSUT> GetGuardMembers()
        {
            return new MemberCollection<TSUT>(_bindingFlags);
        }

        public virtual MemberCollection<TSUT> GetInitializedMembers()
        {
            return new MemberCollection<TSUT>(_bindingFlags);
        }
    }

    public abstract class IdiomaticTest<TSUT, TTestClass> : IdiomaticTest<TSUT>
    {
        [Spec]
        [PropertyData("GuardMemberData")]
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
        [PropertyData("InitializedMemberData")]
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

        public static IEnumerable<object[]> GuardMemberData
        {
            get
            {
                return ((IdiomaticTest<TSUT>)Activator.CreateInstance(typeof(TTestClass)))
                    .GetGuardMembers()
                    .Select(m => new object[] { m });
            }
        }

        public static IEnumerable<object[]> InitializedMemberData
        {
            get
            {
                return ((IdiomaticTest<TSUT>)Activator.CreateInstance(typeof(TTestClass)))
                    .GetInitializedMembers()
                    .Select(m => new object[] { m });
            }
        }
    }
}