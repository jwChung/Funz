using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment.Xunit;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Funz
{
    public class CompositeContainerVisitorTest : IdiomaticTest<CompositeContainerVisitor<object>>
    {
        [Test]
        public void SutIsContainerVisitorOfEnumerable(
            CompositeContainerVisitor<string> sut)
        {
            Assert.IsAssignableFrom<IContainerVisitor<IEnumerable<string>>>(sut);
        }

        [Test]
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

        [Test]
        public void VisitReturnsNewInstance(
            CompositeContainerVisitor<int> sut,
            Container container)
        {
            var actual = sut.Visit(container);
            Assert.NotEqual(sut, actual);
        }

        [Test]
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

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return new Properties<CompositeContainerVisitor<object>>().Select(x => x.Result);
        }
    }
}