using System.Collections.Generic;
using Jwc.AutoFixture.Xunit;
using Xunit;

namespace Jwc.Funz
{
    public class CompositContainerVisitorTest
    {
        [Spec]
        public void SutIsContainerVisitorOfEnumerable(
            CompositContainerVisitor<string> sut)
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.IsAssignableFrom<IContainerVisitor<IEnumerable<string>>>(sut);
        }
    }
}