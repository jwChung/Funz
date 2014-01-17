using System;
using Jwc.AutoFixture.Xunit;

namespace Jwc.Funz
{
    public class ExplicitSpecAttribute : SpecAttribute
    {
        private readonly Run _run;

        public ExplicitSpecAttribute(Run run)
        {
            _run = run;
        }

        public Run Run
        {
            get
            {
                return _run;
            }
        }

        public override string Skip
        {
            get
            {
#if EXPLICITLY
                return base.Skip;
#else
                switch (Run)
                {
                    case Run.Skip:
                        return "Run explicitly as this test is slow.";

                    case Run.Explicitly:
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