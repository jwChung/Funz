using System;
using Xunit;

namespace Jwc.Funz
{
    public class Scenario
    {
        [VersionSpec(major: 0, minor: 1, patch: 0)]
        public void ResolveRegisteredServiceReturnsCorrectInstance(
            Container container)
        {
            // Fixture setup
            container.Register<IFoo>(c => new Foo());

            // Exercise system
            var actual = container.Resolve<IFoo>();

            // Verify outcome
            Assert.IsType<Foo>(actual);
        }

        [VersionSpec(major: 0, minor: 1, patch: 0)]
        public void ResolveUnregisteredServiceThrowsResoutionException(
            Container container)
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ResolutionException>(() => container.Resolve<IFoo>());
            Assert.Throws<ResolutionException>(() => container.Resolve<IBar>());
        }

        [VersionSpec(major: 0, minor: 1, patch: 0)]
        public void TryResolveUnregisteredServiceReturnsDefaultValue(
            Container container)
        {
            // Fixture setup
            // Exercise system
            var actual1 = container.TryResolve<IFoo>();
            var actual2 = container.TryResolve<int>();

            // Verify outcome
            Assert.Null(actual1);
            Assert.Equal(0, actual2);
        }

        [VersionSpec(major: 0, minor: 1, patch: 0)]
        public void LazyResolveServiceReturnsFactory(
            Container container)
        {
            // Fixture setup
            container.Register<IFoo>(c => new Foo());

            // Exercise system
            Func<IFoo> actual = container.LazyResolve<IFoo>();

            // Verify outcome
            Assert.IsType<Foo>(actual.Invoke());
        }

        [VersionSpec(major: 0, minor: 1, patch: 0)]
        public void ResolveKeyedServiceReturnsDifferentInstanceFromUnkeyed(
            Container container,
            Foo foo1,
            Foo foo2,
            string key)
        {
            // Fixture setup
            container.Register<IFoo>(key, c => foo1);
            container.Register<IFoo>(c => foo2);
            var expected = container.Resolve<IFoo>();

            // Exercise system
            var actual = container.ResolveKeyed<IFoo>(key);

            // Verify outcome
            Assert.NotEqual(expected, actual);
        }

        [VersionSpec(major: 0, minor: 1, patch: 1)]
        public void ResolveRecursiveServiceThrowsResolutionException(
            Container container)
        {
            // Fixture setup
            container.Register<IFoo>(c => new Foo { Bar = c.Resolve<IBar>() });
            container.Register<IBar>(c => new Bar { Foo = c.Resolve<IFoo>() });

            // Exercise system
            // Verify outcome
            Assert.Throws<ResolutionException>(() => container.Resolve<IFoo>());
            Assert.Throws<ResolutionException>(() => container.Resolve<IBar>());
        }

        public interface IFoo
        {
            IBar Bar { get; set; }
        }

        public interface IBar
        {
            IFoo Foo { get; set; }
        }

        public class Foo : IFoo
        {
            public IBar Bar
            {
                get;
                set;
            }
        }

        public class Bar : IBar
        {
            public IFoo Foo
            {
                get;
                set;
            }
        }
    }
}