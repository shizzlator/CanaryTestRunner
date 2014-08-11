using CanaryTestRunner;

namespace Example
{
    [CanaryTest]
    public class ExampleCanaryTest : ICanaryTest
    {
        public TestResult Run()
        {
            return new TestResult("Boom!", Result.Passed);
        }
    }
}