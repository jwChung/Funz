namespace Jwc.Funz
{
    using Moq;

    internal static class MockExtensions
    {
        public static Mock<TMocked> ToMock<TMocked>(this TMocked mocked) where TMocked : class
        {
            return Mock.Get(mocked);
        }
    }
}