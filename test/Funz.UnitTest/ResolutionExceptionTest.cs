using System;
using System.Linq;
using Jwc.AutoFixture.Xunit;
using Xunit;

namespace Jwc.Funz
{
    public class ResolutionExceptionTest : IdiomaticTestBase<ResolutionException>
    {
        [Spec]
        public void MessageWithEmptyArgumentTypeIsCorrect(
            Type serviceType)
        {
            // Fixture setup
            var sut = new ResolutionException(serviceType);
            string expected = string.Format("The service of type '{0}' is not registered.", serviceType);

            // Exercise system
            var actual = sut.Message;

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Spec]
        public void MessageWithKeyAndEmptyArgumentTypeIsCorrect(
            Type serviceType,
            string key)
        {
            // Fixture setup
            var sut = new ResolutionException(serviceType, key);
            string expected = string.Format(
                "The service of type '{0}' and key '{1}' is not registered.",
                serviceType,
                key);

            // Exercise system
            var actual = sut.Message;

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Spec]
        public void MessageWithArgumentTypesIsCorrect(
            Type serviceType,
            Type[] argumentTypes)
        {
            // Fixture setup
            var sut = new ResolutionException(serviceType, argumentTypes);
            string expected = string.Format(
                "The service of type '{0}' with arguments '{1}' is not registered.",
                serviceType,
                string.Join(", ", argumentTypes.Select(x => x.Name)));

            // Exercise system
            var actual = sut.Message;

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Spec]
        public void MessageWithKeyAndArgumentTypesIsCorrect(
            Type serviceType,
            string key,
            Type[] argumentTypes)
        {
            // Fixture setup
            var sut = new ResolutionException(serviceType, key, argumentTypes);
            string expected = string.Format(
                "The service of type '{0}' and key '{1}' with arguments '{2}' is not registered.",
                serviceType,
                key,
                string.Join(", ", argumentTypes.Select(x => x.Name)));

            // Exercise system
            var actual = sut.Message;

            // Verify outcome
            Assert.Equal(expected, actual);
        }
    }
}