using Moq;

namespace Jwc.Funz
{
    internal static class MockExtensions
    {
        public static Mock<TMocked> ToMock<TMocked>(this TMocked mocked) where TMocked : class
        {
            return Mock.Get(mocked);
        }
    }
}