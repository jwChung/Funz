using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.AutoFixture.Idioms;
using Jwc.AutoFixture.Xunit;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Funz
{
    public class CompositeContainerVisitorTest
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
            CompositeContainerVisitor<string> sut)
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.IsAssignableFrom<IContainerVisitor<IEnumerable<string>>>(sut);
        }

        [Spec]
        public void VisitMakesAllVisitorsVisitContainer(
            [Inject] IContainerVisitor<int>[] visitors,
            [Build] CompositeContainerVisitor<int> sut,
            Container container,
            IContainerVisitor<int>[] returnedVisitors)
        {
            // Fixture setup
            Mock.Get(visitors[0]).Setup(x => x.Visit(container)).Returns(returnedVisitors[0]);
            Mock.Get(visitors[1]).Setup(x => x.Visit(container)).Returns(returnedVisitors[1]);
            Mock.Get(visitors[2]).Setup(x => x.Visit(container)).Returns(returnedVisitors[2]);
            
            // Exercise system
            var actual = sut.Visit(container);

            // Verify outcome
            var result = Assert.IsType<CompositeContainerVisitor<int>>(actual);
            Assert.Equal(returnedVisitors, result.Visitors);
        }

        [Spec]
        public void VisitReturnsNewInstance(
            CompositeContainerVisitor<int> sut,
            Container container)
        {
            // Fixture setup
            // Exercise system
            var actual = sut.Visit(container);

            // Verify outcome
            Assert.NotEqual(sut, actual);
        }

        [Spec]
        public void ResultReturnsEnumerableOfVisitorResult(
            [Inject] IContainerVisitor<string>[] visitors,
            [Build] CompositeContainerVisitor<string> sut,
            string[] expected)
        {
            // Fixture setup
            Mock.Get(visitors[0]).SetupGet(x => x.Result).Returns(expected[0]);
            Mock.Get(visitors[1]).SetupGet(x => x.Result).Returns(expected[1]);
            Mock.Get(visitors[2]).SetupGet(x => x.Result).Returns(expected[2]);

            // Exercise system
            var actual = sut.Result;

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        private class GuardMemberDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                return new MemberCollection<CompositeContainerVisitor<object>>(
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Select(m => new object[] { m });
            }
        }

        private class InitializedMemberDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                return new MemberCollection<CompositeContainerVisitor<object>>(
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Exclude(x => x.Result)
                .Select(m => new object[] { m });
            }
        }
    }
}