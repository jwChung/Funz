using System.Collections.Generic;
using Jwc.AutoFixture.Idioms;
using Jwc.AutoFixture.Xunit;
using Xunit;

namespace Jwc.Funz
{
    public class CompositeContainerVisitorTest 
        : IdiomaticTest<CompositeContainerVisitor<object>, CompositeContainerVisitorTest>
    {
        public override MemberCollection<CompositeContainerVisitor<object>> GetInitializedMembers()
        {
            return base.GetInitializedMembers().Exclude(x => x.Result);
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
            visitors[0].ToMock().Setup(x => x.Visit(container)).Returns(returnedVisitors[0]);
            visitors[1].ToMock().Setup(x => x.Visit(container)).Returns(returnedVisitors[1]);
            visitors[2].ToMock().Setup(x => x.Visit(container)).Returns(returnedVisitors[2]);
            
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
            visitors[0].ToMock().SetupGet(x => x.Result).Returns(expected[0]);
            visitors[1].ToMock().SetupGet(x => x.Result).Returns(expected[1]);
            visitors[2].ToMock().SetupGet(x => x.Result).Returns(expected[2]);

            // Exercise system
            var actual = sut.Result;

            // Verify outcome
            Assert.Equal(expected, actual);
        }
    }
}