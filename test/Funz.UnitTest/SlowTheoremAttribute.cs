using System;

namespace Jwc.Funz
{
    public class SlowTheoremAttribute : TheoremAttribute
    {
        private readonly RunOn _runOn;

        public SlowTheoremAttribute(RunOn runOn)
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