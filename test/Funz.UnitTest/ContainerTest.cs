using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.AutoFixture;
using Jwc.AutoFixture.Idioms;
using Jwc.AutoFixture.Xunit;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Funz
{
    public class ContainerTest
    {
        [Spec]
        [MemberData]
        public void SutHasAppropriateGuards(
            MemberInfo member,
            GuardClauseAssertion assertion)
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            assertion.Verify(member);
        }

        [Spec]
        [MemberData]
        public void SutHasCorrectInitializedMembers(
            MemberInfo member,
            IFixture fixture)
        {
            // Fixture setup
            var assertion = new ConstructorInitializedMemberAssertion(
                fixture,
                EqualityComparer<object>.Default,
                new ParameterPropertyMatcher());

            // Exercise system
            // Verify outcome
            assertion.Verify(member);
        }

        [Spec]
        public void SutIsDisposable(
            Container sut)
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.IsAssignableFrom<IDisposable>(sut);
        }

        [Spec]
        public void RegisterServiceWithSameKeyManyTimeDoesNotThrow(
             Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo());

            // Exercise system
            // Verify outcome
            Assert.DoesNotThrow(() => sut.Register(c => new Foo()));
        }

        [Spec]
        public void ResolveUnregisteredServiceThrows(
            Container sut)
        {
            // Fixture setup
            var expected = string.Format(
                "The service type '{0}' was not registered.",
                typeof(Foo));

            // Exercise system
            var e = Assert.Throws<ResolutionException>(() => sut.Resolve<Foo>());

            // Verify outcome
            Assert.Equal(expected, e.Message);
        }

        [Spec]
        public void ResolveUnregisteredServiceWithArgumentThrows(
            Container sut,
            string stringArg)
        {
            // Fixture setup
            var expected = string.Format(
                "The service type '{0}' with the argument(s) '{1}' was not registered.",
                typeof(Foo),
                "System.String");

            // Exercise system
            var e = Assert.Throws<ResolutionException>(() => sut.Resolve<Foo, string>(stringArg));

            // Verify outcome
            Assert.Equal(expected, e.Message);
        }

        [Spec]
        public void ResolveUnregisteredKeyedServiceThrows(
            Container sut,
            object key)
        {
            // Fixture setup
            sut.Register(c => new Foo());
            var expected = string.Format(
                "The service type '{0}' with the key '{1}' was not registered.",
                typeof(Foo),
                key);

            // Exercise system
            var e = Assert.Throws<ResolutionException>(() => sut.ResolveKeyed<Foo>(key));

            // Verify outcome
            Assert.Equal(expected, e.Message);
        }

        [Spec]
        public void ResolveUnregisteredKeyedServiceWithArgumentThrows(
            Container sut,
            object key,
            string stringArg)
        {
            // Fixture setup
            var expected = string.Format(
                "The service type '{0}' with the key '{1}' and the argument(s) '{2}' was not registered.",
                typeof(Foo),
                key,
                "System.String");

            // Exercise system
            var e = Assert.Throws<ResolutionException>(() => sut.ResolveKeyed<Foo, string>(key, stringArg));

            // Verify outcome
            Assert.Equal(expected, e.Message);
        }

        [Spec]
        public void ResolveServiceReturnsCorrectInstance(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo());

            // Exercise system
            var actual = sut.Resolve<Foo>();

            // Verify outcome
            Assert.NotNull(actual);
        }

        [Spec]
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

            // Exercise system
            // Verify outcome
            Assert.Equal(fooValue, sut.Resolve<Foo>());
            Assert.Equal(intValue, sut.Resolve<int>());
            Assert.Equal(stringValue, sut.Resolve<string>());
        }

        [Spec]
        public void ResolveManySameTypedServicesReturnsCorrectInstances(
            Container sut,
            string stringValue)
        {
            // Fixture setup
            sut.Register(c => new Foo());
            sut.Register<Foo, string>((c, s) => new Foo(s));

            // Exercise system
            var actual1 = sut.Resolve<Foo>();
            var actual2 = sut.Resolve<Foo, string>(stringValue);

            // Verify outcome
            Assert.NotNull(actual1);
            Assert.Equal(stringValue, actual2.Arg);
        }

        [Spec]
        public void ResolveServiceOnContainerReturnsSharedInstance(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo());
            var expected = sut.Resolve<Foo>();

            // Exercise system
            var actual = sut.Resolve<Foo>();

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Spec]
        public void ResolveServiceOnChildReturnsSharedInstanceOnContainer(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo());
            var child = sut.CreateChild();
            var expected = sut.Resolve<Foo>();

            // Exercise system
            var actual = child.Resolve<Foo>();

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Spec]
        public void ResolveServiceUsingContainerReturnsCorrectInstance(
            Container sut,
            string stringValue)
        {
            // Fixture setup
            sut.Register(c => stringValue);
            sut.Register(c => new Foo(c.Resolve<string>()));

            // Exercise system
            var actual = sut.Resolve<Foo>();

            // Verify outcome
            Assert.Equal(stringValue, actual.Arg);
        }

        [Spec]
        public void ResolveKeyedServiceReturnsCorrectInstance(
            Container sut,
            object key)
        {
            // Fixture setup
            sut.Register(key, c => new Foo());

            // Exercise system
            var actual = sut.ResolveKeyed<Foo>(key);

            // Verify outcome
            Assert.NotNull(actual);
        }

        [Spec]
        public void ResolveServiceWithArgumentReturnsCorrectInstance(
            Container sut,
            string stringValue)
        {
            // Fixture setup
            sut.Register<Foo, string>((c, s) => new Foo(s));

            // Exercise system
            var actual = sut.Resolve<Foo, string>(stringValue);

            // Verify outcome
            Assert.Equal(stringValue, actual.Arg);
        }

        [Spec]
        public void ResolveKeyedServiceWithArgumentReturnsCorrectInstance(
            Container sut,
            string key,
            string stringValue)
        {
            // Fixture setup
            sut.Register<Foo, string>(key, (c, s) => new Foo(s));

            // Exercise system
            var actual = sut.ResolveKeyed<Foo, string>(key, stringValue);

            // Verify outcome
            Assert.Equal(stringValue, actual.Arg);
        }

        [Spec]
        public void ResolveServiceReusedWithinNoneOnContainerReturnsNonSharedInstance(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo()).ReusedWithinNone();
            var expected = sut.Resolve<Foo>();

            // Exercise system
            var actual = sut.Resolve<Foo>();

            // Verify outcome
            Assert.NotEqual(expected, actual);
        }

        [Spec]
        public void ResolveServiceReusedWithinNoneOnChildReturnsNonSharedInstance(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo()).ReusedWithinNone();
            var child = sut.CreateChild();
            var expected = child.Resolve<Foo>();

            // Exercise system
            var actual = child.Resolve<Foo>();

            // Verify outcome
            Assert.NotEqual(expected, actual);
        }

        [Spec]
        public void ResolveServiceReusedWithinNoneOnContainerReturnsNonSharedInstanceOnChild(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo()).ReusedWithinNone();
            var child = sut.CreateChild();
            var expected = child.Resolve<Foo>();

            // Exercise system
            var actual = sut.Resolve<Foo>();

            // Verify outcome
            Assert.NotEqual(expected, actual);
        }

        [Spec]
        public void ResolveServiceReusedWithinContainerOnContainerReturnsSharedInstance(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo()).ReusedWithinContainer();
            var expected = sut.Resolve<Foo>();

            // Exercise system
            var actual = sut.Resolve<Foo>();

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Spec]
        public void ResolveServiceReusedWithinContainerOnChildReturnsSharedInstance(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo()).ReusedWithinContainer();
            var child = sut.CreateChild();
            var expected = child.Resolve<Foo>();

            // Exercise system
            var actual = child.Resolve<Foo>();

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Spec]
        public void ResolveServiceReusedWithinContainerOnContainerReturnsNonSharedInstanceOnChild(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo()).ReusedWithinContainer();
            var child = sut.CreateChild();
            var expected = child.Resolve<Foo>();

            // Exercise system
            var actual = sut.Resolve<Foo>();

            // Verify outcome
            Assert.NotEqual(expected, actual);
        }

        [Spec]
        public void ResolveServiceReusedWithinHierarchyOnContainerReturnsSharedInstance(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo()).ReusedWithinHierarchy();
            var expected = sut.Resolve<Foo>();

            // Exercise system
            var actual = sut.Resolve<Foo>();

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Spec]
        public void ResolveServiceReusedWithinContainerOnContainerReturnsSharedInstanceOnChild(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo()).ReusedWithinHierarchy();
            var child = sut.CreateChild();
            var expected = child.Resolve<Foo>();

            // Exercise system
            var actual = sut.Resolve<Foo>();

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Spec]
        public void ResolveServiceReusedWithinContainerOnChildReturnsSharedInstanceOnContainer(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo()).ReusedWithinHierarchy();
            var child = sut.CreateChild();
            var expected = sut.Resolve<Foo>();

            // Exercise system
            var actual = child.Resolve<Foo>();

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [ExplicitSpec(Run.Skip)]
        public void ResolveServiceReusedWithinContainerDoesNotThrowOutOfMemoryException(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Dummy()).ReusedWithinContainer();

            // Exercise system
            // Verify outcome
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

        [Spec]
        public void ResolveServiceReusedWithinCustomOnNonScopedContainerReturnsNonSharedInstance(
            Container sut,
            object scope)
        {
            // Fixture setup
            sut.Register(c => new Foo()).ReusedWithin(scope);
            var expected = sut.Resolve<Foo>();

            // Exercise system
            var actual = sut.Resolve<Foo>();

            // Verify outcome
            Assert.NotEqual(expected, actual);
        }

        [Spec]
        public void ResolveServiceReusedWithinCustomOnScopedContainerReturnsSharedInstance(
            [Inject] object scope,
            [Build] Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo()).ReusedWithin(scope);
            var expected = sut.Resolve<Foo>();

            // Exercise system
            var actual = sut.Resolve<Foo>();

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Spec]
        public void ResolveServiceReusedWithinCustomOnScopedChildReturnsNonSharedInstanceOnNonScopedContainer(
            object scope,
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo()).ReusedWithin(scope);
            var child = sut.CreateChild(scope);
            var expected = sut.Resolve<Foo>();

            // Exercise system
            var actual = child.Resolve<Foo>();

            // Verify outcome
            Assert.NotEqual(expected, actual);
        }

        [Spec]
        public void ResolveServiceReusedWithinCustomOnScopedChildReturnsSharedInstanceOnScopedGrandChild(
            string scope,
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo()).ReusedWithin(scope);
            var child = sut.CreateChild(scope);
            var grandChild = child.CreateChild();
            var expected = grandChild.Resolve<Foo>();

            // Exercise system
            var actual = child.Resolve<Foo>();

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Spec]
        public void ResolveServiceReusedWithinCustomOnNewScopedGrandChildReturnsSharedInstanceOnScopedChild(
            string scope,
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo()).ReusedWithin(scope);
            var child = sut.CreateChild(scope);
            var grandChild = child.CreateChild(scope);
            var expected = child.Resolve<Foo>();

            // Exercise system
            var actual = grandChild.Resolve<Foo>();

            // Verify outcome
            Assert.NotEqual(expected, actual);
        }

        [Spec]
        public void ResolveServiceWithArgumentReusedWithinNoneReturnsNonSharedInstance(
            Container sut,
            string argument)
        {
            // Fixture setup
            sut.Register<Foo, string>((c, s) => new Foo()).ReusedWithinNone();
            var expected = sut.Resolve<Foo, string>(argument);

            // Exercise system
            var actual = sut.Resolve<Foo, string>(argument);

            // Verify outcome
            Assert.NotEqual(expected, actual);
        }

        [Spec]
        public void ResolveKeyedServiceReusedWithinNoneReturnsNonSharedInstance(
            Container sut,
            object key)
        {
            // Fixture setup
            sut.Register(key, c => new Foo()).ReusedWithinNone();
            var expected = sut.ResolveKeyed<Foo>(key);

            // Exercise system
            var actual = sut.ResolveKeyed<Foo>(key);

            // Verify outcome
            Assert.NotEqual(expected, actual);
        }

        [Spec]
        public void ResolveKeyedServiceWithArgumentReusedWithinNoneReturnsNonSharedInstance(
            Container sut,
            object key,
            string argument)
        {
            // Fixture setup
            sut.Register<Foo, string>(key, (c, s) => new Foo(s)).ReusedWithinNone();
            var expected = sut.ResolveKeyed<Foo, string>(key, argument);

            // Exercise system
            var actual = sut.ResolveKeyed<Foo, string>(key, argument);

            // Verify outcome
            Assert.NotEqual(expected, actual);
        }

        [Spec]
        public void ResolveServiceWithArgumentReusedWithinContainerReturnsSharedInstance(
            Container sut,
            string argument)
        {
            // Fixture setup
            sut.Register<Foo, string>((c, s) => new Foo()).OwnedByContainer();
            var expected = sut.Resolve<Foo, string>(argument);

            // Exercise system
            var actual = sut.Resolve<Foo, string>(argument);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Spec]
        public void ResolveKeyedServiceReusedWithinContainerReturnsSharedInstance(
            Container sut,
            object key)
        {
            // Fixture setup
            sut.Register(key, c => new Foo()).OwnedByContainer();
            var expected = sut.ResolveKeyed<Foo>(key);

            // Exercise system
            var actual = sut.ResolveKeyed<Foo>(key);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Spec]
        public void ResolveKeyedServiceWithArgumentReusedWithinContainerReturnsSharedInstance(
            Container sut,
            object key,
            string stringValue)
        {
            // Fixture setup
            sut.Register<Foo, string>(key, (c, s) => new Foo(s)).OwnedByContainer();
            var expected = sut.ResolveKeyed<Foo, string>(key, stringValue);

            // Exercise system
            var actual = sut.ResolveKeyed<Foo, string>(key, stringValue);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Spec]
        public void ResolveContainerServiceReturnsSutItself(Container sut)
        {
            // Fixture setup
            // Exercise system
            var actual = sut.Resolve<Container>();

            // Verify outcome
            Assert.Equal(sut, actual);
        }

        [Spec]
        public void ResolveContainerServiceOnChildReturnsChildItself(Container sut)
        {
            // Fixture setup
            var child = sut.CreateChild();

            // Exercise system
            var actual = child.Resolve<Container>();

            // Verify outcome
            Assert.Equal(child, actual);
        }

        [Spec]
        public void TryResolveNotRegisteredServiceReturnsDefaultValue(
            Container sut)
        {
            // Fixture setup
            // Exercise system
            var actual = sut.TryResolve<Foo>();

            // Verify outcome
            Assert.Null(actual);
        }

        [Spec]
        public void TryResolveRegisteredServiceReturnsCorrectInstance(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo());

            // Exercise system
            var actual = sut.TryResolve<Foo>();

            // Verify outcome
            Assert.NotNull(actual);
        }

        [Spec]
        public void TryResolveNotRegisteredServiceWithArgumentReturnsDefaultValue(
            Container sut,
            string stringValue)
        {
            // Fixture setup
            // Exercise system
            var actual = sut.TryResolve<Foo, string>(stringValue);

            // Verify outcome
            Assert.Null(actual);
        }

        [Spec]
        public void TryResolveRegisteredServiceWithArgumentReturnsCorrectInstance(
            Container sut,
            string stringValue)
        {
            // Fixture setup
            sut.Register<Foo, string>((c, s) => new Foo(s));

            // Exercise system
            var actual = sut.TryResolve<Foo, string>(stringValue);

            // Verify outcome
            Assert.Equal(stringValue, actual.Arg);
        }

        [Spec]
        public void TryResolveNotRegisteredKeyedServiceReturnsDefaultValue(
            Container sut,
            object key)
        {
            // Fixture setup
            // Exercise system
            var actual = sut.TryResolveKeyed<Foo>(key);

            // Verify outcome
            Assert.Null(actual);
        }

        [Spec]
        public void TryResolveRegisteredKeyedServiceReturnsCorrectInstance(
            Container sut,
            object key)
        {
            // Fixture setup
            sut.Register(key, c => new Foo());

            // Exercise system
            var actual = sut.TryResolveKeyed<Foo>(key);

            // Verify outcome
            Assert.NotNull(actual);
        }

        [Spec]
        public void TryResolveNotRegisteredKeyedServiceWithArgumentReturnsDefaultValue(
            Container sut,
            object key,
            string stringValue)
        {
            // Fixture setup
            // Exercise system
            var actual = sut.TryResolveKeyed<Foo, string>(key, stringValue);

            // Verify outcome
            Assert.Null(actual);
        }

        [Spec]
        public void TryResolveRegisteredKeyedServiceWithArgumentReturnsCorrectInstance(
            Container sut,
            object key,
            string stringValue)
        {
            // Fixture setup
            sut.Register<Foo, string>(key, (c, s) => new Foo(s));

            // Exercise system
            var actual = sut.TryResolveKeyed<Foo, string>(key, stringValue);

            // Verify outcome
            Assert.Equal(stringValue, actual.Arg);
        }

        [Spec]
        public void LazyResolveNotRegisteredServiceThrows(
            Container sut)
        {
            // Fixture setup
            var expected = string.Format(
                "The service type '{0}' was not registered.",
                typeof(Foo));

            // Exercise system
            var e = Assert.Throws<ResolutionException>(() => sut.LazyResolve<Foo>().Invoke());

            // Verify outcome
            Assert.Equal(expected, e.Message);
        }

        [Spec]
        public void LazyResolveRegisteredServiceReturnsCorrectFactory(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo()).ReusedWithinNone();
            var expected = sut.LazyResolve<Foo>().Invoke();

            // Exercise system
            var actual = sut.LazyResolve<Foo>().Invoke();

            // Verify outcome
            Assert.NotNull(actual);
            Assert.NotSame(expected, actual);
        }

        [Spec]
        public void LazyResolveNotRegisteredServiceWithArgumentThrows(
            Container sut,
            string argument)
        {
            // Fixture setup
            var expected = string.Format(
                "The service type '{0}' with the argument(s) '{1}' was not registered.",
                typeof(Foo),
                "System.String");

            // Exercise system
            var e = Assert.Throws<ResolutionException>(() => sut.LazyResolve<Foo, string>().Invoke(argument));

            // Verify outcome
            Assert.Equal(expected, e.Message);
        }

        [Spec]
        public void LazyResolveRegisteredServiceWithArgumentReturnsCorrectFactory(
            Container sut,
            string argument)
        {
            // Fixture setup
            sut.Register<Foo, string>((c, s) => new Foo(s)).ReusedWithinNone();
            var expected = sut.LazyResolve<Foo, string>().Invoke(argument);

            // Exercise system
            var actual = sut.LazyResolve<Foo, string>().Invoke(argument);

            // Verify outcome
            Assert.NotNull(actual);
            Assert.NotSame(expected, actual);
            Assert.Equal(argument, actual.Arg);
        }

        [Spec]
        public void LazyResolveNotRegisteredKeyedServiceThrows(
            Container sut,
            object key)
        {
            // Fixture setup
            var expected = string.Format(
                "The service type '{0}' with the key '{1}' was not registered.",
                typeof(Foo),
                key);

            // Exercise system
            var e = Assert.Throws<ResolutionException>(() => sut.LazyResolveKeyed<Foo>(key).Invoke());

            // Verify outcome
            Assert.Equal(expected, e.Message);
        }

        [Spec]
        public void LazyResolveRegisteredKeyedServiceReturnsCorrectFactory(
            Container sut,
            object key)
        {
            // Fixture setup
            sut.Register(key, c => new Foo()).ReusedWithinNone();
            var expected = sut.LazyResolveKeyed<Foo>(key).Invoke();

            // Exercise system
            var actual = sut.LazyResolveKeyed<Foo>(key).Invoke();

            // Verify outcome
            Assert.NotNull(actual);
            Assert.NotSame(expected, actual);
        }

        [Spec]
        public void LazyResolveNotRegisteredKeyedServiceWithArgumentThrows(
            Container sut,
            object key,
            string argument)
        {
            // Fixture setup
            var expected = string.Format(
                "The service type '{0}' with the key '{1}' and the argument(s) '{2}' was not registered.",
                typeof(Foo),
                key,
                "System.String");

            // Exercise system
            var e = Assert.Throws<ResolutionException>(() => sut.LazyResolveKeyed<Foo, string>(key).Invoke(argument));

            // Verify outcome
            Assert.Equal(expected, e.Message);
        }

        [Spec]
        public void LazyResolveRegisteredKeyedServiceWithArgumentReturnsCorrectFactory(
            Container sut,
            object key,
            string argument)
        {
            // Fixture setup
            sut.Register<Foo, string>(key, (c, s) => new Foo(s)).ReusedWithinNone();
            var expected = sut.LazyResolveKeyed<Foo, string>(key).Invoke(argument);

            // Exercise system
            var actual = sut.LazyResolveKeyed<Foo, string>(key).Invoke(argument);

            // Verify outcome
            Assert.NotNull(actual);
            Assert.NotSame(expected, actual);
            Assert.Equal(argument, actual.Arg);
        }

        [Spec]
        public void CreateChildReturnsCorrectContainer(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo());

            // Exercise system
            var actual = sut.CreateChild();
            
            // Verify outcome
            Assert.NotNull(actual.Resolve<Foo>());
        }

        [Spec]
        public void CreateChildWithScopeReturnsCorrectContainer(
            object scope,
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo());

            // Exercise system
            var actual = sut.CreateChild(scope);

            // Verify outcome
            Assert.NotNull(actual.Resolve<Foo>());
            Assert.Equal(scope, actual.Scope);
        }

        [Spec]
        public void DisposeDisposesServices(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Disposable()).ReusedWithinContainer();
            var disposable1 = sut.Resolve<Disposable>();
            var disposable2 = sut.Resolve<Disposable>();

            // Exercise system
            sut.Dispose();

            // Verify outcome
            Assert.Equal(1, disposable1.Count);
            Assert.Equal(1, disposable2.Count);
        }

        [Spec]
        public void DisposeManyTimeDisposesOnlyOnce(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Disposable());
            var disposable = sut.Resolve<Disposable>();

            // Exercise system
            sut.Dispose();
            sut.Dispose();

            // Verify outcome
            Assert.Equal(1, disposable.Count);
        }

        [Spec]
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

        [Spec]
        public void DisposeDoesNotDisposeServicesOwnedByExternal(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Disposable()).ReusedWithinContainer().OwnedByExternal();
            var disposable1 = sut.Resolve<Disposable>();
            var disposable2 = sut.Resolve<Disposable>();

            // Exercise system
            sut.Dispose();

            // Verify outcome
            Assert.Equal(0, disposable1.Count);
            Assert.Equal(0, disposable2.Count);
        }

        [Spec]
        public void DisposeDisposesServicesOwnedByContainer(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Disposable()).ReusedWithinContainer().OwnedByContainer();
            var disposable1 = sut.Resolve<Disposable>();
            var disposable2 = sut.Resolve<Disposable>();

            // Exercise system
            sut.Dispose();

            // Verify outcome
            Assert.Equal(1, disposable1.Count);
            Assert.Equal(1, disposable2.Count);
        }

        [Spec]
        public void DisposeDoesNotDisposeServicesOwnedByExternalOnChild(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Disposable()).ReusedWithinContainer().OwnedByExternal();
            var child = sut.CreateChild();
            var disposable = child.Resolve<Disposable>();

            // Exercise system
            sut.Dispose();

            // Verify outcome
            Assert.Equal(0, disposable.Count);
        }

        [Spec]
        public void DisposeDoesNotDisposeServicesOwnedByExternalOnScopedChild(
            object scope,
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Disposable()).ReusedWithin(scope).OwnedByExternal();
            var child = sut.CreateChild(scope);
            var disposable = child.Resolve<Disposable>();

            // Exercise system
            sut.Dispose();

            // Verify outcome
            Assert.Equal(0, disposable.Count);
        }

        [ExplicitSpec(Run.Skip)]
        [PublicMethodData]
        public void CallAllPublicMethodAfterDisposedThrowsDisposedException(
            MethodInfo method,
            Container sut,
            IFixture fixture)
        {
            // Fixture setup
            var arugments = method.GetParameters().Select(p => fixture.Create((object)p.ParameterType)).ToArray();
            sut.Dispose();

            // Exercise system
            var e = Assert.Throws<TargetInvocationException>(() => method.Invoke(sut, arugments));

            // Verify outcome
            Assert.IsType<ObjectDisposedException>(e.InnerException);
        }

        [Spec]
        public void DisposeWithFalseDisposingDoesNotDispose(
            DerivedContainer sut)
        {
            // Fixture setup
            sut.Register(c => new Disposable()).ReusedWithinContainer();
            var disposable = sut.Resolve<Disposable>();
            sut.Disposing = false;

            // Exercise system
            sut.Dispose();

            // Verify outcome
            Assert.Equal(0, disposable.Count);
        }

        [Spec]
        public void DisposeWithDisposingTwiceDoesNotDispose(
            DerivedContainer sut)
        {
            // Fixture setup
            sut.Register(c => new Disposable()).ReusedWithinContainer();
            var disposable = sut.Resolve<Disposable>();
            sut.Disposing = false;
            sut.Dispose();
            sut.Disposing = true;

            // Exercise system
            sut.Dispose();

            // Verify outcome
            Assert.Equal(0, disposable.Count);
        }

        [Spec]
        public void LazyResolveWithNullKeyThrows(
            DerivedContainer sut)
        {
            // Fixture setup
            // Exercise system
            var e = Assert.Throws<ArgumentNullException>(() => sut.LazyResolveKeyed<Foo>(null).Invoke());

            // Verify outcome
            Assert.Equal("key", e.ParamName);
        }

        [Spec]
        public void LazyResolveWithNullKeyAndArgumentThrows(
            DerivedContainer sut,
            string argument)
        {
            // Fixture setup
            // Exercise system
            var e = Assert.Throws<ArgumentNullException>(
                () => sut.LazyResolveKeyed<Foo, string>(null).Invoke(argument));

            // Verify outcome
            Assert.Equal("key", e.ParamName);
        }

        [Spec]
        public void ResolveCreatedByTemplateReturnsCorrectInstance(
            DerivedContainer sut,
            object arg1,
            int arg2,
            string arg3)
        {
            // Fixture setup
            sut.Register<Bar, object, int, string>((c, o, i, s) => new Bar(o, i, s));

            // Exercise system
            var actual = sut.Resolve<Bar, object, int, string>(arg1, arg2, arg3);

            // Verify outcome
            Assert.Equal(arg1, actual.Arg1);
            Assert.Equal(arg2, actual.Arg2);
            Assert.Equal(arg3, actual.Arg3);
        }

        [Spec]
        public void TryResolveCreatedByTemplateReturnsCorrectInstance(
            DerivedContainer sut,
            object arg1,
            int arg2,
            string arg3)
        {
            // Fixture setup
            sut.Register<Bar, object, int, string>((c, o, i, s) => new Bar(o, i, s));

            // Exercise system
            var actual = sut.TryResolve<Bar, object, int, string>(arg1, arg2, arg3);

            // Verify outcome
            Assert.Equal(arg1, actual.Arg1);
            Assert.Equal(arg2, actual.Arg2);
            Assert.Equal(arg3, actual.Arg3);
        }

        [Spec]
        public void TryResolveCreatedByTemplateIfNotRegisteredReturnsNull(
            DerivedContainer sut,
            object arg1,
            int arg2,
            string arg3)
        {
            // Fixture setup
            // Exercise system
            var actual = sut.TryResolve<Bar, object, int, string>(arg1, arg2, arg3);

            // Verify outcome
            Assert.Null(actual);
        }

        [Spec]
        public void LazyResolveCreatedByTemplateReturnsCorrectFactory(
            DerivedContainer sut,
            object arg1,
            int arg2,
            string arg3)
        {
            // Fixture setup
            sut.Register<Bar, object, int, string>((c, o, i, s) => new Bar(o, i, s));

            // Exercise system
            var actual = sut.LazyResolve<Bar, object, int, string>().Invoke(arg1, arg2, arg3);

            // Verify outcome
            Assert.Equal(arg1, actual.Arg1);
            Assert.Equal(arg2, actual.Arg2);
            Assert.Equal(arg3, actual.Arg3);
        }

        [Spec]
        public void ResolveKeyedCreatedByTemplateReturnsCorrectInstance(
            DerivedContainer sut,
            object key,
            object arg1,
            int arg2,
            string arg3)
        {
            // Fixture setup
            sut.Register<Bar, object, int, string>(key, (c, o, i, s) => new Bar(o, i, s));

            // Exercise system
            var actual = sut.ResolveKeyed<Bar, object, int, string>(key, arg1, arg2, arg3);

            // Verify outcome
            Assert.Equal(arg1, actual.Arg1);
            Assert.Equal(arg2, actual.Arg2);
            Assert.Equal(arg3, actual.Arg3);
        }

        [Spec]
        public void TryResolveKeyedCreatedByTemplateReturnsCorrectInstance(
            DerivedContainer sut,
            object key,
            object arg1,
            int arg2,
            string arg3)
        {
            // Fixture setup
            sut.Register<Bar, object, int, string>(key, (c, o, i, s) => new Bar(o, i, s));

            // Exercise system
            var actual = sut.TryResolveKeyed<Bar, object, int, string>(key, arg1, arg2, arg3);

            // Verify outcome
            Assert.Equal(arg1, actual.Arg1);
            Assert.Equal(arg2, actual.Arg2);
            Assert.Equal(arg3, actual.Arg3);
        }

        [Spec]
        public void TryResolveKeyedCreatedByTemplateIfNotRegisteredReturnsNull(
            DerivedContainer sut,
            object key,
            object arg1,
            int arg2,
            string arg3)
        {
            // Fixture setup
            // Exercise system
            var actual = sut.TryResolveKeyed<Bar, object, int, string>(key, arg1, arg2, arg3);

            // Verify outcome
            Assert.Null(actual);
        }

        [Spec]
        public void LazyResolveKeyedCreatedByTemplateReturnsCorrectFactory(
            DerivedContainer sut,
            object key,
            object arg1,
            int arg2,
            string arg3)
        {
            // Fixture setup
            sut.Register<Bar, object, int, string>(key, (c, o, i, s) => new Bar(o, i, s));

            // Exercise system
            var actual = sut.LazyResolveKeyed<Bar, object, int, string>(key).Invoke(arg1, arg2, arg3);

            // Verify outcome
            Assert.Equal(arg1, actual.Arg1);
            Assert.Equal(arg2, actual.Arg2);
            Assert.Equal(arg3, actual.Arg3);
        }

        [Spec]
        public void ResolveRecursiveServiceThrows(
            Container sut)
        {
            // Fixture setup
            sut.Register(c =>
            {
                c.Resolve<Foo>();
                return new Foo();
            });

            // Exercise system
            var e = Assert.Throws<ResolutionException>(() => sut.Resolve<Foo>());

            // Verify outcome
            Assert.Equal(
                "The service type 'Jwc.Funz.ContainerTest+Foo' was registered recursively.",
                e.Message);
        }

        [Spec]
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

            // Exercise system
            var e = Assert.Throws<ResolutionException>(() => sut.ResolveKeyed<Foo>(key));

            // Verify outcome
            Assert.Equal(
                "The service type 'Jwc.Funz.ContainerTest+Foo' with the key 'System.Object' " +
                "was registered recursively.",
                e.Message);
        }

        [Spec]
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

            // Exercise system
            var e = Assert.Throws<ResolutionException>(() => sut.Resolve<Foo, string>(argument));

            // Verify outcome
            Assert.Equal(
                "The service type 'Jwc.Funz.ContainerTest+Foo' with the argument(s) " +
                "'System.String' was registered recursively.",
                e.Message);
        }

        [Spec]
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

            // Exercise system
            var e = Assert.Throws<ResolutionException>(() => sut.ResolveKeyed<Foo, string>(key, argument));

            // Verify outcome
            Assert.Equal(
                "The service type 'Jwc.Funz.ContainerTest+Foo' with the key 'System.Object' " +
                "and the argument(s) 'System.String' was registered recursively.",
                e.Message);
        }

        [Spec]
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

            // Exercise system
            var e = Assert.Throws<ResolutionException>(() => sut.Resolve<Foo>());

            // Verify outcome
            Assert.Equal(
                "The service type 'Jwc.Funz.ContainerTest+Foo' was registered recursively.",
                e.Message);
        }

        [Spec]
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

            // Exercise system
            var e = Assert.Throws<ResolutionException>(() => sut.Resolve<Bar, object, int, string>(arg1, arg2, arg3));

            // Verify outcome
            Assert.Equal(
                "The service type 'Jwc.Funz.ContainerTest+Bar' with the argument(s) " +
                "'System.Object, System.Int32, System.String' was registered recursively.",
                e.Message);
        }

        [Spec]
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

            // Exercise system
            var e = Assert.Throws<ResolutionException>(
                () => sut.ResolveKeyed<Bar, object, int, string>(key, arg1, arg2, arg3));
            
            // Verify outcome
            Assert.Equal(
                "The service type 'Jwc.Funz.ContainerTest+Bar' with the key 'System.Object' and the argument(s) " +
                "'System.Object, System.Int32, System.String' was registered recursively.",
                e.Message);
        }

        [Spec]
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

        private class MemberDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                return new MemberCollection<Container>(
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Exclude(t => t.GetMethods().Where(m => m.Name.StartsWith("LazyResolve")))
                .Select(m => new object[] { m });
            }
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

                return new MemberCollection<Container>(BindingFlags.Default)
                    .Include(t => t.GetMethods(bindingFlags))
                    .Exclude(x => x.Dispose())
                    .Select(m => new object[] { m });
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
                get
                {
                    return _arg;
                }
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
                get
                {
                    return _arg1;
                }
            }

            public int Arg2
            {
                get
                {
                    return _arg2;
                }
            }

            public string Arg3
            {
                get
                {
                    return _arg3;
                }
            }
        }

        public class Disposable : IDisposable
        {
            public int Count
            {
                get;
                set;
            }

            public void Dispose()
            {
                Count++;
            }
        }
        
        private class Dummy
        {
#pragma warning disable once UnusedAutoPropertyAccessor.Local
            public string Content { get; set; }

            public void Generate(int size)
            {
                Content = new string('X', size * 3000);
            }
        }

        public class DerivedContainer : Container
        {
            public bool Disposing
            {
                get;
                set;
            }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(Disposing);
            }
        }
    }
}