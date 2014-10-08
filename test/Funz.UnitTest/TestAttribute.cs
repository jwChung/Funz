namespace Jwc.Funz
{
    using Experiment;
    using Experiment.AutoFixture;
    using Experiment.Xunit;

    public class TestAttribute : TestBaseAttribute
    {
        private readonly TestFixtureFactory factory = new TestFixtureFactory();
        private bool runsOnCI = false;

        public bool RunsOnCI
        {
            get
            {
                return this.runsOnCI;
            }

            set
            {
#if !CI
                if (value)
                    this.Skip = "Run this test only on CI server.";
#endif
                this.runsOnCI = value;
            }
        }

        protected override ITestFixture Create(ITestMethodContext context)
        {
            return this.factory.Create(context);
        }
    }
}