using Jwc.AutoFixture.Xunit;
using Xunit;

namespace Jwc.Funz
{
    public class ContainerTest : IdiomaticTestBase<Container>
    {
        [Spec]
        public void RegisterServiceWithSameKeyManytimeDoesNotThrow(
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
            // Exercise system
            // Verify outcome
            var e = Assert.Throws<ResolutionException>(() => sut.Resolve<Foo>());
            Assert.Equal(typeof(Foo), e.ServiceType);
        }

        [Spec]
        public void ResolveUnregisteredKeyedServiceThrows(
            Container sut,
            object key)
        {
            // Fixture setup
            sut.Register(c => new Foo());

            // Exercise system
            // Verify outcome
            var e = Assert.Throws<ResolutionException>(() => sut.ResolveKeyed<Foo>(key));
            Assert.Equal(typeof(Foo), e.ServiceType);
            Assert.Equal(key, e.Key);
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
        public void ResolveServiceReturnsNewInstanceEverytime(
            Container sut)
        {
            // Fixture setup
            sut.Register(c => new Foo());
            var expected = sut.Resolve<Foo>();

            // Exercise system
            var actual = sut.Resolve<Foo>();

            // Verify outcome
            Assert.NotEqual(expected, actual);
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
            Assert.Equal(stringValue, actual.StringArg);
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
            Assert.Equal(stringValue, actual.StringArg);
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
            Assert.Equal(stringValue, actual.StringArg);
        }

        public class Foo
        {
            private readonly string _stringArg;

            public Foo()
            {
            }

            public Foo(string stringArg)
            {
                _stringArg = stringArg;
            }

            public string StringArg
            {
                get
                {
                    return _stringArg;
                }
            }
        }
    }
}