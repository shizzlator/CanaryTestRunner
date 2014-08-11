using System;
using System.Collections.Generic;

namespace CanaryTestRunner
{
    public interface ITestRunner
    {
        CanaryTestOutput RunAllTests();
        void RegisterTests(params ICanaryTest[] canaryTests);
        TestRunner RegisterTest(ICanaryTest canaryTest);
    }

    public class TestRunner : ITestRunner
    {
        private readonly IList<ICanaryTest> _canaryTestCollection;

        public TestRunner()
        {
            _canaryTestCollection = new List<ICanaryTest>();
            foreach (var testType in GetTypesWith<CanaryTestAttribute>(false))
            {
                _canaryTestCollection.Add((ICanaryTest)Activator.CreateInstance(testType));
            }
        }

        private static IEnumerable<Type> GetTypesWith<TAttribute>(bool inherit) where TAttribute : Attribute
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsDefined(typeof (TAttribute), inherit)) yield return type;
                }
        }

        public TestRunner RegisterTest(ICanaryTest canaryTest)
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