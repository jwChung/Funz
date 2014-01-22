using System;
using Jwc.AutoFixture.Xunit;

namespace Jwc.Funz
{
    public class SlowSpecAttribute : SpecAttribute
    {
        private readonly RunOn _runOn;

        public SlowSpecAttribute(RunOn runOn)
        {
            _runOn = runOn;
        }

        public RunOn RunOn
        {
            get
            {
                return _runOn;
            }
        }

        public override string Skip
        {
            get
            {
#if CI
                return base.Skip;
#else
                switch (RunOn)
                {
                    case RunOn.CI:
                        return "Run explicitly as this test is slow.";

                    case RunOn.Explicit:
                        return base.Skip;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
#endif
            }
            set
            {
                throw new NotSupportedException();
            }
        }
    }
}