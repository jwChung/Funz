using System;
using Jwc.Experiment.Xunit;

namespace Jwc.Funz
{
    public class SlowTestAttribute : TestAttribute
    {
        private readonly RunOn _runOn;

        public SlowTestAttribute(RunOn runOn)
        {
            _runOn = runOn;
        }

        public RunOn RunOn
        {
            get { return _runOn; }
        }

        public override string Skip
        {
            get
            {
                switch (RunOn)
                {
                    case RunOn.CI:
#if CI
                        return null;
#else
                        return "Run explicitly as this test is slow.";
#endif

                    case RunOn.Local:
                        return null;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            set { throw new NotSupportedException(); }
        }
    }
}