namespace CanaryTestRunner
{
    public class TestResult
    {
        public TestResult(string message, Result result)
        {
            Result = result;
            Message = message;
        }

        public string Message { get; private set; }
        public Result Result { get; private set; }
    }
}