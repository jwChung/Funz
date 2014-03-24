using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Ploeh.AutoFixture.Xunit;
using Xunit;

namespace Jwc.Funz
{
    public class CompositeContainerVisitorTest
        : IdiomaticTest<CompositeContainerVisitor<object>, CompositeContainerVisitorTest>
    {
        public override IEnumerable<MemberInfo> GetInitializedMembers()
        {
            return base.GetInitializedMembers().Except(new[]
            {
                new Properties<CompositeContainerVisitor<object>>().Select(x => x.Result)
            });
        }

        [Theorem]
        public void SutIsContainerVisitorOfEnumerable(
            CompositeContainerVisitor<string> sut)
        {
            Assert.IsAssignableFrom<IContainerVisitor<IEnumerable<string>>>(sut);
        }

        [Theorem]
        public void VisitMakesAllVisitorsVisitContainer(
            CompositeContainerVisitor<int> sut,
            Container container,
            IContainerVisitor<int>[] returnedVisitors)
        {
            var visitors = sut.Visitors.ToArray();
            visitors[0].ToMock().Setup(x => x.Visit(container)).Returns(returnedVisitors[0]);
            visitors[1].ToMock().Setup(x => x.Visit(container)).Returns(returnedVisitors[1]);
            visitors[2].ToMock().Setup(x => x.Visit(container)).Returns(returnedVisitors[2]);

            var actual = sut.Visit(container);

            var result = Assert.IsType<CompositeContainerVisitor<int>>(actual);
            Assert.Equal(returnedVisitors, result.Visitors);
        }

        [Theorem]
        public void VisitReturnsNewInstance(
            CompositeContainerVisitor<int> sut,
            Container container)
        {
            var actual = sut.Visit(container);
            Assert.NotEqual(sut, actual);
        }

        [Theorem]
        public void ResultReturnsEnumerableOfVisitorResult(
            CompositeContainerVisitor<string> sut,
            string[] expected)
        {
            var visitors = sut.Visitors.ToArray();
            visitors[0].ToMock().SetupGet(x => x.Result).Returns(expected[0]);
            visitors[1].ToMock().SetupGet(x => x.Result).Returns(expected[1]);
            visitors[2].ToMock().SetupGet(x => x.Result).Returns(expected[2]);

            var actual = sut.Result;

            Assert.Equal(expected, actual);
        }
    }
}