using System;
using Xunit;

namespace Jwc.Funz
{
    public class Scenario
    {
        [VersionSpec(0, 1, 0)]
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

        [VersionSpec(0, 1, 0)]
        public void ResolveUnregisteredServiceThrowsResolutionException(
            Container container)
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ResolutionException>(() => container.Resolve<IFoo>());
            Assert.Throws<ResolutionException>(() => container.Resolve<IBar>());
        }

        [VersionSpec(0, 1, 0)]
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

        [VersionSpec(0, 1, 0)]
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

        [VersionSpec(0, 1, 0)]
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

        [VersionSpec(0, 1, 0)]
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

        [VersionSpec(0, 1, 0)]
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

        [VersionSpec(0, 1, 0)]
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

        [VersionSpec(0, 1, 0)]
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

        [VersionSpec(0, 1, 0)]
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

        [VersionSpec(0, 1, 0)]
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

        [VersionSpec(0, 1, 0)]
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

        [VersionSpec(0, 1, 0)]
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

        [VersionSpec(0, 1, 0)]
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

        [VersionSpec(0, 1, 0)]
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

        [VersionSpec(0, 1, 0)]
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

        [VersionSpec(0, 1, 0)]
        public void DisposeReusedInstanceCorrectlyDisposes(
            Container container)
        {
            // Fixture setup
            container.Register(c => new Disposable()).ReusedWithinContainer();
            var service = container.Resolve<Disposable>();

            // Exercise system
            container.Dispose();

            // Verify outcome
            Assert.True(service.Disposed, "Disposed");
        }

        [VersionSpec(0, 1, 0)]
        public void DisposeNonReusedInstanceDoesNotDispose(
            Container container)
        {
            // Fixture setup
            container.Register(c => new Disposable()).ReusedWithinNone();
            var service = container.Resolve<Disposable>();

            // Exercise system
            container.Dispose();

            // Verify outcome
            Assert.False(service.Disposed, "Disposed");
        }

        [VersionSpec(0, 1, 0)]
        public void DisposeReusedInstanceOwnedByExternalDoesNotDispose(
            Container container)
        {
            // Fixture setup
            container.Register(c => new Disposable()).ReusedWithinContainer().OwnedByExternal();
            var service = container.Resolve<Disposable>();

            // Exercise system
            container.Dispose();

            // Verify outcome
            Assert.False(service.Disposed, "Disposed");
        }

        [VersionSpec(0, 1, 0)]
        public void DisposeAlsoDisposesChildContainer(
            Container container)
        {
            // Fixture setup
            container.Register(c => new Disposable()).ReusedWithinContainer();
            var child = container.CreateChild();
            var service = child.Resolve<Disposable>();

            // Exercise system
            container.Dispose();

            // Verify outcome
            Assert.True(service.Disposed, "Disposed");
        }

        [VersionSpec(0, 1, 1)]
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

        [VersionSpec(0, 2, 0)]
        public void ContainerVisitorCanBeUsedToBatchRegistrations(
            Container container,
            RegistrationBatch batch)
        {
            // Fixture setup
            container.Accept(batch);

            // Exercise system
            var actual1 = container.Resolve<IFoo>();
            var actual2 = container.Resolve<IBar>();

            // Verify outcome
            Assert.IsType<Foo>(actual1);
            Assert.IsType<Bar>(actual2);
        }

        [VersionSpec(0, 2, 0)]
        public void ContainerVisitorCanProvideOtherFunction(
            Container container,
            RegistrationBatch batch,
            FooBuilder builder)
        {
            // Fixture setup
            container.Accept(batch);
            
            // Exercise system
            var actual = container.Accept(builder).Result;

            // Verify outcome
            Assert.IsType<Foo>(actual);
            Assert.IsType<Bar>(actual.Bar);
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

        public class Disposable : IDisposable
        {
            public bool Disposed
            {
                get;
                private set;
            }

            public void Dispose()
            {
                Disposed = true;
            }
        }

        public class RegistrationBatch : IContainerVisitor<object>
        {
            public object Result
            {
                get;
                private set;
            }

            public IContainerVisitor<object> Visit(Container container)
            {
                container.Register<IFoo>(c => new Foo());
                container.Register<IBar>(c => new Bar());
                return this;
            }
        }

        public class FooBuilder : IContainerVisitor<IFoo>
        {
            public IFoo Result
            {
                get;
                private set;
            }

            public IContainerVisitor<IFoo> Visit(Container container)
            {
                var foo = container.Resolve<IFoo>();
                var bar = container.Resolve<IBar>();
                foo.Bar = bar;
                return new FooBuilder { Result = foo };
            }
        }
    }
}