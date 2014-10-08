namespace Jwc.Funz
{
    public class VersionTestAttribute : TestAttribute
    {
        private readonly int major;
        private readonly int minor;
        private readonly int patch;

        public VersionTestAttribute(int major, int minor, int patch)
        {
            this.major = major;
            this.minor = minor;
            this.patch = patch;
        }

        public int Major
        {
            get { return this.major; }
        }

        public int Minor
        {
            get { return this.minor; }
        }

        public int Patch
        {
            get { return this.patch; }
        }
    }
}