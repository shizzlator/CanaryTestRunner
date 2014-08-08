using System.Collections.Generic;

namespace CanaryTestRunner
{
    public interface ICanaryTestRunner
    {
        CanaryTestOutput RunAllTests();
        void RegisterTests(params ICanaryTest[] canaryTests);
        CanaryTestRunner RegisterTest(ICanaryTest canaryTest);
    }

    public class CanaryTestRunner : ICanaryTestRunner
    {
        private readonly IList<ICanaryTest> _canaryTestCollection;

        public CanaryTestRunner()
        {
            _canaryTestCollection = new List<ICanaryTest>();
        }

        public CanaryTestRunner RegisterTest(ICanaryTest canaryTest)
        {
            _canaryTestCollection.Add(canaryTest);
            return this;
        }

        public void RegisterTests(params ICanaryTest[] canaryTests)
        {
            foreach (var canaryTest in canaryTests)
            {
                _canaryTestCollection.Add(canaryTest);
            }
        }

        public CanaryTestOutput RunAllTests()
        {
            if (_canaryTestCollection.Count > 0)
            {
                return DetermineTestOutput();
            }
            return new CanaryTestOutput();
        }

        private CanaryTestOutput DetermineTestOutput()
        {
            var canaryTestOutput = new CanaryTestOutput();
            foreach (var canaryTest in _canaryTestCollection)
            {
                var testResult = canaryTest.Run();

                if (testResult.Result == Result.Passed)
                {
                    canaryTestOutput.Results.Add(testResult);
                }
            }
            return canaryTestOutput;
        }
    }
}