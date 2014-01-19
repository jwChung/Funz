using Xunit;

namespace Jwc.Funz
{
    public class Scenario
    {
        [VersionSpec(major: 0, minor: 1, patch: 1)]
        public void ResolveRecursiveServiceThrowsResolutionException(
            Container container)
        {
            // Fixture setup
            container.Register<IFoo>(c => new Foo { Bar = c.Resolve<IBar>() });
            container.Register<IBar>(c => new Bar { Foo = c.Resolve<IFoo>() });

            // Exercise system & Verify outcome
            Assert.Throws<ResolutionException>(() => container.Resolve<IFoo>());
            Assert.Throws<ResolutionException>(() => container.Resolve<IBar>());
        }

        private interface IFoo
        {
            IBar Bar { get; set; }
        }

        private interface IBar
        {
            IFoo Foo { get; set; }
        }

        private class Foo : IFoo
        {
            public IBar Bar
            {
                get;
                set;
            }
        }

        private class Bar : IBar
        {
            public IFoo Foo
            {
                get;
                set;
            }
        }
    }
}