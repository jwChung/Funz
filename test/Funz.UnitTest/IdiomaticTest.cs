using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Xunit.Extensions;

namespace Jwc.Funz
{
    public abstract class IdiomaticTest<TSUT>
    {
        public virtual IEnumerable<MemberInfo> GetGuardMembers()
        {
            return GetMembers();
        }

        public virtual IEnumerable<MemberInfo> GetInitializedMembers()
        {
            return GetMembers();
        }

        public IEnumerable<MemberInfo> GetMembers()
        {
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Static |
                                         BindingFlags.Public | BindingFlags.DeclaredOnly;
            var accessors = typeof(TSUT).GetProperties(binding).SelectMany(p => p.GetAccessors(true));
            return typeof(TSUT).GetMembers(binding).Except(accessors);
        }
    }

    public abstract class IdiomaticTest<TSUT, TTestClass>
        : IdiomaticTest<TSUT> where TTestClass : IdiomaticTest<TSUT>
    {
        [Theorem]
        [PropertyData("GuardMemberData")]
        public void SutHasAppropriateGuards(
            MemberInfo member,
            GuardClauseAssertion assertion)
        {
            assertion.Verify(member);
        }

        [Theorem]
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

            // Exercise system & Verify outcome
            assertion.Verify(member);
        }

        public static IEnumerable<object[]> GuardMemberData
        {
            get
            {
                return Activator.CreateInstance<TTestClass>()
                    .GetGuardMembers()
                    .Select(m => new object[] { m });
            }
        }

        public static IEnumerable<object[]> InitializedMemberData
        {
            get
            {
                return Activator.CreateInstance<TTestClass>()
                    .GetInitializedMembers()
                    .Select(m => new object[] { m });
            }
        }
    }
}