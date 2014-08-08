using System.Collections.Generic;
using System.Linq;

namespace CanaryTestRunner
{
    public class CanaryTestOutput
    {
        public IList<TestResult> Results = new List<TestResult>();

        public string TotalResult
        {
            get
            {
                if (Results.Count > 0)
                    return Results.Any(x => x.Result == Result.Failed) ? "Canary Tests Failed" : "Canary Tests Passed";
                else
                    return "No canary tests have been registered!";
            }
        }
    }
}