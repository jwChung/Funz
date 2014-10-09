namespace Jwc.Funz
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Xunit;

    public class Scenario
    {
        public interface IFoo
        {
            IBar Bar { get; set; }
        }

        public interface IBar
        {
            IFoo Foo { get; set; }
        }

        public interface IBaz
        {
            IFoo Foo { get; }

            IBar Bar { get; }
        }

        [VersionTest(0, 1, 0)]
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

        [VersionTest(0, 1, 0)]
        public void ResolveUnregisteredServiceThrowsResolutionException(
            Container container)
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ResolutionException>(() => container.Resolve<IFoo>());
            Assert.Throws<ResolutionException>(() => container.Resolve<IBar>());
        }

        [VersionTest(0, 1, 0)]
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

        [VersionTest(0, 1, 0)]
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

        [VersionTest(0, 1, 0)]
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

        [VersionTest(0, 1, 0)]
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

        [VersionTest(0, 1, 0)]
        public void ResolveServiceReusedWithinNoneAlwaysReturnsNewInstance(
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

        [VersionTest(0, 1, 0)]
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

        [VersionTest(0, 1, 0)]
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

        [VersionTest(0, 1, 0)]
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

        [VersionTest(0, 1, 0)]
        public void ResolveServiceReusedWithinHierarchyReturnsSharedInstanceWithChildAndGrandchild(
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

        [VersionTest(0, 1, 0)]
        public void ResolveServiceReusedWithinScopeReturnsNonSharedInstanceWithDifferentScope(
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

        [VersionTest(0, 1, 0)]
        public void ResolveServiceReusedWithinScopeReturnsSharedInstanceWithSameScope(
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

        [VersionTest(0, 1, 0)]
        public void ResolveServiceReusedWithinScopeReturnsSharedInstanceWithSameScopedChild(
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

        [VersionTest(0, 1, 0)]
        public void ResolveServiceReusedWithinScopeReturnsNonSharedInstanceWithDifferentScopedChild(
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

        [VersionTest(0, 1, 0)]
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

        [VersionTest(0, 1, 0)]
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

        [VersionTest(0, 1, 0)]
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

        [VersionTest(0, 1, 0)]
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

        [VersionTest(0, 1, 0)]
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

        [VersionTest(0, 1, 1)]
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

        [VersionTest(0, 2, 0)]
        public void ContainerVisitorCanBatchRegistrations(
            Container container,
            BatchRegistration batch)
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

        [VersionTest(0, 2, 0)]
        public void ContainerVisitorCanResolveService(
            Container container,
            BatchRegistration batch,
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

        [VersionTest(0, 2, 0)]
        public void ContainerVisitorCanRegisterType(
            Container container)
        {
            // Fixture setup
            container.Accept(new BatchRegistration());
            container.Accept(new TypeRegistration<Baz, IBaz>()).Result.ReusedWithinContainer();

            // Exercise sysetm
            var actual = container.Resolve<IBaz>();

            // Verify outcome
            Assert.IsType<Baz>(actual);
            Assert.IsType<Foo>(actual.Foo);
            Assert.IsType<Bar>(actual.Bar);
        }

        [VersionTest(0, 2, 0)]
        public void CompositeContainerVisitorCorrectlyComposesVisitors(
            Container container,
            BatchRegistration batch,
            FooBuilder fooBuilder,
            BarBuilder barBuilder)
        {
            // Fixture setup
            var visitors = new CompositeContainerVisitor<object>(batch, fooBuilder, barBuilder);

            // Exercise system
            var actual = container.Accept(visitors);

            // Verify outcome
            var result = actual.Result.ToArray();

            Assert.Null(result[0]);
            var foo = Assert.IsType<Foo>(result[1]);
            var bar = Assert.IsType<Bar>(result[2]);

            Assert.Same(foo, bar.Foo);
            Assert.Same(bar, foo.Bar);
        }

        [VersionTest(0, 3, 0)]
        public void CanResolveUnregisteredServiceReturnsFalse(
            Container container)
        {
            // Fixture setup
            // Exercise system
            var actual = container.CanResolve<IFoo>();

            // Verify outcome
            Assert.False(actual, "CanResolve");
        }

        [VersionTest(0, 3, 0)]
        public void CanResolveRegisteredServiceReturnsTrue(
            Container container)
        {
            // Fixture setup
            container.Register<IFoo>(c => new Foo());

            // Exercise system
            var actual = container.CanResolve<IFoo>();

            // Verify outcome
            Assert.True(actual, "CanResolve");
        }

        public class Foo : IFoo
        {
            public IBar Bar { get; set; }
        }

        public class Bar : IBar
        {
            public IFoo Foo { get; set; }
        }

        public class Baz : IBaz
        {
            private readonly IFoo foo;
            private readonly IBar bar;

            public Baz()
            {
            }

            public Baz(IFoo foo, IBar bar)
            {
                this.foo = foo;
                this.bar = bar;
            }

            public IFoo Foo
            {
                get { return this.foo; }
            }

            public IBar Bar
            {
                get { return this.bar; }
            }
        }

        public class Disposable : IDisposable
        {
            public bool Disposed { get; private set; }

            public void Dispose()
            {
                this.Disposed = true;
            }
        }

        public class BatchRegistration : IContainerVisitor<object>
        {
            public object Result
            {
                get { return null; }
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
            private readonly IFoo result;

            public FooBuilder()
            {
            }

            private FooBuilder(IFoo result)
            {
                this.result = result;
            }

            public IFoo Result
            {
                get { return this.result; }
            }

            public IContainerVisitor<IFoo> Visit(Container container)
            {
                var foo = container.Resolve<IFoo>();
                var bar = container.Resolve<IBar>();
                foo.Bar = bar;
                return new FooBuilder(foo);
            }
        }

        public class BarBuilder : IContainerVisitor<IBar>
        {
            private readonly IBar result;

            public BarBuilder()
            {
            }

            private BarBuilder(IBar result)
            {
                this.result = result;
            }

            public IBar Result
            {
                get { return this.result; }
            }

            public IContainerVisitor<IBar> Visit(Container container)
            {
                var foo = container.Resolve<IFoo>();
                var bar = container.Resolve<IBar>();
                bar.Foo = foo;
                return new BarBuilder(bar);
            }
        }

        public class TypeRegistration<TFrom, TTo> : IContainerVisitor<IRegistration> where TFrom : TTo
        {
            private readonly MethodInfo resolveMethod =
                typeof(Container).GetMethods().Single(m => m.Name == "Resolve" && m.GetParameters().Length == 0);

            private readonly IRegistration registration;

            public TypeRegistration()
            {
            }

            private TypeRegistration(IRegistration registration)
            {
                this.registration = registration;
            }

            public IRegistration Result
            {
                get { return this.registration; }
            }

            public IContainerVisitor<IRegistration> Visit(Container container)
            {
                var constructor = GetGreedyConstructor();
                IRegistration registration = container.Register(c =>
                {
                    var arguments = constructor.GetParameters().Select(p => this.Resolve(c, p.ParameterType)).ToArray();
                    return (TTo)constructor.Invoke(arguments);
                });

                return new TypeRegistration<TFrom, TTo>(registration);
            }

            private static ConstructorInfo GetGreedyConstructor()
            {
                return typeof(TFrom).GetConstructors()
                    .OrderByDescending(m => m.GetParameters().Length).First();
            }

            private object Resolve(Container container, Type serviceType)
            {
                return this.resolveMethod.MakeGenericMethod(serviceType).Invoke(container, new object[0]);
            }
        }
    }
}