using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using Ploeh.Albedo;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Funz
{
    public class ContainerTest : IdiomaticTest<Container, ContainerTest>
    {
        public override IEnumerable<MemberInfo> GetGuardMembers()
        {
            MemberInfo[] members = typeof(Container)
                .GetMethods()
                .Where(m => m.Name.StartsWith("LazyResolve"))
                .Cast<MemberInfo>()
                .ToArray();

            return base.GetGuardMembers().Except(members);
        }

        [Theorem]
        public void SutIsDisposable(
            Container sut)
        {
            Assert.IsAssignableFrom<IDisposable>(sut);
        }

        [Theorem]
        public void RegisterServiceWithSameKeyManyTimeDoesNotThrow(
            Container sut)
        {
            sut.Register(c => new Foo());
            Assert.DoesNotThrow(() => sut.Register(c => new Foo()));
        }

        [Theorem]
        public void ResolveUnregisteredServiceThrows(
            Container sut)
        {
            var expected = string.Format(
                "The service type '{0}' was not registered.",
                typeof(Foo));

            var e = Assert.Throws<ResolutionException>(() => sut.Resolve<Foo>());

            Assert.Equal(expected, e.Message);
        }

        [Theorem]
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

        [Theorem]
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

        [Theorem]
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

        [Theorem]
        public void ResolveServiceReturnsCorrectInstance(
            Container sut)
        {
            sut.Register(c => new Foo());
            var actual = sut.Resolve<Foo>();
            Assert.NotNull(actual);
        }

        [Theorem]
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

        [Theorem]
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

        [Theorem]
        public void ResolveServiceOnContainerReturnsSharedInstance(
            Container sut)
        {
            sut.Register(c => new Foo());
            var expected = sut.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [Theorem]
        public void ResolveServiceOnChildReturnsSharedInstanceOnContainer(
            Container sut)
        {
            sut.Register(c => new Foo());
            var child = sut.CreateChild();
            var expected = sut.Resolve<Foo>();

            var actual = child.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [Theorem]
        public void ResolveServiceUsingContainerReturnsCorrectInstance(
            Container sut,
            string stringValue)
        {
            sut.Register(c => stringValue);
            sut.Register(c => new Foo(c.Resolve<string>()));

            var actual = sut.Resolve<Foo>();

            Assert.Equal(stringValue, actual.Arg);
        }

        [Theorem]
        public void ResolveKeyedServiceReturnsCorrectInstance(
            Container sut,
            object key)
        {
            sut.Register(key, c => new Foo());
            var actual = sut.ResolveKeyed<Foo>(key);
            Assert.NotNull(actual);
        }

        [Theorem]
        public void ResolveServiceWithArgumentReturnsCorrectInstance(
            Container sut,
            string stringValue)
        {
            sut.Register<Foo, string>((c, s) => new Foo(s));
            var actual = sut.Resolve<Foo, string>(stringValue);
            Assert.Equal(stringValue, actual.Arg);
        }

        [Theorem]
        public void ResolveKeyedServiceWithArgumentReturnsCorrectInstance(
            Container sut,
            string key,
            string stringValue)
        {
            sut.Register<Foo, string>(key, (c, s) => new Foo(s));
            var actual = sut.ResolveKeyed<Foo, string>(key, stringValue);
            Assert.Equal(stringValue, actual.Arg);
        }

        [Theorem]
        public void ResolveServiceReusedWithinNoneOnContainerReturnsNonSharedInstance(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinNone();
            var expected = sut.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.NotEqual(expected, actual);
        }

        [Theorem]
        public void ResolveServiceReusedWithinNoneOnChildReturnsNonSharedInstance(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinNone();
            var child = sut.CreateChild();
            var expected = child.Resolve<Foo>();

            var actual = child.Resolve<Foo>();

            Assert.NotEqual(expected, actual);
        }

        [Theorem]
        public void ResolveServiceReusedWithinNoneOnContainerReturnsNonSharedInstanceOnChild(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinNone();
            var child = sut.CreateChild();
            var expected = child.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.NotEqual(expected, actual);
        }

        [Theorem]
        public void ResolveServiceReusedWithinContainerOnContainerReturnsSharedInstance(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinContainer();
            var expected = sut.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [Theorem]
        public void ResolveServiceReusedWithinContainerOnChildReturnsSharedInstance(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinContainer();
            var child = sut.CreateChild();
            var expected = child.Resolve<Foo>();

            var actual = child.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [Theorem]
        public void ResolveServiceReusedWithinContainerOnContainerReturnsNonSharedInstanceOnChild(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinContainer();
            var child = sut.CreateChild();
            var expected = child.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.NotEqual(expected, actual);
        }

        [Theorem]
        public void ResolveServiceReusedWithinHierarchyOnContainerReturnsSharedInstance(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinHierarchy();
            var expected = sut.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [Theorem]
        public void ResolveServiceReusedWithinContainerOnContainerReturnsSharedInstanceOnChild(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinHierarchy();
            var child = sut.CreateChild();
            var expected = child.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [Theorem]
        public void ResolveServiceReusedWithinContainerOnChildReturnsSharedInstanceOnContainer(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinHierarchy();
            var child = sut.CreateChild();
            var expected = sut.Resolve<Foo>();

            var actual = child.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [SlowTheorem(RunOn.CI)]
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

        [Theorem]
        public void ResolveServiceReusedWithinCustomOnNonScopedContainerReturnsNonSharedInstance(
            Container sut,
            object scope)
        {
            sut.Register(c => new Foo()).ReusedWithin(scope);
            var expected = sut.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.NotEqual(expected, actual);
        }

        [Theorem]
        public void ResolveServiceReusedWithinCustomOnScopedContainerReturnsSharedInstance(
            [Greedy] Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithin(sut.Scope);
            var expected = sut.Resolve<Foo>();

            var actual = sut.Resolve<Foo>();

            Assert.Equal(expected, actual);
        }

        [Theorem]
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

        [Theorem]
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

        [Theorem]
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

        [Theorem]
        public void ResolveServiceWithArgumentReusedWithinNoneReturnsNonSharedInstance(
            Container sut,
            string argument)
        {
            sut.Register<Foo, string>((c, s) => new Foo()).ReusedWithinNone();
            var expected = sut.Resolve<Foo, string>(argument);

            var actual = sut.Resolve<Foo, string>(argument);

            Assert.NotEqual(expected, actual);
        }

        [Theorem]
        public void ResolveKeyedServiceReusedWithinNoneReturnsNonSharedInstance(
            Container sut,
            object key)
        {
            sut.Register(key, c => new Foo()).ReusedWithinNone();
            var expected = sut.ResolveKeyed<Foo>(key);

            var actual = sut.ResolveKeyed<Foo>(key);

            Assert.NotEqual(expected, actual);
        }

        [Theorem]
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

        [Theorem]
        public void ResolveServiceWithArgumentReusedWithinContainerReturnsSharedInstance(
            Container sut,
            string argument)
        {
            sut.Register<Foo, string>((c, s) => new Foo()).OwnedByContainer();
            var expected = sut.Resolve<Foo, string>(argument);

            var actual = sut.Resolve<Foo, string>(argument);

            Assert.Equal(expected, actual);
        }

        [Theorem]
        public void ResolveKeyedServiceReusedWithinContainerReturnsSharedInstance(
            Container sut,
            object key)
        {
            sut.Register(key, c => new Foo()).OwnedByContainer();
            var expected = sut.ResolveKeyed<Foo>(key);

            var actual = sut.ResolveKeyed<Foo>(key);

            Assert.Equal(expected, actual);
        }

        [Theorem]
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

        [Theorem]
        public void ResolveContainerServiceReturnsSutItself(Container sut)
        {
            var actual = sut.Resolve<Container>();
            Assert.Equal(sut, actual);
        }

        [Theorem]
        public void ResolveContainerServiceOnChildReturnsChildItself(Container sut)
        {
            var child = sut.CreateChild();
            var actual = child.Resolve<Container>();
            Assert.Equal(child, actual);
        }

        [Theorem]
        public void TryResolveUnregisteredServiceReturnsDefaultValue(
            Container sut)
        {
            var actual = sut.TryResolve<Foo>();
            Assert.Null(actual);
        }

        [Theorem]
        public void TryResolveRegisteredServiceReturnsCorrectInstance(
            Container sut)
        {
            sut.Register(c => new Foo());
            var actual = sut.TryResolve<Foo>();
            Assert.NotNull(actual);
        }

        [Theorem]
        public void TryResolveUnregisteredServiceWithArgumentReturnsDefaultValue(
            Container sut,
            string stringValue)
        {
            var actual = sut.TryResolve<Foo, string>(stringValue);
            Assert.Null(actual);
        }

        [Theorem]
        public void TryResolveRegisteredServiceWithArgumentReturnsCorrectInstance(
            Container sut,
            string stringValue)
        {
            sut.Register<Foo, string>((c, s) => new Foo(s));
            var actual = sut.TryResolve<Foo, string>(stringValue);
            Assert.Equal(stringValue, actual.Arg);
        }

        [Theorem]
        public void TryResolveUnregisteredKeyedServiceReturnsDefaultValue(
            Container sut,
            object key)
        {
            var actual = sut.TryResolveKeyed<Foo>(key);
            Assert.Null(actual);
        }

        [Theorem]
        public void TryResolveRegisteredKeyedServiceReturnsCorrectInstance(
            Container sut,
            object key)
        {
            sut.Register(key, c => new Foo());
            var actual = sut.TryResolveKeyed<Foo>(key);
            Assert.NotNull(actual);
        }

        [Theorem]
        public void TryResolveUnregisteredKeyedServiceWithArgumentReturnsDefaultValue(
            Container sut,
            object key,
            string stringValue)
        {
            var actual = sut.TryResolveKeyed<Foo, string>(key, stringValue);
            Assert.Null(actual);
        }

        [Theorem]
        public void TryResolveRegisteredKeyedServiceWithArgumentReturnsCorrectInstance(
            Container sut,
            object key,
            string stringValue)
        {
            sut.Register<Foo, string>(key, (c, s) => new Foo(s));
            var actual = sut.TryResolveKeyed<Foo, string>(key, stringValue);
            Assert.Equal(stringValue, actual.Arg);
        }

        [Theorem]
        public void LazyResolveUnregisteredServiceThrows(
            Container sut)
        {
            var expected = string.Format(
                "The service type '{0}' was not registered.",
                typeof(Foo));

            var e = Assert.Throws<ResolutionException>(() => sut.LazyResolve<Foo>().Invoke());

            Assert.Equal(expected, e.Message);
        }

        [Theorem]
        public void LazyResolveRegisteredServiceReturnsCorrectFactory(
            Container sut)
        {
            sut.Register(c => new Foo()).ReusedWithinNone();
            var expected = sut.LazyResolve<Foo>().Invoke();

            var actual = sut.LazyResolve<Foo>().Invoke();

            Assert.NotNull(actual);
            Assert.NotSame(expected, actual);
        }

        [Theorem]
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

        [Theorem]
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

        [Theorem]
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

        [Theorem]
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

        [Theorem]
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

        [Theorem]
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

        [Theorem]
        public void CreateChildReturnsCorrectContainer(
            Container sut)
        {
            sut.Register(c => new Foo());
            var actual = sut.CreateChild();
            Assert.NotNull(actual.Resolve<Foo>());
        }

        [Theorem]
        public void CreateChildWithScopeReturnsCorrectContainer(
            object scope,
            Container sut)
        {
            sut.Register(c => new Foo());

            var actual = sut.CreateChild(scope);

            Assert.NotNull(actual.Resolve<Foo>());
            Assert.Equal(scope, actual.Scope);
        }

        [Theorem]
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

        [Theorem]
        public void DisposeManyTimeDisposesOnlyOnce(
            Container sut)
        {
            sut.Register(c => new Disposable());
            var disposable = sut.Resolve<Disposable>();

            sut.Dispose();
            sut.Dispose();

            Assert.Equal(1, disposable.Count);
        }

        [Theorem]
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

        [Theorem]
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

        [Theorem]
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

        [Theorem]
        public void DisposeDoesNotDisposeServicesOwnedByExternalOnChild(
            Container sut)
        {
            sut.Register(c => new Disposable()).ReusedWithinContainer().OwnedByExternal();
            var child = sut.CreateChild();
            var disposable = child.Resolve<Disposable>();

            sut.Dispose();

            Assert.Equal(0, disposable.Count);
        }

        [Theorem]
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

        [Theorem]
        [PublicMethodData]
        public void CallAllPublicMethodAfterDisposedThrowsDisposedException(
            MethodInfo method,
            Container sut,
            IFixture fixture)
        {
            var context = new SpecimenContext(fixture);
            var arugments = method.GetParameters().Select(p => context.Resolve(p.ParameterType)).ToArray();
            sut.Dispose();

            var e = Assert.Throws<TargetInvocationException>(() => method.Invoke(sut, arugments));

            Assert.IsType<ObjectDisposedException>(e.InnerException);
        }

        [Theorem]
        public void DisposeWithFalseDisposingDoesNotDispose(
            DerivedContainer sut)
        {
            sut.Register(c => new Disposable()).ReusedWithinContainer();
            var disposable = sut.Resolve<Disposable>();
            sut.Disposing = false;

            sut.Dispose();

            Assert.Equal(0, disposable.Count);
        }

        [Theorem]
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

        [Theorem]
        public void LazyResolveWithNullKeyThrows(
            DerivedContainer sut)
        {
            var e = Assert.Throws<ArgumentNullException>(() => sut.LazyResolveKeyed<Foo>(null).Invoke());
            Assert.Equal("key", e.ParamName);
        }

        [Theorem]
        public void LazyResolveWithNullKeyAndArgumentThrows(
            DerivedContainer sut,
            string argument)
        {
            var e = Assert.Throws<ArgumentNullException>(
                () => sut.LazyResolveKeyed<Foo, string>(null).Invoke(argument));
            Assert.Equal("key", e.ParamName);
        }

        [Theorem]
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

        [Theorem]
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

        [Theorem]
        public void TryResolveCreatedByTemplateIfUnregisteredReturnsNull(
            DerivedContainer sut,
            object arg1,
            int arg2,
            string arg3)
        {
            var actual = sut.TryResolve<Bar, object, int, string>(arg1, arg2, arg3);
            Assert.Null(actual);
        }

        [Theorem]
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

        [Theorem]
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

        [Theorem]
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

        [Theorem]
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

        [Theorem]
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

        [Theorem]
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

        [Theorem]
        public void ResolveKeyedRecursiveServiceThrows(
            Container sut,
            object key)
        {
            // Fixture setup
            sut.Register(key, c =>
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

        [Theorem]
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

        [Theorem]
        public void ResolveKeyedRecursiveServiceWithArgumentThrows(
            Container sut,
            object key,
            string argument)
        {
            // Fixture setup
            sut.Register<Foo, string>(key, (c, s) =>
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

        [Theorem]
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

        [Theorem]
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

        [Theorem]
        public void ResolveKeyedRecursiveServiceCreatedByTemplateThrows(
            Container sut,
            object key,
            object arg1,
            int arg2,
            string arg3)
        {
            // Fixture setup
            sut.Register<Bar, object, int, string>(key, (c, o, i, s) =>
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

        [Theorem]
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

        [Theorem]
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

        [Theorem]
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
                Assert.Equal(threads.Length*100, StaticDisposable.Count);
            }
            finally
            {
                // Teardown
                StaticDisposable.Count = 0;
            }
        }

        [Theorem]
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
                        using (sut.CreateChild()) { }
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

        [Theorem]
        public void AcceptCorrectlyAcceptsVisitor(
            Container sut,
            IContainerVisitor<object> visitor,
            IContainerVisitor<object> expected)
        {
            visitor.ToMock().Setup(x => x.Visit(sut)).Returns(expected);
            var actual = sut.Accept(visitor);
            Assert.Equal(expected, actual);
        }

        [Theorem]
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

        [Theorem]
        public void CanResolveUnregisteredServiceReturnsFalse(
            Container sut)
        {
            var actual = sut.CanResolve<Foo>();
            Assert.False(actual, "CanResolve");
        }

        [Theorem]
        public void CanResolveRegisteredServiceReturnsTrue(
            Container sut)
        {
            sut.Register(c => new Foo());
            var actual = sut.CanResolve<Foo>();
            Assert.True(actual, "CanResolve");
        }

        [Theorem]
        public void CanResolveUnregisteredServiceWithArgumentReturnsFalse(
            Container sut)
        {
            var actual = sut.CanResolve<Foo, string>();
            Assert.False(actual, "CanResolve");
        }

        [Theorem]
        public void CanResolveRegisteredServiceWithArgumentReturnsTrue(
            Container sut)
        {
            sut.Register<Foo, string>((c, s) => new Foo(s));
            var actual = sut.CanResolve<Foo, string>();
            Assert.True(actual, "CanResolve");
        }

        [Theorem]
        public void CanResolveKeyedUnregisteredServiceReturnsFalse(
            Container sut,
            object key)
        {
            var actual = sut.CanResolveKeyed<Foo>(key);
            Assert.False(actual, "CanResolve");
        }

        [Theorem]
        public void CanResolveKeyedRegisteredServiceReturnsTrue(
            Container sut,
            object key)
        {
            sut.Register(key, c => new Foo());
            var actual = sut.CanResolveKeyed<Foo>(key);
            Assert.True(actual, "CanResolve");
        }

        [Theorem]
        public void CanResolveKeyedUnregisteredServiceWithArgumentReturnsFalse(
            Container sut,
            object key)
        {
            var actual = sut.CanResolveKeyed<Foo, string>(key);
            Assert.False(actual, "CanResolve");
        }

        [Theorem]
        public void CanResolveKeyedRegisteredServiceWithArgumentReturnsTrue(
            Container sut,
            object key)
        {
            sut.Register<Foo, string>(key, (c, s) => new Foo(s));
            var actual = sut.CanResolveKeyed<Foo, string>(key);
            Assert.True(actual, "CanResolve");
        }

        [Theorem]
        public void CanResolveUnregisteredServiceIfCreatedByTemplateReturnsFalse(
            Container sut)
        {
            var actual = sut.CanResolve<Bar, object, int, string>();
            Assert.False(actual, "CanResolve");
        }

        [Theorem]
        public void CanResolveRegisteredServiceIfCreatedByTemplateReturnsTrue(
            Container sut)
        {
            sut.Register<Bar, object, int, string>((c, o, i, s) => new Bar(o, i, s));
            var actual = sut.CanResolve<Bar, object, int, string>();
            Assert.True(actual, "CanResolve");
        }

        [Theorem]
        public void CanResolveKeyedUnregisteredServiceIfCreatedByTemplateReturnsFalse(
            Container sut,
            object key)
        {
            var actual = sut.CanResolveKeyed<Bar, object, int, string>(key);
            Assert.False(actual, "CanResolve");
        }

        [Theorem]
        public void CanResolveKeyedRegisteredServiceIfCreatedByTemplateReturnsTrue(
            Container sut,
            object key)
        {
            sut.Register<Bar, object, int, string>(key, (c, o, i, s) => new Bar(o, i, s));
            var actual = sut.CanResolveKeyed<Bar, object, int, string>(key);
            Assert.True(actual, "CanResolve");
        }

        [Theorem]
        public void ScopeIsString(
            Container sut)
        {
            var actual = sut.Scope;
            Assert.IsType<string>(actual);
        }

        [Theorem]
        public void ScopeIsUnique(
            IFixture fixture)
        {
            var actual = Enumerable.Range(0, 10).Select(i => fixture.Create<Container>().Scope);
            Assert.Equal(10, actual.Distinct().Count());
        }

        [Theorem]
        public void ScopeIsCorrectFormat(
            Container sut)
        {
            var actual = sut.Scope;
            Assert.True(Regex.IsMatch(actual.ToString(), "Container[0-9a-f]{32}"), "format");
        }

        [Theorem]
        public void ToStringReturnsScopeString(
            [Frozen(As = typeof(object))] string scope,
            [Greedy] Container sut)
        {
            var actual = sut.ToString();
            Assert.Equal(scope, actual);
        }

        [Theorem]
        public void ScopeOfChildIsCorrectFormat(
            Container sut)
        {
            var child = sut.CreateChild();
            var actual = child.Scope;
            Assert.True(Regex.IsMatch(actual.ToString(), "Container[0-9a-f]{32}"), "format");
        }

        [Theorem]
        public void ScopeOfChildIsUnique(
            Container sut)
        {
            var actual = Enumerable.Range(0, 10).Select(i => sut.CreateChild().Scope);
            Assert.Equal(10, actual.Distinct().Count());
        }

        private class PublicMethodDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                const BindingFlags bindingFlags =
                    BindingFlags.Public
                    | BindingFlags.Static
                    | BindingFlags.Instance
                    | BindingFlags.DeclaredOnly;

                var methods = typeof(Container)
                    .GetMethods(bindingFlags)
                    .Where(m => !m.Name.StartsWith("LazyResolve"))
                    .Except(new[] { new Methods<Container>().Select(x => x.Dispose()) })
                    .Select(m =>
                        !m.ContainsGenericParameters
                        ? m
                        : m.MakeGenericMethod(m.GetGenericArguments().Select(a => typeof(object)).ToArray()));

                return methods.Select(m => new object[] { m });
            }
        }

        public class Foo
        {
            private readonly string _arg;

            public Foo()
            {
            }

            public Foo(string arg)
            {
                _arg = arg;
            }

            public string Arg
            {
                get { return _arg; }
            }
        }

        public class Bar
        {
            private readonly object _arg1;
            private readonly int _arg2;
            private readonly string _arg3;

            public Bar(object arg1, int arg2, string arg3)
            {
                _arg1 = arg1;
                _arg2 = arg2;
                _arg3 = arg3;
            }

            public object Arg1
            {
                get { return _arg1; }
            }

            public int Arg2
            {
                get { return _arg2; }
            }

            public string Arg3
            {
                get { return _arg3; }
            }
        }

        public class Disposable : IDisposable
        {
            public int Count { get; set; }

            public void Dispose()
            {
                Count++;
            }
        }

        private class Dummy
        {
            public string Content { get; set; }

            public void Generate(int size)
            {
                Content = new string('X', size*3000);
            }
        }

        public class DerivedContainer : Container
        {
            public bool Disposing { get; set; }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(Disposing);
            }
        }

        public class StaticDisposable : IDisposable
        {
            private static int _count;

            public static int Count
            {
                get { return _count; }
                set { _count = value; }
            }

            public void Dispose()
            {
                Interlocked.Increment(ref _count);
            }
        }
    }
}