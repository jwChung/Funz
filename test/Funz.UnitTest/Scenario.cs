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

        [VersionSpec(major: 0, minor: 1, patch: 0)]
        public void ResolveOnChildReturnsCorrectInstance(
            Container container)
        {
            // Fixture setup
            container.Register<IFoo>(c => new Foo());
            var child = container.CreateChild();

            // Exercise system
            var actual = child.Resolve<IFoo>();

            // Verify outcome
            Assert.IsType<Foo>(actual);
        }

        [VersionSpec(major: 0, minor: 1, patch: 0)]
        public void ResolveServiceReusedWithinNoneAlwayReturnsNewInstance(
            Container container)
        {
            // Fixture setup
            container.Register<IFoo>(c => new Foo()).ReusedWithinNone();
            var expected = container.Resolve<IFoo>();

            // Exercise system
            var actual = container.Resolve<IFoo>();

            // Verify outcome
            Assert.NotSame(expected, actual);
        }

        [VersionSpec(major: 0, minor: 1, patch: 0)]
        public void ResolveServiceReusedWithinContainerReturnsSharedInstance(
            Container container)
        {
            // Fixture setup
            container.Register<IFoo>(c => new Foo()).ReusedWithinContainer();
            var expected = container.Resolve<IFoo>();

            // Exercise system
            var actual = container.Resolve<IFoo>();

            // Verify outcome
            Assert.Same(expected, actual);
        }

        [VersionSpec(major: 0, minor: 1, patch: 0)]
        public void ResolveServiceReusedWithinContainerReturnsNonSharedInstanceWithChild(
            Container container)
        {
            // Fixture setup
            container.Register<IFoo>(c => new Foo()).ReusedWithinContainer();
            var child = container.CreateChild();
            var expected = child.Resolve<IFoo>();

            // Exercise system
            var actual = container.Resolve<IFoo>();

            // Verify outcome
            Assert.NotSame(expected, actual);
        }

        [VersionSpec(major: 0, minor: 1, patch: 0)]
        public void ResolveServiceReusedWithinContainerOnChildReturnsSharedInstance(
            Container container)
        {
            // Fixture setup
            container.Register<IFoo>(c => new Foo()).ReusedWithinContainer();
            var child = container.CreateChild();
            var expected = child.Resolve<IFoo>();

            // Exercise system
            var actual = child.Resolve<IFoo>();

            // Verify outcome
            Assert.Same(expected, actual);
        }

        [VersionSpec(major: 0, minor: 1, patch: 0)]
        public void ResolveServiceReusedWithinHierarchyReturnsSharedInstanceWithChildAndGrandChild(
            Container container)
        {
            // Fixture setup
            container.Register<IFoo>(c => new Foo()).ReusedWithinHierarchy();
            var child = container.CreateChild();
            var grandChild = child.CreateChild();
            var expected1 = child.Resolve<IFoo>();
            var expected2 = grandChild.Resolve<IFoo>();

            // Exercise system
            var actual = container.Resolve<IFoo>();

            // Verify outcome
            Assert.Same(expected1, actual);
            Assert.Same(expected2, actual);
        }

        [VersionSpec(major: 0, minor: 1, patch: 0)]
        public void ResolveServieReusedWithinScopeReturnsNonSharedInstanceWithDifferentScope(
            Container container,
            string scope)
        {
            // Fixture setup
            Assert.NotEqual(scope, container.Scope);
            container.Register<IFoo>(c => new Foo()).ReusedWithin(scope);
            var expected = container.Resolve<IFoo>();

            // Exercise system
            var actual = container.Resolve<IFoo>();

            // Verify outcome
            Assert.NotSame(expected, actual);
        }

        [VersionSpec(major: 0, minor: 1, patch: 0)]
        public void ResolveServieReusedWithinScopeReturnsSharedInstanceWithSameScope(
            Container container)
        {
            // Fixture setup
            container.Register<IFoo>(c => new Foo()).ReusedWithin(container.Scope);
            var expected = container.Resolve<IFoo>();

            // Exercise system
            var actual = container.Resolve<IFoo>();

            // Verify outcome
            Assert.Same(expected, actual);
        }

        [VersionSpec(major: 0, minor: 1, patch: 0)]
        public void ResolveServieReusedWithinScopeReturnsSharedInstanceWithSameScopedChild(
            Container container)
        {
            // Fixture setup
            container.Register<IFoo>(c => new Foo()).ReusedWithin(container.Scope);
            var scopedChild = container.CreateChild();
            var expected = container.Resolve<IFoo>();

            // Exercise system
            var actual = scopedChild.Resolve<IFoo>();

            // Verify outcome
            Assert.Same(expected, actual);
        }

        [VersionSpec(major: 0, minor: 1, patch: 0)]
        public void ResolveServieReusedWithinScopeReturnsNonSharedInstanceWithDifferentScopedChild(
            Container container,
            int scope)
        {
            // Fixture setup
            container.Register<IFoo>(c => new Foo()).ReusedWithin(scope);
            var child = container.CreateChild(scope);
            var expected = container.Resolve<IFoo>();

            // Exercise system
            var actual = child.Resolve<IFoo>();

            // Verify outcome
            Assert.NotSame(expected, actual);
        }
        
        [VersionSpec(major: 0, minor: 1, patch: 0)]
        public void ContainerHasReusedWithinHierarchyOptionAsDefault(
            Container container)
        {
            // Fixture setup
            container.Register<IFoo>(c => new Foo());
            var child = container.CreateChild();
            var expected = child.Resolve<IFoo>();

            // Exercise system
            var actual = container.Resolve<IFoo>();

            // Verify outcome
            Assert.Same(expected, actual);
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