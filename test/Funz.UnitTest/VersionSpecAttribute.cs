using Jwc.AutoFixture.Xunit;

namespace Jwc.Funz
{
    public class VersionSpecAttribute : SpecAttribute
    {
        private readonly int _major;
        private readonly int _minor;
        private readonly int _patch;

        public VersionSpecAttribute(int major, int minor, int patch)
        {
            _major = major;
            _minor = minor;
            _patch = patch;
        }

        public int Major
        {
            get { return _major; }
        }

        public int Minor
        {
            get { return _minor; }
        }

        public int Patch
        {
            get { return _patch; }
        }
    }
}