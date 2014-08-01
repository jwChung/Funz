using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using Jwc.Experiment;
using Jwc.Experiment.AutoFixture;
using Jwc.Experiment.Idioms;
using Jwc.Experiment.Xunit;
using Ploeh.Albedo;
using Ploeh.Albedo.Refraction;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Jwc.Funz
{
    public class ContainerTest : IdiomaticTest<Container>
    {
        [Test]
        public void SutIsDisposable(
            Container sut)
        {
            Assert.IsAssignableFrom<IDisposable>(sut);
        }

        [Test]
        public void RegisterServiceWithSameKeyManyTimeDoesNotThrow(
            Container sut)
        {
            sut.Register(c => new Foo());
            Assert.DoesNotThrow(() => sut.Register(c => new Foo()));
        }

        [Test]
        public void ResolveUnregisteredServiceThrows(
            Container sut)
        {
            var expected = string.Format(
                "The service type '{0}' was not registered.",
                typeof(Foo));

            var e = Assert.Throws<ResolutionException>(() => sut.Resolve<Foo>());

            Assert.Equal(expected, e.Message);
        }

        [Test]
        public void ResolveUnregisteredServiceWithArgumentThrows(
            Container sut,
            string stringArg)
        {
            var expected = string.Format(
                "The service type '{0}' with the argument(s) '{1}' was not registered.",
                typeof(Foo),
                "System.String");

            var e = Assert.Throws<ResolutionException>(() => sut.Resolve<Foo, string>(stringArg));

            Assert.Equal(expected, e.Message);
        }

        [Test]
        public void ResolveUnregisteredKeyedServiceThrows(
            Container sut,
            object key)
        {
            sut.Register(c => new Foo());
            var expected = string.Format(
                "The service type '{0}' with the key '{1}' was not registered.",
                typeof(Foo),
                key);

            var e = Assert.Throws<ResolutionException>(() => sut.ResolveKeyed<Foo>(key));

            Assert.Equal(expected, e.Message);
        }

        [Test]
        public void ResolveUnregisteredKeyedServiceWithArgumentThrows(
            Container sut,
            object key,
            string stringArg)
        {
            var expected = string.Format(
                "The service type '{0}' with the key '{1}' and the argument(s) '{2}' was not registered.",
                typeof(Foo),
                key,
                "System.String");

            var e = Assert.Throws<ResolutionException>(() => sut.ResolveKeyed<Foo, string>(key, stringArg));

            Assert.Equal(expected, e.Message);
        }

        [Test]
        public void ResolveServiceReturnsCorrectInstance(
            Container sut)
        {
            sut.Register(c => new Foo());
            var actual = sut.Resolve<Foo>();
            Assert.NotNull(actual);
        }

        [Test]
        public void ResolveManyServicesReturnsCorrectInstances(
            Container sut,
            Foo fooValue,
            int intValue,
            string stringValue)
        {
            // Fixture setup
            sut.Register(c => fooValue);
            sut.Register(c => intValue);
            sut.Register(c => stringValue);

            // Exercise system & Verify outcome
            Assert.Equal(fooValue, sut.Resolve<Foo>());
            Assert.Equal(intValue, sut.Resolve<int>());
            Assert.Equal(stringValue, sut.Resolve<string>());
        }

        [Test]
        public void ResolveManySameTypedServicesReturnsCorrectInstances(
            Container sut,
            string stringValue)
        {
            sut.Register(c => new Foo());
            sut.Register<Foo, string>((c, s) => new Foo(s));

            var actual1 = sut.Resolve<Foo>();
            var actual2 = sut.Resolve<Foo, string>(stringValue);

            Assert.NotNull(actual1);
            Assert.Equal(stringValue, actual2.Arg);
        }

        [Test]
        public void ResolveServiceOnContainerReturnsSharedInstance(
            Container sut)
        {
            sut.Register(c => new Foo());
            var expected = sut.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [Test]
        public void ResolveServiceOnChildReturnsSharedInstanceOnContainer(
            Container sut)
        {
            sut.Register(c => new Foo());
            var child = sut.CreateChild();
            var expected = sut.Resolve<Foo>();

            var actual = child.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [Test]
        public void ResolveServiceUsingContainerReturnsCorrectInstance(
            Container sut,
            string stringValue)
        {
            sut.Register(c => stringValue);
            sut.Register(c => new Foo(c.Resolve<string>()));

            var actual = sut.Resolve<Foo>();

            Assert.Equal(stringValue, actual.Arg);
        }

        [Test]
        public void ResolveKeyedServiceReturnsCorrectInstance(
            Container sut,
            object key)
        {
            sut.Register(key, c => new Foo());
            var actual = sut.ResolveKeyed<Foo>(key);
            Assert.NotNull(actual);
        }

        [Test]
        public void ResolveServiceWithArgumentReturnsCorrectInstance(
            Container sut,
            string stringValue)
        {
            sut.Register<Foo, string>((c, s) => new Foo(s));
            var actual = sut.Resolve<Foo, string>(stringValue);
            Assert.Equal(stringValue, actual.Arg);
        }

        [Test]
        public void ResolveKeyedServiceWithArgumentReturnsCorrectInstance(
            Container sut,
            string key,
            string stringValue)
        {
            sut.Register<Foo, string>(key, (c, s) => new Foo(s));
            var actual = sut.ResolveKeyed<Foo, string>(key, stringValue);
            Assert.Equal(stringValue, actual.Arg);
        }

        [Test]
        public void ResolveServiceReusedWithinNoneOnContainerReturnsNonSharedInstance(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinNone();
            var expected = sut.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.NotEqual(expected, actual);
        }

        [Test]
        public void ResolveServiceReusedWithinNoneOnChildReturnsNonSharedInstance(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinNone();
            var child = sut.CreateChild();
            var expected = child.Resolve<Foo>();

            var actual = child.Resolve<Foo>();

            Assert.NotEqual(expected, actual);
        }

        [Test]
        public void ResolveServiceReusedWithinNoneOnContainerReturnsNonSharedInstanceOnChild(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinNone();
            var child = sut.CreateChild();
            var expected = child.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.NotEqual(expected, actual);
        }

        [Test]
        public void ResolveServiceReusedWithinContainerOnContainerReturnsSharedInstance(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinContainer();
            var expected = sut.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [Test]
        public void ResolveServiceReusedWithinContainerOnChildReturnsSharedInstance(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinContainer();
            var child = sut.CreateChild();
            var expected = child.Resolve<Foo>();

            var actual = child.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [Test]
        public void ResolveServiceReusedWithinContainerOnContainerReturnsNonSharedInstanceOnChild(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinContainer();
            var child = sut.CreateChild();
            var expected = child.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.NotEqual(expected, actual);
        }

        [Test]
        public void ResolveServiceReusedWithinHierarchyOnContainerReturnsSharedInstance(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinHierarchy();
            var expected = sut.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [Test]
        public void ResolveServiceReusedWithinContainerOnContainerReturnsSharedInstanceOnChild(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinHierarchy();
            var child = sut.CreateChild();
            var expected = child.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [Test]
        public void ResolveServiceReusedWithinContainerOnChildReturnsSharedInstanceOnContainer(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinHierarchy();
            var child = sut.CreateChild();
            var expected = sut.Resolve<Foo>();

            var actual = child.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [SlowTest(RunOn.CI)]
        public void ResolveServiceReusedWithinContainerDoesNotThrowOutOfMemoryException(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Dummy()).ReusedWithinContainer();

            // Exercise system & Verify outcome
            Assert.DoesNotThrow(() =>
            {
                const int iteration = 1000;
                for (int i = 0; i < iteration; i++)
                {
                    using (var child = sut.CreateChild())
                    {
                        var d = child.Resolve<Dummy>();
                        d.Generate(iteration);
                    }
                }
            });
        }

        [Test]
        public void ResolveServiceReusedWithinCustomOnNonScopedContainerReturnsNonSharedInstance(
            Container sut,
            object scope)
        {
            sut.Register(c => new Foo()).ReusedWithin(scope);
            var expected = sut.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.NotEqual(expected, actual);
        }

        [Test]
        public void ResolveServiceReusedWithinCustomOnScopedContainerReturnsSharedInstance(
            [Greedy] Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithin(sut.Scope);
            var expected = sut.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [Test]
        public void ResolveServiceReusedWithinCustomOnScopedChildReturnsNonSharedInstanceOnNonScopedContainer(
            object scope,
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithin(scope);
            var child = sut.CreateChild(scope);
            var expected = sut.Resolve<Foo>();

            var actual = child.Resolve<Foo>();

            Assert.NotEqual(expected, actual);
        }

        [Test]
        public void ResolveServiceReusedWithinCustomOnScopedChildReturnsSharedInstanceOnScopedGrandChild(
            string scope,
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithin(scope);
            var child = sut.CreateChild(scope);
            var grandChild = child.CreateChild();
            var expected = grandChild.Resolve<Foo>();

            var actual = child.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [Test]
        public void ResolveServiceReusedWithinCustomOnNewScopedGrandChildReturnsSharedInstanceOnScopedChild(
            string scope,
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithin(scope);
            var child = sut.CreateChild(scope);
            var grandChild = child.CreateChild(scope);
            var expected = child.Resolve<Foo>();

            var actual = grandChild.Resolve<Foo>();

            Assert.NotEqual(expected, actual);
        }

        [Test]
        public void ResolveServiceWithArgumentReusedWithinNoneReturnsNonSharedInstance(
            Container sut,
            string argument)
        {
            sut.Register<Foo, string>((c, s) => new Foo()).ReusedWithinNone();
            var expected = sut.Resolve<Foo, string>(argument);

            var actual = sut.Resolve<Foo, string>(argument);

            Assert.NotEqual(expected, actual);
        }

        [Test]
        public void ResolveKeyedServiceReusedWithinNoneReturnsNonSharedInstance(
            Container sut,
            object key)
        {
            sut.Register(key, c => new Foo()).ReusedWithinNone();
            var expected = sut.ResolveKeyed<Foo>(key);

            var actual = sut.ResolveKeyed<Foo>(key);

            Assert.NotEqual(expected, actual);
        }

        [Test]
        public void ResolveKeyedServiceWithArgumentReusedWithinNoneReturnsNonSharedInstance(
            Container sut,
            object key,
            string argument)
        {
            sut.Register<Foo, string>(key, (c, s) => new Foo(s)).ReusedWithinNone();
            var expected = sut.ResolveKeyed<Foo, string>(key, argument);

            var actual = sut.ResolveKeyed<Foo, string>(key, argument);

            Assert.NotEqual(expected, actual);
        }

        [Test]
        public void ResolveServiceWithArgumentReusedWithinContainerReturnsSharedInstance(
            Container sut,
            string argument)
        {
            sut.Register<Foo, string>((c, s) => new Foo()).OwnedByContainer();
            var expected = sut.Resolve<Foo, string>(argument);

            var actual = sut.Resolve<Foo, string>(argument);

            Assert.Equal(expected, actual);
        }

        [Test]
        public void ResolveKeyedServiceReusedWithinContainerReturnsSharedInstance(
            Container sut,
            object key)
        {
            sut.Register(key, c => new Foo()).OwnedByContainer();
            var expected = sut.ResolveKeyed<Foo>(key);

            var actual = sut.ResolveKeyed<Foo>(key);

            Assert.Equal(expected, actual);
        }

        [Test]
        public void ResolveKeyedServiceWithArgumentReusedWithinContainerReturnsSharedInstance(
            Container sut,
            object key,
            string stringValue)
        {
            sut.Register<Foo, string>(key, (c, s) => new Foo(s)).OwnedByContainer();
            var expected = sut.ResolveKeyed<Foo, string>(key, stringValue);

            var actual = sut.ResolveKeyed<Foo, string>(key, stringValue);

            Assert.Equal(expected, actual);
        }

        [Test]
        public void ResolveContainerServiceReturnsSutItself(Container sut)
        {
            var actual = sut.Resolve<Container>();
            Assert.Equal(sut, actual);
        }

        [Test]
        public void ResolveContainerServiceOnChildReturnsChildItself(Container sut)
        {
            var child = sut.CreateChild();
            var actual = child.Resolve<Container>();
            Assert.Equal(child, actual);
        }

        [Test]
        public void TryResolveUnregisteredServiceReturnsDefaultValue(
            Container sut)
        {
            var actual = sut.TryResolve<Foo>();
            Assert.Null(actual);
        }

        [Test]
        public void TryResolveRegisteredServiceReturnsCorrectInstance(
            Container sut)
        {
            sut.Register(c => new Foo());
            var actual = sut.TryResolve<Foo>();
            Assert.NotNull(actual);
        }

        [Test]
        public void TryResolveUnregisteredServiceWithArgumentReturnsDefaultValue(
            Container sut,
            string stringValue)
        {
            var actual = sut.TryResolve<Foo, string>(stringValue);
            Assert.Null(actual);
        }

        [Test]
        public void TryResolveRegisteredServiceWithArgumentReturnsCorrectInstance(
            Container sut,
            string stringValue)
        {
            sut.Register<Foo, string>((c, s) => new Foo(s));
            var actual = sut.TryResolve<Foo, string>(stringValue);
            Assert.Equal(stringValue, actual.Arg);
        }

        [Test]
        public void TryResolveUnregisteredKeyedServiceReturnsDefaultValue(
            Container sut,
            object key)
        {
            var actual = sut.TryResolveKeyed<Foo>(key);
            Assert.Null(actual);
        }

        [Test]
        public void TryResolveRegisteredKeyedServiceReturnsCorrectInstance(
            Container sut,
            object key)
        {
            sut.Register(key, c => new Foo());
            var actual = sut.TryResolveKeyed<Foo>(key);
            Assert.NotNull(actual);
        }

        [Test]
        public void TryResolveUnregisteredKeyedServiceWithArgumentReturnsDefaultValue(
            Container sut,
            object key,
            string stringValue)
        {
            var actual = sut.TryResolveKeyed<Foo, string>(key, stringValue);
            Assert.Null(actual);
        }

        [Test]
        public void TryResolveRegisteredKeyedServiceWithArgumentReturnsCorrectInstance(
            Container sut,
            object key,
            string stringValue)
        {
            sut.Register<Foo, string>(key, (c, s) => new Foo(s));
            var actual = sut.TryResolveKeyed<Foo, string>(key, stringValue);
            Assert.Equal(stringValue, actual.Arg);
        }

        [Test]
        public void LazyResolveUnregisteredServiceThrows(
            Container sut)
        {
            var expected = string.Format(
                "The service type '{0}' was not registered.",
                typeof(Foo));

            var e = Assert.Throws<ResolutionException>(() => sut.LazyResolve<Foo>().Invoke());

            Assert.Equal(expected, e.Message);
        }

        [Test]
        public void LazyResolveRegisteredServiceReturnsCorrectFactory(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinNone();
            var expected = sut.LazyResolve<Foo>().Invoke();

            var actual = sut.LazyResolve<Foo>().Invoke();

            Assert.NotNull(actual);
            Assert.NotSame(expected, actual);
        }

        [Test]
        public void LazyResolveUnregisteredServiceWithArgumentThrows(
            Container sut,
            string argument)
        {
            var expected = string.Format(
                "The service type '{0}' with the argument(s) '{1}' was not registered.",
                typeof(Foo),
                "System.String");

            var e = Assert.Throws<ResolutionException>(() => sut.LazyResolve<Foo, string>().Invoke(argument));

            Assert.Equal(expected, e.Message);
        }

        [Test]
        public void LazyResolveRegisteredServiceWithArgumentReturnsCorrectFactory(
            Container sut,
            string argument)
        {
            sut.Register<Foo, string>((c, s) => new Foo(s)).ReusedWithinNone();
            var expected = sut.LazyResolve<Foo, string>().Invoke(argument);

            var actual = sut.LazyResolve<Foo, string>().Invoke(argument);

            Assert.NotNull(actual);
            Assert.NotSame(expected, actual);
            Assert.Equal(argument, actual.Arg);
        }

        [Test]
        public void LazyResolveUnregisteredKeyedServiceThrows(
            Container sut,
            object key)
        {
            var expected = string.Format(
                "The service type '{0}' with the key '{1}' was not registered.",
                typeof(Foo),
                key);

            var e = Assert.Throws<ResolutionException>(() => sut.LazyResolveKeyed<Foo>(key).Invoke());

            Assert.Equal(expected, e.Message);
        }

        [Test]
        public void LazyResolveRegisteredKeyedServiceReturnsCorrectFactory(
            Container sut,
            object key)
        {
            sut.Register(key, c => new Foo()).ReusedWithinNone();
            var expected = sut.LazyResolveKeyed<Foo>(key).Invoke();

            var actual = sut.LazyResolveKeyed<Foo>(key).Invoke();

            Assert.NotNull(actual);
            Assert.NotSame(expected, actual);
        }

        [Test]
        public void LazyResolveUnregisteredKeyedServiceWithArgumentThrows(
            Container sut,
            object key,
            string argument)
        {
            var expected = string.Format(
                "The service type '{0}' with the key '{1}' and the argument(s) '{2}' was not registered.",
                typeof(Foo),
                key,
                "System.String");

            var e = Assert.Throws<ResolutionException>(() => sut.LazyResolveKeyed<Foo, string>(key).Invoke(argument));

            Assert.Equal(expected, e.Message);
        }

        [Test]
        public void LazyResolveRegisteredKeyedServiceWithArgumentReturnsCorrectFactory(
            Container sut,
            object key,
            string argument)
        {
            sut.Register<Foo, string>(key, (c, s) => new Foo(s)).ReusedWithinNone();
            var expected = sut.LazyResolveKeyed<Foo, string>(key).Invoke(argument);

            var actual = sut.LazyResolveKeyed<Foo, string>(key).Invoke(argument);

            Assert.NotNull(actual);
            Assert.NotSame(expected, actual);
            Assert.Equal(argument, actual.Arg);
        }

        [Test]
        public void CreateChildReturnsCorrectContainer(
            Container sut)
        {
            sut.Register(c => new Foo());
            var actual = sut.CreateChild();
            Assert.NotNull(actual.Resolve<Foo>());
        }

        [Test]
        public void CreateChildWithScopeReturnsCorrectContainer(
            object scope,
            Container sut)
        {
            sut.Register(c => new Foo());

            var actual = sut.CreateChild(scope);

            Assert.NotNull(actual.Resolve<Foo>());
            Assert.Equal(scope, actual.Scope);
        }

        [Test]
        public void DisposeDisposesServices(
            Container sut)
        {
            sut.Register(c => new Disposable()).ReusedWithinContainer();
            var disposable1 = sut.Resolve<Disposable>();
            var disposable2 = sut.Resolve<Disposable>();

            sut.Dispose();

            Assert.Equal(1, disposable1.Count);
            Assert.Equal(1, disposable2.Count);
        }

        [Test]
        public void DisposeManyTimeDisposesOnlyOnce(
            Container sut)
        {
            sut.Register(c => new Disposable());
            var disposable = sut.Resolve<Disposable>();

            sut.Dispose();
            sut.Dispose();

            Assert.Equal(1, disposable.Count);
        }

        [Test]
        public void DisposeDisposesServicesOnChild(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Disposable()).ReusedWithinContainer();
            var child = sut.CreateChild();
            var grandChild = child.CreateChild();

            var disposable1 = sut.Resolve<Disposable>();
            var disposable2 = child.Resolve<Disposable>();
            var disposable3 = grandChild.Resolve<Disposable>();

            // Exercise system
            sut.Dispose();

            // Verify outcome
            Assert.Equal(1, disposable1.Count);
            Assert.Equal(1, disposable2.Count);
            Assert.Equal(1, disposable3.Count);
        }

        [Test]
        public void DisposeDoesNotDisposeServicesOwnedByExternal(
            Container sut)
        {
            sut.Register(c => new Disposable()).ReusedWithinContainer().OwnedByExternal();
            var disposable1 = sut.Resolve<Disposable>();
            var disposable2 = sut.Resolve<Disposable>();

            sut.Dispose();

            Assert.Equal(0, disposable1.Count);
            Assert.Equal(0, disposable2.Count);
        }

        [Test]
        public void DisposeDisposesServicesOwnedByContainer(
            Container sut)
        {
            sut.Register(c => new Disposable()).ReusedWithinContainer().OwnedByContainer();
            var disposable1 = sut.Resolve<Disposable>();
            var disposable2 = sut.Resolve<Disposable>();

            sut.Dispose();

            Assert.Equal(1, disposable1.Count);
            Assert.Equal(1, disposable2.Count);
        }

        [Test]
        public void DisposeDoesNotDisposeServicesOwnedByExternalOnChild(
            Container sut)
        {
            sut.Register(c => new Disposable()).ReusedWithinContainer().OwnedByExternal();
            var child = sut.CreateChild();
            var disposable = child.Resolve<Disposable>();

            sut.Dispose();

            Assert.Equal(0, disposable.Count);
        }

        [Test]
        public void DisposeDoesNotDisposeServicesOwnedByExternalOnScopedChild(
            object scope,
            Container sut)
        {
            sut.Register(c => new Disposable()).ReusedWithin(scope).OwnedByExternal();
            var child = sut.CreateChild(scope);
            var disposable = child.Resolve<Disposable>();

            sut.Dispose();

            Assert.Equal(0, disposable.Count);
        }

        [FirstClassTest]
        public IEnumerable<ITestCase> CallAllPublicMethodAfterDisposedThrowsDisposedException()
        {
            const BindingFlags BindingFlags =
                BindingFlags.Public
                | BindingFlags.Static
                | BindingFlags.Instance
                | BindingFlags.DeclaredOnly;

            var methods = typeof(Container)
                .GetMethods(BindingFlags)
                .Where(m => !m.Name.StartsWith("LazyResolve"))
                .Except(new[] { new Methods<Container>().Select(x => x.Dispose()) })
                .Select(m => !m.ContainsGenericParameters
                    ? m : m.MakeGenericMethod(m.GetGenericArguments().Select(a => typeof(object)).ToArray()));

            return methods.Select(
                m => TestCase.New<ObjectDisposalAssertion>(
                    a => a.Verify(m),
                    m.GetDisplayName()));
        }

        [Test]
        public void DisposeWithFalseDisposingDoesNotDispose(
            DerivedContainer sut)
        {
            sut.Register(c => new Disposable()).ReusedWithinContainer();
            var disposable = sut.Resolve<Disposable>();
            sut.Disposing = false;

            sut.Dispose();

            Assert.Equal(0, disposable.Count);
        }

        [Test]
        public void DisposeWithDisposingTwiceDoesNotDispose(
            DerivedContainer sut)
        {
            sut.Register(c => new Disposable()).ReusedWithinContainer();
            var disposable = sut.Resolve<Disposable>();
            sut.Disposing = false;
            sut.Dispose();
            sut.Disposing = true;

            sut.Dispose();

            Assert.Equal(0, disposable.Count);
        }

        [Test]
        public void LazyResolveWithNullKeyThrows(
            DerivedContainer sut)
        {
            var e = Assert.Throws<ArgumentNullException>(() => sut.LazyResolveKeyed<Foo>(null).Invoke());
            Assert.Equal("key", e.ParamName);
        }

        [Test]
        public void LazyResolveWithNullKeyAndArgumentThrows(
            DerivedContainer sut,
            string argument)
        {
            var e = Assert.Throws<ArgumentNullException>(
                () => sut.LazyResolveKeyed<Foo, string>(null).Invoke(argument));
            Assert.Equal("key", e.ParamName);
        }

        [Test]
        public void ResolveCreatedByTemplateReturnsCorrectInstance(
            DerivedContainer sut,
            object arg1,
            int arg2,
            string arg3)
        {
            sut.Register<Bar, object, int, string>((c, o, i, s) => new Bar(o, i, s));

            var actual = sut.Resolve<Bar, object, int, string>(arg1, arg2, arg3);

            Assert.Equal(arg1, actual.Arg1);
            Assert.Equal(arg2, actual.Arg2);
            Assert.Equal(arg3, actual.Arg3);
        }

        [Test]
        public void TryResolveCreatedByTemplateReturnsCorrectInstance(
            DerivedContainer sut,
            object arg1,
            int arg2,
            string arg3)
        {
            sut.Register<Bar, object, int, string>((c, o, i, s) => new Bar(o, i, s));

            var actual = sut.TryResolve<Bar, object, int, string>(arg1, arg2, arg3);

            Assert.Equal(arg1, actual.Arg1);
            Assert.Equal(arg2, actual.Arg2);
            Assert.Equal(arg3, actual.Arg3);
        }

        [Test]
        public void TryResolveCreatedByTemplateIfUnregisteredReturnsNull(
            DerivedContainer sut,
            object arg1,
            int arg2,
            string arg3)
        {
            var actual = sut.TryResolve<Bar, object, int, string>(arg1, arg2, arg3);
            Assert.Null(actual);
        }

        [Test]
        public void LazyResolveCreatedByTemplateReturnsCorrectFactory(
            DerivedContainer sut,
            object arg1,
            int arg2,
            string arg3)
        {
            sut.Register<Bar, object, int, string>((c, o, i, s) => new Bar(o, i, s));

            var actual = sut.LazyResolve<Bar, object, int, string>().Invoke(arg1, arg2, arg3);

            Assert.Equal(arg1, actual.Arg1);
            Assert.Equal(arg2, actual.Arg2);
            Assert.Equal(arg3, actual.Arg3);
        }

        [Test]
        public void ResolveKeyedCreatedByTemplateReturnsCorrectInstance(
            DerivedContainer sut,
            object key,
            object arg1,
            int arg2,
            string arg3)
        {
            sut.Register<Bar, object, int, string>(key, (c, o, i, s) => new Bar(o, i, s));

            var actual = sut.ResolveKeyed<Bar, object, int, string>(key, arg1, arg2, arg3);

            Assert.Equal(arg1, actual.Arg1);
            Assert.Equal(arg2, actual.Arg2);
            Assert.Equal(arg3, actual.Arg3);
        }

        [Test]
        public void TryResolveKeyedCreatedByTemplateReturnsCorrectInstance(
            DerivedContainer sut,
            object key,
            object arg1,
            int arg2,
            string arg3)
        {
            sut.Register<Bar, object, int, string>(key, (c, o, i, s) => new Bar(o, i, s));

            var actual = sut.TryResolveKeyed<Bar, object, int, string>(key, arg1, arg2, arg3);

            Assert.Equal(arg1, actual.Arg1);
            Assert.Equal(arg2, actual.Arg2);
            Assert.Equal(arg3, actual.Arg3);
        }

        [Test]
        public void TryResolveKeyedCreatedByTemplateIfUnregisteredReturnsNull(
            DerivedContainer sut,
            object key,
            object arg1,
            int arg2,
            string arg3)
        {
            var actual = sut.TryResolveKeyed<Bar, object, int, string>(key, arg1, arg2, arg3);
            Assert.Null(actual);
        }

        [Test]
        public void LazyResolveKeyedCreatedByTemplateReturnsCorrectFactory(
            DerivedContainer sut,
            object key,
            object arg1,
            int arg2,
            string arg3)
        {
            sut.Register<Bar, object, int, string>(key, (c, o, i, s) => new Bar(o, i, s));

            var actual = sut.LazyResolveKeyed<Bar, object, int, string>(key).Invoke(arg1, arg2, arg3);

            Assert.Equal(arg1, actual.Arg1);
            Assert.Equal(arg2, actual.Arg2);
            Assert.Equal(arg3, actual.Arg3);
        }

        [Test]
        public void ResolveRecursiveServiceThrows(
            Container sut)
        {
            // Fixture setup
            sut.Register(c =>
            {
                c.Resolve<Foo>();
                return new Foo();
            });

            // Exercise system & Verify outcome
            var e = Assert.Throws<ResolutionException>(() => sut.Resolve<Foo>());
            Assert.Equal(
                "The service type 'Jwc.Funz.ContainerTest+Foo' was registered recursively.",
                e.Message);
        }

        [Test]
        public void ResolveKeyedRecursiveServiceThrows(
            Container sut,
            object key)
        {
            // Fixture setup
            sut.Register(
                key,
                c =>
                {
                    c.ResolveKeyed<Foo>(key);
                    return new Foo();
                });

            // Exercise system & Verify outcome
            var e = Assert.Throws<ResolutionException>(() => sut.ResolveKeyed<Foo>(key));
            Assert.Equal(
                "The service type 'Jwc.Funz.ContainerTest+Foo' with the key 'System.Object' " +
                "was registered recursively.",
                e.Message);
        }

        [Test]
        public void ResolveRecursiveServiceWithArgumentThrows(
            Container sut,
            string argument)
        {
            // Fixture setup
            sut.Register<Foo, string>((c, s) =>
            {
                c.Resolve<Foo, string>(argument);
                return new Foo(s);
            });

            // Exercise system & Verify outcome
            var e = Assert.Throws<ResolutionException>(() => sut.Resolve<Foo, string>(argument));
            Assert.Equal(
                "The service type 'Jwc.Funz.ContainerTest+Foo' with the argument(s) " +
                "'System.String' was registered recursively.",
                e.Message);
        }

        [Test]
        public void ResolveKeyedRecursiveServiceWithArgumentThrows(
            Container sut,
            object key,
            string argument)
        {
            // Fixture setup
            sut.Register<Foo, string>(
                key,
                (c, s) =>
                {
                    c.ResolveKeyed<Foo, string>(key, argument);
                    return new Foo(s);
                });

            // Exercise system & Verify outcome
            var e = Assert.Throws<ResolutionException>(() => sut.ResolveKeyed<Foo, string>(key, argument));
            Assert.Equal(
                "The service type 'Jwc.Funz.ContainerTest+Foo' with the key 'System.Object' " +
                "and the argument(s) 'System.String' was registered recursively.",
                e.Message);
        }

        [Test]
        public void ResolveRecursiveManyStepsThrows(
            Container sut)
        {
            // Fixture setup
            sut.Register(c =>
            {
                c.Resolve<object>();
                return new Foo();
            });

            sut.Register(c =>
            {
                c.Resolve<Bar>();
                return new object();
            });

            sut.Register(c =>
            {
                c.Resolve<Foo>();
                return new Bar(null, 0, string.Empty);
            });

            // Exercise system & Verify outcome
            var e = Assert.Throws<ResolutionException>(() => sut.Resolve<Foo>());
            Assert.Equal(
                "The service type 'Jwc.Funz.ContainerTest+Foo' was registered recursively.",
                e.Message);
        }

        [Test]
        public void ResolveRecursiveServiceCreatedByTemplateThrows(
            Container sut,
            object arg1,
            int arg2,
            string arg3)
        {
            // Fixture setup
            sut.Register<Bar, object, int, string>((c, o, i, s) =>
            {
                c.Resolve<Bar, object, int, string>(o, i, s);
                return new Bar(o, i, s);
            });

            // Exercise system & Verify outcome
            var e = Assert.Throws<ResolutionException>(() => sut.Resolve<Bar, object, int, string>(arg1, arg2, arg3));
            Assert.Equal(
                "The service type 'Jwc.Funz.ContainerTest+Bar' with the argument(s) " +
                "'System.Object, System.Int32, System.String' was registered recursively.",
                e.Message);
        }

        [Test]
        public void ResolveKeyedRecursiveServiceCreatedByTemplateThrows(
            Container sut,
            object key,
            object arg1,
            int arg2,
            string arg3)
        {
            // Fixture setup
            sut.Register<Bar, object, int, string>(
                key,
                (c, o, i, s) =>
                {
                    c.ResolveKeyed<Bar, object, int, string>(key, o, i, s);
                    return new Bar(o, i, s);
                });

            // Exercise system & Verify outcome
            var e = Assert.Throws<ResolutionException>(
                () => sut.ResolveKeyed<Bar, object, int, string>(key, arg1, arg2, arg3));
            Assert.Equal(
                "The service type 'Jwc.Funz.ContainerTest+Bar' with the key 'System.Object' and the argument(s) " +
                "'System.Object, System.Int32, System.String' was registered recursively.",
                e.Message);
        }

        [Test]
        public void ResolveServiceAfterRecursionExceptionReturnsCorrectInstance(
            Container sut)
        {
            // Fixture setup
            sut.Register(c =>
            {
                c.Resolve<object>();
                return new Foo();
            });

            sut.Register(c =>
            {
                c.Resolve<Foo>();
                return new object();
            });

            Assert.Throws<ResolutionException>(() => sut.Resolve<Foo>());
            sut.Register(c => new Foo());
            sut.Register(c => new object());

            // Exercise system
            var actual1 = sut.Resolve<Foo>();
            var actual2 = sut.Resolve<object>();

            // Verify outcome
            Assert.NotNull(actual1);
            Assert.NotNull(actual2);
        }

        [Test]
        public void ResolveShouldBeThreadSafe(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo());
            var exceptions = new ConcurrentBag<Exception>();

            UnhandledExceptionEventHandler handler =
                (s, e) => exceptions.Add((Exception)e.ExceptionObject);
            AppDomain.CurrentDomain.UnhandledException += handler;

            var threads = new Thread[100];
            for (int i = 0; i < threads.Length; i++)
                threads[i] = new Thread(() => sut.Resolve<Foo>());

            try
            {
                // Exercise system
                foreach (Thread thread in threads)
                    thread.Start();

                foreach (Thread thread in threads)
                    thread.Join();

                // Verify outcome
                Assert.Empty(exceptions);
            }
            finally
            {
                // Teardown
                AppDomain.CurrentDomain.UnhandledException -= handler;
            }
        }

        [Test]
        public void CreateChildShouldBeThreadSafe(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new StaticDisposable()).ReusedWithinContainer();
            var threads = new Thread[100];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() =>
                {
                    for (int j = 0; j < 100; j++)
                    {
                        var child = sut.CreateChild();
                        child.Resolve<StaticDisposable>();
                    }
                });
            }

            try
            {
                // Exercise sysetm
                foreach (Thread thread in threads)
                    thread.Start();

                foreach (Thread thread in threads)
                    thread.Join();

                // Verify outcome
                sut.Dispose();
                Assert.Equal(threads.Length * 100, StaticDisposable.Count);
            }
            finally
            {
                // Teardown
                StaticDisposable.Count = 0;
            }
        }

        [Test]
        public void DisposeShouldBeThreadSafe(
            Container sut)
        {
            // Fixture setup
            var exceptions = new ConcurrentBag<Exception>();

            UnhandledExceptionEventHandler handler =
                (s, e) => exceptions.Add((Exception)e.ExceptionObject);
            AppDomain.CurrentDomain.UnhandledException += handler;

            var threads = new Thread[100];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() =>
                {
                    for (int j = 0; j < 100; j++)
                    {
                        using (sut.CreateChild())
                        {
                        }
                    }
                });
            }

            try
            {
                // Exercise system
                foreach (Thread thread in threads)
                    thread.Start();

                foreach (Thread thread in threads)
                    thread.Join();

                // Verify outcome
                Assert.Empty(exceptions);
            }
            finally
            {
                // Teardown
                AppDomain.CurrentDomain.UnhandledException -= handler;
            }
        }

        [Test]
        public void AcceptCorrectlyAcceptsVisitor(
            Container sut,
            IContainerVisitor<object> visitor,
            IContainerVisitor<object> expected)
        {
            visitor.ToMock().Setup(x => x.Visit(sut)).Returns(expected);
            var actual = sut.Accept(visitor);
            Assert.Equal(expected, actual);
        }

        [Test]
        public void DisposeIfOneChildWereDisposedCorrectlyDisposesOtherChildren(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Disposable()).ReusedWithinContainer();
            var child1 = sut.CreateChild();
            var child2 = sut.CreateChild();
            var child3 = sut.CreateChild();
            child2.Dispose();

            var disposable1 = child1.Resolve<Disposable>();
            var disposable3 = child3.Resolve<Disposable>();

            // Exercise system
            sut.Dispose();

            // Verify outcome
            Assert.Equal(1, disposable1.Count);
            Assert.Equal(1, disposable3.Count);
        }

        [Test]
        public void CanResolveUnregisteredServiceReturnsFalse(
            Container sut)
        {
            var actual = sut.CanResolve<Foo>();
            Assert.False(actual, "CanResolve");
        }

        [Test]
        public void CanResolveRegisteredServiceReturnsTrue(
            Container sut)
        {
            sut.Register(c => new Foo());
            var actual = sut.CanResolve<Foo>();
            Assert.True(actual, "CanResolve");
        }

        [Test]
        public void CanResolveUnregisteredServiceWithArgumentReturnsFalse(
            Container sut)
        {
            var actual = sut.CanResolve<Foo, string>();
            Assert.False(actual, "CanResolve");
        }

        [Test]
        public void CanResolveRegisteredServiceWithArgumentReturnsTrue(
            Container sut)
        {
            sut.Register<Foo, string>((c, s) => new Foo(s));
            var actual = sut.CanResolve<Foo, string>();
            Assert.True(actual, "CanResolve");
        }

        [Test]
        public void CanResolveKeyedUnregisteredServiceReturnsFalse(
            Container sut,
            object key)
        {
            var actual = sut.CanResolveKeyed<Foo>(key);
            Assert.False(actual, "CanResolve");
        }

        [Test]
        public void CanResolveKeyedRegisteredServiceReturnsTrue(
            Container sut,
            object key)
        {
            sut.Register(key, c => new Foo());
            var actual = sut.CanResolveKeyed<Foo>(key);
            Assert.True(actual, "CanResolve");
        }

        [Test]
        public void CanResolveKeyedUnregisteredServiceWithArgumentReturnsFalse(
            Container sut,
            object key)
        {
            var actual = sut.CanResolveKeyed<Foo, string>(key);
            Assert.False(actual, "CanResolve");
        }

        [Test]
        public void CanResolveKeyedRegisteredServiceWithArgumentReturnsTrue(
            Container sut,
            object key)
        {
            sut.Register<Foo, string>(key, (c, s) => new Foo(s));
            var actual = sut.CanResolveKeyed<Foo, string>(key);
            Assert.True(actual, "CanResolve");
        }

        [Test]
        public void CanResolveUnregisteredServiceIfCreatedByTemplateReturnsFalse(
            Container sut)
        {
            var actual = sut.CanResolve<Bar, object, int, string>();
            Assert.False(actual, "CanResolve");
        }

        [Test]
        public void CanResolveRegisteredServiceIfCreatedByTemplateReturnsTrue(
            Container sut)
        {
            sut.Register<Bar, object, int, string>((c, o, i, s) => new Bar(o, i, s));
            var actual = sut.CanResolve<Bar, object, int, string>();
            Assert.True(actual, "CanResolve");
        }

        [Test]
        public void CanResolveKeyedUnregisteredServiceIfCreatedByTemplateReturnsFalse(
            Container sut,
            object key)
        {
            var actual = sut.CanResolveKeyed<Bar, object, int, string>(key);
            Assert.False(actual, "CanResolve");
        }

        [Test]
        public void CanResolveKeyedRegisteredServiceIfCreatedByTemplateReturnsTrue(
            Container sut,
            object key)
        {
            sut.Register<Bar, object, int, string>(key, (c, o, i, s) => new Bar(o, i, s));
            var actual = sut.CanResolveKeyed<Bar, object, int, string>(key);
            Assert.True(actual, "CanResolve");
        }

        [Test]
        public void ScopeIsString(
            Container sut)
        {
            var actual = sut.Scope;
            Assert.IsType<string>(actual);
        }

        [Test]
        public void ScopeIsUnique(
            IFixture fixture)
        {
            var actual = Enumerable.Range(0, 10).Select(i => fixture.Create<Container>().Scope);
            Assert.Equal(10, actual.Distinct().Count());
        }

        [Test]
        public void ScopeIsCorrectFormat(
            Container sut)
        {
            var actual = sut.Scope;
            Assert.True(Regex.IsMatch(actual.ToString(), "Container[0-9a-f]{32}"), "format");
        }

        [Test]
        public void ToStringReturnsScopeString(
            [Frozen(As = typeof(object))] string scope,
            [Greedy] Container sut)
        {
            var actual = sut.ToString();
            Assert.Equal(scope, actual);
        }

        [Test]
        public void ScopeOfChildIsCorrectFormat(
            Container sut)
        {
            var child = sut.CreateChild();
            var actual = child.Scope;
            Assert.True(Regex.IsMatch(actual.ToString(), "Container[0-9a-f]{32}"), "format");
        }

        [Test]
        public void ScopeOfChildIsUnique(
            Container sut)
        {
            var actual = Enumerable.Range(0, 10).Select(i => sut.CreateChild().Scope);
            Assert.Equal(10, actual.Distinct().Count());
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            return typeof(Container).GetMethods()
                .Where(m => m.Name.StartsWith("LazyResolve"));
        }

        public class Foo
        {
            private readonly string arg;

            public Foo()
            {
            }

            public Foo(string arg)
            {
                this.arg = arg;
            }

            public string Arg
            {
                get { return this.arg; }
            }
        }

        public class Bar
        {
            private readonly object arg1;
            private readonly int arg2;
            private readonly string arg3;

            public Bar(object arg1, int arg2, string arg3)
            {
                this.arg1 = arg1;
                this.arg2 = arg2;
                this.arg3 = arg3;
            }

            public object Arg1
            {
                get { return this.arg1; }
            }

            public int Arg2
            {
                get { return this.arg2; }
            }

            public string Arg3
            {
                get { return this.arg3; }
            }
        }

        public class Disposable : IDisposable
        {
            public int Count { get; set; }

            public void Dispose()
            {
                this.Count++;
            }
        }

        public class DerivedContainer : Container
        {
            public bool Disposing { get; set; }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(this.Disposing);
            }
        }

        public class StaticDisposable : IDisposable
        {
            private static int count;

            public static int Count
            {
                get { return count; }
                set { count = value; }
            }

            public void Dispose()
            {
                Interlocked.Increment(ref count);
            }
        }

        private class Dummy
        {
            public string Content { get; set; }

            public void Generate(int size)
            {
                this.Content = new string('X', size * 3000);
            }
        }
    }
}